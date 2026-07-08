using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    public partial class QuoteNoPricingForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly PrintDocument _printDocument;
        private int _projectId;
        private Project? _project;
        private List<QuoteLine> _lines = new();

        public QuoteNoPricingForm(DatabaseContext db)
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
            _lines = _db.GetQuoteLines(projectId)
                .Where(l => !string.IsNullOrWhiteSpace(l.ExtrusionCode) || !string.IsNullOrWhiteSpace(l.Description))
                .OrderBy(l => l.LineNumber == 0 ? l.RowInBlock : l.LineNumber)
                .ToList();

            lblCustomerValue.Text = _project?.CustomerName ?? string.Empty;
            lblProjectValue.Text = _project?.ProjectName ?? string.Empty;
            lblQuoteDateValue.Text = (_project?.RevisionDate ?? DateTime.Today).ToString("dd/MM/yyyy");
            lblValidityValue.Text = $"Quote valid {(_project?.QuoteValidity ?? "30 days")}";
            lblFinishValue.Text = string.IsNullOrWhiteSpace(_project?.FinishName) ? _project?.FinishType ?? string.Empty : _project.FinishName;
            lblQuotedByValue.Text = string.IsNullOrWhiteSpace(_project?.QuotedBy) ? _project?.AccountManager ?? string.Empty : _project.QuotedBy;
            gridQuoteNoPricing.DataSource = _lines;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using var preview = new PrintPreviewDialog { Document = _printDocument, Width = 1000, Height = 700 };
            preview.ShowDialog(this);
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"QuoteNoPricing_{DateTime.Today:yyyyMMdd}.pdf"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                BuildPdf(sfd.FileName);
                MessageBox.Show($"Quote saved to:\n{sfd.FileName}", "Save as PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null) return;
            using var bmp = new Bitmap(pnlContent.Width, pnlContent.Height);
            pnlContent.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            var bounds = e.MarginBounds;
            float ratio = Math.Min((float)bounds.Width / bmp.Width, (float)bounds.Height / bmp.Height);
            e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, bmp.Width * ratio, bmp.Height * ratio);
        }

        private void BuildPdf(string filePath)
        {
            var project = _project ?? new Project();
            var finish = string.IsNullOrWhiteSpace(project.FinishName) ? project.FinishType : project.FinishName;
            var quotedBy = string.IsNullOrWhiteSpace(project.QuotedBy) ? project.AccountManager : project.QuotedBy;
            var validity = string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity;

            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(24);
                    page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(9));
                    page.Content().Column(column =>
                    {
                        column.Spacing(8);
                        column.Item().Text("QUOTATION - NOT ITEMISED").Bold().FontSize(18);
                        column.Item().Text($"Customer: {project.CustomerName}");
                        column.Item().Text($"Project: {project.ProjectName}");
                        column.Item().Text($"Quote Date: {(project.RevisionDate ?? DateTime.Today):dd/MM/yyyy}");
                        column.Item().Text($"Validity: Quote valid {validity}");
                        column.Item().Text($"Finish: {finish}");
                        column.Item().Text($"Quoted By: {quotedBy}");
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(36);
                                columns.ConstantColumn(80);
                                columns.RelativeColumn(2);
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(70);
                                columns.ConstantColumn(50);
                            });

                            static IContainer HeaderStyle(IContainer container) => container.Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.Grey.Lighten3).Padding(4);
                            static IContainer CellStyle(IContainer container) => container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(4);

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderStyle).Text("Ln").Bold();
                                header.Cell().Element(HeaderStyle).Text("Section").Bold();
                                header.Cell().Element(HeaderStyle).Text("Description").Bold();
                                header.Cell().Element(HeaderStyle).Text("Alloy").Bold();
                                header.Cell().Element(HeaderStyle).Text("Temper").Bold();
                                header.Cell().Element(HeaderStyle).Text("Length (m)").Bold();
                                header.Cell().Element(HeaderStyle).Text("Qty").Bold();
                            });

                            foreach (var line in _lines)
                            {
                                table.Cell().Element(CellStyle).Text((line.LineNumber == 0 ? line.RowInBlock : line.LineNumber).ToString());
                                table.Cell().Element(CellStyle).Text(line.ExtrusionCode);
                                table.Cell().Element(CellStyle).Text(line.Description);
                                table.Cell().Element(CellStyle).Text(line.Alloy);
                                table.Cell().Element(CellStyle).Text(line.Temper);
                                table.Cell().Element(CellStyle).Text(line.LengthM.ToString("N2"));
                                table.Cell().Element(CellStyle).Text(line.Quantity.ToString("N2"));
                            }
                        });
                    });
                });
            }).GeneratePdf(filePath);
        }
    }
}
