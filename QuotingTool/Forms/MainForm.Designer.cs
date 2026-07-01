namespace QuotingTool.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsFixLengths = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsResetPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNewProject = new System.Windows.Forms.ToolStripButton();
            this.btnOpenProject = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportQuote = new System.Windows.Forms.ToolStripButton();
            this.toolStripSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFixLengths = new System.Windows.Forms.ToolStripButton();
            this.btnResetPrices = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.projectDetailsTab = new System.Windows.Forms.TabPage();
            this.quoteTab = new System.Windows.Forms.TabPage();
            this.editPricingTab = new System.Windows.Forms.TabPage();
            this.bomTab = new System.Windows.Forms.TabPage();
            this.monthlyUpdateTab = new System.Windows.Forms.TabPage();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuFile, this.menuTools, this.menuHelp });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip.TabIndex = 0;

            // menuFile
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuFileNewProject, this.menuFileOpenProject, this.menuFileSave,
                this.menuFileSep1, this.menuFileExit });
            this.menuFile.Name = "menuFile";
            this.menuFile.Text = "&File";

            this.menuFileNewProject.Name = "menuFileNewProject";
            this.menuFileNewProject.Text = "&New Project";
            this.menuFileNewProject.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            this.menuFileNewProject.Click += new System.EventHandler(this.menuFileNewProject_Click);

            this.menuFileOpenProject.Name = "menuFileOpenProject";
            this.menuFileOpenProject.Text = "&Open Project...";
            this.menuFileOpenProject.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            this.menuFileOpenProject.Click += new System.EventHandler(this.menuFileOpenProject_Click);

            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.Text = "&Save";
            this.menuFileSave.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);

            this.menuFileSep1.Name = "menuFileSep1";

            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Text = "E&xit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);

            // menuTools
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuToolsFixLengths, this.menuToolsResetPrices, this.menuToolsRefresh });
            this.menuTools.Name = "menuTools";
            this.menuTools.Text = "&Tools";

            this.menuToolsFixLengths.Name = "menuToolsFixLengths";
            this.menuToolsFixLengths.Text = "Fix Lengths (mm → m)";
            this.menuToolsFixLengths.Click += new System.EventHandler(this.menuToolsFixLengths_Click);

            this.menuToolsResetPrices.Name = "menuToolsResetPrices";
            this.menuToolsResetPrices.Text = "Reset Price Formulas";
            this.menuToolsResetPrices.Click += new System.EventHandler(this.menuToolsResetPrices_Click);

            this.menuToolsRefresh.Name = "menuToolsRefresh";
            this.menuToolsRefresh.Text = "Refresh Summary";
            this.menuToolsRefresh.Click += new System.EventHandler(this.menuToolsRefresh_Click);

            // menuHelp
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuHelpAbout });
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Text = "&Help";

            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);

            // toolStrip
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnNewProject, this.btnOpenProject, this.btnSave,
                this.toolStripSep1, this.btnExportQuote,
                this.toolStripSep2, this.btnFixLengths, this.btnResetPrices, this.btnRefresh });
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1200, 25);
            this.toolStrip.TabIndex = 1;

            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Text = "New Project";
            this.btnNewProject.ToolTipText = "Create a new project";
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);

            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Text = "Open Project";
            this.btnOpenProject.ToolTipText = "Open an existing project";
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);

            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "Save current tab";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.toolStripSep1.Name = "toolStripSep1";
            this.toolStripSep2.Name = "toolStripSep2";

            this.btnExportQuote.Name = "btnExportQuote";
            this.btnExportQuote.Text = "Export Quote";
            this.btnExportQuote.ToolTipText = "Export quote to Excel/CSV";
            this.btnExportQuote.Click += new System.EventHandler(this.btnExportQuote_Click);

            this.btnFixLengths.Name = "btnFixLengths";
            this.btnFixLengths.Text = "Fix Lengths";
            this.btnFixLengths.ToolTipText = "Fix lengths from mm to m";
            this.btnFixLengths.Click += new System.EventHandler(this.btnFixLengths_Click);

            this.btnResetPrices.Name = "btnResetPrices";
            this.btnResetPrices.Text = "Reset Prices";
            this.btnResetPrices.ToolTipText = "Reset price formulas";
            this.btnResetPrices.Click += new System.EventHandler(this.btnResetPrices_Click);

            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.ToolTipText = "Refresh summary pivots";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // tabControl
            this.tabControl.Controls.Add(this.projectDetailsTab);
            this.tabControl.Controls.Add(this.quoteTab);
            this.tabControl.Controls.Add(this.editPricingTab);
            this.tabControl.Controls.Add(this.bomTab);
            this.tabControl.Controls.Add(this.monthlyUpdateTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 49);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 700);
            this.tabControl.TabIndex = 2;

            // Tab pages
            this.projectDetailsTab.Name = "projectDetailsTab";
            this.projectDetailsTab.Text = "Project Details";
            this.projectDetailsTab.Padding = new System.Windows.Forms.Padding(3);

            this.quoteTab.Name = "quoteTab";
            this.quoteTab.Text = "Quote";
            this.quoteTab.Padding = new System.Windows.Forms.Padding(3);

            this.editPricingTab.Name = "editPricingTab";
            this.editPricingTab.Text = "Edit Pricing";
            this.editPricingTab.Padding = new System.Windows.Forms.Padding(3);

            this.bomTab.Name = "bomTab";
            this.bomTab.Text = "BOM Import";
            this.bomTab.Padding = new System.Windows.Forms.Padding(3);

            this.monthlyUpdateTab.Name = "monthlyUpdateTab";
            this.monthlyUpdateTab.Text = "Monthly Update";
            this.monthlyUpdateTab.Padding = new System.Windows.Forms.Padding(3);

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel });
            this.statusStrip.Location = new System.Drawing.Point(0, 749);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 3;

            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Text = "No project open";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 771);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "MainForm";
            this.Text = "QuotingTool – Aluminium Extrusion Pricing";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            // Embed child forms into tabs
            EmbedForm(new ProjectDetailsForm(_db), projectDetailsTab);
            EmbedForm(new QuoteForm(_db), quoteTab);
            EmbedForm(new EditPricingForm(_db), editPricingTab);
            EmbedForm(new BomImportForm(_db), bomTab);
            EmbedForm(new MonthlyUpdateForm(_db), monthlyUpdateTab);
        }

        private static void EmbedForm(System.Windows.Forms.Form form, System.Windows.Forms.TabPage tab)
        {
            form.TopLevel = false;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(form);
            form.Show();
        }

        // Controls
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileNewProject;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpenProject;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuToolsFixLengths;
        private System.Windows.Forms.ToolStripMenuItem menuToolsResetPrices;
        private System.Windows.Forms.ToolStripMenuItem menuToolsRefresh;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNewProject;
        private System.Windows.Forms.ToolStripButton btnOpenProject;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSep1;
        private System.Windows.Forms.ToolStripButton btnExportQuote;
        private System.Windows.Forms.ToolStripSeparator toolStripSep2;
        private System.Windows.Forms.ToolStripButton btnFixLengths;
        private System.Windows.Forms.ToolStripButton btnResetPrices;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        public System.Windows.Forms.TabControl tabControl;
        public System.Windows.Forms.TabPage projectDetailsTab;
        public System.Windows.Forms.TabPage quoteTab;
        public System.Windows.Forms.TabPage editPricingTab;
        public System.Windows.Forms.TabPage bomTab;
        public System.Windows.Forms.TabPage monthlyUpdateTab;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
