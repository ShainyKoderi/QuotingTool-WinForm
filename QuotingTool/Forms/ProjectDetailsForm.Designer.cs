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
            pnlScroll = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblCustomer = new System.Windows.Forms.Label();
            txtCustomerName = new System.Windows.Forms.TextBox();
            lblProject = new System.Windows.Forms.Label();
            txtProjectName = new System.Windows.Forms.TextBox();
            lblRevDate = new System.Windows.Forms.Label();
            dtpRevisionDate = new System.Windows.Forms.DateTimePicker();
            lblLME = new System.Windows.Forms.Label();
            txtLMERate = new System.Windows.Forms.TextBox();
            lblBillet = new System.Windows.Forms.Label();
            txtBilletPremium = new System.Windows.Forms.TextBox();
            lblRegion = new System.Windows.Forms.Label();
            cmbRegion = new System.Windows.Forms.ComboBox();
            lblQuotedBy = new System.Windows.Forms.Label();
            txtQuotedBy = new System.Windows.Forms.TextBox();
            lblAccountMgr = new System.Windows.Forms.Label();
            txtAccountManager = new System.Windows.Forms.TextBox();
            lblAttention = new System.Windows.Forms.Label();
            txtAttention = new System.Windows.Forms.TextBox();
            lblMarketSeg = new System.Windows.Forms.Label();
            cmbMarketSegment = new System.Windows.Forms.ComboBox();
            lblProductSeg = new System.Windows.Forms.Label();
            cmbProductSegment = new System.Windows.Forms.ComboBox();
            lblNotes = new System.Windows.Forms.Label();
            txtProjectNotes = new System.Windows.Forms.TextBox();
            lblFinishType = new System.Windows.Forms.Label();
            cmbFinishType = new System.Windows.Forms.ComboBox();
            lblFinishApplicator = new System.Windows.Forms.Label();
            cmbFinishApplicator = new System.Windows.Forms.ComboBox();
            lblFinishGroup = new System.Windows.Forms.Label();
            txtFinishGroup = new System.Windows.Forms.TextBox();
            lblFinishName = new System.Windows.Forms.Label();
            txtFinishName = new System.Windows.Forms.TextBox();
            lblFinishCustom = new System.Windows.Forms.Label();
            txtFinishCustomName = new System.Windows.Forms.TextBox();
            lblSapCode = new System.Windows.Forms.Label();
            txtFinishSapCode = new System.Windows.Forms.TextBox();
            lblThreshold = new System.Windows.Forms.Label();
            cmbThresholdChoice = new System.Windows.Forms.ComboBox();
            lblLocalLogo = new System.Windows.Forms.Label();
            cmbLocalLogo = new System.Windows.Forms.ComboBox();
            lblGenerateMfAlt = new System.Windows.Forms.Label();
            cmbGenerateMillFinishAlternative = new System.Windows.Forms.ComboBox();
            lblQuoteValidity = new System.Windows.Forms.Label();
            txtQuoteValidity = new System.Windows.Forms.TextBox();
            lblCustomerSapDiscount = new System.Windows.Forms.Label();
            txtCustomerSapDiscount = new System.Windows.Forms.TextBox();
            lblDeliveryCharge = new System.Windows.Forms.Label();
            txtDeliveryCharge = new System.Windows.Forms.TextBox();
            btnImportCre8 = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            pnlScroll.SuspendLayout();
            SuspendLayout();

            pnlScroll.AutoScroll = true;
            pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlScroll.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTitle,
                lblCustomer, txtCustomerName,
                lblProject, txtProjectName,
                lblRevDate, dtpRevisionDate,
                lblLME, txtLMERate,
                lblBillet, txtBilletPremium,
                lblRegion, cmbRegion,
                lblQuotedBy, txtQuotedBy,
                lblAccountMgr, txtAccountManager,
                lblAttention, txtAttention,
                lblMarketSeg, cmbMarketSegment,
                lblProductSeg, cmbProductSegment,
                lblNotes, txtProjectNotes,
                lblFinishType, cmbFinishType,
                lblFinishApplicator, cmbFinishApplicator,
                lblFinishGroup, txtFinishGroup,
                lblFinishName, txtFinishName,
                lblFinishCustom, txtFinishCustomName,
                lblSapCode, txtFinishSapCode,
                lblThreshold, cmbThresholdChoice,
                lblLocalLogo, cmbLocalLogo,
                lblGenerateMfAlt, cmbGenerateMillFinishAlternative,
                lblQuoteValidity, txtQuoteValidity,
                lblCustomerSapDiscount, txtCustomerSapDiscount,
                lblDeliveryCharge, txtDeliveryCharge,
                btnImportCre8, btnSave
            });

            int lx = 20, cx = 220, cw = 320, lh = 30;
            int y = 20;

            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(lx, y);
            lblTitle.Size = new System.Drawing.Size(500, 28);
            lblTitle.Text = "1. Project Details";
            y += 40;

            AddTextRow(lblCustomer, txtCustomerName, "Customer Name:", lx, cx, cw, ref y, lh);
            AddTextRow(lblProject, txtProjectName, "Project Name:", lx, cx, cw, ref y, lh);

            lblRevDate.AutoSize = true;
            lblRevDate.Location = new System.Drawing.Point(lx, y + 3);
            lblRevDate.Text = "Revision Date:";
            dtpRevisionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpRevisionDate.Location = new System.Drawing.Point(cx, y);
            dtpRevisionDate.Size = new System.Drawing.Size(180, 23);
            y += lh;

            AddTextRow(lblLME, txtLMERate, "LME Rate (read-only):", lx, cx, 140, ref y, lh);
            txtLMERate.ReadOnly = true;
            txtLMERate.BackColor = System.Drawing.SystemColors.Control;

            AddTextRow(lblBillet, txtBilletPremium, "Billet Premium (read-only):", lx, cx, 140, ref y, lh);
            txtBilletPremium.ReadOnly = true;
            txtBilletPremium.BackColor = System.Drawing.SystemColors.Control;

            y += 10;
            AddComboRow(lblRegion, cmbRegion, "Region:", lx, cx, cw, ref y, lh,
                new[] { "NQ", "NSW", "NT", "SA", "SEQ", "TAS", "VIC", "WA" });
            AddTextRow(lblQuotedBy, txtQuotedBy, "Quoted By:", lx, cx, cw, ref y, lh);
            AddTextRow(lblAccountMgr, txtAccountManager, "Account Manager:", lx, cx, cw, ref y, lh);
            AddTextRow(lblAttention, txtAttention, "Attention To:", lx, cx, cw, ref y, lh);
            txtAttention.Text = DefaultAttentionTo;

            AddComboRow(lblMarketSeg, cmbMarketSegment, "Market Segment:", lx, cx, cw, ref y, lh,
                new[] { "Private Residential - Detached", "Private Residential - Other", "Public Dwelling", "Office", "Retail", "Hotel", "Factory", "Warehouse", "Education", "Health", "Entertainment/Recreation", "Other" });
            AddComboRow(lblProductSeg, cmbProductSegment, "Product Segment:", lx, cx, cw, ref y, lh,
                new[] { "Commercial", "Residential", "Security", "Industrial", "Shower & Robe" });

            lblNotes.AutoSize = true;
            lblNotes.Location = new System.Drawing.Point(lx, y + 3);
            lblNotes.Text = "Project Notes:";
            txtProjectNotes.Location = new System.Drawing.Point(cx, y);
            txtProjectNotes.Multiline = true;
            txtProjectNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtProjectNotes.Size = new System.Drawing.Size(cw, 80);
            y += 90;

            AddComboRow(lblFinishType, cmbFinishType, "Finish Type:", lx, cx, cw, ref y, lh,
                new[] { "Mill Finish", "Powder", "Anodised" });
            AddComboRow(lblFinishApplicator, cmbFinishApplicator, "Finish Applicator:", lx, cx, cw, ref y, lh,
                new[] { "n/a", "Capral P&P - Bremer (AP)", "Capral P&P - Campbellfield (AP)", "Capral P&P - Canningvale (PP)", "Capral P&P - HWood Non-Bulk (AP)", "Capral P&P - HWood Bulk (AP)", "Capral Mill (AP)", "AAF powdercoat", "Powderline WA", "Capral/AAF QLD anodising", "WA Anodisers", "Other - pls check pricing" });
            AddTextRow(lblFinishGroup, txtFinishGroup, "Finish Group:", lx, cx, cw, ref y, lh);
            AddTextRow(lblFinishName, txtFinishName, "Finish Name:", lx, cx, cw, ref y, lh);
            AddTextRow(lblFinishCustom, txtFinishCustomName, "Finish Custom Name:", lx, cx, cw, ref y, lh);
            AddTextRow(lblSapCode, txtFinishSapCode, "Finish SAP Code:", lx, cx, 140, ref y, lh);
            AddComboRow(lblThreshold, cmbThresholdChoice, "Threshold Choice:", lx, cx, cw, ref y, lh,
                new[] { "No", "Anodised Thresholds", "20um Clear", "25um Clear" });

            AddComboRow(lblLocalLogo, cmbLocalLogo, "Add LocAl Logos:", lx, cx, cw, ref y, lh,
                new[] { "None", "Green", "SuperGreen" });
            AddComboRow(lblGenerateMfAlt, cmbGenerateMillFinishAlternative, "Generate Mill Finish Alternative:", lx, cx, 140, ref y, lh,
                new[] { "Yes", "No" });
            cmbGenerateMillFinishAlternative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbGenerateMillFinishAlternative.SelectedIndex = 1;

            AddTextRow(lblQuoteValidity, txtQuoteValidity, "Quote Validity:", lx, cx, 140, ref y, lh);
            txtQuoteValidity.Text = "30 days";
            AddTextRow(lblCustomerSapDiscount, txtCustomerSapDiscount, "Customer SAP Discount %:", lx, cx, 140, ref y, lh);
            AddTextRow(lblDeliveryCharge, txtDeliveryCharge, "Delivery Charge:", lx, cx, 140, ref y, lh);
            txtDeliveryCharge.Text = "97.00";

            btnImportCre8.Location = new System.Drawing.Point(cx, y);
            btnImportCre8.Size = new System.Drawing.Size(140, 30);
            btnImportCre8.Text = "Import CRE8";
            btnImportCre8.Click += new System.EventHandler(btnImportCre8_Click);

            btnSave.Location = new System.Drawing.Point(cx + 160, y);
            btnSave.Size = new System.Drawing.Size(120, 30);
            btnSave.Text = "Save";
            btnSave.Click += new System.EventHandler(btnSave_Click);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(700, 700);
            Controls.Add(pnlScroll);
            Name = "ProjectDetailsForm";
            Text = "Project Details";
            pnlScroll.ResumeLayout(false);
            pnlScroll.PerformLayout();
            ResumeLayout(false);
        }

        private static void AddTextRow(System.Windows.Forms.Label label, System.Windows.Forms.TextBox textBox, string text, int lx, int cx, int cw, ref int y, int lh)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(lx, y + 3);
            label.Text = text;
            textBox.Location = new System.Drawing.Point(cx, y);
            textBox.Size = new System.Drawing.Size(cw, 23);
            y += lh;
        }

        private static void AddComboRow(System.Windows.Forms.Label label, System.Windows.Forms.ComboBox comboBox, string text, int lx, int cx, int cw, ref int y, int lh, string[] items)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(lx, y + 3);
            label.Text = text;
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            comboBox.Location = new System.Drawing.Point(cx, y);
            comboBox.Size = new System.Drawing.Size(cw, 23);
            comboBox.Items.AddRange(items);
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
        private System.Windows.Forms.Label lblLocalLogo;
        private System.Windows.Forms.ComboBox cmbLocalLogo;
        private System.Windows.Forms.Label lblGenerateMfAlt;
        private System.Windows.Forms.ComboBox cmbGenerateMillFinishAlternative;
        private System.Windows.Forms.Label lblQuoteValidity;
        private System.Windows.Forms.TextBox txtQuoteValidity;
        private System.Windows.Forms.Label lblCustomerSapDiscount;
        private System.Windows.Forms.TextBox txtCustomerSapDiscount;
        private System.Windows.Forms.Label lblDeliveryCharge;
        private System.Windows.Forms.TextBox txtDeliveryCharge;
        private System.Windows.Forms.Button btnImportCre8;
        private System.Windows.Forms.Button btnSave;
    }
}
