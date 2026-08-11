using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class MainForm : Form
    {
        public int CurrentProjectId { get; private set; }

        private readonly DatabaseContext _db;
        private readonly NavigationService _nav;
        private readonly PageSetupService _pageSetup;
        private readonly SapExportService _sapExport;
        private readonly List<TabPage> _allTabs = new();

        public MainForm()
        {
            _db = new DatabaseContext();
            _db.EnsureSchemaExists();
            _nav = new NavigationService();
            _pageSetup = new PageSetupService(_db);
            _sapExport = new SapExportService(_db);
            InitializeComponent();
        
            EmbedForm(new ProjectDetailsForm(_db), projectDetailsTab);
            EmbedForm(new QuoteForm(_db), quoteTab);
            EmbedForm(new EditPricingForm(_db), editPricingTab);
            EmbedForm(new QuoteLetterForm(_db), quoteLetterTab);           
            EmbedForm(new BomImportForm(_db), bomTab);
            EmbedForm(new MonthlyUpdateForm(_db), monthlyUpdateTab);

            _allTabs.AddRange(new[] { projectDetailsTab, quoteTab, editPricingTab, quoteLetterTab, bomTab, monthlyUpdateTab });
            ApplyRoleAccess();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl.Deselecting += TabControl_Deselecting;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            LoadLastProject();
            Text = $"QuotingTool – {UserSession.CurrentUser?.DisplayName ?? "Guest"}";
        }

        private void TabControl_Deselecting(object? sender, TabControlCancelEventArgs e)
        {
            if (CurrentProjectId <= 0) return;
            var leaving = e.TabPage;
            if (leaving == null || leaving.Controls.Count == 0) return;

            // Auto-save the tab being left so the user never has to click Save
            // before switching tabs.
            var form = leaving.Controls[0];
            if (form is ProjectDetailsForm pdf)
                pdf.SaveProject(showConfirmation: false);
            else if (form is QuoteForm qf)
                qf.SaveQuote(showConfirmation: false);
        }

        private void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) return;
            var selected = tabControl.SelectedTab;
            if (selected == null) return;
            if (selected.Controls.Count == 0) return;

            // Reload the newly-active tab so that pricing / project data is always fresh.
            // This prevents stale cached values (e.g. _metalPricePerKg in QuoteForm) when
            // the user edits pricing on one tab and then switches to another.
            var form = selected.Controls[0];
            if (form is QuoteForm qf)
                qf.LoadProject(CurrentProjectId);
            else if (form is EditPricingForm epf)
                epf.LoadProject(CurrentProjectId);
            else if (form is ProjectDetailsForm pdf)
                pdf.LoadProject(CurrentProjectId);
            else if (form is QuoteNoPricingForm qnp)
                qnp.LoadProject(CurrentProjectId);
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
                    RefreshAllTabs();
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

        public void RefreshAllTabs()
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
            if (quoteLetterTab.Controls.Count > 0 && quoteLetterTab.Controls[0] is QuoteLetterForm qlf)
                qlf.LoadProject(CurrentProjectId);
            //if (noPricingTab.Controls.Count > 0 && noPricingTab.Controls[0] is QuoteNoPricingForm qnp)
            //    qnp.LoadProject(CurrentProjectId);
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            string? defaultRegion = CurrentProjectId > 0
                ? _db.GetProject(CurrentProjectId)?.Region
                : UserSession.CurrentUser?.Region;
            using var dialog = new NewQuoteDialog(defaultRegion);
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            int newId = _db.CreateProject(new Project
            {
                CustomerName = dialog.CustomerName,
                ProjectName = dialog.ProjectName,
                Region = dialog.SelectedRegion,
                RevisionDate = dialog.QuoteDate,
                QuotedBy = UserSession.CurrentUser?.DisplayName ?? string.Empty,
                AccountManager = UserSession.CurrentUser?.DisplayName ?? string.Empty
            });
            _db.UpsertEditPricing(new EditPricing { ProjectId = newId });
            SetCurrentProject(newId);
            _nav.NavigateTo("Project Details", tabControl);
            MessageBox.Show($"New quote created (ID {newId}).", "New Quote", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            using var form = new OpenProjectDialog(_db);
            if (form.ShowDialog(this) == DialogResult.OK && form.SelectedProjectId > 0)
                SetCurrentProject(form.SelectedProjectId);
        }

        private void menuFileNewProject_Click(object sender, EventArgs e) => btnNewProject_Click(sender, e);
        private void menuFileOpenProject_Click(object sender, EventArgs e) => btnOpenProject_Click(sender, e);
        private void menuFileExit_Click(object sender, EventArgs e) => Application.Exit();

        private void menuToolsSundryItems_Click(object sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) { MessageBox.Show("No project open."); return; }
            using var form = new SundryItemsForm(_db);
            form.LoadProject(CurrentProjectId);
            form.ShowDialog(this);
            RefreshAllTabs();
        }

        private void menuToolsCopyForSap_Click(object sender, EventArgs e)
        {
            if (CurrentProjectId <= 0) { MessageBox.Show("No project open."); return; }
            int count = _sapExport.CopyToClipboard(CurrentProjectId);
            MessageBox.Show($"{count} lines copied to clipboard for SAP", "Copy for SAP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuToolsUserManagement_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdministrator)
            {
                MessageBox.Show("Administrator access required.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new UserManagementForm(_db);
            form.ShowDialog(this);
        }

        private void ApplyRoleAccess()
        {
            menuToolsUserManagement.Visible = UserSession.IsAdministrator;
            tabControl.TabPages.Clear();

            IEnumerable<TabPage> allowedTabs = UserSession.CurrentUser?.Role switch
            {
                UserRole.Administrator => _allTabs,
                UserRole.SalesManager => _allTabs.Where(t => t != monthlyUpdateTab),
                _ => _allTabs.Where(t => t == projectDetailsTab
                                         || t == quoteTab
                                         || t == quoteLetterTab                                        
                                         || t == bomTab)
            };

            foreach (var tab in allowedTabs)
                tabControl.TabPages.Add(tab);
        }

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "QuotingTool v1.0\nAluminium Extrusion Pricing Quotation Tool\n\n" +
                "Built with .NET 8 WinForms + SQL Server\nMigrated from 14 Office Scripts (Excel VBA)",
                "About QuotingTool", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
