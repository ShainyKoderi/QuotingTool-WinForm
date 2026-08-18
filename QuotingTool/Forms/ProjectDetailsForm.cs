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
        private bool _refreshing;

        // Applicator options matching Excel named ranges: Applicator_ano, Applicator (powder), finish_mill
        private static readonly string[] _applicatorAnodised =
            ["n/a", "Capral/AAF QLD anodising", "WA Anodisers", "Other - pls check pricing"];
        private static readonly string[] _applicatorPowder =
            ["n/a", "Capral P&P - Bremer (AP)", "Capral P&P - Campbellfield (AP)",
             "Capral P&P - Canningvale (PP)", "Capral P&P - HWood Non-Bulk (AP)",
             "Capral P&P - HWood Bulk (AP)", "Capral Mill (AP)",
             "AAF powdercoat", "Powderline WA", "Other - pls check pricing"];
        private static readonly string[] _applicatorMillFinish = ["n/a"];

        // Finish Group options matching Excel named ranges
        private static readonly string[] _finishGroupMillFinish   = ["n/a"];
        private static readonly string[] _finishGroupAnodised     = ["20um Clear", "25um Clear", "Group POA"];
        private static readonly string[] _finishGroupPowderCapral = ["Group 1", "Group 2", "Group 3", "Group 4", "Group 5", "Group POA"];
        private static readonly string[] _finishGroupPowderOther  = ["Group 1", "Group 2", "Group 3", "Group POA"];

        private int CurrentProjectId => _projectId;
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
            // Cascade: populate each dependent list before setting its value
            SetComboOrText(cmbFinishType, project.FinishType);
            RefreshApplicatorOptions();
            SetComboOrText(cmbFinishApplicator, project.FinishApplicator);
            RefreshFinishGroupOptions();
            SetComboOrText(cmbFinishGroup, project.FinishGroup);
            RefreshFinishNameOptions();
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

            // Load all simple dropdowns from the ValidationData table (single source of
            // truth extracted from the Excel Validation sheet). Falls back to any items
            // already in the combo (designer defaults) when the DB has no rows yet.
            LoadComboFromDb(cmbRegion,                       "Region");
            LoadComboFromDb(cmbMarketSegment,                 "MarketSegment");
            LoadComboFromDb(cmbProductSegment,                "ProductSegment");
            LoadComboFromDb(cmbLocalLogo,                     "LocAlLogoOptions");
            LoadComboFromDb(cmbFinishType,                    "FinishType");
            LoadComboFromDb(cmbGenerateMillFinishAlternative, "YesNo");

            // Clear any designer-default items from the cascaded finish combos so they
            // start blank.  They are repopulated dynamically by the Refresh* methods
            // when a project is loaded (RefreshApplicatorOptions / RefreshFinishGroupOptions
            // / RefreshFinishNameOptions).
            cmbFinishApplicator.Items.Clear();
            cmbFinishGroup.Items.Clear();
            cmbFinishName.Items.Clear();

            // ThresholdChoice: the DB stores "No" but the UI label is "Project Colour".
            // Build the items list with the translation applied.
            var thresholdItems = _db.GetValidationItems("ThresholdChoice").ToList();
            if (thresholdItems.Count > 0)
            {
                cmbThresholdChoice.Items.Clear();
                // "No" → display as "Project Colour" (see ToThresholdDisplayValue)
                cmbThresholdChoice.Items.Add(ProjectColourThresholdOption);
                foreach (var item in thresholdItems
                             .Where(v => !string.Equals(v.Value, "No", StringComparison.OrdinalIgnoreCase)))
                    cmbThresholdChoice.Items.Add(item.Value);
            }

            // Finish Applicator and Finish Group are populated dynamically based on Finish Type
            // (mirrors the Excel IF-formula cascade in B26/B28).
            cmbFinishType.SelectedIndexChanged += (_, _) =>
            {
                if (_loadingProject || _refreshing) return;
                RefreshApplicatorOptions();
                RefreshFinishGroupOptions();
                RefreshFinishNameOptions();
            };
            cmbFinishApplicator.SelectedIndexChanged += (_, _) =>
            {
                if (_loadingProject || _refreshing) return;
                RefreshFinishGroupOptions();
                RefreshFinishNameOptions();
            };
            cmbFinishGroup.SelectedIndexChanged += (_, _) =>
            {
                if (_loadingProject || _refreshing) return;
                RefreshFinishNameOptions();
            };
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

        /// <summary>
        /// Populates <paramref name="cmb"/> from the ValidationData table for the given
        /// <paramref name="category"/>. No-ops when the DB returns no rows so the
        /// designer-defined items remain as a fallback.
        /// </summary>
        private void LoadComboFromDb(ComboBox cmb, string category)
        {
            var items = _db.GetValidationItems(category).Select(v => v.Value).ToList();
            if (items.Count == 0) return;
            cmb.Items.Clear();
            foreach (var item in items)
                cmb.Items.Add(item);
        }

        /// <summary>
        /// Repopulates <see cref="cmbFinishApplicator"/> based on the selected Finish Type,
        /// mirroring the Excel formula =IF(B24="Anodised",Applicator_ano,IF(B24="Powder",Applicator,finish_mill)).
        /// Preserves the current selection when it remains valid; resets to "n/a" otherwise.
        /// </summary>
        private void RefreshApplicatorOptions()
        {
            var current = cmbFinishApplicator.Text;
            string[] options = cmbFinishType.Text.Trim() switch
            {
                "Anodised" => _applicatorAnodised,
                "Powder"   => _applicatorPowder,
                _          => _applicatorMillFinish  // Mill Finish (or blank)
            };

            _refreshing = true;
            try
            {
                cmbFinishApplicator.Items.Clear();
                foreach (var o in options)
                    cmbFinishApplicator.Items.Add(o);
                SetComboOrText(cmbFinishApplicator, current);
                // If previously selected value is no longer valid, default to "n/a"
                if (!_loadingProject &&
                    !options.Any(o => string.Equals(o, cmbFinishApplicator.Text, StringComparison.OrdinalIgnoreCase)))
                    SetComboOrText(cmbFinishApplicator, "n/a");
            }
            finally { _refreshing = false; }
        }

        /// <summary>
        /// Repopulates <see cref="cmbFinishGroup"/> based on Finish Type and whether the
        /// selected applicator is a Capral vendor, mirroring Excel B28:
        /// Mill Finish→n/a | Anodised→20um Clear/25um Clear/Group POA |
        /// Powder+Capral→Groups 1-5+POA | Powder+Other→Groups 1-3+POA.
        /// </summary>
        private void RefreshFinishGroupOptions()
        {
            var current = cmbFinishGroup.Text;
            var finishType = cmbFinishType.Text.Trim();
            bool isCapral = cmbFinishApplicator.Text.Trim()
                .StartsWith("Capral", StringComparison.OrdinalIgnoreCase);

            string[] options = finishType switch
            {
                "Anodised" => _finishGroupAnodised,
                "Powder"   => isCapral ? _finishGroupPowderCapral : _finishGroupPowderOther,
                _          => _finishGroupMillFinish  // Mill Finish (or blank)
            };

            _refreshing = true;
            try
            {
                cmbFinishGroup.Items.Clear();
                foreach (var o in options)
                    cmbFinishGroup.Items.Add(o);
                SetComboOrText(cmbFinishGroup, current);
                // If previously selected value is no longer valid, default to first option
                if (!_loadingProject &&
                    !options.Any(o => string.Equals(o, cmbFinishGroup.Text, StringComparison.OrdinalIgnoreCase)))
                    cmbFinishGroup.SelectedIndex = 0;
            }
            finally { _refreshing = false; }
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


        private void cmbGenerateMillFinishAlternative_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_loadingProject) return;

            // Auto-save so the DB reflects the new Yes/No value, then refresh the
            // MainForm tabs so the embedded Mill Finish Quote tab appears or disappears.
            // The tab is already embedded in MainForm – we must NOT open a floating window.
            SaveProject(showConfirmation: false);

            var main = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            main?.RefreshAllTabs();
        }

    }
}
