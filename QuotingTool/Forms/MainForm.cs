using System;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Helpers;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Main application window with tabbed interface.
    /// Tabs: Project Details | Quote | Edit Pricing | BOM Import | Monthly Update
    /// </summary>
    public partial class MainForm : Form
    {
        public int CurrentProjectId { get; private set; }

        private readonly DatabaseContext _db;
        private readonly NavigationService _nav;
        private readonly PageSetupService _pageSetup;
        private readonly LengthFixService _lengthFix;
        private readonly ResetPriceService _resetPrice;
        private readonly ExportQuoteService _exportQuote;
        private readonly PivotRefreshService _pivotRefresh;

        public MainForm()
        {
            InitializeComponent();
            _db           = new DatabaseContext();
            _db.EnsureSchemaExists();
            _nav          = new NavigationService();
            _pageSetup    = new PageSetupService(_db);
            _lengthFix    = new LengthFixService(_db);
            _resetPrice   = new ResetPriceService(_db);
            _exportQuote  = new ExportQuoteService(_db);
            _pivotRefresh = new PivotRefreshService(_db);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadLastProject();
        }

        private void LoadLastProject()
        {
            try
            {
                var projects = new System.Collections.Generic.List<Project>(_db.GetAllProjects());
                if (projects.Count > 0)
                {
                    CurrentProjectId = projects[0].Id;
                    UpdateStatusBar(projects[0]);
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"DB not connected: {ex.Message}";
            }
        }

        public void SetCurrentProject(int projectId)
        {
            CurrentProjectId = projectId;
            var project = _db.GetProject(projectId);
            if (project != null) UpdateStatusBar(project);
            RefreshAllTabs();
        }

        private void UpdateStatusBar(Project project)
        {
            var ps = _db.GetPrintSetting(project.Id, "2.Quote");
            int pages = ps?.PageCount ?? 1;
            statusLabel.Text = $"Project: {project.CustomerName} – {project.ProjectName}  |  Pages: {pages}";
        }

        private void RefreshAllTabs()
        {
            if (projectDetailsTab.Controls.Count > 0 && projectDetailsTab.Controls[0] is ProjectDetailsForm pdf)
                pdf.LoadProject(CurrentProjectId);
            if (quoteTab.Controls.Count > 0 && quoteTab.Controls[0] is QuoteForm qf)
                qf.LoadProject(CurrentProjectId);
            if (editPricingTab.Controls.Count > 0 && editPricingTab.Controls[0] is EditPricingForm epf)
                epf.LoadProject(CurrentProjectId);
            if (bomTab.Controls.Count > 0 && bomTab.Controls[0] is BomImportForm bif)
                bif.LoadProject(CurrentProjectId);
            if (monthlyUpdateTab.Controls.Count > 0 && monthlyUpdateTab.Controls[0] is MonthlyUpdateForm muf)
                muf.LoadProject(CurrentProjectId);
        }

        // ── Toolbar buttons ──────────────────────────────────────────────────

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            int newId = _db.CreateProject(new Project
            {
                CustomerName = "New Customer",
                ProjectName  = "New Project",
                RevisionDate = DateTime.Today
            });
            // Seed edit pricing defaults
            _db.UpsertEditPricing(new EditPricing { ProjectId = newId });
            SetCurrentProject(newId);
            _nav.NavigateTo("Project Details", tabControl);
            MessageBox.Show($"New project created (ID {newId}).", "New Project",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            using var form = new OpenProjectDialog(_db);
            if (form.ShowDialog(this) == DialogResult.OK && form.SelectedProjectId > 0)
                SetCurrentProject(form.SelectedProjectId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Delegate save to active tab
            if (tabControl.SelectedTab == projectDetailsTab && projectDetailsTab.Controls[0] is ProjectDetailsForm pdf)
                pdf.SaveProject();
            else if (tabControl.SelectedTab == editPricingTab && editPricingTab.Controls[0] is EditPricingForm epf)
                epf.SavePricing();
            else
                MessageBox.Show("Nothing to save on the current tab.", "Save",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportQuote_Click(object sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) { MessageBox.Show("No project open."); return; }
            var result = _exportQuote.ExportQuoteData(CurrentProjectId);
            if (result.Status != ExportStatus.OK)
                MessageBox.Show(result.Message, "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            using var sfd = new SaveFileDialog
            {
                Filter     = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                FileName   = $"Quote_{DateTime.Today:yyyyMMdd}",
                DefaultExt = "xlsx"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                var exporter = new ExcelExporter(_db);
                if (sfd.FilterIndex == 1)
                    exporter.ExportToExcel(CurrentProjectId, sfd.FileName);
                else
                    exporter.ExportToCsv(CurrentProjectId, sfd.FileName);

                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFixLengths_Click(object sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) { MessageBox.Show("No project open."); return; }
            int fixed_ = _lengthFix.FixLengths(CurrentProjectId);
            MessageBox.Show($"Fixed {fixed_} length(s) from mm to m.", "Fix Lengths",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAllTabs();
        }

        private void btnResetPrices_Click(object sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) { MessageBox.Show("No project open."); return; }
            int reset = _resetPrice.ResetPriceFormulas(CurrentProjectId);
            MessageBox.Show($"Reset {reset} price formula(s).", "Reset Prices",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAllTabs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) { MessageBox.Show("No project open."); return; }
            var summary = _pivotRefresh.RefreshSummary(CurrentProjectId);
            RefreshAllTabs();
            MessageBox.Show($"Summary refreshed.\nTotal rows: {summary.TotalRows}\nGrand total price: {summary.GrandTotalPrice:C}",
                "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Menu items ───────────────────────────────────────────────────────

        private void menuFileNewProject_Click(object sender, EventArgs e) => btnNewProject_Click(sender, e);
        private void menuFileOpenProject_Click(object sender, EventArgs e) => btnOpenProject_Click(sender, e);
        private void menuFileSave_Click(object sender, EventArgs e) => btnSave_Click(sender, e);

        private void menuFileExit_Click(object sender, EventArgs e) => Application.Exit();

        private void menuToolsFixLengths_Click(object sender, EventArgs e) => btnFixLengths_Click(sender, e);
        private void menuToolsResetPrices_Click(object sender, EventArgs e) => btnResetPrices_Click(sender, e);
        private void menuToolsRefresh_Click(object sender, EventArgs e) => btnRefresh_Click(sender, e);

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "QuotingTool v1.0\nAluminium Extrusion Pricing Quotation Tool\n\n" +
                "Built with .NET 8 WinForms + SQL Server\nMigrated from 14 Office Scripts (Excel VBA)",
                "About QuotingTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
