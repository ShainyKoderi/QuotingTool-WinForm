using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 09_BOM_UniqueAndMode.ts and 10_ImportBOM_Distribute.ts
    /// Handles BOM CSV import, finish analysis, and distribution to QuoteLines.
    /// </summary>
    public class BomService
    {
        // Page count thresholds (rows → pages)
        private static readonly int[] PageThresholds = { 40, 80, 120, 160, 200 };

        // Subsill surcharge per unit ($12 as per spec)
        private const decimal SubsillSurcharge = 12m;

        private readonly DatabaseContext _db;

        public BomService(DatabaseContext db) => _db = db;

        // ── Finish Analysis (09_BOM_UniqueAndMode.ts) ────────────────────────

        /// <summary>
        /// Returns all distinct Finish values from BomLines for the project.
        /// </summary>
        public List<string> GetUniqueFinishes(int projectId)
        {
            return _db.GetBomLines(projectId)
                .Select(b => b.Finish)
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(f => f)
                .ToList();
        }

        /// <summary>
        /// Returns the most frequent Finish value (mode) from BomLines.
        /// </summary>
        public string GetMostCommonFinish(int projectId)
        {
            var lines = _db.GetBomLines(projectId).ToList();
            if (!lines.Any()) return string.Empty;

            return lines
                .Where(b => !string.IsNullOrWhiteSpace(b.Finish))
                .GroupBy(b => b.Finish, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? string.Empty;
        }

        // ── CSV Import (10_ImportBOM_Distribute.ts) ──────────────────────────

        /// <summary>
        /// Reads a CSV file into BomLines.
        /// Expected columns (by position): B=ExtrusionCode, C=Description,
        /// D=Quantity, E=LengthMM, G=Finish, I=ImportedPrice, V=SubsillFlag.
        /// Converts LengthMM to metres automatically.
        /// </summary>
        public int ImportBomFromCsv(int projectId, string csvFilePath)
        {
            if (!File.Exists(csvFilePath))
                throw new FileNotFoundException($"CSV file not found: {csvFilePath}");

            _db.DeleteBomLines(projectId);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null
            };

            int rowNumber = 0;
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, config);

            // Read header
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                rowNumber++;
                string extCode    = GetField(csv, "ExtrusionCode", "B", rowNumber - 1);
                string desc       = GetField(csv, "Description", "C", rowNumber - 1);
                decimal qty       = ParseDecimal(GetField(csv, "Quantity", "D", rowNumber - 1));
                decimal lengthMM  = ParseDecimal(GetField(csv, "LengthMM", "E", rowNumber - 1));
                string finish     = GetField(csv, "Finish", "G", rowNumber - 1);
                decimal impPrice  = ParseDecimal(GetField(csv, "ImportedPrice", "I", rowNumber - 1));
                bool subsill      = ParseBool(GetField(csv, "SubsillFlag", "V", rowNumber - 1));

                // Convert mm → m if value > 1000
                decimal lengthM = lengthMM > 1000 ? lengthMM / 1000m : lengthMM;

                _db.InsertBomLine(new BomLine
                {
                    ProjectId     = projectId,
                    RowNumber     = rowNumber,
                    ExtrusionCode = extCode,
                    Description   = desc,
                    Quantity      = qty,
                    LengthM       = lengthM,
                    Finish        = finish,
                    ImportedPrice = impPrice,
                    SubsillFlag   = subsill
                });
            }

            return rowNumber;
        }

        /// <summary>
        /// Distributes BomLines into QuoteLines.
        /// - Determines page count from row count thresholds.
        /// - Applies $12 subsill surcharge where SubsillFlag = true.
        /// - Clears existing QuoteLines for the project first.
        /// </summary>
        public int DistributeBomToQuote(int projectId)
        {
            var bomLines = _db.GetBomLines(projectId).ToList();
            if (!bomLines.Any()) return 0;

            int totalRows = bomLines.Count;
            int pageCount = DeterminePageCount(totalRows);

            // Clear existing quote lines
            _db.DeleteQuoteLines(projectId);

            int distributed = 0;
            int maxRowsPerPage = 40;

            for (int i = 0; i < bomLines.Count; i++)
            {
                int pageBlock  = (i / maxRowsPerPage) + 1;
                int rowInBlock = (i % maxRowsPerPage) + 1;

                if (pageBlock > pageCount) break; // safety cap

                var bom = bomLines[i];
                decimal surcharge = bom.SubsillFlag ? SubsillSurcharge : 0m;

                _db.UpsertQuoteLine(new QuoteLine
                {
                    ProjectId       = projectId,
                    PageBlock       = pageBlock,
                    RowInBlock      = rowInBlock,
                    LineNumber      = i + 1,
                    ExtrusionCode   = bom.ExtrusionCode,
                    Description     = bom.Description,
                    Quantity        = bom.Quantity,
                    LengthM         = bom.LengthM,
                    Price           = bom.ImportedPrice,
                    SellPrice       = 0,
                    TotalPrice      = bom.Quantity * bom.ImportedPrice,
                    SubsillFlag     = bom.SubsillFlag,
                    SubsillSurcharge = surcharge
                });
                distributed++;
            }

            return distributed;
        }

        /// <summary>
        /// Determines how many quote pages are needed based on row count.
        /// </summary>
        public static int DeterminePageCount(int rowCount)
        {
            if (rowCount > 200) return 6;
            if (rowCount > 160) return 5;
            if (rowCount > 120) return 4;
            if (rowCount > 80)  return 3;
            if (rowCount > 40)  return 2;
            return 1;
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private static string GetField(CsvReader csv, string header, string fallbackIndex, int rowIndex)
        {
            try { return csv.GetField(header) ?? string.Empty; }
            catch
            {
                try
                {
                    // Try column index fallback (A=0, B=1, ...)
                    int idx = fallbackIndex.ToUpper()[0] - 'A';
                    return csv.GetField(idx) ?? string.Empty;
                }
                catch { return string.Empty; }
            }
        }

        private static decimal ParseDecimal(string value)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                return result;
            return 0m;
        }

        private static bool ParseBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.Trim() == "1" || value.Trim().Equals("true", StringComparison.OrdinalIgnoreCase)
                || value.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase);
        }
    }
}
