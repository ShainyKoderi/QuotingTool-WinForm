namespace QuotingTool.Forms
{
    partial class ProjectDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlScroll       = new System.Windows.Forms.Panel();
            this.lblTitle        = new System.Windows.Forms.Label();
            this.lblCustomer     = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblProject      = new System.Windows.Forms.Label();
            this.txtProjectName  = new System.Windows.Forms.TextBox();
            this.lblRevDate      = new System.Windows.Forms.Label();
            this.dtpRevisionDate = new System.Windows.Forms.DateTimePicker();
            this.lblLME          = new System.Windows.Forms.Label();
            this.txtLMERate      = new System.Windows.Forms.TextBox();
            this.lblBillet       = new System.Windows.Forms.Label();
            this.txtBilletPremium = new System.Windows.Forms.TextBox();
            // New fields from .xlsm 1.Project Details sheet
            this.lblRegion       = new System.Windows.Forms.Label();
            this.cmbRegion       = new System.Windows.Forms.ComboBox();
            this.lblQuotedBy     = new System.Windows.Forms.Label();
            this.txtQuotedBy     = new System.Windows.Forms.TextBox();
            this.lblAccountMgr   = new System.Windows.Forms.Label();
            this.txtAccountManager = new System.Windows.Forms.TextBox();
            this.lblAttention    = new System.Windows.Forms.Label();
            this.txtAttention    = new System.Windows.Forms.TextBox();
            this.lblMarketSeg    = new System.Windows.Forms.Label();
            this.cmbMarketSegment = new System.Windows.Forms.ComboBox();
            this.lblProductSeg   = new System.Windows.Forms.Label();
            this.cmbProductSegment = new System.Windows.Forms.ComboBox();
            this.lblNotes        = new System.Windows.Forms.Label();
            this.txtProjectNotes = new System.Windows.Forms.TextBox();
            this.lblFinishType   = new System.Windows.Forms.Label();
            this.cmbFinishType   = new System.Windows.Forms.ComboBox();
            this.lblFinishApplicator = new System.Windows.Forms.Label();
            this.cmbFinishApplicator = new System.Windows.Forms.ComboBox();
            this.lblFinishGroup  = new System.Windows.Forms.Label();
            this.txtFinishGroup  = new System.Windows.Forms.TextBox();
            this.lblFinishName   = new System.Windows.Forms.Label();
            this.txtFinishName   = new System.Windows.Forms.TextBox();
            this.lblFinishCustom = new System.Windows.Forms.Label();
            this.txtFinishCustomName = new System.Windows.Forms.TextBox();
            this.lblSapCode      = new System.Windows.Forms.Label();
            this.txtFinishSapCode = new System.Windows.Forms.TextBox();
            this.lblThreshold    = new System.Windows.Forms.Label();
            this.cmbThresholdChoice = new System.Windows.Forms.ComboBox();
            this.chkGenerateMfAlt = new System.Windows.Forms.CheckBox();
            this.lblLocalLogo    = new System.Windows.Forms.Label();
            this.cmbLocalLogo    = new System.Windows.Forms.ComboBox();
            this.btnSave         = new System.Windows.Forms.Button();
            this.pnlScroll.SuspendLayout();
            this.SuspendLayout();

            // Scroll panel
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Name = "pnlScroll";

            int lx = 20, cx = 200, cw = 300, lh = 30;
            int y = 20;

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(lx, y);
            this.lblTitle.Size = new System.Drawing.Size(500, 28);
            this.lblTitle.Text = "1. Project Details";
            y += 40;

            AddRow("lblCustomer", "Customer Name:", lx, cx, cw, ref y, lh, this.lblCustomer, this.txtCustomerName);
            AddRow("lblProject", "Project Name:", lx, cx, cw, ref y, lh, this.lblProject, this.txtProjectName);

            this.lblRevDate.AutoSize = true;
            this.lblRevDate.Location = new System.Drawing.Point(lx, y + 3);
            this.lblRevDate.Text = "Revision Date:";
            this.dtpRevisionDate.Location = new System.Drawing.Point(cx, y);
            this.dtpRevisionDate.Size = new System.Drawing.Size(200, 23);
            this.dtpRevisionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            y += lh;

            AddRow("lblLME", "LME Rate (read-only):", lx, cx, 150, ref y, lh, this.lblLME, this.txtLMERate);
            this.txtLMERate.ReadOnly = true;
            this.txtLMERate.BackColor = System.Drawing.SystemColors.Control;

            AddRow("lblBillet", "Billet Premium (read-only):", lx, cx, 150, ref y, lh, this.lblBillet, this.txtBilletPremium);
            this.txtBilletPremium.ReadOnly = true;
            this.txtBilletPremium.BackColor = System.Drawing.SystemColors.Control;

            y += 10; // spacer
            AddComboRow("lblRegion", "Region:", lx, cx, cw, ref y, lh, this.lblRegion, this.cmbRegion,
                new[] { "NQ", "NSW", "NT", "SA", "SEQ", "TAS", "VIC", "WA" });

            AddRow("lblQuotedBy", "Quoted By:", lx, cx, cw, ref y, lh, this.lblQuotedBy, this.txtQuotedBy);
            AddRow("lblAccountMgr", "Account Manager:", lx, cx, cw, ref y, lh, this.lblAccountMgr, this.txtAccountManager);
            AddRow("lblAttention", "Attention To:", lx, cx, cw, ref y, lh, this.lblAttention, this.txtAttention);

            AddComboRow("lblMarketSeg", "Market Segment:", lx, cx, cw, ref y, lh, this.lblMarketSeg, this.cmbMarketSegment,
                new[] { "Private Residential - Detached", "Private Residential - Other", "Public Dwelling",
                        "Office", "Retail", "Hotel", "Factory", "Warehouse", "Education", "Health",
                        "Entertainment/Recreation", "Other" });

            AddComboRow("lblProductSeg", "Product Segment:", lx, cx, cw, ref y, lh, this.lblProductSeg, this.cmbProductSegment,
                new[] { "Commercial", "Residential", "Security", "Industrial", "Shower & Robe" });

            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(lx, y + 3);
            this.lblNotes.Text = "Project Notes:";
            this.txtProjectNotes.Location = new System.Drawing.Point(cx, y);
            this.txtProjectNotes.Size = new System.Drawing.Size(cw, 60);
            this.txtProjectNotes.Multiline = true;
            this.txtProjectNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            y += 70;

            y += 10; // spacer
            AddComboRow("lblFinishType", "Finish Type:", lx, cx, cw, ref y, lh, this.lblFinishType, this.cmbFinishType,
                new[] { "Mill Finish", "Powder", "Anodised" });

            AddComboRow("lblFinishApplicator", "Finish Applicator:", lx, cx, cw, ref y, lh, this.lblFinishApplicator, this.cmbFinishApplicator,
                new[] { "n/a", "Capral P&P - Bremer (AP)", "Capral P&P - Campbellfield (AP)",
                        "Capral P&P - Canningvale (PP)", "Capral P&P - HWood Non-Bulk (AP)",
                        "Capral P&P - HWood Bulk (AP)", "Capral Mill (AP)", "AAF powdercoat",
                        "Powderline WA", "Capral/AAF QLD anodising", "WA Anodisers",
                        "Other - pls check pricing" });

            AddRow("lblFinishGroup", "Finish Group:", lx, cx, cw, ref y, lh, this.lblFinishGroup, this.txtFinishGroup);
            AddRow("lblFinishName", "Finish Name:", lx, cx, cw, ref y, lh, this.lblFinishName, this.txtFinishName);
            AddRow("lblFinishCustom", "Finish Custom Name:", lx, cx, cw, ref y, lh, this.lblFinishCustom, this.txtFinishCustomName);

            AddRow("lblSapCode", "Finish SAP Code:", lx, cx, 120, ref y, lh, this.lblSapCode, this.txtFinishSapCode);

            AddComboRow("lblThreshold", "Threshold Choice:", lx, cx, cw, ref y, lh, this.lblThreshold, this.cmbThresholdChoice,
                new[] { "No", "Anodised Thresholds", "20um Clear", "25um Clear" });

            AddComboRow("lblLocalLogo", "Add Local Logo:", lx, cx, cw, ref y, lh, this.lblLocalLogo, this.cmbLocalLogo,
                new[] { "None", "LocAl Green", "LocAl SuperGreen" });

            this.chkGenerateMfAlt.AutoSize = true;
            this.chkGenerateMfAlt.Location = new System.Drawing.Point(cx, y);
            this.chkGenerateMfAlt.Text = "Generate Mill Finish Alternative (MF ALT)";
            y += lh;

            y += 10;
            this.btnSave.Location = new System.Drawing.Point(cx, y);
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            y += 40;

            this.pnlScroll.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle,
                this.lblCustomer, this.txtCustomerName,
                this.lblProject, this.txtProjectName,
                this.lblRevDate, this.dtpRevisionDate,
                this.lblLME, this.txtLMERate,
                this.lblBillet, this.txtBilletPremium,
                this.lblRegion, this.cmbRegion,
                this.lblQuotedBy, this.txtQuotedBy,
                this.lblAccountMgr, this.txtAccountManager,
                this.lblAttention, this.txtAttention,
                this.lblMarketSeg, this.cmbMarketSegment,
                this.lblProductSeg, this.cmbProductSegment,
                this.lblNotes, this.txtProjectNotes,
                this.lblFinishType, this.cmbFinishType,
                this.lblFinishApplicator, this.cmbFinishApplicator,
                this.lblFinishGroup, this.txtFinishGroup,
                this.lblFinishName, this.txtFinishName,
                this.lblFinishCustom, this.txtFinishCustomName,
                this.lblSapCode, this.txtFinishSapCode,
                this.lblThreshold, this.cmbThresholdChoice,
                this.lblLocalLogo, this.cmbLocalLogo,
                this.chkGenerateMfAlt,
                this.btnSave
            });
            this.pnlScroll.ResumeLayout(false);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 600);
            this.Controls.Add(this.pnlScroll);
            this.Name = "ProjectDetailsForm";
            this.Text = "Project Details";
            this.ResumeLayout(false);
        }

        private static void AddRow(string name, string labelText, int lx, int cx, int cw, ref int y, int lh,
            System.Windows.Forms.Label lbl, System.Windows.Forms.TextBox txt)
        {
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(lx, y + 3);
            lbl.Text = labelText;
            txt.Location = new System.Drawing.Point(cx, y);
            txt.Size = new System.Drawing.Size(cw, 23);
            y += lh;
        }

        private static void AddComboRow(string name, string labelText, int lx, int cx, int cw, ref int y, int lh,
            System.Windows.Forms.Label lbl, System.Windows.Forms.ComboBox cmb, string[] items)
        {
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(lx, y + 3);
            lbl.Text = labelText;
            cmb.Location = new System.Drawing.Point(cx, y);
            cmb.Size = new System.Drawing.Size(cw, 23);
            cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            foreach (var item in items) cmb.Items.Add(item);
            y += lh;
        }

        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblRevDate;
        private System.Windows.Forms.DateTimePicker dtpRevisionDate;
        private System.Windows.Forms.Label lblLME;
        private System.Windows.Forms.TextBox txtLMERate;
        private System.Windows.Forms.Label lblBillet;
        private System.Windows.Forms.TextBox txtBilletPremium;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.Label lblQuotedBy;
        private System.Windows.Forms.TextBox txtQuotedBy;
        private System.Windows.Forms.Label lblAccountMgr;
        private System.Windows.Forms.TextBox txtAccountManager;
        private System.Windows.Forms.Label lblAttention;
        private System.Windows.Forms.TextBox txtAttention;
        private System.Windows.Forms.Label lblMarketSeg;
        private System.Windows.Forms.ComboBox cmbMarketSegment;
        private System.Windows.Forms.Label lblProductSeg;
        private System.Windows.Forms.ComboBox cmbProductSegment;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtProjectNotes;
        private System.Windows.Forms.Label lblFinishType;
        private System.Windows.Forms.ComboBox cmbFinishType;
        private System.Windows.Forms.Label lblFinishApplicator;
        private System.Windows.Forms.ComboBox cmbFinishApplicator;
        private System.Windows.Forms.Label lblFinishGroup;
        private System.Windows.Forms.TextBox txtFinishGroup;
        private System.Windows.Forms.Label lblFinishName;
        private System.Windows.Forms.TextBox txtFinishName;
        private System.Windows.Forms.Label lblFinishCustom;
        private System.Windows.Forms.TextBox txtFinishCustomName;
        private System.Windows.Forms.Label lblSapCode;
        private System.Windows.Forms.TextBox txtFinishSapCode;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.ComboBox cmbThresholdChoice;
        private System.Windows.Forms.CheckBox chkGenerateMfAlt;
        private System.Windows.Forms.Label lblLocalLogo;
        private System.Windows.Forms.ComboBox cmbLocalLogo;
        private System.Windows.Forms.Button btnSave;
    }
}
