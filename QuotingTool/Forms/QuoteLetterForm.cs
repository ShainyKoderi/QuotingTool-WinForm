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
        private readonly QuotePdfService _quotePdf;
        private int _projectId;
        private Project? _project;
        private decimal _projectPrice;

        private const string LetterBodyText =
            "We have pleasure in supplying our quotation for Capral Architectural Glazing Systems " +
            "to the above project. Your quotation is based on the attached schedule of materials " +
            "delivered in a single order unless otherwise agreed in writing. Any project variations " +
            "or quantity make-ups may be charged at your standard pricing. Please confirm all " +
            "sections are correct prior to placing your order.\r\n\r\n" +
            "On acceptance of the above quotation please forward your purchase order by fax to " +
            "1300 133 975.\r\n\r\n" +
            "If you require a warranty certificate please forward a warranty request form prior to " +
            "us submitting a firm quotation. All warranty powder coating attracts a minimum $250.00 charge.\r\n\r\n" +
            "This quotation is subject to Capral Limited's standard conditions of sale (unless " +
            "otherwise agreed in writing) which can be viewed on our website at www.capral.com.au.\r\n\r\n" +
            "If you require further information please contact your Account Manager.\r\n\r\n" +
            "Thank you for choosing Capral Architectural Glazing Systems.\r\n\r\n" +
            "Yours truly,";

        public QuoteLetterForm(DatabaseContext db)
        {
            _db = db;
            _quotePdf = new QuotePdfService(db);
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
                lblBrand.Text = "CAPRAL ALUMINIUM";
                lblCompanyInfo.Text = string.Empty;
                lblTo.Text = "To:";
                lblToValue.Text = string.Empty;
                lblAttention.Text = "Attention:";
                lblAttentionValue.Text = string.Empty;
                lblDate.Text = "Date:";
                lblDateValue.Text = string.Empty;
                lblRe.Text = "Re:";
                lblReValue.Text = string.Empty;
                lblLetterBody.Text = string.Empty;
                lblColour.Text = "Colour / Finish:";
                lblColourValue.Text = string.Empty;
                lblProjectPrice.Text = "Project Price (ex GST):";
                lblProjectPriceValue.Text = string.Empty;
                lblDelivery.Text = "Delivery:";
                lblDeliveryValue.Text = string.Empty;
                lblFooterLeft.Text = "All prices exclude GST";
                lblFooterRight.Text = "Quote valid for 30 days";
                lblAccountManager.Text = string.Empty;
                lblLocalLogoValue.Text = string.Empty;
                lblCapralLogoPlaceholder.Text = "[ CAPRAL LOGO ]";
                lblLocalLogoPlaceholder.Text = string.Empty;
                return;
            }

            // j. Header changes based on Region
            var region = _project.Region ?? string.Empty;
            lblBrand.Text = QuotePdfService.GetRegionalHeader(region);
            lblCompanyInfo.Text = _quotePdf.GetRegionalContactInfo(region);

            // i. Local logo placeholder
            var logoLabel = QuotePdfService.GetLocalLogoLabel(_project);
            lblLocalLogoValue.Text = logoLabel;
            lblLocalLogoPlaceholder.Text = string.IsNullOrWhiteSpace(logoLabel)
                ? string.Empty
                : $"[ {logoLabel} ]";

            // i. Capral logo placeholder (always shown)
            lblCapralLogoPlaceholder.Text = "[ CAPRAL LOGO ]";

            // Field labels
            lblTo.Text = "To:";
            lblAttention.Text = "Attention:";
            lblDate.Text = "Date:";
            lblRe.Text = "Re:";
            lblColour.Text = "Colour / Finish:";
            lblProjectPrice.Text = "Project Price (ex GST):";
            lblDelivery.Text = "Delivery:";

            // Values
            lblToValue.Text = _project.CustomerName;
            lblAttentionValue.Text = string.IsNullOrWhiteSpace(_project.AttentionTo) ? _project.Attention : _project.AttentionTo;
            lblDateValue.Text = (_project.RevisionDate ?? DateTime.Today).ToString("dd MMMM yyyy");
            lblReValue.Text = _project.ProjectName;

            // k. Letter body
            lblLetterBody.Text = LetterBodyText;

            lblColourValue.Text = QuotePdfService.GetFinishDisplay(_project);
            lblProjectPriceValue.Text = _projectPrice.ToString("C2");
            lblDeliveryValue.Text = $"${_project.DeliveryCharge:N2} (standard delivery charge)";
            lblFooterLeft.Text = "All prices exclude GST";
            lblFooterRight.Text = $"Quote valid for {(_project.QuoteValidity ?? "30 days")}";
            lblAccountManager.Text = QuotePdfService.GetQuotedByDisplay(_project);
            lblTerms.Text =
                "Standard terms and conditions apply. Please review Capral Limited's standard " +
                "conditions of sale before ordering.\r\n\r\n" +
                "Delivery timing, final quantities, and any project-specific variations remain " +
                "subject to written confirmation and acceptance by Capral Limited.";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_projectId > 0)
                LoadProject(_projectId);
        }

    }
}
