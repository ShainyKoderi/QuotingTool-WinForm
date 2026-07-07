using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
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
                Filter = "HTML Files (*.html)|*.html",
                DefaultExt = "html",
                FileName = $"QuoteNoPricing_{DateTime.Today:yyyyMMdd}.html"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, BuildHtml(), Encoding.UTF8);
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

        private string BuildHtml()
        {
            string Encode(string value) => WebUtility.HtmlEncode(value ?? string.Empty);
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html><head><meta charset='utf-8' /><title>Quotation - Not Itemised</title>");
            sb.Append("<style>body{font-family:Segoe UI,Arial,sans-serif;margin:32px;}h1{font-size:24px;}table{width:100%;border-collapse:collapse;margin-top:18px;}th,td{border:1px solid #ccc;padding:8px;}th{background:#f2f2f2;}</style></head><body>");
            sb.Append("<h1>QUOTATION - NOT ITEMISED</h1>");
            sb.Append($"<p><strong>Customer:</strong> {Encode(_project?.CustomerName ?? string.Empty)}<br />");
            sb.Append($"<strong>Project:</strong> {Encode(_project?.ProjectName ?? string.Empty)}<br />");
            sb.Append($"<strong>Quote Date:</strong> {Encode((_project?.RevisionDate ?? DateTime.Today).ToString("dd/MM/yyyy"))}<br />");
            sb.Append($"<strong>Validity:</strong> Quote valid {Encode(_project?.QuoteValidity ?? "30 days")}<br />");
            sb.Append($"<strong>Finish:</strong> {Encode(string.IsNullOrWhiteSpace(_project?.FinishName) ? _project?.FinishType ?? string.Empty : _project!.FinishName)}<br />");
            sb.Append($"<strong>Quoted By:</strong> {Encode(string.IsNullOrWhiteSpace(_project?.QuotedBy) ? _project?.AccountManager ?? string.Empty : _project.QuotedBy)}</p>");
            sb.Append("<table><thead><tr><th>Ln</th><th>Section</th><th>Description</th><th>Alloy</th><th>Temper</th><th>Length (m)</th><th>Qty</th></tr></thead><tbody>");
            foreach (var line in _lines)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{line.LineNumber}</td><td>{Encode(line.ExtrusionCode)}</td><td>{Encode(line.Description)}</td><td>{Encode(line.Alloy)}</td><td>{Encode(line.Temper)}</td><td>{line.LengthM}</td><td>{line.Quantity}</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table></body></html>");
            return sb.ToString();
        }
    }
}
