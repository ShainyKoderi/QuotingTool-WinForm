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
            menuStrip = new System.Windows.Forms.MenuStrip();
            menuFile = new System.Windows.Forms.ToolStripMenuItem();
            menuFileNewProject = new System.Windows.Forms.ToolStripMenuItem();
            menuFileOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            menuTools = new System.Windows.Forms.ToolStripMenuItem();
            menuToolsFixLengths = new System.Windows.Forms.ToolStripMenuItem();
            menuToolsResetPrices = new System.Windows.Forms.ToolStripMenuItem();
            menuToolsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            menuToolsSundryItems = new System.Windows.Forms.ToolStripMenuItem();
            menuToolsCopyForSap = new System.Windows.Forms.ToolStripMenuItem();
            menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            toolStrip = new System.Windows.Forms.ToolStrip();
            btnNewProject = new System.Windows.Forms.ToolStripButton();
            btnOpenProject = new System.Windows.Forms.ToolStripButton();
            btnSave = new System.Windows.Forms.ToolStripButton();
            toolStripSep1 = new System.Windows.Forms.ToolStripSeparator();
            btnExportQuote = new System.Windows.Forms.ToolStripButton();
            toolStripSep2 = new System.Windows.Forms.ToolStripSeparator();
            btnFixLengths = new System.Windows.Forms.ToolStripButton();
            btnResetPrices = new System.Windows.Forms.ToolStripButton();
            btnRefresh = new System.Windows.Forms.ToolStripButton();
            tabControl = new System.Windows.Forms.TabControl();
            projectDetailsTab = new System.Windows.Forms.TabPage();
            quoteTab = new System.Windows.Forms.TabPage();
            editPricingTab = new System.Windows.Forms.TabPage();
            quoteLetterTab = new System.Windows.Forms.TabPage();
            noPricingTab = new System.Windows.Forms.TabPage();
            bomTab = new System.Windows.Forms.TabPage();
            monthlyUpdateTab = new System.Windows.Forms.TabPage();
            statusStrip = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            tabControl.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();

            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { menuFile, menuTools, menuHelp });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Size = new System.Drawing.Size(1200, 24);

            menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { menuFileNewProject, menuFileOpenProject, menuFileSave, menuFileSep1, menuFileExit });
            menuFile.Text = "&File";
            menuFileNewProject.Text = "&New Project";
            menuFileNewProject.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            menuFileNewProject.Click += new System.EventHandler(menuFileNewProject_Click);
            menuFileOpenProject.Text = "&Open Project...";
            menuFileOpenProject.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            menuFileOpenProject.Click += new System.EventHandler(menuFileOpenProject_Click);
            menuFileSave.Text = "&Save";
            menuFileSave.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            menuFileSave.Click += new System.EventHandler(menuFileSave_Click);
            menuFileExit.Text = "E&xit";
            menuFileExit.Click += new System.EventHandler(menuFileExit_Click);

            menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { menuToolsFixLengths, menuToolsResetPrices, menuToolsRefresh, menuToolsSundryItems, menuToolsCopyForSap });
            menuTools.Text = "&Tools";
            menuToolsFixLengths.Text = "Fix Lengths (mm → m)";
            menuToolsFixLengths.Click += new System.EventHandler(menuToolsFixLengths_Click);
            menuToolsResetPrices.Text = "Reset Price Formulas";
            menuToolsResetPrices.Click += new System.EventHandler(menuToolsResetPrices_Click);
            menuToolsRefresh.Text = "Refresh Summary";
            menuToolsRefresh.Click += new System.EventHandler(menuToolsRefresh_Click);
            menuToolsSundryItems.Text = "Sundry Items";
            menuToolsSundryItems.Click += new System.EventHandler(menuToolsSundryItems_Click);
            menuToolsCopyForSap.Text = "Copy for SAP";
            menuToolsCopyForSap.Click += new System.EventHandler(menuToolsCopyForSap_Click);

            menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { menuHelpAbout });
            menuHelp.Text = "&Help";
            menuHelpAbout.Text = "&About";
            menuHelpAbout.Click += new System.EventHandler(menuHelpAbout_Click);

            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnNewProject, btnOpenProject, btnSave, toolStripSep1, btnExportQuote, toolStripSep2, btnFixLengths, btnResetPrices, btnRefresh });
            toolStrip.Location = new System.Drawing.Point(0, 24);
            toolStrip.Size = new System.Drawing.Size(1200, 25);
            btnNewProject.Text = "New Project";
            btnNewProject.Click += new System.EventHandler(btnNewProject_Click);
            btnOpenProject.Text = "Open Project";
            btnOpenProject.Click += new System.EventHandler(btnOpenProject_Click);
            btnSave.Text = "Save";
            btnSave.Click += new System.EventHandler(btnSave_Click);
            btnExportQuote.Text = "Export Quote";
            btnExportQuote.Click += new System.EventHandler(btnExportQuote_Click);
            btnFixLengths.Text = "Fix Lengths";
            btnFixLengths.Click += new System.EventHandler(btnFixLengths_Click);
            btnResetPrices.Text = "Reset Prices";
            btnResetPrices.Click += new System.EventHandler(btnResetPrices_Click);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += new System.EventHandler(btnRefresh_Click);

            tabControl.Controls.Add(projectDetailsTab);
            tabControl.Controls.Add(quoteTab);
            tabControl.Controls.Add(editPricingTab);
            tabControl.Controls.Add(quoteLetterTab);
            tabControl.Controls.Add(noPricingTab);
            tabControl.Controls.Add(bomTab);
            tabControl.Controls.Add(monthlyUpdateTab);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 49);
            tabControl.Size = new System.Drawing.Size(1200, 700);

            projectDetailsTab.Text = "Project Details";
            projectDetailsTab.Padding = new System.Windows.Forms.Padding(3);
            quoteTab.Text = "Quote";
            quoteTab.Padding = new System.Windows.Forms.Padding(3);
            editPricingTab.Text = "Edit Pricing";
            editPricingTab.Padding = new System.Windows.Forms.Padding(3);
            quoteLetterTab.Text = "Quote Letter";
            quoteLetterTab.Padding = new System.Windows.Forms.Padding(3);
            noPricingTab.Text = "No Pricing";
            noPricingTab.Padding = new System.Windows.Forms.Padding(3);
            bomTab.Text = "BOM Import";
            bomTab.Padding = new System.Windows.Forms.Padding(3);
            monthlyUpdateTab.Text = "Monthly Update";
            monthlyUpdateTab.Padding = new System.Windows.Forms.Padding(3);

            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
            statusStrip.Location = new System.Drawing.Point(0, 749);
            statusStrip.Size = new System.Drawing.Size(1200, 22);
            statusLabel.Text = "No project open";

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 771);
            Controls.Add(tabControl);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new System.Drawing.Size(900, 600);
            Text = "QuotingTool – Aluminium Extrusion Pricing";
            Load += new System.EventHandler(MainForm_Load);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            tabControl.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

            EmbedForm(new ProjectDetailsForm(_db), projectDetailsTab);
            EmbedForm(new QuoteForm(_db), quoteTab);
            EmbedForm(new EditPricingForm(_db), editPricingTab);
            EmbedForm(new QuoteLetterForm(_db), quoteLetterTab);
            EmbedForm(new QuoteNoPricingForm(_db), noPricingTab);
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
        private System.Windows.Forms.ToolStripMenuItem menuToolsSundryItems;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCopyForSap;
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
        public System.Windows.Forms.TabPage quoteLetterTab;
        public System.Windows.Forms.TabPage noPricingTab;
        public System.Windows.Forms.TabPage bomTab;
        public System.Windows.Forms.TabPage monthlyUpdateTab;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
