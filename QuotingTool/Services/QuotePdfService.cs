using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class QuotePdfService
    {
        // Capral brand colours
        private const string CapralTeal   = "#006060";
        private const string CapralWhite  = "#FFFFFF";
        private const string CapralYellow = "#F5C500";

        private readonly DatabaseContext _db;

        public QuotePdfService(DatabaseContext db) => _db = db;

        // ── Image helpers ────────────────────────────────────────────────────

        private static byte[]? BitmapToBytes(System.Drawing.Bitmap? bmp)
        {
            if (bmp == null) return null;
            using var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        /// <summary>
        /// Returns the PNG bytes for the LocAl logo that matches the project's
        /// <c>AddLocAlLogos</c> / <c>LocalLogo</c> selection, or <c>null</c> when
        /// no logo should be shown (value is blank or "None").
        /// </summary>
        private static byte[]? GetLocalLogoBytes(Project project)
        {
            var value = string.IsNullOrWhiteSpace(project.AddLocAlLogos)
                ? project.LocalLogo
                : project.AddLocAlLogos;

            if (string.IsNullOrWhiteSpace(value) ||
                string.Equals(value, "None", StringComparison.OrdinalIgnoreCase))
                return null;

            if (value.IndexOf("Super", StringComparison.OrdinalIgnoreCase) >= 0)
                return BitmapToBytes(Properties.Resources.local_super_green);

            return BitmapToBytes(Properties.Resources.local_green);
        }

        private static byte[]? GetCapralLogoBytes()
            => BitmapToBytes(Properties.Resources.capral_logo);

        // ── Public export entry point ────────────────────────────────────────

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
            var total       = lines.Sum(l => l.TotalPrice);
            var finish      = GetFinishDisplay(project);
            var quotedBy    = GetQuotedByDisplay(project);
            var contactInfo = GetRegionalContactInfo(project.Region);
            var capralLogo  = GetCapralLogoBytes();
            var localLogo   = includeLocalLogo ? GetLocalLogoBytes(project) : null;

            Document.Create(document =>
            {
                if (includeQuoteLetter)
                    document.Page(page => ComposeLetterPage(page, project, total, finish, quotedBy,
                        contactInfo, capralLogo, localLogo));

                document.Page(page => ComposeQuotePage(page, project, lines, finish, quotedBy,
                    unpriced, capralLogo, localLogo));

                if (millFinishLines != null && millFinishLines.Count > 0)
                    document.Page(page => ComposeQuotePage(page, project, millFinishLines,
                        "Mill Finish", quotedBy, unpriced, capralLogo, localLogo: null,
                        titleSuffix: " - MILL FINISH ALTERNATIVE"));
            }).GeneratePdf(filePath);
        }

        // ── Quote Letter page ────────────────────────────────────────────────

        private static void ComposeLetterPage(
            PageDescriptor page,
            Project         project,
            decimal         total,
            string          finish,
            string          quotedBy,
            string          contactInfo,
            byte[]?         capralLogo,
            byte[]?         localLogo)
        {
            page.Size(PageSizes.A4);
            page.MarginHorizontal(40);
            page.MarginTop(32);
            page.MarginBottom(24);
            page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(10));

            page.Footer().AlignRight().Text(text =>
            {
                text.Span("Page ").FontSize(8).FontColor(Colors.Grey.Medium);
                text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                text.Span(" of ").FontSize(8).FontColor(Colors.Grey.Medium);
                text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
            });

            page.Content().Column(col =>
            {
                col.Spacing(0);

                // ── Header row: Capral logo | contact block ──────────────────
                col.Item().PaddingBottom(6).Row(row =>
                {
                    row.RelativeItem().AlignBottom().Column(logoCol =>
                    {
                        if (capralLogo != null)
                            logoCol.Item().Width(140).Image(capralLogo);
                        else
                            logoCol.Item().Text("CAPRAL ALUMINIUM")
                                .Bold().FontSize(18).FontColor(CapralTeal);
                    });

                    row.ConstantItem(220).AlignRight().Text(contactInfo)
                        .FontSize(8).LineHeight(1.35f);
                });

                // Horizontal rule
                col.Item().PaddingBottom(14)
                   .BorderBottom(1).BorderColor(Colors.Grey.Darken2);

                // ── To / Attention / Date / Re ───────────────────────────────
                var attention = string.IsNullOrWhiteSpace(project.AttentionTo)
                    ? project.Attention
                    : project.AttentionTo;
                var quoteDate = (project.RevisionDate ?? DateTime.Today).ToString("dd MMMM yyyy");

                col.Item().PaddingBottom(4).Row(row =>
                {
                    row.ConstantItem(90).Text("To:").FontSize(10);
                    row.RelativeItem().Text(project.CustomerName).FontSize(10);
                });
                col.Item().PaddingBottom(4).Row(row =>
                {
                    row.ConstantItem(90).Text("Attention:").FontSize(10);
                    row.RelativeItem(2).Text(attention).Bold().FontSize(10);
                    row.ConstantItem(55).Text("Date:").FontSize(10);
                    row.RelativeItem().AlignRight().Text(quoteDate).Bold().FontSize(10);
                });
                col.Item().PaddingBottom(16).Row(row =>
                {
                    row.ConstantItem(90).Text("Re:").FontSize(10);
                    row.RelativeItem().Text(project.ProjectName).Bold().FontSize(10);
                });

                // ── Body paragraph ───────────────────────────────────────────
                const string bodyText =
                    "We have pleasure in supplying our quotation for Capral Architectural Glazing Systems " +
                    "to the above project. Your quotation is based on the attached schedule of materials " +
                    "delivered in a single order unless otherwise agreed in writing. Any project variations " +
                    "or quantity make-ups may be charged at your standard pricing. Please confirm all " +
                    "sections are correct prior to placing your order.";

                col.Item().PaddingBottom(14).Text(bodyText).FontSize(10).LineHeight(1.4f);

                // ── Colour / Price / Delivery + LocAl logo ───────────────────
                col.Item().PaddingBottom(6).Row(row =>
                {
                    row.RelativeItem().Column(detailCol =>
                    {
                        detailCol.Item().PaddingBottom(6).Row(r =>
                        {
                            r.ConstantItem(100).Text(txt =>
                            {
                                txt.Span("Colour:").Underline();
                            });
                            r.RelativeItem().Text(finish).Bold();
                        });
                        detailCol.Item().PaddingBottom(6).Row(r =>
                        {
                            r.ConstantItem(100).Text(txt =>
                            {
                                txt.Span("Project Price:").Underline();
                            });
                            r.ConstantItem(20).Text("$");
                            r.RelativeItem().Text(total.ToString("N2")).Bold();
                        });
                        detailCol.Item().Row(r =>
                        {
                            r.ConstantItem(100).Text(txt =>
                            {
                                txt.Span("Delivery:").Underline();
                            });
                            r.ConstantItem(20).Text("$");
                            r.RelativeItem().Text(project.DeliveryCharge.ToString("N2")).Bold();
                        });
                    });

                    // LocAl logo (right side of the price block)
                    if (localLogo != null)
                        row.ConstantItem(130).AlignMiddle().AlignRight().Image(localLogo);
                });

                col.Item().PaddingTop(14).PaddingBottom(4)
                   .Background(CapralYellow).Height(8);

                // ── Footer text ──────────────────────────────────────────────
                col.Item().PaddingTop(8).PaddingBottom(4)
                   .Text("All prices exclude GST").FontSize(10);
                col.Item().PaddingBottom(10)
                   .Text($"Quote valid {(string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity)}")
                   .FontSize(10);

                const string bodyText2 =
                    "On acceptance of the above quotation please forward your purchase order by fax to 1300 133 975.\r\n\r\n" +
                    "If you require a warranty certificate please forward a warranty request form prior to us submitting " +
                    "a firm quotation. All warranty powder coating attracts a minimum $250.00 charge.\r\n\r\n" +
                    "This quotation is subject to Capral Limited's standard conditions of sale (unless otherwise agreed " +
                    "in writing) which can be viewed on our website at www.capral.com.au.\r\n\r\n" +
                    "If you require further information please contact your Account Manager.\r\n\r\n" +
                    "Thank you for choosing Capral Architectural Glazing Systems.\r\n\r\n" +
                    "Yours truly,";

                col.Item().PaddingBottom(18).Text(bodyText2).FontSize(10).LineHeight(1.4f);

                col.Item().Text(quotedBy).Bold().FontSize(10);
            });
        }

        // ── Quote (schedule) page ────────────────────────────────────────────

        private static void ComposeQuotePage(
            PageDescriptor          page,
            Project                 project,
            IReadOnlyList<QuoteLine> lines,
            string                  finish,
            string                  quotedBy,
            bool                    unpriced,
            byte[]?                 capralLogo,
            byte[]?                 localLogo,
            string                  titleSuffix = "")
        {
            page.Size(PageSizes.A4.Landscape());
            page.MarginHorizontal(24);
            page.MarginTop(20);
            page.MarginBottom(18);
            page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(9));

            page.Footer().AlignRight().Text(text =>
            {
                text.Span("Page ").FontSize(8).FontColor(Colors.Grey.Medium);
                text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                text.Span(" of ").FontSize(8).FontColor(Colors.Grey.Medium);
                text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
            });

            page.Content().Column(col =>
            {
                col.Spacing(0);

                // ── Page header: logo | title | timestamp | LocAl logo ───────
                col.Item().PaddingBottom(6).Row(row =>
                {
                    // Capral logo
                    row.AutoItem().AlignBottom().Column(logoCol =>
                    {
                        if (capralLogo != null)
                            logoCol.Item().Width(120).Image(capralLogo);
                        else
                            logoCol.Item().Text("CAPRAL ALUMINIUM")
                                .Bold().FontSize(14).FontColor(CapralTeal);
                    });

                    // Title
                    row.RelativeItem().AlignBottom().PaddingLeft(16).Text(
                        (unpriced ? "QUOTATION - NOT ITEMISED" : "QUOTATION") + titleSuffix)
                        .Bold().FontSize(16);

                    // Timestamp + LocAl logo stacked top-right
                    row.AutoItem().AlignRight().Column(rightCol =>
                    {
                        rightCol.Item().AlignRight()
                            .Text(DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt"))
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                        if (localLogo != null)
                            rightCol.Item().PaddingTop(4).AlignRight().Width(110).Image(localLogo);
                    });
                });

                // ── Project info table ───────────────────────────────────────
                col.Item().PaddingBottom(4).Table(tbl =>
                {
                    tbl.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(75);   // label 1
                        c.RelativeColumn(3);    // value 1
                        c.ConstantColumn(75);   // label 2
                        c.RelativeColumn(2);    // value 2
                    });

                    static IContainer InfoLbl(IContainer c) =>
                        c.Background(CapralTeal).Padding(5)
                         .DefaultTextStyle(s => s.FontColor(CapralWhite).Bold().FontSize(9));
                    static IContainer InfoVal(IContainer c) =>
                        c.Border(1).BorderColor("#CCCCCC").Padding(5)
                         .DefaultTextStyle(s => s.FontSize(9));

                    var quoteDate = (project.RevisionDate ?? DateTime.Today).ToString("dd.MM.yyyy");
                    var validity  = string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity;

                    // Row 1: Customer | value | Quote Date | value
                    tbl.Cell().Element(InfoLbl).Text("Customer");
                    tbl.Cell().Element(InfoVal).Text(project.CustomerName);
                    tbl.Cell().Element(InfoLbl).Text("Quote Date");
                    tbl.Cell().Element(InfoVal).Text(quoteDate);

                    // Row 2: Project | value | (blank) | quote validity italic
                    tbl.Cell().Element(InfoLbl).Text("Project");
                    tbl.Cell().Element(InfoVal).Text(project.ProjectName);
                    tbl.Cell().Element(InfoVal);   // blank
                    tbl.Cell().Element(InfoVal).Text($"Quote valid {validity}").Italic();

                    // Row 3: Finish | value (spans 1 col) | Quoted By | value
                    tbl.Cell().Element(InfoLbl).Text("Finish");
                    tbl.Cell().Element(InfoVal).Text(finish);
                    tbl.Cell().Element(InfoLbl).Text("Quoted By");
                    tbl.Cell().Element(InfoVal).Text(quotedBy);
                });

                // Finish sub-label (italic note)
                if (!string.Equals(project.FinishType, "Mill Finish", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(project.FinishGroup))
                {
                    col.Item().PaddingBottom(6).PaddingLeft(2)
                        .Text($"Finish quoted as {project.FinishType} - {project.FinishGroup}.")
                        .Italic().FontSize(8).FontColor(Colors.Grey.Darken1);
                }
                else
                {
                    col.Item().Height(6);
                }

                // ── Quote lines table ────────────────────────────────────────
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(30);   // Ln
                        c.ConstantColumn(72);   // Section
                        c.RelativeColumn(3);    // Description
                        c.ConstantColumn(50);   // Alloy
                        c.ConstantColumn(52);   // Temper
                        c.ConstantColumn(68);   // Length (m)
                        c.ConstantColumn(46);   // Qty
                        if (!unpriced)
                        {
                            c.ConstantColumn(68); // $ Each
                            c.ConstantColumn(72); // $ Total
                        }
                    });

                    static IContainer TblHdr(IContainer c) =>
                        c.Background(CapralTeal).Padding(4)
                         .DefaultTextStyle(s => s.FontColor(CapralWhite).Bold().FontSize(9));

                    static IContainer TblCell(IContainer c) =>
                        c.BorderBottom(1).BorderColor("#DDDDDD").PaddingVertical(3).PaddingHorizontal(4);

                    table.Header(hdr =>
                    {
                        hdr.Cell().Element(TblHdr).Text("Ln");
                        hdr.Cell().Element(TblHdr).Text("Section");
                        hdr.Cell().Element(TblHdr).Text("Description");
                        hdr.Cell().Element(TblHdr).Text("Alloy");
                        hdr.Cell().Element(TblHdr).Text("Temper");
                        hdr.Cell().Element(TblHdr).Text("Length (m)");
                        hdr.Cell().Element(TblHdr).Text("Qty");
                        if (!unpriced)
                        {
                            hdr.Cell().Element(TblHdr).Text("$ Each");
                            hdr.Cell().Element(TblHdr).Text("$ Total");
                        }
                    });

                    int rowIndex = 0;
                    foreach (var line in lines)
                    {
                        rowIndex++;
                        var bgColor = rowIndex % 2 == 0 ? "#F5F5F5" : CapralWhite;

                        IContainer RowCell(IContainer c) =>
                            TblCell(c).Background(bgColor);

                        var lineNum = (line.LineNumber <= 0 ? line.RowInBlock : line.LineNumber).ToString();
                        table.Cell().Element(RowCell).Text(lineNum).FontSize(9);
                        table.Cell().Element(RowCell).Text(line.ExtrusionCode).FontSize(9);
                        table.Cell().Element(RowCell).Text(line.Description).FontSize(9);
                        table.Cell().Element(RowCell).Text(line.Alloy).FontSize(9);
                        table.Cell().Element(RowCell).Text(line.Temper).FontSize(9);
                        table.Cell().Element(RowCell).AlignRight().Text(line.LengthM.ToString("N1")).FontSize(9);
                        table.Cell().Element(RowCell).AlignRight().Text(line.Quantity.ToString("N0")).FontSize(9);
                        if (!unpriced)
                        {
                            table.Cell().Element(RowCell).AlignRight().Text(line.Price.ToString("N4")).FontSize(9);
                            table.Cell().Element(RowCell).AlignRight().Text(line.TotalPrice.ToString("N4")).FontSize(9);
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
