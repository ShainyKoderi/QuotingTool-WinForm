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
            menuStrip = new MenuStrip();
            menuFile = new ToolStripMenuItem();
            menuFileNewProject = new ToolStripMenuItem();
            menuFileOpenProject = new ToolStripMenuItem();
            menuFileSep1 = new ToolStripSeparator();
            menuFileExit = new ToolStripMenuItem();
            menuTools = new ToolStripMenuItem();
            menuToolsSundryItems = new ToolStripMenuItem();
            menuToolsCopyForSap = new ToolStripMenuItem();
            menuToolsUserManagement = new ToolStripMenuItem();
            menuHelp = new ToolStripMenuItem();
            menuHelpAbout = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            btnNewProject = new ToolStripButton();
            btnOpenProject = new ToolStripButton();
            tabControl = new TabControl();
            projectDetailsTab = new TabPage();
            quoteTab = new TabPage();
            editPricingTab = new TabPage();
            quoteLetterTab = new TabPage();
            bomTab = new TabPage();
            monthlyUpdateTab = new TabPage();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            tabControl.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { menuFile, menuTools, menuHelp });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1200, 24);
            menuStrip.TabIndex = 2;
            // 
            // menuFile
            // 
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuFileNewProject, menuFileOpenProject, menuFileSep1, menuFileExit });
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(37, 20);
            menuFile.Text = "&File";
            // 
            // menuFileNewProject
            // 
            menuFileNewProject.Name = "menuFileNewProject";
            menuFileNewProject.ShortcutKeys = Keys.Control | Keys.N;
            menuFileNewProject.Size = new Size(195, 22);
            menuFileNewProject.Text = "&New Project";
            menuFileNewProject.Click += menuFileNewProject_Click;
            // 
            // menuFileOpenProject
            // 
            menuFileOpenProject.Name = "menuFileOpenProject";
            menuFileOpenProject.ShortcutKeys = Keys.Control | Keys.O;
            menuFileOpenProject.Size = new Size(195, 22);
            menuFileOpenProject.Text = "&Open Project...";
            menuFileOpenProject.Click += menuFileOpenProject_Click;
            // 
            // menuFileSep1
            // 
            menuFileSep1.Name = "menuFileSep1";
            menuFileSep1.Size = new Size(192, 6);
            // 
            // menuFileExit
            // 
            menuFileExit.Name = "menuFileExit";
            menuFileExit.Size = new Size(195, 22);
            menuFileExit.Text = "E&xit";
            menuFileExit.Click += menuFileExit_Click;
            // 
            // menuTools
            // 
            menuTools.DropDownItems.AddRange(new ToolStripItem[] { menuToolsSundryItems, menuToolsCopyForSap, menuToolsUserManagement });
            menuTools.Name = "menuTools";
            menuTools.Size = new Size(47, 20);
            menuTools.Text = "&Tools";
            // 
            // menuToolsSundryItems
            // 
            menuToolsSundryItems.Name = "menuToolsSundryItems";
            menuToolsSundryItems.Size = new Size(171, 22);
            menuToolsSundryItems.Text = "Sundry Items";
            menuToolsSundryItems.Click += menuToolsSundryItems_Click;
            // 
            // menuToolsCopyForSap
            // 
            menuToolsCopyForSap.Name = "menuToolsCopyForSap";
            menuToolsCopyForSap.Size = new Size(171, 22);
            menuToolsCopyForSap.Text = "Copy for SAP";
            menuToolsCopyForSap.Click += menuToolsCopyForSap_Click;
            // 
            // menuToolsUserManagement
            // 
            menuToolsUserManagement.Name = "menuToolsUserManagement";
            menuToolsUserManagement.Size = new Size(171, 22);
            menuToolsUserManagement.Text = "User Management";
            menuToolsUserManagement.Click += menuToolsUserManagement_Click;
            // 
            // menuHelp
            // 
            menuHelp.DropDownItems.AddRange(new ToolStripItem[] { menuHelpAbout });
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(44, 20);
            menuHelp.Text = "&Help";
            // 
            // menuHelpAbout
            // 
            menuHelpAbout.Name = "menuHelpAbout";
            menuHelpAbout.Size = new Size(107, 22);
            menuHelpAbout.Text = "&About";
            menuHelpAbout.Click += menuHelpAbout_Click;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { btnNewProject, btnOpenProject });
            toolStrip.Location = new Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1200, 25);
            toolStrip.TabIndex = 1;
            // 
            // btnNewProject
            // 
            btnNewProject.Name = "btnNewProject";
            btnNewProject.Size = new Size(75, 22);
            btnNewProject.Text = "New Project";
            btnNewProject.Click += btnNewProject_Click;
            // 
            // btnOpenProject
            // 
            btnOpenProject.Name = "btnOpenProject";
            btnOpenProject.Size = new Size(80, 22);
            btnOpenProject.Text = "Open Project";
            btnOpenProject.Click += btnOpenProject_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(projectDetailsTab);
            tabControl.Controls.Add(quoteTab);
            tabControl.Controls.Add(editPricingTab);
            tabControl.Controls.Add(quoteLetterTab);
            tabControl.Controls.Add(bomTab);
            tabControl.Controls.Add(monthlyUpdateTab);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 49);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1200, 700);
            tabControl.TabIndex = 0;
            // 
            // projectDetailsTab
            // 
            projectDetailsTab.Location = new Point(4, 24);
            projectDetailsTab.Name = "projectDetailsTab";
            projectDetailsTab.Padding = new Padding(3);
            projectDetailsTab.Size = new Size(1192, 672);
            projectDetailsTab.TabIndex = 0;
            projectDetailsTab.Text = "Project Details";
            // 
            // quoteTab
            // 
            quoteTab.Location = new Point(4, 24);
            quoteTab.Name = "quoteTab";
            quoteTab.Padding = new Padding(3);
            quoteTab.Size = new Size(1192, 672);
            quoteTab.TabIndex = 1;
            quoteTab.Text = "Quote";
            // 
            // editPricingTab
            // 
            editPricingTab.Location = new Point(4, 24);
            editPricingTab.Name = "editPricingTab";
            editPricingTab.Padding = new Padding(3);
            editPricingTab.Size = new Size(1192, 672);
            editPricingTab.TabIndex = 2;
            editPricingTab.Text = "Edit Pricing";
            // 
            // quoteLetterTab
            // 
            quoteLetterTab.Location = new Point(4, 24);
            quoteLetterTab.Name = "quoteLetterTab";
            quoteLetterTab.Padding = new Padding(3);
            quoteLetterTab.Size = new Size(1192, 672);
            quoteLetterTab.TabIndex = 3;
            quoteLetterTab.Text = "Quote Letter";
            // 
            // bomTab
            // 
            bomTab.Location = new Point(4, 24);
            bomTab.Name = "bomTab";
            bomTab.Padding = new Padding(3);
            bomTab.Size = new Size(1192, 672);
            bomTab.TabIndex = 5;
            bomTab.Text = "BOM Import";
            // 
            // monthlyUpdateTab
            // 
            monthlyUpdateTab.Location = new Point(4, 24);
            monthlyUpdateTab.Name = "monthlyUpdateTab";
            monthlyUpdateTab.Padding = new Padding(3);
            monthlyUpdateTab.Size = new Size(1192, 672);
            monthlyUpdateTab.TabIndex = 6;
            monthlyUpdateTab.Text = "Monthly Update";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 749);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1200, 22);
            statusStrip.TabIndex = 3;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(93, 17);
            statusLabel.Text = "No project open";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 771);
            Controls.Add(tabControl);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new Size(900, 600);
            Name = "MainForm";
            Text = "QuotingTool – Aluminium Extrusion Pricing";
            Load += MainForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            tabControl.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuToolsSundryItems;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCopyForSap;
        private System.Windows.Forms.ToolStripMenuItem menuToolsUserManagement;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNewProject;
        private System.Windows.Forms.ToolStripButton btnOpenProject;
        public System.Windows.Forms.TabControl tabControl;
        public System.Windows.Forms.TabPage projectDetailsTab;
        public System.Windows.Forms.TabPage quoteTab;
        public System.Windows.Forms.TabPage editPricingTab;
        public System.Windows.Forms.TabPage quoteLetterTab;
        public System.Windows.Forms.TabPage bomTab;
        public System.Windows.Forms.TabPage monthlyUpdateTab;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
