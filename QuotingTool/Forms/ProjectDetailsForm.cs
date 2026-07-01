using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Tab 1 – Project Details
    /// Maps to "1.Project Details" sheet in the .xlsm.
    /// Fields: Customer, Project, RevisionDate, LME, BilletPremium (read-only),
    ///   Region, QuotedBy, AccountManager, Attention, MarketSegment, ProductSegment,
    ///   ProjectNotes, FinishType, FinishApplicator, FinishGroup, FinishName,
    ///   FinishCustomName, FinishSapCode, ThresholdChoice, GenerateMfAlt, LocalLogo.
    /// </summary>
    public partial class ProjectDetailsForm : Form
    {
        private readonly DatabaseContext _db;
        private int _projectId;

        public ProjectDetailsForm(DatabaseContext db)
        {
            _db = db;
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var project = _db.GetProject(projectId);
            if (project == null) return;

            txtCustomerName.Text      = project.CustomerName;
            txtProjectName.Text       = project.ProjectName;
            dtpRevisionDate.Value     = project.RevisionDate ?? DateTime.Today;
            txtLMERate.Text           = project.LMERate.ToString("F4");
            txtBilletPremium.Text     = project.BilletPremium.ToString("F4");
            // New fields
            SetComboOrText(cmbRegion, project.Region);
            txtQuotedBy.Text          = project.QuotedBy;
            txtAccountManager.Text    = project.AccountManager;
            txtAttention.Text         = project.Attention;
            SetComboOrText(cmbMarketSegment, project.MarketSegment);
            SetComboOrText(cmbProductSegment, project.ProductSegment);
            txtProjectNotes.Text      = project.ProjectNotes;
            SetComboOrText(cmbFinishType, project.FinishType);
            SetComboOrText(cmbFinishApplicator, project.FinishApplicator);
            txtFinishGroup.Text       = project.FinishGroup;
            txtFinishName.Text        = project.FinishName;
            txtFinishCustomName.Text  = project.FinishCustomName;
            txtFinishSapCode.Text     = project.FinishSapCode;
            SetComboOrText(cmbThresholdChoice, project.ThresholdChoice);
            SetComboOrText(cmbLocalLogo, project.LocalLogo);
            chkGenerateMfAlt.Checked  = project.GenerateMfAlt;
        }

        public void SaveProject()
        {
            if (_projectId <= 0) return;

            var project = _db.GetProject(_projectId) ?? new Project { Id = _projectId };
            project.CustomerName    = txtCustomerName.Text.Trim();
            project.ProjectName     = txtProjectName.Text.Trim();
            project.RevisionDate    = dtpRevisionDate.Value.Date;
            // New fields
            project.Region          = cmbRegion.Text.Trim();
            project.QuotedBy        = txtQuotedBy.Text.Trim();
            project.AccountManager  = txtAccountManager.Text.Trim();
            project.Attention       = txtAttention.Text.Trim();
            project.MarketSegment   = cmbMarketSegment.Text.Trim();
            project.ProductSegment  = cmbProductSegment.Text.Trim();
            project.ProjectNotes    = txtProjectNotes.Text.Trim();
            project.LocalLogo       = cmbLocalLogo.Text.Trim();
            project.FinishType      = cmbFinishType.Text.Trim();
            project.FinishApplicator = cmbFinishApplicator.Text.Trim();
            project.FinishGroup     = txtFinishGroup.Text.Trim();
            project.FinishName      = txtFinishName.Text.Trim();
            project.FinishCustomName = txtFinishCustomName.Text.Trim();
            project.FinishSapCode   = txtFinishSapCode.Text.Trim();
            project.ThresholdChoice = cmbThresholdChoice.Text.Trim();
            project.GenerateMfAlt   = chkGenerateMfAlt.Checked;

            _db.UpdateProject(project);
            MessageBox.Show("Project details saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e) => SaveProject();

        /// <summary>Sets combo selection to value, or sets Text if not in items list.</summary>
        private static void SetComboOrText(ComboBox cmb, string value)
        {
            int idx = cmb.FindStringExact(value);
            if (idx >= 0)
                cmb.SelectedIndex = idx;
            else
                cmb.Text = value ?? string.Empty;
        }
    }
}
