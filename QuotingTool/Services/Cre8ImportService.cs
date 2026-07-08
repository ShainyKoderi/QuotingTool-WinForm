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
    /// Best-effort CRE8 CSV import for the Project Details tab.
    /// The exact CRE8 export schema is not present in this repository, so header aliases and
    /// fallback column positions are inferred from the current project-details fields and should
    /// be adjusted once a real CRE8 sample/export specification is available.
    /// </summary>
    public class Cre8ImportService
    {
        private readonly DatabaseContext _db;

        public Cre8ImportService(DatabaseContext db) => _db = db;

        public Cre8ImportResult ImportProjectDetails(int projectId, string csvFilePath)
        {
            if (!File.Exists(csvFilePath))
                throw new FileNotFoundException($"CSV file not found: {csvFilePath}");

            var project = _db.GetProject(projectId) ?? throw new InvalidOperationException("Project not found.");
            var result = new Cre8ImportResult();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null
            };

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, config);

            if (!csv.Read())
                throw new InvalidOperationException("CSV file is empty.");

            csv.ReadHeader();
            if (!csv.Read())
                throw new InvalidOperationException("CSV file does not contain any data rows.");

            var headerLookup = BuildHeaderLookup(csv);

            Apply(result, csv, headerLookup, 0, project, nameof(Project.CustomerName), value => project.CustomerName = value,
                "Customer Name", "CustomerName", "Customer", "Customer_Name", "Client", "ClientName");
            Apply(result, csv, headerLookup, 1, project, nameof(Project.ProjectName), value => project.ProjectName = value,
                "Project Name", "ProjectName", "Project", "Project_Name", "Job Name", "JobName");
            Apply(result, csv, headerLookup, 2, project, nameof(Project.Region), value => project.Region = value,
                "Region", "Sales Region", "SalesRegion", "State");
            Apply(result, csv, headerLookup, 3, project, nameof(Project.QuotedBy), value => project.QuotedBy = value,
                "Quoted By", "QuotedBy", "Estimator", "Prepared By", "PreparedBy");
            Apply(result, csv, headerLookup, 4, project, nameof(Project.AccountManager), value => project.AccountManager = value,
                "Account Manager", "AccountManager", "Salesperson", "Sales Person", "Manager");
            Apply(result, csv, headerLookup, 5, project, nameof(Project.AttentionTo), value =>
            {
                project.AttentionTo = value;
                project.Attention = value;
            }, "Attention To", "AttentionTo", "Attention", "Contact", "Contact Name", "ContactName");
            Apply(result, csv, headerLookup, 6, project, nameof(Project.MarketSegment), value => project.MarketSegment = value,
                "Market Segment", "MarketSegment", "Market");
            Apply(result, csv, headerLookup, 7, project, nameof(Project.ProductSegment), value => project.ProductSegment = value,
                "Product Segment", "ProductSegment", "Product");
            Apply(result, csv, headerLookup, 8, project, nameof(Project.FinishType), value => project.FinishType = value,
                "Finish Type", "FinishType", "Finish", "Finish Category", "FinishCategory");
            Apply(result, csv, headerLookup, 9, project, nameof(Project.FinishApplicator), value => project.FinishApplicator = value,
                "Finish Applicator", "FinishApplicator", "Applicator", "Coater", "Finisher");
            Apply(result, csv, headerLookup, 10, project, nameof(Project.FinishGroup), value => project.FinishGroup = value,
                "Finish Group", "FinishGroup", "Colour Group", "ColourGroup");
            Apply(result, csv, headerLookup, 11, project, nameof(Project.FinishName), value => project.FinishName = value,
                "Finish Name", "FinishName", "Colour Name", "ColourName", "Colour");
            Apply(result, csv, headerLookup, 12, project, nameof(Project.FinishCustomName), value => project.FinishCustomName = value,
                "Finish Custom Name", "FinishCustomName", "Custom Finish", "CustomFinish", "Custom Colour", "CustomColour");
            Apply(result, csv, headerLookup, 13, project, nameof(Project.FinishSapCode), value => project.FinishSapCode = value,
                "Finish SAP Code", "FinishSapCode", "SAP Code", "SAPCode", "Colour Code", "ColourCode");
            Apply(result, csv, headerLookup, 14, project, nameof(Project.ThresholdChoice), value => project.ThresholdChoice = value,
                "Threshold Choice", "ThresholdChoice", "Threshold");
            Apply(result, csv, headerLookup, 15, project, nameof(Project.DeliveryCharge), value => project.DeliveryCharge = ParseDecimal(value, project.DeliveryCharge),
                "Delivery Charge", "DeliveryCharge", "Freight", "Freight Charge", "Delivery");
            Apply(result, csv, headerLookup, 16, project, nameof(Project.CustomerSapDiscount), value => project.CustomerSapDiscount = ParseDecimal(value, project.CustomerSapDiscount),
                "Customer SAP Discount", "CustomerSapDiscount", "Customer SAP Discount %", "Sap Discount", "SAP Discount", "Discount");
            Apply(result, csv, headerLookup, 17, project, nameof(Project.QuoteValidity), value => project.QuoteValidity = value,
                "Quote Validity", "QuoteValidity", "Validity", "Valid For");
            Apply(result, csv, headerLookup, 18, project, nameof(Project.AddLocAlLogos), value =>
            {
                project.AddLocAlLogos = value;
                project.LocalLogo = value;
            }, "Add LocAl Logos", "AddLocAlLogos", "Local Logo", "LocalLogo", "LocAl Logos", "Logo Option");
            Apply(result, csv, headerLookup, 19, project, nameof(Project.GenerateMillFinishAlternative), value =>
            {
                bool enabled = ParseBool(value);
                project.GenerateMillFinishAlternative = enabled;
                project.GenerateMfAlt = enabled;
            }, "Generate Mill Finish Alternative", "GenerateMillFinishAlternative", "Generate MF Alt", "GenerateMfAlt", "MF ALT");

            if (result.FieldsUpdated == 0)
                result.Warnings.Add("No supported CRE8 columns were matched in the selected CSV.");

            if (string.IsNullOrWhiteSpace(project.AttentionTo))
            {
                project.AttentionTo = "To whom it may concern";
                project.Attention = "To whom it may concern";
            }

            _db.UpdateProject(project);
            result.Message = BuildMessage(result, project.ProjectName);
            return result;
        }

        private static void Apply(Cre8ImportResult result, CsvReader csv, IReadOnlyDictionary<string, int> headerLookup, int fallbackIndex, Project project, string fieldName, Action<string> setter, params string[] aliases)
        {
            string? value = TryGetField(csv, headerLookup, fallbackIndex, aliases);
            if (string.IsNullOrWhiteSpace(value))
                return;

            setter(value.Trim());
            result.FieldsUpdated++;
            result.UpdatedFields.Add(fieldName);
        }

        private static string? TryGetField(CsvReader csv, IReadOnlyDictionary<string, int> headerLookup, int fallbackIndex, params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                if (!headerLookup.TryGetValue(Normalize(alias), out var index))
                    continue;

                try
                {
                    var value = csv.GetField(index);
                    if (!string.IsNullOrWhiteSpace(value))
                        return value;
                }
                catch
                {
                }
            }

            try
            {
                var value = csv.GetField(fallbackIndex);
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }
            catch
            {
            }

            return null;
        }

        private static IReadOnlyDictionary<string, int> BuildHeaderLookup(CsvReader csv)
        {
            var headers = csv.HeaderRecord ?? Array.Empty<string>();
            var lookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headers.Length; i++)
            {
                var key = Normalize(headers[i]);
                if (!string.IsNullOrWhiteSpace(key) && !lookup.ContainsKey(key))
                    lookup[key] = i;
            }
            return lookup;
        }

        private static string Normalize(string value)
            => new string((value ?? string.Empty).Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();

        private static decimal ParseDecimal(string value, decimal fallback)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var invariant))
                return invariant;
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var current))
                return current;
            return fallback;
        }

        private static bool ParseBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();
            return value == "1"
                || value.Equals("true", StringComparison.OrdinalIgnoreCase)
                || value.Equals("yes", StringComparison.OrdinalIgnoreCase)
                || value.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        private static string BuildMessage(Cre8ImportResult result, string projectName)
        {
            var lines = new List<string>
            {
                $"Imported CRE8 data into project '{projectName}'.",
                $"Updated {result.FieldsUpdated} field(s)."
            };

            if (result.UpdatedFields.Count > 0)
                lines.Add($"Fields: {string.Join(", ", result.UpdatedFields)}");

            var distinctWarnings = result.Warnings.Distinct().ToList();
            if (distinctWarnings.Count > 0)
            {
                lines.Add(string.Empty);
                lines.Add("Warnings:");
                lines.AddRange(distinctWarnings.Select(w => $"- {w}"));
            }

            return string.Join(Environment.NewLine, lines);
        }
    }

    public class Cre8ImportResult
    {
        public int FieldsUpdated { get; set; }
        public List<string> UpdatedFields { get; } = new();
        public List<string> Warnings { get; } = new();
        public string Message { get; set; } = string.Empty;
    }
}
