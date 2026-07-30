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
            pnlScroll = new Panel();
            lblTitle = new Label();
            lblCustomer = new Label();
            txtCustomerName = new TextBox();
            lblProject = new Label();
            txtProjectName = new TextBox();
            lblRevDate = new Label();
            dtpRevisionDate = new DateTimePicker();
            lblRegion = new Label();
            cmbRegion = new ComboBox();
            lblQuotedBy = new Label();
            txtQuotedBy = new TextBox();
            lblAccountMgr = new Label();
            txtAccountManager = new TextBox();
            lblAttention = new Label();
            txtAttention = new TextBox();
            lblMarketSeg = new Label();
            cmbMarketSegment = new ComboBox();
            lblProductSeg = new Label();
            cmbProductSegment = new ComboBox();
            lblNotes = new Label();
            txtProjectNotes = new TextBox();
            lblFinishType = new Label();
            cmbFinishType = new ComboBox();
            lblFinishApplicator = new Label();
            cmbFinishApplicator = new ComboBox();
            lblFinishGroup = new Label();
            cmbFinishGroup = new ComboBox();
            lblFinishName = new Label();
            cmbFinishName = new ComboBox();
            lblFinishCustom = new Label();
            txtFinishCustomName = new TextBox();
            lblSapCode = new Label();
            txtFinishSapCode = new TextBox();
            lblThreshold = new Label();
            cmbThresholdChoice = new ComboBox();
            lblLocalLogo = new Label();
            cmbLocalLogo = new ComboBox();
            lblGenerateMfAlt = new Label();
            cmbGenerateMillFinishAlternative = new ComboBox();
            lblQuoteValidity = new Label();
            txtQuoteValidity = new TextBox();
            btnSave = new Button();
            pnlScroll.SuspendLayout();
            SuspendLayout();
            // 
            // pnlScroll
            // 
            pnlScroll.AutoScroll = true;
            pnlScroll.Controls.Add(lblTitle);
            pnlScroll.Controls.Add(lblCustomer);
            pnlScroll.Controls.Add(txtCustomerName);
            pnlScroll.Controls.Add(lblProject);
            pnlScroll.Controls.Add(txtProjectName);
            pnlScroll.Controls.Add(lblRevDate);
            pnlScroll.Controls.Add(dtpRevisionDate);
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
            pnlScroll.Controls.Add(cmbFinishGroup);
            pnlScroll.Controls.Add(lblFinishName);
            pnlScroll.Controls.Add(cmbFinishName);
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
            pnlScroll.Controls.Add(btnSave);
            pnlScroll.Dock = DockStyle.Fill;
            pnlScroll.Location = new Point(0, 0);
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(700, 700);
            pnlScroll.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(205, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Project Details";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(20, 185);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(97, 15);
            lblCustomer.TabIndex = 1;
            lblCustomer.Text = "Customer Name:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(141, 187);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(285, 23);
            txtCustomerName.TabIndex = 2;
            // 
            // lblProject
            // 
            lblProject.AutoSize = true;
            lblProject.Location = new Point(20, 156);
            lblProject.Name = "lblProject";
            lblProject.Size = new Size(82, 15);
            lblProject.TabIndex = 3;
            lblProject.Text = "Project Name:";
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(141, 158);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(285, 23);
            txtProjectName.TabIndex = 4;
            // 
            // lblRevDate
            // 
            lblRevDate.AutoSize = true;
            lblRevDate.Location = new Point(484, 71);
            lblRevDate.Name = "lblRevDate";
            lblRevDate.Size = new Size(70, 15);
            lblRevDate.TabIndex = 5;
            lblRevDate.Text = "Quote Date:";
            // 
            // dtpRevisionDate
            // 
            dtpRevisionDate.Format = DateTimePickerFormat.Short;
            dtpRevisionDate.Location = new Point(574, 68);
            dtpRevisionDate.Name = "dtpRevisionDate";
            dtpRevisionDate.Size = new Size(102, 23);
            dtpRevisionDate.TabIndex = 6;
            // 
            // lblRegion
            // 
            lblRegion.AutoSize = true;
            lblRegion.Location = new Point(20, 69);
            lblRegion.Name = "lblRegion";
            lblRegion.Size = new Size(47, 15);
            lblRegion.TabIndex = 11;
            lblRegion.Text = "Region:";
            // 
            // cmbRegion
            // 
            cmbRegion.Items.AddRange(new object[] { "NQ", "NSW", "NT", "SA", "SEQ", "TAS", "VIC", "WA" });
            cmbRegion.Location = new Point(141, 71);
            cmbRegion.Name = "cmbRegion";
            cmbRegion.Size = new Size(285, 23);
            cmbRegion.TabIndex = 12;
            // 
            // lblQuotedBy
            // 
            lblQuotedBy.AutoSize = true;
            lblQuotedBy.Location = new Point(20, 99);
            lblQuotedBy.Name = "lblQuotedBy";
            lblQuotedBy.Size = new Size(66, 15);
            lblQuotedBy.TabIndex = 13;
            lblQuotedBy.Text = "Quoted By:";
            // 
            // txtQuotedBy
            // 
            txtQuotedBy.Location = new Point(141, 101);
            txtQuotedBy.Name = "txtQuotedBy";
            txtQuotedBy.Size = new Size(285, 23);
            txtQuotedBy.TabIndex = 14;
            // 
            // lblAccountMgr
            // 
            lblAccountMgr.AutoSize = true;
            lblAccountMgr.Location = new Point(20, 129);
            lblAccountMgr.Name = "lblAccountMgr";
            lblAccountMgr.Size = new Size(105, 15);
            lblAccountMgr.TabIndex = 15;
            lblAccountMgr.Text = "Account Manager:";
            // 
            // txtAccountManager
            // 
            txtAccountManager.Location = new Point(141, 131);
            txtAccountManager.Name = "txtAccountManager";
            txtAccountManager.Size = new Size(285, 23);
            txtAccountManager.TabIndex = 16;
            // 
            // lblAttention
            // 
            lblAttention.AutoSize = true;
            lblAttention.Location = new Point(20, 214);
            lblAttention.Name = "lblAttention";
            lblAttention.Size = new Size(76, 15);
            lblAttention.TabIndex = 17;
            lblAttention.Text = "Attention To:";
            // 
            // txtAttention
            // 
            txtAttention.Location = new Point(141, 216);
            txtAttention.Name = "txtAttention";
            txtAttention.Size = new Size(285, 23);
            txtAttention.TabIndex = 18;
            // 
            // lblMarketSeg
            // 
            lblMarketSeg.AutoSize = true;
            lblMarketSeg.Location = new Point(20, 244);
            lblMarketSeg.Name = "lblMarketSeg";
            lblMarketSeg.Size = new Size(97, 15);
            lblMarketSeg.TabIndex = 19;
            lblMarketSeg.Text = "Market Segment:";
            // 
            // cmbMarketSegment
            // 
            cmbMarketSegment.Items.AddRange(new object[] { "Private Residential - Detached", "Private Residential - Other", "Public Dwelling", "Office", "Retail", "Hotel", "Factory", "Warehouse", "Education", "Health", "Entertainment/Recreation", "Other" });
            cmbMarketSegment.Location = new Point(141, 246);
            cmbMarketSegment.Name = "cmbMarketSegment";
            cmbMarketSegment.Size = new Size(285, 23);
            cmbMarketSegment.TabIndex = 20;
            // 
            // lblProductSeg
            // 
            lblProductSeg.AutoSize = true;
            lblProductSeg.Location = new Point(20, 274);
            lblProductSeg.Name = "lblProductSeg";
            lblProductSeg.Size = new Size(102, 15);
            lblProductSeg.TabIndex = 21;
            lblProductSeg.Text = "Product Segment:";
            // 
            // cmbProductSegment
            // 
            cmbProductSegment.Items.AddRange(new object[] { "Commercial", "Residential", "Security", "Industrial", "Shower & Robe" });
            cmbProductSegment.Location = new Point(141, 276);
            cmbProductSegment.Name = "cmbProductSegment";
            cmbProductSegment.Size = new Size(285, 23);
            cmbProductSegment.TabIndex = 22;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Location = new Point(20, 304);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(81, 15);
            lblNotes.TabIndex = 23;
            lblNotes.Text = "Project Notes:";
            // 
            // txtProjectNotes
            // 
            txtProjectNotes.Location = new Point(141, 306);
            txtProjectNotes.Multiline = true;
            txtProjectNotes.Name = "txtProjectNotes";
            txtProjectNotes.ScrollBars = ScrollBars.Vertical;
            txtProjectNotes.Size = new Size(285, 80);
            txtProjectNotes.TabIndex = 24;
            // 
            // lblFinishType
            // 
            lblFinishType.AutoSize = true;
            lblFinishType.Location = new Point(20, 419);
            lblFinishType.Name = "lblFinishType";
            lblFinishType.Size = new Size(69, 15);
            lblFinishType.TabIndex = 25;
            lblFinishType.Text = "Finish Type:";
            // 
            // cmbFinishType
            // 
            cmbFinishType.Items.AddRange(new object[] { "Mill Finish", "Powder", "Anodised" });
            cmbFinishType.Location = new Point(141, 421);
            cmbFinishType.Name = "cmbFinishType";
            cmbFinishType.Size = new Size(285, 23);
            cmbFinishType.TabIndex = 26;
            // 
            // lblFinishApplicator
            // 
            lblFinishApplicator.AutoSize = true;
            lblFinishApplicator.Location = new Point(20, 448);
            lblFinishApplicator.Name = "lblFinishApplicator";
            lblFinishApplicator.Size = new Size(99, 15);
            lblFinishApplicator.TabIndex = 27;
            lblFinishApplicator.Text = "Finish Applicator:";
            // 
            // cmbFinishApplicator
            // 
            cmbFinishApplicator.Items.AddRange(new object[] { "n/a", "Capral P&P - Bremer (AP)", "Capral P&P - Campbellfield (AP)", "Capral P&P - Canningvale (PP)", "Capral P&P - HWood Non-Bulk (AP)", "Capral P&P - HWood Bulk (AP)", "Capral Mill (AP)", "AAF powdercoat", "Powderline WA", "Capral/AAF QLD anodising", "WA Anodisers", "Other - pls check pricing" });
            cmbFinishApplicator.Location = new Point(141, 450);
            cmbFinishApplicator.Name = "cmbFinishApplicator";
            cmbFinishApplicator.Size = new Size(285, 23);
            cmbFinishApplicator.TabIndex = 28;
            // 
            // lblFinishGroup
            // 
            lblFinishGroup.AutoSize = true;
            lblFinishGroup.Location = new Point(20, 478);
            lblFinishGroup.Name = "lblFinishGroup";
            lblFinishGroup.Size = new Size(77, 15);
            lblFinishGroup.TabIndex = 29;
            lblFinishGroup.Text = "Finish Group:";
            // 
            // cmbFinishGroup
            // 
            cmbFinishGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinishGroup.Location = new Point(141, 480);
            cmbFinishGroup.Name = "cmbFinishGroup";
            cmbFinishGroup.Size = new Size(285, 23);
            cmbFinishGroup.TabIndex = 30;
            // 
            // lblFinishName
            // 
            lblFinishName.AutoSize = true;
            lblFinishName.Location = new Point(20, 508);
            lblFinishName.Name = "lblFinishName";
            lblFinishName.Size = new Size(76, 15);
            lblFinishName.TabIndex = 31;
            lblFinishName.Text = "Finish Name:";
            // 
            // cmbFinishName
            // 
            cmbFinishName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinishName.Location = new Point(141, 510);
            cmbFinishName.Name = "cmbFinishName";
            cmbFinishName.Size = new Size(285, 23);
            cmbFinishName.TabIndex = 32;
            // 
            // lblFinishCustom
            // 
            lblFinishCustom.AutoSize = true;
            lblFinishCustom.Location = new Point(20, 538);
            lblFinishCustom.Name = "lblFinishCustom";
            lblFinishCustom.Size = new Size(121, 15);
            lblFinishCustom.TabIndex = 33;
            lblFinishCustom.Text = "Finish Custom Name:";
            // 
            // txtFinishCustomName
            // 
            txtFinishCustomName.Location = new Point(141, 540);
            txtFinishCustomName.Name = "txtFinishCustomName";
            txtFinishCustomName.Size = new Size(285, 23);
            txtFinishCustomName.TabIndex = 34;
            // 
            // lblSapCode
            // 
            lblSapCode.AutoSize = true;
            lblSapCode.Location = new Point(452, 424);
            lblSapCode.Name = "lblSapCode";
            lblSapCode.Size = new Size(96, 15);
            lblSapCode.TabIndex = 35;
            lblSapCode.Text = "Finish SAP Code:";
            // 
            // txtFinishSapCode
            // 
            txtFinishSapCode.Location = new Point(452, 442);
            txtFinishSapCode.Name = "txtFinishSapCode";
            txtFinishSapCode.Size = new Size(140, 23);
            txtFinishSapCode.TabIndex = 36;
            // 
            // lblThreshold
            // 
            lblThreshold.AutoSize = true;
            lblThreshold.Location = new Point(20, 572);
            lblThreshold.Name = "lblThreshold";
            lblThreshold.Size = new Size(116, 15);
            lblThreshold.TabIndex = 37;
            lblThreshold.Text = "Anodised Threshold:";
            // 
            // cmbThresholdChoice
            // 
            cmbThresholdChoice.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThresholdChoice.Items.AddRange(new object[] { "Project Colour", "20um Clear", "25um Clear" });
            cmbThresholdChoice.Location = new Point(141, 569);
            cmbThresholdChoice.Name = "cmbThresholdChoice";
            cmbThresholdChoice.Size = new Size(285, 23);
            cmbThresholdChoice.TabIndex = 38;
            // 
            // lblLocalLogo
            // 
            lblLocalLogo.AutoSize = true;
            lblLocalLogo.Location = new Point(20, 395);
            lblLocalLogo.Name = "lblLocalLogo";
            lblLocalLogo.Size = new Size(100, 15);
            lblLocalLogo.TabIndex = 39;
            lblLocalLogo.Text = "Add LocAl Logos:";
            // 
            // cmbLocalLogo
            // 
            cmbLocalLogo.Items.AddRange(new object[] { "None", "Green", "SuperGreen" });
            cmbLocalLogo.Location = new Point(141, 392);
            cmbLocalLogo.Name = "cmbLocalLogo";
            cmbLocalLogo.Size = new Size(285, 23);
            cmbLocalLogo.TabIndex = 40;
            // 
            // lblGenerateMfAlt
            // 
            lblGenerateMfAlt.AutoSize = true;
            lblGenerateMfAlt.Location = new Point(452, 371);
            lblGenerateMfAlt.Name = "lblGenerateMfAlt";
            lblGenerateMfAlt.Size = new Size(174, 15);
            lblGenerateMfAlt.TabIndex = 41;
            lblGenerateMfAlt.Text = "Generate Mill Finish Alternative:";
            // 
            // cmbGenerateMillFinishAlternative
            // 
            cmbGenerateMillFinishAlternative.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenerateMillFinishAlternative.Items.AddRange(new object[] { "Yes", "No" });
            cmbGenerateMillFinishAlternative.Location = new Point(452, 389);
            cmbGenerateMillFinishAlternative.Name = "cmbGenerateMillFinishAlternative";
            cmbGenerateMillFinishAlternative.Size = new Size(140, 23);
            cmbGenerateMillFinishAlternative.TabIndex = 42;
            // 
            // lblQuoteValidity
            // 
            lblQuoteValidity.AutoSize = true;
            lblQuoteValidity.Location = new Point(484, 99);
            lblQuoteValidity.Name = "lblQuoteValidity";
            lblQuoteValidity.Size = new Size(84, 15);
            lblQuoteValidity.TabIndex = 43;
            lblQuoteValidity.Text = "Quote Validity:";
            // 
            // txtQuoteValidity
            // 
            txtQuoteValidity.Location = new Point(574, 96);
            txtQuoteValidity.Name = "txtQuoteValidity";
            txtQuoteValidity.Size = new Size(102, 23);
            txtQuoteValidity.TabIndex = 44;
            txtQuoteValidity.Text = "30 days";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(545, 14);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(126, 34);
            btnSave.TabIndex = 50;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // ProjectDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 700);
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
        private System.Windows.Forms.ComboBox cmbFinishGroup;
        private System.Windows.Forms.Label lblFinishName;
        private System.Windows.Forms.ComboBox cmbFinishName;
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
        private System.Windows.Forms.Button btnSave;
    }
}
