using System;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class ProjectDetailsForm : Form
    {
        internal const string DefaultAttentionTo = "To whom it may concern";
        private const string CustomFinishOption = "Custom";
        private const string ProjectColourThresholdOption = "Project Colour";

        private readonly DatabaseContext _db;
        private List<ColourGroup> _colourGroups = new();
        private int _projectId;
        private bool _loadingProject;

        public ProjectDetailsForm(DatabaseContext db)
        {
            _db = db;
            InitializeComponent();
            ConfigureLookups();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var project = _db.GetProject(projectId);
            if (project == null) return;

            RefreshFinishNameOptions();
            _loadingProject = true;
            txtCustomerName.Text = project.CustomerName;
            txtProjectName.Text = project.ProjectName;
            dtpRevisionDate.Value = project.RevisionDate ?? DateTime.Today;
            SetComboOrText(cmbRegion, project.Region);
            txtQuotedBy.Text = project.QuotedBy;
            txtAccountManager.Text = project.AccountManager;
            if (string.IsNullOrWhiteSpace(txtQuotedBy.Text))
                txtQuotedBy.Text = UserSession.CurrentUser?.DisplayName ?? string.Empty;
            if (string.IsNullOrWhiteSpace(txtAccountManager.Text))
                txtAccountManager.Text = UserSession.CurrentUser?.DisplayName ?? string.Empty;
            txtAttention.Text = string.IsNullOrWhiteSpace(project.AttentionTo) ? project.Attention : project.AttentionTo;
            SetComboOrText(cmbMarketSegment, project.MarketSegment);
            SetComboOrText(cmbProductSegment, project.ProductSegment);
            txtProjectNotes.Text = project.ProjectNotes;
            SetComboOrText(cmbFinishType, project.FinishType);
            SetComboOrText(cmbFinishApplicator, project.FinishApplicator);
            SetComboOrText(cmbFinishGroup, project.FinishGroup);
            SetComboOrText(cmbFinishName, string.IsNullOrWhiteSpace(project.FinishName) ? string.Empty : project.FinishName);
            txtFinishCustomName.Text = project.FinishCustomName;
            txtFinishSapCode.Text = project.FinishSapCode;
            SetComboOrText(cmbThresholdChoice, ToThresholdDisplayValue(project.ThresholdChoice));
            SetComboOrText(cmbLocalLogo, string.IsNullOrWhiteSpace(project.AddLocAlLogos) ? project.LocalLogo : project.AddLocAlLogos);
            SetComboOrText(cmbGenerateMillFinishAlternative, (project.GenerateMillFinishAlternative || project.GenerateMfAlt) ? "Yes" : "No");
            txtQuoteValidity.Text = string.IsNullOrWhiteSpace(project.QuoteValidity) ? "30 days" : project.QuoteValidity;
            _loadingProject = false;
            UpdateFinishCustomState();
            UpdateFinishSapCode(autoOnly: true);
        }

        public void SaveProject(bool showConfirmation = true)
        {
            if (_projectId <= 0) return;

            var project = _db.GetProject(_projectId) ?? new Project { Id = _projectId };
            bool wasMillFinishAltSet = project.GenerateMillFinishAlternative || project.GenerateMfAlt;
            project.CustomerName = txtCustomerName.Text.Trim();
            project.ProjectName = txtProjectName.Text.Trim();
            project.RevisionDate = dtpRevisionDate.Value.Date;
            project.Region = cmbRegion.Text.Trim();
            project.QuotedBy = txtQuotedBy.Text.Trim();
            project.AccountManager = txtAccountManager.Text.Trim();
            if (string.IsNullOrWhiteSpace(project.QuotedBy))
                project.QuotedBy = UserSession.CurrentUser?.DisplayName ?? string.Empty;
            if (string.IsNullOrWhiteSpace(project.AccountManager))
                project.AccountManager = UserSession.CurrentUser?.DisplayName ?? string.Empty;
            project.Attention = txtAttention.Text.Trim();
            project.AttentionTo = string.IsNullOrWhiteSpace(txtAttention.Text) ? DefaultAttentionTo : txtAttention.Text.Trim();
            project.MarketSegment = cmbMarketSegment.Text.Trim();
            project.ProductSegment = cmbProductSegment.Text.Trim();
            project.ProjectNotes = txtProjectNotes.Text.Trim();
            project.LocalLogo = cmbLocalLogo.Text.Trim();
            project.AddLocAlLogos = cmbLocalLogo.Text.Trim();
            project.FinishType = cmbFinishType.Text.Trim();
            project.FinishApplicator = cmbFinishApplicator.Text.Trim();
            project.FinishGroup = cmbFinishGroup.Text.Trim();
            project.FinishName = string.Equals(cmbFinishName.Text.Trim(), CustomFinishOption, StringComparison.OrdinalIgnoreCase)
                ? txtFinishCustomName.Text.Trim()
                : cmbFinishName.Text.Trim();
            project.FinishCustomName = txtFinishCustomName.Text.Trim();
            project.FinishSapCode = txtFinishSapCode.Text.Trim();
            project.ThresholdChoice = ToThresholdStoredValue(cmbThresholdChoice.Text.Trim());
            project.GenerateMillFinishAlternative = string.Equals(cmbGenerateMillFinishAlternative.Text, "Yes", StringComparison.OrdinalIgnoreCase);
            project.GenerateMfAlt = project.GenerateMillFinishAlternative;
            project.Attention = string.IsNullOrWhiteSpace(project.Attention) ? DefaultAttentionTo : project.Attention;
            project.QuoteValidity = string.IsNullOrWhiteSpace(txtQuoteValidity.Text) ? "30 days" : txtQuoteValidity.Text.Trim();

            _db.UpdateProject(project);
            if (showConfirmation)
            {
                MessageBox.Show("Project details saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // c. Generate Mill Finish Alternative creates a SEPARATE duplicate project with
                // everything set to Mill Finish.  The original project's finish type and all
                // price calculations remain completely unchanged – only the new duplicate uses
                // Mill Finish settings.  We reload the form after duplication to confirm that
                // the displayed fields still reflect the original (non-Mill-Finish) project.
                if (project.GenerateMillFinishAlternative && !wasMillFinishAltSet)
                {
                    var answer = MessageBox.Show(
                        "Generate Mill Finish Alternative is set to Yes.\n\n" +
                        "Create a duplicate of this quote now with everything set to Mill Finish?\n\n" +
                        "The original quote prices will not be affected.",
                        "Mill Finish Alternative", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        int newId = _db.DuplicateProjectAsMillFinish(_projectId);
                        MessageBox.Show(
                            $"Mill Finish alternative created (quote ID {newId}).\n" +
                            "Use File → Open Project to open it.\n\n" +
                            "Your current quote prices have not changed.",
                            "Mill Finish Alternative", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Reload the original project to ensure the form reflects the unchanged
                        // original finish type (not the Mill Finish used in the duplicate).
                        LoadProject(_projectId);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e) => SaveProject();

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

        private void ConfigureLookups()
        {
            _colourGroups = _db.GetColourGroups().ToList();
            cmbFinishGroup.Items.Clear();
            cmbFinishGroup.Items.Add(string.Empty);
            foreach (var group in _colourGroups
                         .Select(c => c.GroupName)
                         .Where(g => g.StartsWith("GROUP", StringComparison.OrdinalIgnoreCase) || string.Equals(g, "n/a", StringComparison.OrdinalIgnoreCase))
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .OrderBy(g => g, StringComparer.OrdinalIgnoreCase))
            {
                cmbFinishGroup.Items.Add(group);
            }

            cmbFinishGroup.SelectedIndexChanged += (_, _) => RefreshFinishNameOptions();
            cmbFinishName.SelectedIndexChanged += (_, _) =>
            {
                UpdateFinishCustomState();
                UpdateFinishSapCode(autoOnly: true);
            };
            txtFinishSapCode.Leave += (_, _) => UpdateFinishSapCode(autoOnly: false);
            txtFinishCustomName.TextChanged += (_, _) =>
            {
                if (!_loadingProject && string.Equals(cmbFinishName.Text, CustomFinishOption, StringComparison.OrdinalIgnoreCase))
                    UpdateFinishSapCode(autoOnly: false);
            };
        }

        private void RefreshFinishNameOptions()
        {
            var selected = cmbFinishName.Text;
            cmbFinishName.Items.Clear();
            cmbFinishName.Items.Add(string.Empty);

            var matches = _colourGroups
                .Where(c => string.Equals(c.GroupName, cmbFinishGroup.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                .Select(c => c.LongDesc)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(v => v, StringComparer.OrdinalIgnoreCase);

            foreach (var match in matches)
                cmbFinishName.Items.Add(match);

            cmbFinishName.Items.Add(CustomFinishOption);
            SetComboOrText(cmbFinishName, selected);
            UpdateFinishCustomState();
        }

        private void UpdateFinishCustomState()
        {
            bool isCustom = string.Equals(cmbFinishName.Text, CustomFinishOption, StringComparison.OrdinalIgnoreCase);
            txtFinishCustomName.Enabled = isCustom;
            txtFinishCustomName.BackColor = isCustom ? System.Drawing.SystemColors.Window : System.Drawing.SystemColors.Control;
            if (!isCustom && !_loadingProject)
                txtFinishCustomName.Text = string.Empty;
        }

        private void UpdateFinishSapCode(bool autoOnly)
        {
            if (_loadingProject)
                return;

            if (autoOnly && !string.IsNullOrWhiteSpace(txtFinishSapCode.Text))
            {
                var existing = txtFinishSapCode.Text.Trim();
                var currentLookup = FindSelectedColourGroup();
                if (currentLookup == null || string.Equals(existing, currentLookup.ColourCode, StringComparison.OrdinalIgnoreCase))
                    return;
            }

            var colour = FindSelectedColourGroup();
            if (colour != null)
            {
                txtFinishSapCode.Text = colour.ColourCode;
                return;
            }

            if (string.Equals(cmbFinishName.Text, CustomFinishOption, StringComparison.OrdinalIgnoreCase))
                txtFinishSapCode.Text = string.IsNullOrWhiteSpace(txtFinishSapCode.Text) ? "NON" : txtFinishSapCode.Text.Trim();
        }

        private ColourGroup? FindSelectedColourGroup()
        {
            var finishName = cmbFinishName.Text.Trim();
            if (string.IsNullOrWhiteSpace(finishName) || string.Equals(finishName, CustomFinishOption, StringComparison.OrdinalIgnoreCase))
                return null;

            return _colourGroups.FirstOrDefault(c =>
                string.Equals(c.GroupName, cmbFinishGroup.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                (string.Equals(c.LongDesc, finishName, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(c.ShortDesc, finishName, StringComparison.OrdinalIgnoreCase)));
        }

        // The legacy workbook stores "Project Colour" as "No" because threshold rows only
        // receive a special threshold SAP code when an explicit clear-anodised option is chosen.
        // The WinForms UI shows the friendlier "Project Colour" label, but persistence keeps
        // compatibility with the existing workbook/database value.
        private static string ToThresholdStoredValue(string displayValue)
            => string.Equals(displayValue, ProjectColourThresholdOption, StringComparison.OrdinalIgnoreCase)
                ? "No"
                : displayValue;

        private static string ToThresholdDisplayValue(string storedValue)
            => string.Equals(storedValue, "No", StringComparison.OrdinalIgnoreCase)
                ? ProjectColourThresholdOption
                : storedValue;
    }
}
