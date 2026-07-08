using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    public partial class QuoteLetterForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly PrintDocument _printDocument;
        private int _projectId;
        private Project? _project;
        private decimal _projectPrice;

        private const string LetterBodyText = """
            We have pleasure in supplying our quotation for Capral Architectural Glazing Systems to the above project. Your quotation is based on the attached schedule of materials delivered in a single order unless otherwise agreed in writing. Any project variations or quantity make-ups may be charged at your standard pricing. Please confirm all sections are correct prior to placing your order.

            On acceptance of the above quotation please forward your purchase order by fax to 1300 133 975.

            If you require a warranty certificate please forward a warranty request form prior to us submitting a firm quotation. All warranty powder coating attracts a minimum $250.00 charge.

            This quotation is subject to Capral Limited's standard conditions of sale (unless otherwise agreed in writing) which can be viewed on our website at www.capral.com.au.

            If you require further information please contact your Account Manager.

            Thank you for choosing Capral Architectural Glazing Systems.

            Yours truly,
            """;

        public QuoteLetterForm(DatabaseContext db)
        {
            _db = db;
            InitializeComponent();
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            _project = _db.GetProject(projectId);
            var lines = _db.GetQuoteLines(projectId).ToList();
            _projectPrice = lines.Sum(l => l.TotalPrice);
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (_project == null)
            {
                lblToValue.Text = string.Empty;
                lblAttentionValue.Text = string.Empty;
                lblDateValue.Text = string.Empty;
                lblReValue.Text = string.Empty;
                lblColourValue.Text = string.Empty;
                lblProjectPriceValue.Text = string.Empty;
                lblDeliveryValue.Text = string.Empty;
                lblFooterLeft.Text = "All prices exclude GST";
                lblFooterRight.Text = "Quote valid for 30 days";
                lblAccountManager.Text = string.Empty;
                return;
            }

            lblToValue.Text = _project.CustomerName;
            lblAttentionValue.Text = string.IsNullOrWhiteSpace(_project.AttentionTo) ? _project.Attention : _project.AttentionTo;
            lblDateValue.Text = (_project.RevisionDate ?? DateTime.Today).ToString("dd MMMM yyyy");
            lblReValue.Text = _project.ProjectName;
            lblColourValue.Text = string.IsNullOrWhiteSpace(_project.FinishName) ? _project.FinishType : _project.FinishName;
            lblProjectPriceValue.Text = _projectPrice.ToString("C2");
            lblDeliveryValue.Text = _project.DeliveryCharge.ToString("C2");
            lblFooterLeft.Text = "All prices exclude GST";
            lblFooterRight.Text = $"Quote valid for {(_project.QuoteValidity ?? "30 days")}";
            lblAccountManager.Text = string.IsNullOrWhiteSpace(_project.AccountManager)
                ? string.IsNullOrWhiteSpace(_project.QuotedBy) ? string.Empty : _project.QuotedBy
                : _project.AccountManager;
            lblLetterBody.Text = LetterBodyText;
            lblTerms.Text = "Standard terms and conditions apply. Please review Capral Limited's standard conditions of sale before ordering.\n\nDelivery timing, final quantities, and any project-specific variations remain subject to written confirmation and acceptance by Capral Limited.";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_projectId > 0)
                LoadProject(_projectId);
        }

        private void btnPrintLetter_Click(object sender, EventArgs e)
        {
            using var preview = new PrintPreviewDialog { Document = _printDocument, Width = 1000, Height = 700 };
            preview.ShowDialog(this);
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"QuoteLetter_{SanitizeFileNamePart(_project.ProjectName)}_{DateTime.Today:yyyyMMdd}.pdf"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                BuildPdf(sfd.FileName);
                MessageBox.Show($"Letter saved to:\n{sfd.FileName}", "Save as PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null) return;
            using var bmp = new Bitmap(pnlPreview.Width, pnlPreview.Height);
            pnlPreview.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            var bounds = e.MarginBounds;
            float ratio = Math.Min((float)bounds.Width / bmp.Width, (float)bounds.Height / bmp.Height);
            int width = (int)(bmp.Width * ratio);
            int height = (int)(bmp.Height * ratio);
            e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, width, height);
        }

        private void BuildPdf(string filePath)
        {
            var project = _project ?? new Project();
            var attention = string.IsNullOrWhiteSpace(project.AttentionTo) ? project.Attention : project.AttentionTo;
            var colour = string.IsNullOrWhiteSpace(project.FinishName) ? project.FinishType : project.FinishName;
            var quotedBy = string.IsNullOrWhiteSpace(project.AccountManager) ? project.QuotedBy : project.AccountManager;
            var quoteValidity = string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity;
            var letterParagraphs = LetterBodyText.Trim().Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            const string terms = "Standard terms and conditions apply. Please review Capral Limited's standard conditions of sale before ordering.\n\nDelivery timing, final quantities, and any project-specific variations remain subject to written confirmation and acceptance by Capral Limited.";

            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(32);
                    page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(10));
                    page.Content().Column(column =>
                    {
                        column.Spacing(10);
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("CAPRAL ALUMINIUM").Bold().FontSize(22).FontColor(Colors.Teal.Darken2);
                            row.ConstantItem(220).AlignRight().Column(meta =>
                            {
                                meta.Item().Text("Capral Limited");
                                meta.Item().Text("ABN 78 004 213 692");
                                meta.Item().Text("Building System Group and Distribution");
                                meta.Item().Text("T: 1300 366 517   F: 1300 133 975");
                                meta.Item().Text("www.capral.com.au");
                            });
                        });

                        column.Item().Text($"To: {project.CustomerName}");
                        column.Item().Text($"Attention: {attention}");
                        column.Item().Text($"Date: {(project.RevisionDate ?? DateTime.Today):dd MMMM yyyy}");
                        column.Item().Text($"Re: {project.ProjectName}");

                        foreach (var paragraph in letterParagraphs)
                            column.Item().Text(paragraph.Trim());

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(180);
                                columns.RelativeColumn();
                            });

                            static IContainer CellStyle(IContainer container) => container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(6);

                            table.Cell().Element(CellStyle).Text("Colour").Bold();
                            table.Cell().Element(CellStyle).Text(colour);
                            table.Cell().Element(CellStyle).Text("Project Price $").Bold();
                            table.Cell().Element(CellStyle).Text(_projectPrice.ToString("C2"));
                            table.Cell().Element(CellStyle).Text("Delivery $").Bold();
                            table.Cell().Element(CellStyle).Text(project.DeliveryCharge.ToString("C2"));
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("All prices exclude GST").Bold();
                            row.RelativeItem().AlignRight().Text($"Quote valid for {quoteValidity}").Bold();
                        });

                        column.Item().Text(terms);
                        column.Item().Text(quotedBy);
                    });
                });
            }).GeneratePdf(filePath);
        }

        private static string SanitizeFileNamePart(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Project";

            foreach (var invalid in Path.GetInvalidFileNameChars())
                value = value.Replace(invalid, '_');

            return value;
        }
    }
}
