using System;
using System.Collections.Generic;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class QuotePdfService
    {
        private readonly DatabaseContext _db;

        public QuotePdfService(DatabaseContext db) => _db = db;

        public void ExportQuotePdf(int projectId, string filePath, bool includeQuoteLetter, bool unpriced,
            bool includeLocalLogo = true, IReadOnlyList<QuoteLine>? millFinishLines = null)
        {
            var project = _db.GetProject(projectId)
                ?? throw new InvalidOperationException($"Project {projectId} was not found.");
            var lines = _db.GetQuoteLines(projectId)
                .Where(l => !string.IsNullOrWhiteSpace(l.ExtrusionCode) || !string.IsNullOrWhiteSpace(l.Description))
                .OrderBy(l => l.PageBlock)
                .ThenBy(l => l.RowInBlock)
                .ToList();
            var total = lines.Sum(l => l.TotalPrice);
            var finish = GetFinishDisplay(project);
            var quotedBy = GetQuotedByDisplay(project);
            var header = GetRegionalHeader(project.Region);
            var contactInfo = GetRegionalContactInfo(project.Region);
            var logo = includeLocalLogo ? GetLocalLogoLabel(project) : string.Empty;

            Document.Create(document =>
            {
                if (includeQuoteLetter)
                    document.Page(page => ComposeLetterPage(page, project, total, finish, quotedBy, header, contactInfo, logo));

                document.Page(page => ComposeQuotePage(page, project, lines, finish, quotedBy, header, unpriced));

                if (millFinishLines != null && millFinishLines.Count > 0)
                    document.Page(page => ComposeQuotePage(page, project, millFinishLines, "Mill Finish", quotedBy, header, unpriced,
                        titleSuffix: " - MILL FINISH ALTERNATIVE"));
            }).GeneratePdf(filePath);
        }

        private static void ComposeLetterPage(PageDescriptor page, Project project, decimal total, string finish, string quotedBy, string header, string contactInfo, string logo)
        {
            page.Size(PageSizes.A4);
            page.Margin(32);
            page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(10));
            page.Footer().AlignRight().Text(text =>
            {
                text.Span("Page ");
                text.CurrentPageNumber();
                text.Span(" of ");
                text.TotalPages();
            });

            page.Content().Column(column =>
            {
                column.Spacing(10);
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text(header).Bold().FontSize(20).FontColor(Colors.Teal.Darken2);
                    row.ConstantItem(220).AlignRight().Text(contactInfo).FontSize(8);
                });
                if (!string.IsNullOrWhiteSpace(logo))
                    column.Item().Text(logo).Bold().FontColor(Colors.Green.Darken2);

                column.Item().Text($"To: {project.CustomerName}");
                column.Item().Text($"Attention: {(string.IsNullOrWhiteSpace(project.AttentionTo) ? project.Attention : project.AttentionTo)}");
                column.Item().Text($"Date: {(project.RevisionDate ?? DateTime.Today):dd MMMM yyyy}");
                column.Item().Text($"Re: {project.ProjectName}");

                column.Item().Text("We have pleasure in supplying our quotation for the above project. Please review the attached quotation schedule and confirm all sections, finishes, and quantities before placing your order.");
                column.Item().Text("Any project variations, split deliveries, or quantity make-ups may require repricing. This quotation remains subject to Capral Limited's standard conditions of sale unless otherwise agreed in writing.");

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(180);
                        columns.RelativeColumn();
                    });

                    static IContainer Cell(IContainer container) => container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(6);

                    table.Cell().Element(Cell).Text("Region").Bold();
                    table.Cell().Element(Cell).Text(project.Region);
                    table.Cell().Element(Cell).Text("Finish").Bold();
                    table.Cell().Element(Cell).Text(finish);
                    table.Cell().Element(Cell).Text("Project Price").Bold();
                    table.Cell().Element(Cell).Text(total.ToString("C2"));
                    table.Cell().Element(Cell).Text("Quote Validity").Bold();
                    table.Cell().Element(Cell).Text(string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity);
                });

                column.Item().Text("All prices exclude GST").Bold();
                column.Item().Text(quotedBy);
            });
        }

        private static void ComposeQuotePage(PageDescriptor page, Project project, IReadOnlyList<QuoteLine> lines, string finish, string quotedBy, string header, bool unpriced, string titleSuffix = "")
        {
            page.Size(PageSizes.A4.Landscape());
            page.Margin(24);
            page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(9));
            page.Footer().AlignRight().Text(text =>
            {
                text.Span("Page ");
                text.CurrentPageNumber();
                text.Span(" of ");
                text.TotalPages();
            });

            page.Content().Column(column =>
            {
                column.Spacing(8);
                column.Item().Text((unpriced ? "QUOTATION - UNPRICED" : "QUOTATION") + titleSuffix).Bold().FontSize(18);
                column.Item().Text(header).Bold();
                column.Item().Text($"Customer: {project.CustomerName}");
                column.Item().Text($"Project: {project.ProjectName}");
                column.Item().Text($"Quote Date: {(project.RevisionDate ?? DateTime.Today):dd/MM/yyyy}");
                column.Item().Text($"Finish: {finish}");
                column.Item().Text($"Quoted By: {quotedBy}");

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(32);
                        columns.ConstantColumn(80);
                        columns.RelativeColumn(2);
                        columns.ConstantColumn(55);
                        columns.ConstantColumn(55);
                        columns.ConstantColumn(65);
                        columns.ConstantColumn(50);
                        if (!unpriced)
                        {
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(75);
                        }
                    });

                    static IContainer HeaderCell(IContainer container) => container.Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.Grey.Lighten3).Padding(4);
                    static IContainer Cell(IContainer container) => container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(4);

                    table.Header(headerRow =>
                    {
                        headerRow.Cell().Element(HeaderCell).Text("Ln").Bold();
                        headerRow.Cell().Element(HeaderCell).Text("Section").Bold();
                        headerRow.Cell().Element(HeaderCell).Text("Description").Bold();
                        headerRow.Cell().Element(HeaderCell).Text("Alloy").Bold();
                        headerRow.Cell().Element(HeaderCell).Text("Temper").Bold();
                        headerRow.Cell().Element(HeaderCell).Text("Length").Bold();
                        headerRow.Cell().Element(HeaderCell).Text("Qty").Bold();
                        if (!unpriced)
                        {
                            headerRow.Cell().Element(HeaderCell).Text("$ Each").Bold();
                            headerRow.Cell().Element(HeaderCell).Text("$ Total").Bold();
                        }
                    });

                    foreach (var line in lines)
                    {
                        table.Cell().Element(Cell).Text((line.LineNumber <= 0 ? line.RowInBlock : line.LineNumber).ToString());
                        table.Cell().Element(Cell).Text(line.ExtrusionCode);
                        table.Cell().Element(Cell).Text(line.Description);
                        table.Cell().Element(Cell).Text(line.Alloy);
                        table.Cell().Element(Cell).Text(line.Temper);
                        table.Cell().Element(Cell).Text(line.LengthM.ToString("N2"));
                        table.Cell().Element(Cell).Text(line.Quantity.ToString("N2"));
                        if (!unpriced)
                        {
                            table.Cell().Element(Cell).Text(line.Price.ToString("N4"));
                            table.Cell().Element(Cell).Text(line.TotalPrice.ToString("N4"));
                        }
                    }
                });
            });
        }

        public static string GetRegionalHeader(string region)
            => string.IsNullOrWhiteSpace(region) ? "CAPRAL ALUMINIUM" : $"CAPRAL ALUMINIUM – {region.ToUpperInvariant()}";

        /// <summary>
        /// Contact details block (shown top-right on the quote letter). Uses the RegionAddresses
        /// table when available; falls back to a generic contact block if no record exists.
        /// </summary>
        public string GetRegionalContactInfo(string region)
        {
            if (!string.IsNullOrWhiteSpace(region))
            {
                var addr = _db.GetRegionAddress(region.Trim());
                if (addr != null)
                {
                    var lines = new System.Text.StringBuilder();
                    lines.AppendLine(addr.OfficeName);
                    lines.AppendLine("ABN 78 004 213 692");
                    if (!string.IsNullOrWhiteSpace(addr.AddressLine1))
                        lines.AppendLine(addr.AddressLine1);
                    if (!string.IsNullOrWhiteSpace(addr.AddressLine2))
                        lines.AppendLine(addr.AddressLine2);
                    var suburb = string.Join(" ", new[] { addr.Suburb, addr.State, addr.Postcode }
                        .Where(s => !string.IsNullOrWhiteSpace(s)));
                    if (!string.IsNullOrWhiteSpace(suburb))
                        lines.AppendLine(suburb);
                    lines.AppendLine($"T: {addr.Phone}  F: {addr.Fax}");
                    lines.Append(addr.Website);
                    return lines.ToString();
                }
            }
            var office = string.IsNullOrWhiteSpace(region) ? "Capral Limited" : $"Capral Limited – {region.ToUpperInvariant()}";
            return $"{office}\nABN 78 004 213 692\nBuilding System Group and Distribution\nT: 1300 366 517  F: 1300 133 975\nwww.capral.com.au";
        }

        // Static overload kept for callers that do not have a DatabaseContext instance.
        public static string GetDefaultRegionalContactInfo(string region)
        {
            var office = string.IsNullOrWhiteSpace(region) ? "Capral Limited" : $"Capral Limited – {region.ToUpperInvariant()}";
            return $"{office}\nABN 78 004 213 692\nBuilding System Group and Distribution\nT: 1300 366 517  F: 1300 133 975\nwww.capral.com.au";
        }

        public static string GetFinishDisplay(Project project)
            => string.IsNullOrWhiteSpace(project.FinishName) ? project.FinishType : project.FinishName;

        public static string GetQuotedByDisplay(Project project)
            => string.IsNullOrWhiteSpace(project.AccountManager) ? project.QuotedBy : project.AccountManager;

        public static string GetLocalLogoLabel(Project project)
        {
            var logo = string.IsNullOrWhiteSpace(project.AddLocAlLogos) ? project.LocalLogo : project.AddLocAlLogos;
            return string.Equals(logo, "None", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(logo)
                ? string.Empty
                : $"{logo} logo selected";
        }
    }
}
