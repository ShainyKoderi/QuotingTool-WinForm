using System;
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
                Filter = "HTML Files (*.html)|*.html",
                DefaultExt = "html",
                FileName = $"QuoteLetter_{_project.ProjectName}_{DateTime.Today:yyyyMMdd}.html"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, BuildHtml(), Encoding.UTF8);
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

        private string BuildHtml()
        {
            var project = _project ?? new Project();
            string Encode(string value) => WebUtility.HtmlEncode(value ?? string.Empty);
            return $@"<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8' />
<title>Quote Letter</title>
<style>
body {{ font-family: Segoe UI, Arial, sans-serif; margin: 40px; color: #222; }}
.header {{ display: flex; justify-content: space-between; margin-bottom: 28px; }}
.brand {{ color: #008080; font-weight: 700; font-size: 28px; }}
.meta p {{ margin: 0; text-align: right; line-height: 1.4; }}
.field-row {{ margin: 10px 0; }}
.label {{ display: inline-block; width: 90px; font-weight: 600; }}
.box {{ margin: 18px 0; }}
.summary {{ margin: 24px 0; border-collapse: collapse; width: 100%; }}
.summary td {{ border: 1px solid #d0d0d0; padding: 8px 10px; }}
.footer {{ display: flex; justify-content: space-between; margin-top: 24px; font-weight: 600; }}
.small {{ margin-top: 24px; font-size: 12px; line-height: 1.5; color: #444; }}
</style>
</head>
<body>
<div class='header'>
<div class='brand'>CAPRAL ALUMINIUM</div>
<div class='meta'>
<p>Capral Limited</p>
<p>ABN 78 004 213 692</p>
<p>Building System Group and Distribution</p>
<p>T: 1300 366 517 &nbsp; F: 1300 133 975</p>
<p>www.capral.com.au</p>
</div>
</div>
<div class='field-row'><span class='label'>To</span>{Encode(project.CustomerName)}</div>
<div class='field-row'><span class='label'>Attention</span>{Encode(string.IsNullOrWhiteSpace(project.AttentionTo) ? project.Attention : project.AttentionTo)}</div>
<div class='field-row'><span class='label'>Date</span>{Encode((project.RevisionDate ?? DateTime.Today).ToString("dd MMMM yyyy"))}</div>
<div class='field-row'><span class='label'>Re</span>{Encode(project.ProjectName)}</div>
<div class='box'>{Encode(LetterBodyText).Replace("\n", "<br />")}</div>
<table class='summary'>
<tr><td><strong>Colour</strong></td><td>{Encode(string.IsNullOrWhiteSpace(project.FinishName) ? project.FinishType : project.FinishName)}</td></tr>
<tr><td><strong>Project Price $</strong></td><td>{Encode(_projectPrice.ToString("C2"))}</td></tr>
<tr><td><strong>Delivery $</strong></td><td>{Encode(project.DeliveryCharge.ToString("C2"))}</td></tr>
</table>
<div class='footer'><div>All prices exclude GST</div><div>Quote valid for {Encode(project.QuoteValidity)}</div></div>
<div class='small'>
<p>Standard terms and conditions apply. Please review Capral Limited's standard conditions of sale before ordering.</p>
<p>Delivery timing, final quantities, and any project-specific variations remain subject to written confirmation and acceptance by Capral Limited.</p>
</div>
<div class='box'>{Encode(string.IsNullOrWhiteSpace(project.AccountManager) ? project.QuotedBy : project.AccountManager)}</div>
</body>
</html>";
        }
    }
}
