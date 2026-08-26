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
            millFinishQuoteTab = new TabPage();
            editPricingTab = new TabPage();
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
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { menuFile, menuTools, menuHelp });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(9, 3, 0, 3);
            menuStrip.Size = new Size(1714, 35);
            menuStrip.TabIndex = 2;
            // 
            // menuFile
            // 
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuFileNewProject, menuFileOpenProject, menuFileSep1, menuFileExit });
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(54, 29);
            menuFile.Text = "&File";
            // 
            // menuFileNewProject
            // 
            menuFileNewProject.Name = "menuFileNewProject";
            menuFileNewProject.ShortcutKeys = Keys.Control | Keys.N;
            menuFileNewProject.Size = new Size(294, 34);
            menuFileNewProject.Text = "&New Project";
            menuFileNewProject.Click += menuFileNewProject_Click;
            // 
            // menuFileOpenProject
            // 
            menuFileOpenProject.Name = "menuFileOpenProject";
            menuFileOpenProject.ShortcutKeys = Keys.Control | Keys.O;
            menuFileOpenProject.Size = new Size(294, 34);
            menuFileOpenProject.Text = "&Open Project...";
            menuFileOpenProject.Click += menuFileOpenProject_Click;
            // 
            // menuFileSep1
            // 
            menuFileSep1.Name = "menuFileSep1";
            menuFileSep1.Size = new Size(291, 6);
            // 
            // menuFileExit
            // 
            menuFileExit.Name = "menuFileExit";
            menuFileExit.Size = new Size(294, 34);
            menuFileExit.Text = "E&xit";
            menuFileExit.Click += menuFileExit_Click;
            // 
            // menuTools
            // 
            menuTools.DropDownItems.AddRange(new ToolStripItem[] { menuToolsSundryItems, menuToolsCopyForSap, menuToolsUserManagement });
            menuTools.Name = "menuTools";
            menuTools.Size = new Size(69, 29);
            menuTools.Text = "&Tools";
            // 
            // menuToolsSundryItems
            // 
            menuToolsSundryItems.Name = "menuToolsSundryItems";
            menuToolsSundryItems.Size = new Size(259, 34);
            menuToolsSundryItems.Text = "Sundry Items";
            menuToolsSundryItems.Click += menuToolsSundryItems_Click;
            // 
            // menuToolsCopyForSap
            // 
            menuToolsCopyForSap.Name = "menuToolsCopyForSap";
            menuToolsCopyForSap.Size = new Size(259, 34);
            menuToolsCopyForSap.Text = "Copy for SAP";
            menuToolsCopyForSap.Click += menuToolsCopyForSap_Click;
            // 
            // menuToolsUserManagement
            // 
            menuToolsUserManagement.Name = "menuToolsUserManagement";
            menuToolsUserManagement.Size = new Size(259, 34);
            menuToolsUserManagement.Text = "User Management";
            menuToolsUserManagement.Click += menuToolsUserManagement_Click;
            // 
            // menuHelp
            // 
            menuHelp.DropDownItems.AddRange(new ToolStripItem[] { menuHelpAbout });
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(65, 29);
            menuHelp.Text = "&Help";
            // 
            // menuHelpAbout
            // 
            menuHelpAbout.Name = "menuHelpAbout";
            menuHelpAbout.Size = new Size(164, 34);
            menuHelpAbout.Text = "&About";
            menuHelpAbout.Click += menuHelpAbout_Click;
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(24, 24);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnNewProject, btnOpenProject });
            toolStrip.Location = new Point(0, 35);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(0, 0, 3, 0);
            toolStrip.Size = new Size(1714, 34);
            toolStrip.TabIndex = 1;
            // 
            // btnNewProject
            // 
            btnNewProject.Name = "btnNewProject";
            btnNewProject.Size = new Size(110, 29);
            btnNewProject.Text = "New Project";
            btnNewProject.Click += btnNewProject_Click;
            // 
            // btnOpenProject
            // 
            btnOpenProject.Name = "btnOpenProject";
            btnOpenProject.Size = new Size(119, 29);
            btnOpenProject.Text = "Open Project";
            btnOpenProject.Click += btnOpenProject_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(projectDetailsTab);
            tabControl.Controls.Add(quoteTab);
            tabControl.Controls.Add(millFinishQuoteTab);
            tabControl.Controls.Add(editPricingTab);
            tabControl.Controls.Add(bomTab);
            tabControl.Controls.Add(monthlyUpdateTab);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 69);
            tabControl.Margin = new Padding(4, 5, 4, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1714, 1069);
            tabControl.TabIndex = 0;
            // 
            // projectDetailsTab
            // 
            projectDetailsTab.Location = new Point(4, 34);
            projectDetailsTab.Margin = new Padding(4, 5, 4, 5);
            projectDetailsTab.Name = "projectDetailsTab";
            projectDetailsTab.Padding = new Padding(4, 5, 4, 5);
            projectDetailsTab.Size = new Size(1706, 1031);
            projectDetailsTab.TabIndex = 0;
            projectDetailsTab.Text = "Project Details";
            // 
            // quoteTab
            // 
            quoteTab.Location = new Point(4, 34);
            quoteTab.Margin = new Padding(4, 5, 4, 5);
            quoteTab.Name = "quoteTab";
            quoteTab.Padding = new Padding(4, 5, 4, 5);
            quoteTab.Size = new Size(1706, 1031);
            quoteTab.TabIndex = 1;
            quoteTab.Text = "Quote";
            // 
            // millFinishQuoteTab
            // 
            millFinishQuoteTab.Location = new Point(4, 34);
            millFinishQuoteTab.Margin = new Padding(4, 5, 4, 5);
            millFinishQuoteTab.Name = "millFinishQuoteTab";
            millFinishQuoteTab.Padding = new Padding(4, 5, 4, 5);
            millFinishQuoteTab.Size = new Size(1706, 1031);
            millFinishQuoteTab.TabIndex = 10;
            millFinishQuoteTab.Text = "Mill Finish Quote";
            // 
            // editPricingTab
            // 
            editPricingTab.Location = new Point(4, 34);
            editPricingTab.Margin = new Padding(4, 5, 4, 5);
            editPricingTab.Name = "editPricingTab";
            editPricingTab.Padding = new Padding(4, 5, 4, 5);
            editPricingTab.Size = new Size(1706, 1031);
            editPricingTab.TabIndex = 2;
            editPricingTab.Text = "Edit Pricing";
            // 
            // bomTab
            // 
            bomTab.Location = new Point(4, 34);
            bomTab.Margin = new Padding(4, 5, 4, 5);
            bomTab.Name = "bomTab";
            bomTab.Padding = new Padding(4, 5, 4, 5);
            bomTab.Size = new Size(1706, 1031);
            bomTab.TabIndex = 5;
            bomTab.Text = "BOM Import";
            // 
            // monthlyUpdateTab
            // 
            monthlyUpdateTab.Location = new Point(4, 34);
            monthlyUpdateTab.Margin = new Padding(4, 5, 4, 5);
            monthlyUpdateTab.Name = "monthlyUpdateTab";
            monthlyUpdateTab.Padding = new Padding(4, 5, 4, 5);
            monthlyUpdateTab.Size = new Size(1706, 1031);
            monthlyUpdateTab.TabIndex = 6;
            monthlyUpdateTab.Text = "Monthly Update";
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 1138);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 20, 0);
            statusStrip.Size = new Size(1714, 32);
            statusStrip.TabIndex = 3;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(142, 25);
            statusLabel.Text = "No project open";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1714, 1170);
            Controls.Add(tabControl);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1276, 963);
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
        public System.Windows.Forms.TabPage millFinishQuoteTab;
        public System.Windows.Forms.TabPage editPricingTab;
        public System.Windows.Forms.TabPage bomTab;
        public System.Windows.Forms.TabPage monthlyUpdateTab;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
