using System;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class ProjectDetailsForm : Form
    {
        internal const string DefaultAttentionTo = "To whom it may concern";

        private readonly DatabaseContext _db;
        private readonly Cre8ImportService _cre8Import;
        private int _projectId;

        public ProjectDetailsForm(DatabaseContext db)
        {
            _db = db;
            _cre8Import = new Cre8ImportService(db);
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var project = _db.GetProject(projectId);
            if (project == null) return;

            txtCustomerName.Text = project.CustomerName;
            txtProjectName.Text = project.ProjectName;
            dtpRevisionDate.Value = project.RevisionDate ?? DateTime.Today;
            txtLMERate.Text = project.LMERate.ToString("F4");
            txtBilletPremium.Text = project.BilletPremium.ToString("F4");
            SetComboOrText(cmbRegion, project.Region);
            txtQuotedBy.Text = project.QuotedBy;
            txtAccountManager.Text = project.AccountManager;
            txtAttention.Text = string.IsNullOrWhiteSpace(project.AttentionTo) ? project.Attention : project.AttentionTo;
            SetComboOrText(cmbMarketSegment, project.MarketSegment);
            SetComboOrText(cmbProductSegment, project.ProductSegment);
            txtProjectNotes.Text = project.ProjectNotes;
            SetComboOrText(cmbFinishType, project.FinishType);
            SetComboOrText(cmbFinishApplicator, project.FinishApplicator);
            txtFinishGroup.Text = project.FinishGroup;
            txtFinishName.Text = project.FinishName;
            txtFinishCustomName.Text = project.FinishCustomName;
            txtFinishSapCode.Text = project.FinishSapCode;
            SetComboOrText(cmbThresholdChoice, project.ThresholdChoice);
            SetComboOrText(cmbLocalLogo, string.IsNullOrWhiteSpace(project.AddLocAlLogos) ? project.LocalLogo : project.AddLocAlLogos);
            SetComboOrText(cmbGenerateMillFinishAlternative, (project.GenerateMillFinishAlternative || project.GenerateMfAlt) ? "Yes" : "No");
            txtQuoteValidity.Text = string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity;
            txtCustomerSapDiscount.Text = project.CustomerSapDiscount.ToString("F2");
            txtDeliveryCharge.Text = project.DeliveryCharge.ToString("F2");
        }

        public void SaveProject()
        {
            if (_projectId <= 0) return;

            var project = _db.GetProject(_projectId) ?? new Project { Id = _projectId };
            project.CustomerName = txtCustomerName.Text.Trim();
            project.ProjectName = txtProjectName.Text.Trim();
            project.RevisionDate = dtpRevisionDate.Value.Date;
            project.Region = cmbRegion.Text.Trim();
            project.QuotedBy = txtQuotedBy.Text.Trim();
            project.AccountManager = txtAccountManager.Text.Trim();
            project.Attention = txtAttention.Text.Trim();
            project.AttentionTo = string.IsNullOrWhiteSpace(txtAttention.Text) ? DefaultAttentionTo : txtAttention.Text.Trim();
            project.MarketSegment = cmbMarketSegment.Text.Trim();
            project.ProductSegment = cmbProductSegment.Text.Trim();
            project.ProjectNotes = txtProjectNotes.Text.Trim();
            project.LocalLogo = cmbLocalLogo.Text.Trim();
            project.AddLocAlLogos = cmbLocalLogo.Text.Trim();
            project.FinishType = cmbFinishType.Text.Trim();
            project.FinishApplicator = cmbFinishApplicator.Text.Trim();
            project.FinishGroup = txtFinishGroup.Text.Trim();
            project.FinishName = txtFinishName.Text.Trim();
            project.FinishCustomName = txtFinishCustomName.Text.Trim();
            project.FinishSapCode = txtFinishSapCode.Text.Trim();
            project.ThresholdChoice = cmbThresholdChoice.Text.Trim();
            project.GenerateMillFinishAlternative = string.Equals(cmbGenerateMillFinishAlternative.Text, "Yes", StringComparison.OrdinalIgnoreCase);
            project.GenerateMfAlt = project.GenerateMillFinishAlternative;
            project.Attention = string.IsNullOrWhiteSpace(project.Attention) ? DefaultAttentionTo : project.Attention;
            project.QuoteValidity = string.IsNullOrWhiteSpace(txtQuoteValidity.Text) ? "30 days" : txtQuoteValidity.Text.Trim();
            project.CustomerSapDiscount = ParseDecimal(txtCustomerSapDiscount.Text);
            project.DeliveryCharge = ParseDecimal(txtDeliveryCharge.Text, 97m);

            _db.UpdateProject(project);
            MessageBox.Show("Project details saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e) => SaveProject();

        private void btnImportCre8_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.");
                return;
            }

            using var ofd = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            };

            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                var result = _cre8Import.ImportProjectDetails(_projectId, ofd.FileName);
                LoadProject(_projectId);
                MessageBox.Show(result.Message, "Import CRE8", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Import failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SetComboOrText(ComboBox cmb, string value)
        {
            int idx = cmb.FindStringExact(value);
            if (idx >= 0)
                cmb.SelectedIndex = idx;
            else
                cmb.Text = value ?? string.Empty;
        }

        private static decimal ParseDecimal(string text, decimal fallback = 0m)
            => decimal.TryParse(text, out var value) ? value : fallback;
    }
}
