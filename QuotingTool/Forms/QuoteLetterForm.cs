using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class QuoteLetterForm : Form
    {
        private readonly DatabaseContext _db;
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
                lblLocalLogoValue.Text = string.Empty;
                return;
            }

            lblBrand.Text = QuotePdfService.GetRegionalHeader(_project.Region);
            lblToValue.Text = _project.CustomerName;
            lblAttentionValue.Text = string.IsNullOrWhiteSpace(_project.AttentionTo) ? _project.Attention : _project.AttentionTo;
            lblDateValue.Text = (_project.RevisionDate ?? DateTime.Today).ToString("dd MMMM yyyy");
            lblReValue.Text = _project.ProjectName;
            lblColourValue.Text = string.IsNullOrWhiteSpace(_project.FinishName) ? _project.FinishType : _project.FinishName;
            lblProjectPriceValue.Text = _projectPrice.ToString("C2");
            lblDeliveryValue.Text = "Shown on the Quote PDF";
            lblFooterLeft.Text = "All prices exclude GST";
            lblFooterRight.Text = $"Quote valid for {(_project.QuoteValidity ?? "30 days")}";
            lblAccountManager.Text = string.IsNullOrWhiteSpace(_project.AccountManager)
                ? string.IsNullOrWhiteSpace(_project.QuotedBy) ? string.Empty : _project.QuotedBy
                : _project.AccountManager;
            lblLocalLogoValue.Text = QuotePdfService.GetLocalLogoLabel(_project);
            lblLetterBody.Text = LetterBodyText;
            lblTerms.Text = "Standard terms and conditions apply. Please review Capral Limited's standard conditions of sale before ordering.\n\nDelivery timing, final quantities, and any project-specific variations remain subject to written confirmation and acceptance by Capral Limited.";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_projectId > 0)
                LoadProject(_projectId);
        }

    }
}
