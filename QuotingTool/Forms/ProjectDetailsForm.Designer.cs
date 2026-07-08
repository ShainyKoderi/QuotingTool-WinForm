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

            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.Size = new System.Drawing.Size(500, 28);
            lblTitle.Text = "1. Project Details";

            lblCustomer.AutoSize = true;
            lblCustomer.Location = new System.Drawing.Point(20, 63);
            lblCustomer.Text = "Customer Name:";
            txtCustomerName.Location = new System.Drawing.Point(220, 60);
            txtCustomerName.Size = new System.Drawing.Size(320, 23);

            lblProject.AutoSize = true;
            lblProject.Location = new System.Drawing.Point(20, 93);
            lblProject.Text = "Project Name:";
            txtProjectName.Location = new System.Drawing.Point(220, 90);
            txtProjectName.Size = new System.Drawing.Size(320, 23);

            lblRevDate.AutoSize = true;
            lblRevDate.Location = new System.Drawing.Point(20, 123);
            lblRevDate.Text = "Revision Date:";
            dtpRevisionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpRevisionDate.Location = new System.Drawing.Point(220, 120);
            dtpRevisionDate.Size = new System.Drawing.Size(180, 23);

            lblLME.AutoSize = true;
            lblLME.Location = new System.Drawing.Point(20, 153);
            lblLME.Text = "LME Rate (read-only):";
            txtLMERate.BackColor = System.Drawing.SystemColors.Control;
            txtLMERate.Location = new System.Drawing.Point(220, 150);
            txtLMERate.ReadOnly = true;
            txtLMERate.Size = new System.Drawing.Size(140, 23);

            lblBillet.AutoSize = true;
            lblBillet.Location = new System.Drawing.Point(20, 183);
            lblBillet.Text = "Billet Premium (read-only):";
            txtBilletPremium.BackColor = System.Drawing.SystemColors.Control;
            txtBilletPremium.Location = new System.Drawing.Point(220, 180);
            txtBilletPremium.ReadOnly = true;
            txtBilletPremium.Size = new System.Drawing.Size(140, 23);

            lblRegion.AutoSize = true;
            lblRegion.Location = new System.Drawing.Point(20, 223);
            lblRegion.Text = "Region:";
            cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbRegion.Items.AddRange(new object[] { "NQ", "NSW", "NT", "SA", "SEQ", "TAS", "VIC", "WA" });
            cmbRegion.Location = new System.Drawing.Point(220, 220);
            cmbRegion.Size = new System.Drawing.Size(320, 23);

            lblQuotedBy.AutoSize = true;
            lblQuotedBy.Location = new System.Drawing.Point(20, 253);
            lblQuotedBy.Text = "Quoted By:";
            txtQuotedBy.Location = new System.Drawing.Point(220, 250);
            txtQuotedBy.Size = new System.Drawing.Size(320, 23);

            lblAccountMgr.AutoSize = true;
            lblAccountMgr.Location = new System.Drawing.Point(20, 283);
            lblAccountMgr.Text = "Account Manager:";
            txtAccountManager.Location = new System.Drawing.Point(220, 280);
            txtAccountManager.Size = new System.Drawing.Size(320, 23);

            lblAttention.AutoSize = true;
            lblAttention.Location = new System.Drawing.Point(20, 313);
            lblAttention.Text = "Attention To:";
            txtAttention.Location = new System.Drawing.Point(220, 310);
            txtAttention.Size = new System.Drawing.Size(320, 23);
            txtAttention.Text = "To whom it may concern";

            lblMarketSeg.AutoSize = true;
            lblMarketSeg.Location = new System.Drawing.Point(20, 343);
            lblMarketSeg.Text = "Market Segment:";
            cmbMarketSegment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbMarketSegment.Items.AddRange(new object[]
            {
                "Private Residential - Detached", "Private Residential - Other", "Public Dwelling", "Office", "Retail", "Hotel",
                "Factory", "Warehouse", "Education", "Health", "Entertainment/Recreation", "Other"
            });
            cmbMarketSegment.Location = new System.Drawing.Point(220, 340);
            cmbMarketSegment.Size = new System.Drawing.Size(320, 23);

            lblProductSeg.AutoSize = true;
            lblProductSeg.Location = new System.Drawing.Point(20, 373);
            lblProductSeg.Text = "Product Segment:";
            cmbProductSegment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbProductSegment.Items.AddRange(new object[] { "Commercial", "Residential", "Security", "Industrial", "Shower & Robe" });
            cmbProductSegment.Location = new System.Drawing.Point(220, 370);
            cmbProductSegment.Size = new System.Drawing.Size(320, 23);

            lblNotes.AutoSize = true;
            lblNotes.Location = new System.Drawing.Point(20, 403);
            lblNotes.Text = "Project Notes:";
            txtProjectNotes.Location = new System.Drawing.Point(220, 400);
            txtProjectNotes.Multiline = true;
            txtProjectNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtProjectNotes.Size = new System.Drawing.Size(320, 80);

            lblFinishType.AutoSize = true;
            lblFinishType.Location = new System.Drawing.Point(20, 493);
            lblFinishType.Text = "Finish Type:";
            cmbFinishType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbFinishType.Items.AddRange(new object[] { "Mill Finish", "Powder", "Anodised" });
            cmbFinishType.Location = new System.Drawing.Point(220, 490);
            cmbFinishType.Size = new System.Drawing.Size(320, 23);

            lblFinishApplicator.AutoSize = true;
            lblFinishApplicator.Location = new System.Drawing.Point(20, 523);
            lblFinishApplicator.Text = "Finish Applicator:";
            cmbFinishApplicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbFinishApplicator.Items.AddRange(new object[]
            {
                "n/a", "Capral P&P - Bremer (AP)", "Capral P&P - Campbellfield (AP)", "Capral P&P - Canningvale (PP)",
                "Capral P&P - HWood Non-Bulk (AP)", "Capral P&P - HWood Bulk (AP)", "Capral Mill (AP)", "AAF powdercoat",
                "Powderline WA", "Capral/AAF QLD anodising", "WA Anodisers", "Other - pls check pricing"
            });
            cmbFinishApplicator.Location = new System.Drawing.Point(220, 520);
            cmbFinishApplicator.Size = new System.Drawing.Size(320, 23);

            lblFinishGroup.AutoSize = true;
            lblFinishGroup.Location = new System.Drawing.Point(20, 553);
            lblFinishGroup.Text = "Finish Group:";
            txtFinishGroup.Location = new System.Drawing.Point(220, 550);
            txtFinishGroup.Size = new System.Drawing.Size(320, 23);

            lblFinishName.AutoSize = true;
            lblFinishName.Location = new System.Drawing.Point(20, 583);
            lblFinishName.Text = "Finish Name:";
            txtFinishName.Location = new System.Drawing.Point(220, 580);
            txtFinishName.Size = new System.Drawing.Size(320, 23);

            lblFinishCustom.AutoSize = true;
            lblFinishCustom.Location = new System.Drawing.Point(20, 613);
            lblFinishCustom.Text = "Finish Custom Name:";
            txtFinishCustomName.Location = new System.Drawing.Point(220, 610);
            txtFinishCustomName.Size = new System.Drawing.Size(320, 23);

            lblSapCode.AutoSize = true;
            lblSapCode.Location = new System.Drawing.Point(20, 643);
            lblSapCode.Text = "Finish SAP Code:";
            txtFinishSapCode.Location = new System.Drawing.Point(220, 640);
            txtFinishSapCode.Size = new System.Drawing.Size(140, 23);

            lblThreshold.AutoSize = true;
            lblThreshold.Location = new System.Drawing.Point(20, 673);
            lblThreshold.Text = "Threshold Choice:";
            cmbThresholdChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbThresholdChoice.Items.AddRange(new object[] { "No", "Anodised Thresholds", "20um Clear", "25um Clear" });
            cmbThresholdChoice.Location = new System.Drawing.Point(220, 670);
            cmbThresholdChoice.Size = new System.Drawing.Size(320, 23);

            lblLocalLogo.AutoSize = true;
            lblLocalLogo.Location = new System.Drawing.Point(20, 703);
            lblLocalLogo.Text = "Add LocAl Logos:";
            cmbLocalLogo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbLocalLogo.Items.AddRange(new object[] { "None", "Green", "SuperGreen" });
            cmbLocalLogo.Location = new System.Drawing.Point(220, 700);
            cmbLocalLogo.Size = new System.Drawing.Size(320, 23);

            lblGenerateMfAlt.AutoSize = true;
            lblGenerateMfAlt.Location = new System.Drawing.Point(20, 733);
            lblGenerateMfAlt.Text = "Generate Mill Finish Alternative:";
            cmbGenerateMillFinishAlternative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbGenerateMillFinishAlternative.Items.AddRange(new object[] { "Yes", "No" });
            cmbGenerateMillFinishAlternative.Location = new System.Drawing.Point(220, 730);
            cmbGenerateMillFinishAlternative.Size = new System.Drawing.Size(140, 23);
            cmbGenerateMillFinishAlternative.SelectedIndex = 1;

            lblQuoteValidity.AutoSize = true;
            lblQuoteValidity.Location = new System.Drawing.Point(20, 763);
            lblQuoteValidity.Text = "Quote Validity:";
            txtQuoteValidity.Location = new System.Drawing.Point(220, 760);
            txtQuoteValidity.Size = new System.Drawing.Size(140, 23);
            txtQuoteValidity.Text = "30 days";

            lblCustomerSapDiscount.AutoSize = true;
            lblCustomerSapDiscount.Location = new System.Drawing.Point(20, 793);
            lblCustomerSapDiscount.Text = "Customer SAP Discount %:";
            txtCustomerSapDiscount.Location = new System.Drawing.Point(220, 790);
            txtCustomerSapDiscount.Size = new System.Drawing.Size(140, 23);

            lblDeliveryCharge.AutoSize = true;
            lblDeliveryCharge.Location = new System.Drawing.Point(20, 823);
            lblDeliveryCharge.Text = "Delivery Charge:";
            txtDeliveryCharge.Location = new System.Drawing.Point(220, 820);
            txtDeliveryCharge.Size = new System.Drawing.Size(140, 23);
            txtDeliveryCharge.Text = "97.00";

            btnImportCre8.Location = new System.Drawing.Point(220, 850);
            btnImportCre8.Size = new System.Drawing.Size(140, 30);
            btnImportCre8.Text = "Import CRE8";
            btnImportCre8.Click += new System.EventHandler(btnImportCre8_Click);

            btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(380, 848);
            btnSave.Size = new System.Drawing.Size(130, 34);
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += new System.EventHandler(btnSave_Click);

            pnlScroll.Controls.Add(lblTitle);
            pnlScroll.Controls.Add(lblCustomer);
            pnlScroll.Controls.Add(txtCustomerName);
            pnlScroll.Controls.Add(lblProject);
            pnlScroll.Controls.Add(txtProjectName);
            pnlScroll.Controls.Add(lblRevDate);
            pnlScroll.Controls.Add(dtpRevisionDate);
            pnlScroll.Controls.Add(lblLME);
            pnlScroll.Controls.Add(txtLMERate);
            pnlScroll.Controls.Add(lblBillet);
            pnlScroll.Controls.Add(txtBilletPremium);
            pnlScroll.Controls.Add(lblRegion);
            pnlScroll.Controls.Add(cmbRegion);
            pnlScroll.Controls.Add(lblQuotedBy);
            pnlScroll.Controls.Add(txtQuotedBy);
            pnlScroll.Controls.Add(lblAccountMgr);
            pnlScroll.Controls.Add(txtAccountManager);
            pnlScroll.Controls.Add(lblAttention);
            pnlScroll.Controls.Add(txtAttention);
            pnlScroll.Controls.Add(lblMarketSeg);
            pnlScroll.Controls.Add(cmbMarketSegment);
            pnlScroll.Controls.Add(lblProductSeg);
            pnlScroll.Controls.Add(cmbProductSegment);
            pnlScroll.Controls.Add(lblNotes);
            pnlScroll.Controls.Add(txtProjectNotes);
            pnlScroll.Controls.Add(lblFinishType);
            pnlScroll.Controls.Add(cmbFinishType);
            pnlScroll.Controls.Add(lblFinishApplicator);
            pnlScroll.Controls.Add(cmbFinishApplicator);
            pnlScroll.Controls.Add(lblFinishGroup);
            pnlScroll.Controls.Add(txtFinishGroup);
            pnlScroll.Controls.Add(lblFinishName);
            pnlScroll.Controls.Add(txtFinishName);
            pnlScroll.Controls.Add(lblFinishCustom);
            pnlScroll.Controls.Add(txtFinishCustomName);
            pnlScroll.Controls.Add(lblSapCode);
            pnlScroll.Controls.Add(txtFinishSapCode);
            pnlScroll.Controls.Add(lblThreshold);
            pnlScroll.Controls.Add(cmbThresholdChoice);
            pnlScroll.Controls.Add(lblLocalLogo);
            pnlScroll.Controls.Add(cmbLocalLogo);
            pnlScroll.Controls.Add(lblGenerateMfAlt);
            pnlScroll.Controls.Add(cmbGenerateMillFinishAlternative);
            pnlScroll.Controls.Add(lblQuoteValidity);
            pnlScroll.Controls.Add(txtQuoteValidity);
            pnlScroll.Controls.Add(lblCustomerSapDiscount);
            pnlScroll.Controls.Add(txtCustomerSapDiscount);
            pnlScroll.Controls.Add(lblDeliveryCharge);
            pnlScroll.Controls.Add(txtDeliveryCharge);
            pnlScroll.Controls.Add(btnImportCre8);
            pnlScroll.Controls.Add(btnSave);

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
