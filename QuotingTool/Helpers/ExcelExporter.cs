using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClosedXML.Excel;
using QuotingTool.Data;

namespace QuotingTool.Helpers
{
    /// <summary>
    /// Provides Excel (.xlsx) and CSV export for QuoteLines.
    /// Uses ClosedXML for Excel export.
    /// </summary>
    public class ExcelExporter
    {
        private readonly DatabaseContext _db;

        public ExcelExporter(DatabaseContext db) => _db = db;

        // Column headers matching the original Excel "2.Quote" layout
        private static readonly string[] Headers =
        {
            "Page", "Row", "Extrusion Code", "Description",
            "Quantity", "Length (m)", "Price", "Sell Price",
            "Subsill", "Surcharge"
        };

        /// <summary>
        /// Exports QuoteLines for the given project to an .xlsx file.
        /// </summary>
        public void ExportToExcel(int projectId, string filePath)
        {
            var project = _db.GetProject(projectId);
            var lines   = _db.GetQuoteLines(projectId);

            using var workbook  = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Quote");

            // Title row
            worksheet.Cell(1, 1).Value = project != null
                ? $"{project.CustomerName} – {project.ProjectName}"
                : "Quote Export";
            worksheet.Range(1, 1, 1, Headers.Length).Merge().Style
                .Font.SetBold(true)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Header row
            for (int c = 0; c < Headers.Length; c++)
            {
                var cell = worksheet.Cell(2, c + 1);
                cell.Value = Headers[c];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.DarkSlateBlue;
                cell.Style.Font.FontColor = XLColor.White;
            }

            // Data rows
            int row = 3;
            foreach (var line in lines)
            {
                worksheet.Cell(row, 1).Value  = line.PageBlock;
                worksheet.Cell(row, 2).Value  = line.RowInBlock;
                worksheet.Cell(row, 3).Value  = line.ExtrusionCode;
                worksheet.Cell(row, 4).Value  = line.Description;
                worksheet.Cell(row, 5).Value  = (double)line.Quantity;
                worksheet.Cell(row, 6).Value  = (double)line.LengthM;
                worksheet.Cell(row, 7).Value  = (double)line.Price;
                worksheet.Cell(row, 8).Value  = (double)line.SellPrice;
                worksheet.Cell(row, 9).Value  = line.SubsillFlag ? "Y" : "N";
                worksheet.Cell(row, 10).Value = (double)line.SubsillSurcharge;
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(filePath);
        }

        /// <summary>
        /// Exports QuoteLines for the given project to a CSV file.
        /// </summary>
        public void ExportToCsv(int projectId, string filePath)
        {
            var lines = _db.GetQuoteLines(projectId);
            var sb    = new StringBuilder();

            sb.AppendLine(string.Join(",", Headers));

            foreach (var line in lines)
            {
                sb.AppendLine(string.Join(",",
                    line.PageBlock,
                    line.RowInBlock,
                    CsvEscape(line.ExtrusionCode),
                    CsvEscape(line.Description),
                    line.Quantity,
                    line.LengthM,
                    line.Price,
                    line.SellPrice,
                    line.SubsillFlag ? "Y" : "N",
                    line.SubsillSurcharge));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string CsvEscape(string value)
        {
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
    }
}
