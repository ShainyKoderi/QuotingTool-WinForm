namespace QuotingTool.Forms
{
    partial class EditPricingForm
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
            grpHeader = new GroupBox();
            lblQuotedBy = new Label();
            txtQuotedBy = new TextBox();
            lblQuoteDate = new Label();
            dtpQuoteDate = new DateTimePicker();
            lblProjectName = new Label();
            txtProjectName = new TextBox();
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblPricingNotes = new Label();
            txtPricingNotes = new TextBox();
            grpFinishSection = new GroupBox();
            lblFinType = new Label();
            cmbFinType = new ComboBox();
            lblFinishGroup = new Label();
            cmbFinishGroup = new ComboBox();
            lblFinishName = new Label();
            cmbFinishName = new ComboBox();
            lblApplicatorGroup = new Label();
            cmbApplicatorGroup = new ComboBox();
            lblFinishApplicator = new Label();
            cmbFinishApplicator = new ComboBox();
            grpQuantitiesSection = new GroupBox();
            lblTotalKg = new Label();
            lblTotalKgValue = new Label();
            lblTotalFinishingM2 = new Label();
            lblTotalFinishingM2Value = new Label();
            lblApNote = new Label();
            lblTotalPieces = new Label();
            lblTotalPiecesValue = new Label();
            btnEditIpCategories = new Button();
            btnResetIpCharges = new Button();
            grpMillFinishPricing = new GroupBox();
            lblSapDiscountHeader = new Label();
            txtCustomerSapDiscount = new TextBox();
            lblMFColHdr = new Label();
            lblMFTypHdr = new Label();
            lblMFMinHdr = new Label();
            lblMFMaxHdr = new Label();
            lblMetalPrice = new Label();
            txtMetalPrice = new TextBox();
            lblMetalPriceMin = new Label();
            lblMetalPriceMax = new Label();
            lblMillFinishMargin = new Label();
            lblMillFinishMarginValue = new Label();
            lblIpHdr = new Label();
            lblIPColHdr = new Label();
            lblIPTypHdr = new Label();
            lblIP1Upcharge = new Label();
            txtIP1Upcharge = new TextBox();
            lblIP1Margin = new Label();
            lblIP1MarginValue = new Label();
            lblIP2Upcharge = new Label();
            txtIP2Upcharge = new TextBox();
            lblIP2Margin = new Label();
            lblIP2MarginValue = new Label();
            lblIP3Rate = new Label();
            txtIP3Rate = new TextBox();
            lblIP3Margin = new Label();
            lblIP3MarginValue = new Label();
            grpThermalBreakPricing = new GroupBox();
            lblTBCalcType = new Label();
            cmbTBCalcType = new ComboBox();
            lblTBTypHdr = new Label();
            lblTBMargin = new Label();
            txtTBMargin = new TextBox();
            grpFinishingPricing = new GroupBox();
            lblPowderPricedAs = new Label();
            cmbPowderPricedAs = new ComboBox();
            lblCostType = new Label();
            cmbCostType = new ComboBox();
            chkCostTypeOverride = new CheckBox();
            btnToggleOverride = new Button();
            lblStdFinishRatesHdr = new Label();
            lblFCColHdr = new Label();
            lblFCTypHdr = new Label();
            lblFCMinHdr = new Label();
            lblFCMaxHdr = new Label();
            lblFinishingCostPricePerM2 = new Label();
            txtFinishingCostPricePerM2 = new TextBox();
            lblB42 = new Label();
            txtB42 = new TextBox();
            lblFinishingPrice = new Label();
            txtFinishingPrice = new TextBox();
            lblB44 = new Label();
            txtB44 = new TextBox();
            lblFinishingMargin = new Label();
            lblFinishingMarginValue = new Label();
            lblBulkHdr = new Label();
            lblB48 = new Label();
            txtB48 = new TextBox();
            lblB50 = new Label();
            txtB50 = new TextBox();
            lblB52 = new Label();
            txtB52 = new TextBox();
            lblB54 = new Label();
            txtB54 = new TextBox();
            grpAnodisedThresholds = new GroupBox();
            lblAnodisedThresholdCost = new Label();
            txtAnodisedThresholdCost = new TextBox();
            lblB57 = new Label();
            txtB57 = new TextBox();
            lblB58 = new Label();
            txtB58 = new TextBox();
            lblThresholdMargin = new Label();
            lblThresholdMarginValue = new Label();
            btnBulkNonBulkRates = new Button();
            grpSummary = new GroupBox();
            gridSummary = new DataGridView();
            lblSummaryNote1 = new Label();
            lblSummaryNote2 = new Label();
            btnEditSundryItems = new Button();
            btnRecalculateAll = new Button();
            btnSave = new Button();
            btnRefreshSummary = new Button();
            btnUpdateVisibility = new Button();
            grpRowVisibility = new GroupBox();
            lblVisibleRows = new Label();
            lblHiddenRows = new Label();
            pnlScroll.SuspendLayout();
            grpHeader.SuspendLayout();
            grpFinishSection.SuspendLayout();
            grpQuantitiesSection.SuspendLayout();
            grpMillFinishPricing.SuspendLayout();
            grpThermalBreakPricing.SuspendLayout();
            grpFinishingPricing.SuspendLayout();
            grpAnodisedThresholds.SuspendLayout();
            grpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridSummary).BeginInit();
            grpRowVisibility.SuspendLayout();
            SuspendLayout();
            // 
            // pnlScroll
            // 
            pnlScroll.AutoScroll = true;
            pnlScroll.Controls.Add(lblTitle);
            pnlScroll.Controls.Add(grpHeader);
            pnlScroll.Controls.Add(grpFinishSection);
            pnlScroll.Controls.Add(grpQuantitiesSection);
            pnlScroll.Controls.Add(grpMillFinishPricing);
            pnlScroll.Controls.Add(grpThermalBreakPricing);
            pnlScroll.Controls.Add(grpFinishingPricing);
            pnlScroll.Controls.Add(grpAnodisedThresholds);
            pnlScroll.Controls.Add(grpSummary);
            pnlScroll.Controls.Add(btnEditSundryItems);
            pnlScroll.Controls.Add(btnRecalculateAll);
            pnlScroll.Controls.Add(btnSave);
            pnlScroll.Controls.Add(btnRefreshSummary);
            pnlScroll.Controls.Add(btnUpdateVisibility);
            pnlScroll.Controls.Add(grpRowVisibility);
            pnlScroll.Dock = DockStyle.Fill;
            pnlScroll.Location = new Point(0, 0);
            pnlScroll.Margin = new Padding(4, 5, 4, 5);
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(1500, 1170);
            pnlScroll.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Location = new Point(11, 13);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(571, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "3. Edit Pricing";
            // 
            // grpHeader
            // 
            grpHeader.Controls.Add(lblQuotedBy);
            grpHeader.Controls.Add(txtQuotedBy);
            grpHeader.Controls.Add(lblQuoteDate);
            grpHeader.Controls.Add(dtpQuoteDate);
            grpHeader.Controls.Add(lblProjectName);
            grpHeader.Controls.Add(txtProjectName);
            grpHeader.Controls.Add(lblCustomerName);
            grpHeader.Controls.Add(txtCustomerName);
            grpHeader.Controls.Add(lblPricingNotes);
            grpHeader.Controls.Add(txtPricingNotes);
            grpHeader.Location = new Point(11, 47);
            grpHeader.Margin = new Padding(4, 5, 4, 5);
            grpHeader.Name = "grpHeader";
            grpHeader.Padding = new Padding(4, 5, 4, 5);
            grpHeader.Size = new Size(707, 231);
            grpHeader.TabIndex = 1;
            grpHeader.TabStop = false;
            grpHeader.Text = "Project Information";
            // 
            // lblQuotedBy
            // 
            lblQuotedBy.AutoSize = true;
            lblQuotedBy.Location = new Point(17, 37);
            lblQuotedBy.Margin = new Padding(4, 0, 4, 0);
            lblQuotedBy.Name = "lblQuotedBy";
            lblQuotedBy.Size = new Size(101, 25);
            lblQuotedBy.TabIndex = 0;
            lblQuotedBy.Text = "Quoted By:";
            // 
            // txtQuotedBy
            // 
            txtQuotedBy.Location = new Point(135, 28);
            txtQuotedBy.Margin = new Padding(4, 5, 4, 5);
            txtQuotedBy.Name = "txtQuotedBy";
            txtQuotedBy.Size = new Size(255, 31);
            txtQuotedBy.TabIndex = 1;
            // 
            // lblQuoteDate
            // 
            lblQuoteDate.AutoSize = true;
            lblQuoteDate.Location = new Point(405, 31);
            lblQuoteDate.Margin = new Padding(4, 0, 4, 0);
            lblQuoteDate.Name = "lblQuoteDate";
            lblQuoteDate.Size = new Size(108, 25);
            lblQuoteDate.TabIndex = 2;
            lblQuoteDate.Text = "Quote Date:";
            // 
            // dtpQuoteDate
            // 
            dtpQuoteDate.Format = DateTimePickerFormat.Short;
            dtpQuoteDate.Location = new Point(514, 26);
            dtpQuoteDate.Margin = new Padding(4, 5, 4, 5);
            dtpQuoteDate.Name = "dtpQuoteDate";
            dtpQuoteDate.Size = new Size(184, 31);
            dtpQuoteDate.TabIndex = 3;
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Location = new Point(17, 72);
            lblProjectName.Margin = new Padding(4, 0, 4, 0);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(70, 25);
            lblProjectName.TabIndex = 4;
            lblProjectName.Text = "Project:";
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(135, 63);
            txtProjectName.Margin = new Padding(4, 5, 4, 5);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(255, 31);
            txtProjectName.TabIndex = 5;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(17, 109);
            lblCustomerName.Margin = new Padding(4, 0, 4, 0);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(93, 25);
            lblCustomerName.TabIndex = 6;
            lblCustomerName.Text = "Customer:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(135, 99);
            txtCustomerName.Margin = new Padding(4, 5, 4, 5);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(255, 31);
            txtCustomerName.TabIndex = 7;
            // 
            // lblPricingNotes
            // 
            lblPricingNotes.AutoSize = true;
            lblPricingNotes.Location = new Point(17, 144);
            lblPricingNotes.Margin = new Padding(4, 0, 4, 0);
            lblPricingNotes.Name = "lblPricingNotes";
            lblPricingNotes.Size = new Size(121, 25);
            lblPricingNotes.TabIndex = 8;
            lblPricingNotes.Text = "Pricing Notes:";
            // 
            // txtPricingNotes
            // 
            txtPricingNotes.Location = new Point(135, 135);
            txtPricingNotes.Margin = new Padding(4, 5, 4, 5);
            txtPricingNotes.Multiline = true;
            txtPricingNotes.Name = "txtPricingNotes";
            txtPricingNotes.Size = new Size(563, 87);
            txtPricingNotes.TabIndex = 9;
            // 
            // grpFinishSection
            // 
            grpFinishSection.Controls.Add(lblFinType);
            grpFinishSection.Controls.Add(cmbFinType);
            grpFinishSection.Controls.Add(lblFinishGroup);
            grpFinishSection.Controls.Add(cmbFinishGroup);
            grpFinishSection.Controls.Add(lblFinishName);
            grpFinishSection.Controls.Add(cmbFinishName);
            grpFinishSection.Controls.Add(lblApplicatorGroup);
            grpFinishSection.Controls.Add(cmbApplicatorGroup);
            grpFinishSection.Controls.Add(lblFinishApplicator);
            grpFinishSection.Controls.Add(cmbFinishApplicator);
            grpFinishSection.Location = new Point(740, 47);
            grpFinishSection.Margin = new Padding(4, 5, 4, 5);
            grpFinishSection.Name = "grpFinishSection";
            grpFinishSection.Padding = new Padding(4, 5, 4, 5);
            grpFinishSection.Size = new Size(574, 231);
            grpFinishSection.TabIndex = 2;
            grpFinishSection.TabStop = false;
            grpFinishSection.Text = "Finish Selection";
            // 
            // lblFinType
            // 
            lblFinType.AutoSize = true;
            lblFinType.Location = new Point(17, 31);
            lblFinType.Margin = new Padding(4, 0, 4, 0);
            lblFinType.Name = "lblFinType";
            lblFinType.Size = new Size(103, 25);
            lblFinType.TabIndex = 0;
            lblFinType.Text = "Finish Type:";
            // 
            // cmbFinType
            // 
            cmbFinType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinType.Location = new Point(221, 26);
            cmbFinType.Margin = new Padding(4, 5, 4, 5);
            cmbFinType.Name = "cmbFinType";
            cmbFinType.Size = new Size(341, 33);
            cmbFinType.TabIndex = 1;
            // 
            // lblFinishGroup
            // 
            lblFinishGroup.AutoSize = true;
            lblFinishGroup.Location = new Point(17, 67);
            lblFinishGroup.Margin = new Padding(4, 0, 4, 0);
            lblFinishGroup.Name = "lblFinishGroup";
            lblFinishGroup.Size = new Size(116, 25);
            lblFinishGroup.TabIndex = 2;
            lblFinishGroup.Text = "Finish Group:";
            // 
            // cmbFinishGroup
            // 
            cmbFinishGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinishGroup.Location = new Point(221, 64);
            cmbFinishGroup.Margin = new Padding(4, 5, 4, 5);
            cmbFinishGroup.Name = "cmbFinishGroup";
            cmbFinishGroup.Size = new Size(341, 33);
            cmbFinishGroup.TabIndex = 3;
            // 
            // lblFinishName
            // 
            lblFinishName.AutoSize = true;
            lblFinishName.Location = new Point(17, 105);
            lblFinishName.Margin = new Padding(4, 0, 4, 0);
            lblFinishName.Name = "lblFinishName";
            lblFinishName.Size = new Size(113, 25);
            lblFinishName.TabIndex = 4;
            lblFinishName.Text = "Finish Name:";
            // 
            // cmbFinishName
            // 
            cmbFinishName.Location = new Point(221, 101);
            cmbFinishName.Margin = new Padding(4, 5, 4, 5);
            cmbFinishName.Name = "cmbFinishName";
            cmbFinishName.Size = new Size(341, 33);
            cmbFinishName.TabIndex = 5;
            // 
            // lblApplicatorGroup
            // 
            lblApplicatorGroup.AutoSize = true;
            lblApplicatorGroup.Location = new Point(17, 141);
            lblApplicatorGroup.Margin = new Padding(4, 0, 4, 0);
            lblApplicatorGroup.Name = "lblApplicatorGroup";
            lblApplicatorGroup.Size = new Size(203, 25);
            lblApplicatorGroup.TabIndex = 6;
            lblApplicatorGroup.Text = "Applicator Finish Group:";
            // 
            // cmbApplicatorGroup
            // 
            cmbApplicatorGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbApplicatorGroup.Location = new Point(221, 138);
            cmbApplicatorGroup.Margin = new Padding(4, 5, 4, 5);
            cmbApplicatorGroup.Name = "cmbApplicatorGroup";
            cmbApplicatorGroup.Size = new Size(341, 33);
            cmbApplicatorGroup.TabIndex = 7;
            // 
            // lblFinishApplicator
            // 
            lblFinishApplicator.AutoSize = true;
            lblFinishApplicator.Location = new Point(17, 180);
            lblFinishApplicator.Margin = new Padding(4, 0, 4, 0);
            lblFinishApplicator.Name = "lblFinishApplicator";
            lblFinishApplicator.Size = new Size(148, 25);
            lblFinishApplicator.TabIndex = 8;
            lblFinishApplicator.Text = "Finish Applicator:";
            // 
            // cmbFinishApplicator
            // 
            cmbFinishApplicator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinishApplicator.Location = new Point(221, 177);
            cmbFinishApplicator.Margin = new Padding(4, 5, 4, 5);
            cmbFinishApplicator.Name = "cmbFinishApplicator";
            cmbFinishApplicator.Size = new Size(341, 33);
            cmbFinishApplicator.TabIndex = 9;
            // 
            // grpQuantitiesSection
            // 
            grpQuantitiesSection.Controls.Add(lblTotalKg);
            grpQuantitiesSection.Controls.Add(lblTotalKgValue);
            grpQuantitiesSection.Controls.Add(lblTotalFinishingM2);
            grpQuantitiesSection.Controls.Add(lblTotalFinishingM2Value);
            grpQuantitiesSection.Controls.Add(lblApNote);
            grpQuantitiesSection.Controls.Add(lblTotalPieces);
            grpQuantitiesSection.Controls.Add(lblTotalPiecesValue);
            grpQuantitiesSection.Controls.Add(btnEditIpCategories);
            grpQuantitiesSection.Controls.Add(btnResetIpCharges);
            grpQuantitiesSection.Location = new Point(11, 288);
            grpQuantitiesSection.Margin = new Padding(4, 5, 4, 5);
            grpQuantitiesSection.Name = "grpQuantitiesSection";
            grpQuantitiesSection.Padding = new Padding(4, 5, 4, 5);
            grpQuantitiesSection.Size = new Size(1303, 79);
            grpQuantitiesSection.TabIndex = 3;
            grpQuantitiesSection.TabStop = false;
            grpQuantitiesSection.Text = "Quantities";
            // 
            // lblTotalKg
            // 
            lblTotalKg.AutoSize = true;
            lblTotalKg.Location = new Point(15, 27);
            lblTotalKg.Margin = new Padding(4, 0, 4, 0);
            lblTotalKg.Name = "lblTotalKg";
            lblTotalKg.Size = new Size(78, 25);
            lblTotalKg.TabIndex = 0;
            lblTotalKg.Text = "Total kg:";
            // 
            // lblTotalKgValue
            // 
            lblTotalKgValue.Location = new Point(129, 27);
            lblTotalKgValue.Margin = new Padding(4, 0, 4, 0);
            lblTotalKgValue.Name = "lblTotalKgValue";
            lblTotalKgValue.Size = new Size(114, 33);
            lblTotalKgValue.TabIndex = 1;
            lblTotalKgValue.Text = "0.00";
            // 
            // lblTotalFinishingM2
            // 
            lblTotalFinishingM2.AutoSize = true;
            lblTotalFinishingM2.Location = new Point(272, 27);
            lblTotalFinishingM2.Margin = new Padding(4, 0, 4, 0);
            lblTotalFinishingM2.Name = "lblTotalFinishingM2";
            lblTotalFinishingM2.Size = new Size(156, 25);
            lblTotalFinishingM2.TabIndex = 2;
            lblTotalFinishingM2.Text = "Total Finishing m²:";
            // 
            // lblTotalFinishingM2Value
            // 
            lblTotalFinishingM2Value.Location = new Point(458, 27);
            lblTotalFinishingM2Value.Margin = new Padding(4, 0, 4, 0);
            lblTotalFinishingM2Value.Name = "lblTotalFinishingM2Value";
            lblTotalFinishingM2Value.Size = new Size(114, 33);
            lblTotalFinishingM2Value.TabIndex = 3;
            lblTotalFinishingM2Value.Text = "0.00";
            // 
            // lblApNote
            // 
            lblApNote.AutoSize = true;
            lblApNote.Location = new Point(587, 32);
            lblApNote.Margin = new Padding(4, 0, 4, 0);
            lblApNote.Name = "lblApNote";
            lblApNote.Size = new Size(44, 25);
            lblApNote.TabIndex = 4;
            lblApNote.Text = "(AP)";
            // 
            // lblTotalPieces
            // 
            lblTotalPieces.AutoSize = true;
            lblTotalPieces.Location = new Point(644, 27);
            lblTotalPieces.Margin = new Padding(4, 0, 4, 0);
            lblTotalPieces.Name = "lblTotalPieces";
            lblTotalPieces.Size = new Size(106, 25);
            lblTotalPieces.TabIndex = 5;
            lblTotalPieces.Text = "Total Pieces:";
            // 
            // lblTotalPiecesValue
            // 
            lblTotalPiecesValue.Location = new Point(787, 27);
            lblTotalPiecesValue.Margin = new Padding(4, 0, 4, 0);
            lblTotalPiecesValue.Name = "lblTotalPiecesValue";
            lblTotalPiecesValue.Size = new Size(86, 33);
            lblTotalPiecesValue.TabIndex = 6;
            lblTotalPiecesValue.Text = "0";
            // 
            // btnEditIpCategories
            // 
            btnEditIpCategories.Location = new Point(867, 23);
            btnEditIpCategories.Margin = new Padding(4, 5, 4, 5);
            btnEditIpCategories.Name = "btnEditIpCategories";
            btnEditIpCategories.Size = new Size(207, 43);
            btnEditIpCategories.TabIndex = 7;
            btnEditIpCategories.Text = "Edit IP Categories";
            btnEditIpCategories.Click += btnEditIpCategories_Click;
            // 
            // btnResetIpCharges
            // 
            btnResetIpCharges.Location = new Point(1088, 23);
            btnResetIpCharges.Margin = new Padding(4, 5, 4, 5);
            btnResetIpCharges.Name = "btnResetIpCharges";
            btnResetIpCharges.Size = new Size(200, 43);
            btnResetIpCharges.TabIndex = 8;
            btnResetIpCharges.Text = "Reset IP Charges";
            btnResetIpCharges.Click += btnResetIpCharges_Click;
            // 
            // grpMillFinishPricing
            // 
            grpMillFinishPricing.Controls.Add(lblSapDiscountHeader);
            grpMillFinishPricing.Controls.Add(txtCustomerSapDiscount);
            grpMillFinishPricing.Controls.Add(lblMFColHdr);
            grpMillFinishPricing.Controls.Add(lblMFTypHdr);
            grpMillFinishPricing.Controls.Add(lblMFMinHdr);
            grpMillFinishPricing.Controls.Add(lblMFMaxHdr);
            grpMillFinishPricing.Controls.Add(lblMetalPrice);
            grpMillFinishPricing.Controls.Add(txtMetalPrice);
            grpMillFinishPricing.Controls.Add(lblMetalPriceMin);
            grpMillFinishPricing.Controls.Add(lblMetalPriceMax);
            grpMillFinishPricing.Controls.Add(lblMillFinishMargin);
            grpMillFinishPricing.Controls.Add(lblMillFinishMarginValue);
            grpMillFinishPricing.Controls.Add(lblIpHdr);
            grpMillFinishPricing.Controls.Add(lblIPColHdr);
            grpMillFinishPricing.Controls.Add(lblIPTypHdr);
            grpMillFinishPricing.Controls.Add(lblIP1Upcharge);
            grpMillFinishPricing.Controls.Add(txtIP1Upcharge);
            grpMillFinishPricing.Controls.Add(lblIP1Margin);
            grpMillFinishPricing.Controls.Add(lblIP1MarginValue);
            grpMillFinishPricing.Controls.Add(lblIP2Upcharge);
            grpMillFinishPricing.Controls.Add(txtIP2Upcharge);
            grpMillFinishPricing.Controls.Add(lblIP2Margin);
            grpMillFinishPricing.Controls.Add(lblIP2MarginValue);
            grpMillFinishPricing.Controls.Add(lblIP3Rate);
            grpMillFinishPricing.Controls.Add(txtIP3Rate);
            grpMillFinishPricing.Controls.Add(lblIP3Margin);
            grpMillFinishPricing.Controls.Add(lblIP3MarginValue);
            grpMillFinishPricing.Location = new Point(11, 364);
            grpMillFinishPricing.Margin = new Padding(4, 5, 4, 5);
            grpMillFinishPricing.Name = "grpMillFinishPricing";
            grpMillFinishPricing.Padding = new Padding(4, 5, 4, 5);
            grpMillFinishPricing.Size = new Size(1443, 199);
            grpMillFinishPricing.TabIndex = 4;
            grpMillFinishPricing.TabStop = false;
            grpMillFinishPricing.Text = "Mill Finish Pricing";
            // 
            // lblSapDiscountHeader
            // 
            lblSapDiscountHeader.AutoSize = true;
            lblSapDiscountHeader.Location = new Point(17, 29);
            lblSapDiscountHeader.Margin = new Padding(4, 0, 4, 0);
            lblSapDiscountHeader.Name = "lblSapDiscountHeader";
            lblSapDiscountHeader.Size = new Size(225, 25);
            lblSapDiscountHeader.TabIndex = 0;
            lblSapDiscountHeader.Text = "Customer SAP Discount %:";
            // 
            // txtCustomerSapDiscount
            // 
            txtCustomerSapDiscount.Location = new Point(257, 26);
            txtCustomerSapDiscount.Margin = new Padding(4, 5, 4, 5);
            txtCustomerSapDiscount.Name = "txtCustomerSapDiscount";
            txtCustomerSapDiscount.Size = new Size(113, 31);
            txtCustomerSapDiscount.TabIndex = 1;
            // 
            // lblMFColHdr
            // 
            lblMFColHdr.Location = new Point(17, 60);
            lblMFColHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFColHdr.Name = "lblMFColHdr";
            lblMFColHdr.Size = new Size(343, 30);
            lblMFColHdr.TabIndex = 2;
            // 
            // lblMFTypHdr
            // 
            lblMFTypHdr.Location = new Point(529, 60);
            lblMFTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFTypHdr.Name = "lblMFTypHdr";
            lblMFTypHdr.Size = new Size(100, 30);
            lblMFTypHdr.TabIndex = 3;
            lblMFTypHdr.Text = "Typical";
            // 
            // lblMFMinHdr
            // 
            lblMFMinHdr.Location = new Point(643, 60);
            lblMFMinHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFMinHdr.Name = "lblMFMinHdr";
            lblMFMinHdr.Size = new Size(79, 30);
            lblMFMinHdr.TabIndex = 4;
            lblMFMinHdr.Text = "Min";
            // 
            // lblMFMaxHdr
            // 
            lblMFMaxHdr.Location = new Point(757, 60);
            lblMFMaxHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFMaxHdr.Name = "lblMFMaxHdr";
            lblMFMaxHdr.Size = new Size(79, 30);
            lblMFMaxHdr.TabIndex = 5;
            lblMFMaxHdr.Text = "Max";
            // 
            // lblMetalPrice
            // 
            lblMetalPrice.Location = new Point(17, 90);
            lblMetalPrice.Margin = new Padding(4, 0, 4, 0);
            lblMetalPrice.Name = "lblMetalPrice";
            lblMetalPrice.Size = new Size(286, 30);
            lblMetalPrice.TabIndex = 6;
            lblMetalPrice.Text = "Mill Finish Sell Price $/kg";
            // 
            // txtMetalPrice
            // 
            txtMetalPrice.Location = new Point(529, 87);
            txtMetalPrice.Margin = new Padding(4, 5, 4, 5);
            txtMetalPrice.Name = "txtMetalPrice";
            txtMetalPrice.Size = new Size(98, 31);
            txtMetalPrice.TabIndex = 7;
            // 
            // lblMetalPriceMin
            // 
            lblMetalPriceMin.Location = new Point(643, 90);
            lblMetalPriceMin.Margin = new Padding(4, 0, 4, 0);
            lblMetalPriceMin.Name = "lblMetalPriceMin";
            lblMetalPriceMin.Size = new Size(79, 30);
            lblMetalPriceMin.TabIndex = 8;
            lblMetalPriceMin.Text = "—";
            // 
            // lblMetalPriceMax
            // 
            lblMetalPriceMax.Location = new Point(757, 90);
            lblMetalPriceMax.Margin = new Padding(4, 0, 4, 0);
            lblMetalPriceMax.Name = "lblMetalPriceMax";
            lblMetalPriceMax.Size = new Size(79, 30);
            lblMetalPriceMax.TabIndex = 9;
            lblMetalPriceMax.Text = "—";
            // 
            // lblMillFinishMargin
            // 
            lblMillFinishMargin.Location = new Point(17, 127);
            lblMillFinishMargin.Margin = new Padding(4, 0, 4, 0);
            lblMillFinishMargin.Name = "lblMillFinishMargin";
            lblMillFinishMargin.Size = new Size(286, 30);
            lblMillFinishMargin.TabIndex = 10;
            lblMillFinishMargin.Text = "Margin %";
            // 
            // lblMillFinishMarginValue
            // 
            lblMillFinishMarginValue.Location = new Point(529, 127);
            lblMillFinishMarginValue.Margin = new Padding(4, 0, 4, 0);
            lblMillFinishMarginValue.Name = "lblMillFinishMarginValue";
            lblMillFinishMarginValue.Size = new Size(100, 30);
            lblMillFinishMarginValue.TabIndex = 11;
            lblMillFinishMarginValue.Text = "0.00%";
            // 
            // lblIpHdr
            // 
            lblIpHdr.AutoSize = true;
            lblIpHdr.Location = new Point(886, 29);
            lblIpHdr.Margin = new Padding(4, 0, 4, 0);
            lblIpHdr.Name = "lblIpHdr";
            lblIpHdr.Size = new Size(116, 25);
            lblIpHdr.TabIndex = 12;
            lblIpHdr.Text = "IP Upcharges";
            // 
            // lblIPColHdr
            // 
            lblIPColHdr.Location = new Point(886, 52);
            lblIPColHdr.Margin = new Padding(4, 0, 4, 0);
            lblIPColHdr.Name = "lblIPColHdr";
            lblIPColHdr.Size = new Size(236, 30);
            lblIPColHdr.TabIndex = 13;
            // 
            // lblIPTypHdr
            // 
            lblIPTypHdr.Location = new Point(1143, 52);
            lblIPTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblIPTypHdr.Name = "lblIPTypHdr";
            lblIPTypHdr.Size = new Size(93, 30);
            lblIPTypHdr.TabIndex = 14;
            lblIPTypHdr.Text = "$/kg";
            // 
            // lblIP1Upcharge
            // 
            lblIP1Upcharge.Location = new Point(886, 82);
            lblIP1Upcharge.Margin = new Padding(4, 0, 4, 0);
            lblIP1Upcharge.Name = "lblIP1Upcharge";
            lblIP1Upcharge.Size = new Size(236, 30);
            lblIP1Upcharge.TabIndex = 15;
            lblIP1Upcharge.Text = "IP1 Upcharge $/kg:";
            // 
            // txtIP1Upcharge
            // 
            txtIP1Upcharge.Location = new Point(1143, 78);
            txtIP1Upcharge.Margin = new Padding(4, 5, 4, 5);
            txtIP1Upcharge.Name = "txtIP1Upcharge";
            txtIP1Upcharge.Size = new Size(84, 31);
            txtIP1Upcharge.TabIndex = 16;
            // 
            // lblIP1Margin
            // 
            lblIP1Margin.Location = new Point(1236, 82);
            lblIP1Margin.Margin = new Padding(4, 0, 4, 0);
            lblIP1Margin.Name = "lblIP1Margin";
            lblIP1Margin.Size = new Size(86, 30);
            lblIP1Margin.TabIndex = 17;
            lblIP1Margin.Text = "Margin:";
            // 
            // lblIP1MarginValue
            // 
            lblIP1MarginValue.Location = new Point(1321, 82);
            lblIP1MarginValue.Margin = new Padding(4, 0, 4, 0);
            lblIP1MarginValue.Name = "lblIP1MarginValue";
            lblIP1MarginValue.Size = new Size(86, 30);
            lblIP1MarginValue.TabIndex = 18;
            lblIP1MarginValue.Text = "0.00%";
            // 
            // lblIP2Upcharge
            // 
            lblIP2Upcharge.Location = new Point(886, 119);
            lblIP2Upcharge.Margin = new Padding(4, 0, 4, 0);
            lblIP2Upcharge.Name = "lblIP2Upcharge";
            lblIP2Upcharge.Size = new Size(236, 30);
            lblIP2Upcharge.TabIndex = 19;
            lblIP2Upcharge.Text = "IP2 Upcharge $/kg:";
            // 
            // txtIP2Upcharge
            // 
            txtIP2Upcharge.Location = new Point(1143, 115);
            txtIP2Upcharge.Margin = new Padding(4, 5, 4, 5);
            txtIP2Upcharge.Name = "txtIP2Upcharge";
            txtIP2Upcharge.Size = new Size(84, 31);
            txtIP2Upcharge.TabIndex = 20;
            // 
            // lblIP2Margin
            // 
            lblIP2Margin.Location = new Point(1236, 119);
            lblIP2Margin.Margin = new Padding(4, 0, 4, 0);
            lblIP2Margin.Name = "lblIP2Margin";
            lblIP2Margin.Size = new Size(86, 30);
            lblIP2Margin.TabIndex = 21;
            lblIP2Margin.Text = "Margin:";
            // 
            // lblIP2MarginValue
            // 
            lblIP2MarginValue.Location = new Point(1321, 119);
            lblIP2MarginValue.Margin = new Padding(4, 0, 4, 0);
            lblIP2MarginValue.Name = "lblIP2MarginValue";
            lblIP2MarginValue.Size = new Size(86, 30);
            lblIP2MarginValue.TabIndex = 22;
            lblIP2MarginValue.Text = "0.00%";
            // 
            // lblIP3Rate
            // 
            lblIP3Rate.Location = new Point(886, 164);
            lblIP3Rate.Margin = new Padding(4, 0, 4, 0);
            lblIP3Rate.Name = "lblIP3Rate";
            lblIP3Rate.Size = new Size(236, 30);
            lblIP3Rate.TabIndex = 23;
            lblIP3Rate.Text = "IP3 – Panoramic Fixed $/kg:";
            // 
            // txtIP3Rate
            // 
            txtIP3Rate.Location = new Point(1143, 160);
            txtIP3Rate.Margin = new Padding(4, 5, 4, 5);
            txtIP3Rate.Name = "txtIP3Rate";
            txtIP3Rate.Size = new Size(84, 31);
            txtIP3Rate.TabIndex = 24;
            // 
            // lblIP3Margin
            // 
            lblIP3Margin.Location = new Point(1236, 164);
            lblIP3Margin.Margin = new Padding(4, 0, 4, 0);
            lblIP3Margin.Name = "lblIP3Margin";
            lblIP3Margin.Size = new Size(86, 30);
            lblIP3Margin.TabIndex = 25;
            lblIP3Margin.Text = "Margin:";
            // 
            // lblIP3MarginValue
            // 
            lblIP3MarginValue.Location = new Point(1321, 164);
            lblIP3MarginValue.Margin = new Padding(4, 0, 4, 0);
            lblIP3MarginValue.Name = "lblIP3MarginValue";
            lblIP3MarginValue.Size = new Size(86, 30);
            lblIP3MarginValue.TabIndex = 26;
            lblIP3MarginValue.Text = "0.00%";
            // 
            // grpThermalBreakPricing
            // 
            grpThermalBreakPricing.Controls.Add(lblTBCalcType);
            grpThermalBreakPricing.Controls.Add(cmbTBCalcType);
            grpThermalBreakPricing.Controls.Add(lblTBTypHdr);
            grpThermalBreakPricing.Controls.Add(lblTBMargin);
            grpThermalBreakPricing.Controls.Add(txtTBMargin);
            grpThermalBreakPricing.Location = new Point(13, 565);
            grpThermalBreakPricing.Margin = new Padding(4, 5, 4, 5);
            grpThermalBreakPricing.Name = "grpThermalBreakPricing";
            grpThermalBreakPricing.Padding = new Padding(4, 5, 4, 5);
            grpThermalBreakPricing.Size = new Size(1443, 94);
            grpThermalBreakPricing.TabIndex = 5;
            grpThermalBreakPricing.TabStop = false;
            grpThermalBreakPricing.Text = "Thermal Break Pricing";
            // 
            // lblTBCalcType
            // 
            lblTBCalcType.Location = new Point(17, 30);
            lblTBCalcType.Margin = new Padding(4, 0, 4, 0);
            lblTBCalcType.Name = "lblTBCalcType";
            lblTBCalcType.Size = new Size(204, 30);
            lblTBCalcType.TabIndex = 0;
            lblTBCalcType.Text = "Thermal Break Method:";
            // 
            // cmbTBCalcType
            // 
            cmbTBCalcType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTBCalcType.Location = new Point(229, 26);
            cmbTBCalcType.Margin = new Padding(4, 5, 4, 5);
            cmbTBCalcType.Name = "cmbTBCalcType";
            cmbTBCalcType.Size = new Size(284, 33);
            cmbTBCalcType.TabIndex = 1;
            // 
            // lblTBTypHdr
            // 
            lblTBTypHdr.Location = new Point(816, 19);
            lblTBTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblTBTypHdr.Name = "lblTBTypHdr";
            lblTBTypHdr.Size = new Size(100, 30);
            lblTBTypHdr.TabIndex = 2;
            lblTBTypHdr.Text = "Typical";
            // 
            // lblTBMargin
            // 
            lblTBMargin.Location = new Point(536, 30);
            lblTBMargin.Margin = new Padding(4, 0, 4, 0);
            lblTBMargin.Name = "lblTBMargin";
            lblTBMargin.Size = new Size(314, 30);
            lblTBMargin.TabIndex = 3;
            lblTBMargin.Text = "Enter Margin %:";
            // 
            // txtTBMargin
            // 
            txtTBMargin.Location = new Point(816, 50);
            txtTBMargin.Margin = new Padding(4, 5, 4, 5);
            txtTBMargin.Name = "txtTBMargin";
            txtTBMargin.Size = new Size(98, 31);
            txtTBMargin.TabIndex = 4;
            // 
            // grpFinishingPricing
            // 
            grpFinishingPricing.Controls.Add(lblPowderPricedAs);
            grpFinishingPricing.Controls.Add(cmbPowderPricedAs);
            grpFinishingPricing.Controls.Add(lblCostType);
            grpFinishingPricing.Controls.Add(cmbCostType);
            grpFinishingPricing.Controls.Add(chkCostTypeOverride);
            grpFinishingPricing.Controls.Add(btnToggleOverride);
            grpFinishingPricing.Controls.Add(lblStdFinishRatesHdr);
            grpFinishingPricing.Controls.Add(lblFCColHdr);
            grpFinishingPricing.Controls.Add(lblFCTypHdr);
            grpFinishingPricing.Controls.Add(lblFCMinHdr);
            grpFinishingPricing.Controls.Add(lblFCMaxHdr);
            grpFinishingPricing.Controls.Add(lblFinishingCostPricePerM2);
            grpFinishingPricing.Controls.Add(txtFinishingCostPricePerM2);
            grpFinishingPricing.Controls.Add(lblB42);
            grpFinishingPricing.Controls.Add(txtB42);
            grpFinishingPricing.Controls.Add(lblFinishingPrice);
            grpFinishingPricing.Controls.Add(txtFinishingPrice);
            grpFinishingPricing.Controls.Add(lblB44);
            grpFinishingPricing.Controls.Add(txtB44);
            grpFinishingPricing.Controls.Add(lblFinishingMargin);
            grpFinishingPricing.Controls.Add(lblFinishingMarginValue);
            grpFinishingPricing.Controls.Add(lblBulkHdr);
            grpFinishingPricing.Controls.Add(lblB48);
            grpFinishingPricing.Controls.Add(txtB48);
            grpFinishingPricing.Controls.Add(lblB50);
            grpFinishingPricing.Controls.Add(txtB50);
            grpFinishingPricing.Controls.Add(lblB52);
            grpFinishingPricing.Controls.Add(txtB52);
            grpFinishingPricing.Controls.Add(lblB54);
            grpFinishingPricing.Controls.Add(txtB54);
            grpFinishingPricing.Location = new Point(11, 663);
            grpFinishingPricing.Margin = new Padding(4, 5, 4, 5);
            grpFinishingPricing.Name = "grpFinishingPricing";
            grpFinishingPricing.Padding = new Padding(4, 5, 4, 5);
            grpFinishingPricing.Size = new Size(1443, 389);
            grpFinishingPricing.TabIndex = 6;
            grpFinishingPricing.TabStop = false;
            grpFinishingPricing.Text = "Powder / Finishing Pricing";
            // 
            // lblPowderPricedAs
            // 
            lblPowderPricedAs.AutoSize = true;
            lblPowderPricedAs.Location = new Point(17, 27);
            lblPowderPricedAs.Margin = new Padding(4, 0, 4, 0);
            lblPowderPricedAs.Name = "lblPowderPricedAs";
            lblPowderPricedAs.Size = new Size(150, 25);
            lblPowderPricedAs.TabIndex = 0;
            lblPowderPricedAs.Text = "Powder Priced as:";
            // 
            // cmbPowderPricedAs
            // 
            cmbPowderPricedAs.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPowderPricedAs.Location = new Point(229, 20);
            cmbPowderPricedAs.Margin = new Padding(4, 5, 4, 5);
            cmbPowderPricedAs.Name = "cmbPowderPricedAs";
            cmbPowderPricedAs.Size = new Size(284, 33);
            cmbPowderPricedAs.TabIndex = 1;
            // 
            // lblCostType
            // 
            lblCostType.AutoSize = true;
            lblCostType.Location = new Point(536, 27);
            lblCostType.Margin = new Padding(4, 0, 4, 0);
            lblCostType.Name = "lblCostType";
            lblCostType.Size = new Size(94, 25);
            lblCostType.TabIndex = 2;
            lblCostType.Text = "Cost Type:";
            // 
            // cmbCostType
            // 
            cmbCostType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCostType.Location = new Point(679, 20);
            cmbCostType.Margin = new Padding(4, 5, 4, 5);
            cmbCostType.Name = "cmbCostType";
            cmbCostType.Size = new Size(313, 33);
            cmbCostType.TabIndex = 3;
            // 
            // chkCostTypeOverride
            // 
            chkCostTypeOverride.AutoSize = true;
            chkCostTypeOverride.Location = new Point(1014, 27);
            chkCostTypeOverride.Margin = new Padding(4, 5, 4, 5);
            chkCostTypeOverride.Name = "chkCostTypeOverride";
            chkCostTypeOverride.Size = new Size(106, 29);
            chkCostTypeOverride.TabIndex = 4;
            chkCostTypeOverride.Text = "Override";
            // 
            // btnToggleOverride
            // 
            btnToggleOverride.Location = new Point(1128, 19);
            btnToggleOverride.Margin = new Padding(4, 5, 4, 5);
            btnToggleOverride.Name = "btnToggleOverride";
            btnToggleOverride.Size = new Size(171, 43);
            btnToggleOverride.TabIndex = 5;
            btnToggleOverride.Text = "Toggle Override";
            btnToggleOverride.Click += btnToggleOverride_Click;
            // 
            // lblStdFinishRatesHdr
            // 
            lblStdFinishRatesHdr.AutoSize = true;
            lblStdFinishRatesHdr.Location = new Point(2, 59);
            lblStdFinishRatesHdr.Margin = new Padding(4, 0, 4, 0);
            lblStdFinishRatesHdr.Name = "lblStdFinishRatesHdr";
            lblStdFinishRatesHdr.Size = new Size(540, 25);
            lblStdFinishRatesHdr.TabIndex = 6;
            lblStdFinishRatesHdr.Text = "STANDARD (non-mill) FINISHING RATES (powdercoat or anodised)";
            // 
            // lblFCColHdr
            // 
            lblFCColHdr.Location = new Point(2, 84);
            lblFCColHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCColHdr.Name = "lblFCColHdr";
            lblFCColHdr.Size = new Size(400, 30);
            lblFCColHdr.TabIndex = 7;
            // 
            // lblFCTypHdr
            // 
            lblFCTypHdr.Location = new Point(556, 84);
            lblFCTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCTypHdr.Name = "lblFCTypHdr";
            lblFCTypHdr.Size = new Size(100, 30);
            lblFCTypHdr.TabIndex = 8;
            lblFCTypHdr.Text = "Typical";
            // 
            // lblFCMinHdr
            // 
            lblFCMinHdr.Location = new Point(668, 84);
            lblFCMinHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCMinHdr.Name = "lblFCMinHdr";
            lblFCMinHdr.Size = new Size(79, 30);
            lblFCMinHdr.TabIndex = 9;
            lblFCMinHdr.Text = "Min";
            // 
            // lblFCMaxHdr
            // 
            lblFCMaxHdr.Location = new Point(771, 84);
            lblFCMaxHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCMaxHdr.Name = "lblFCMaxHdr";
            lblFCMaxHdr.Size = new Size(79, 30);
            lblFCMaxHdr.TabIndex = 10;
            lblFCMaxHdr.Text = "Max";
            // 
            // lblFinishingCostPricePerM2
            // 
            lblFinishingCostPricePerM2.Location = new Point(2, 118);
            lblFinishingCostPricePerM2.Margin = new Padding(4, 0, 4, 0);
            lblFinishingCostPricePerM2.Name = "lblFinishingCostPricePerM2";
            lblFinishingCostPricePerM2.Size = new Size(400, 30);
            lblFinishingCostPricePerM2.TabIndex = 11;
            lblFinishingCostPricePerM2.Text = "Finishing Cost Price $/m²";
            // 
            // txtFinishingCostPricePerM2
            // 
            txtFinishingCostPricePerM2.Location = new Point(556, 114);
            txtFinishingCostPricePerM2.Margin = new Padding(4, 5, 4, 5);
            txtFinishingCostPricePerM2.Name = "txtFinishingCostPricePerM2";
            txtFinishingCostPricePerM2.Size = new Size(98, 31);
            txtFinishingCostPricePerM2.TabIndex = 12;
            // 
            // lblB42
            // 
            lblB42.Location = new Point(2, 154);
            lblB42.Margin = new Padding(4, 0, 4, 0);
            lblB42.Name = "lblB42";
            lblB42.Size = new Size(400, 30);
            lblB42.TabIndex = 13;
            lblB42.Text = "POA/Other Vendor Cost $/m²";
            // 
            // txtB42
            // 
            txtB42.Location = new Point(556, 150);
            txtB42.Margin = new Padding(4, 5, 4, 5);
            txtB42.Name = "txtB42";
            txtB42.Size = new Size(98, 31);
            txtB42.TabIndex = 14;
            // 
            // lblFinishingPrice
            // 
            lblFinishingPrice.Location = new Point(2, 191);
            lblFinishingPrice.Margin = new Padding(4, 0, 4, 0);
            lblFinishingPrice.Name = "lblFinishingPrice";
            lblFinishingPrice.Size = new Size(400, 30);
            lblFinishingPrice.TabIndex = 15;
            lblFinishingPrice.Text = "Finishing Sell Price $/m²";
            // 
            // txtFinishingPrice
            // 
            txtFinishingPrice.Location = new Point(556, 187);
            txtFinishingPrice.Margin = new Padding(4, 5, 4, 5);
            txtFinishingPrice.Name = "txtFinishingPrice";
            txtFinishingPrice.Size = new Size(98, 31);
            txtFinishingPrice.TabIndex = 16;
            // 
            // lblB44
            // 
            lblB44.Location = new Point(2, 227);
            lblB44.Margin = new Padding(4, 0, 4, 0);
            lblB44.Name = "lblB44";
            lblB44.Size = new Size(400, 30);
            lblB44.TabIndex = 17;
            lblB44.Text = "Finishing Sell Price override $/m²";
            // 
            // txtB44
            // 
            txtB44.Location = new Point(556, 223);
            txtB44.Margin = new Padding(4, 5, 4, 5);
            txtB44.Name = "txtB44";
            txtB44.Size = new Size(98, 31);
            txtB44.TabIndex = 18;
            // 
            // lblFinishingMargin
            // 
            lblFinishingMargin.Location = new Point(2, 260);
            lblFinishingMargin.Margin = new Padding(4, 0, 4, 0);
            lblFinishingMargin.Name = "lblFinishingMargin";
            lblFinishingMargin.Size = new Size(400, 30);
            lblFinishingMargin.TabIndex = 19;
            lblFinishingMargin.Text = "Margin %";
            // 
            // lblFinishingMarginValue
            // 
            lblFinishingMarginValue.Location = new Point(556, 260);
            lblFinishingMarginValue.Margin = new Padding(4, 0, 4, 0);
            lblFinishingMarginValue.Name = "lblFinishingMarginValue";
            lblFinishingMarginValue.Size = new Size(100, 30);
            lblFinishingMarginValue.TabIndex = 20;
            lblFinishingMarginValue.Text = "0.00%";
            // 
            // lblBulkHdr
            // 
            lblBulkHdr.AutoSize = true;
            lblBulkHdr.Location = new Point(2, 290);
            lblBulkHdr.Margin = new Padding(4, 0, 4, 0);
            lblBulkHdr.Name = "lblBulkHdr";
            lblBulkHdr.Size = new Size(249, 25);
            lblBulkHdr.TabIndex = 21;
            lblBulkHdr.Text = "Bulk / Non-bulk Rates (AP/PP)";
            // 
            // lblB48
            // 
            lblB48.Location = new Point(2, 319);
            lblB48.Margin = new Padding(4, 0, 4, 0);
            lblB48.Name = "lblB48";
            lblB48.Size = new Size(343, 30);
            lblB48.TabIndex = 22;
            lblB48.Text = "Mill BULK POA Cost $/m²:";
            // 
            // txtB48
            // 
            txtB48.Location = new Point(556, 316);
            txtB48.Margin = new Padding(4, 5, 4, 5);
            txtB48.Name = "txtB48";
            txtB48.Size = new Size(98, 31);
            txtB48.TabIndex = 23;
            // 
            // lblB50
            // 
            lblB50.Location = new Point(788, 319);
            lblB50.Margin = new Padding(4, 0, 4, 0);
            lblB50.Name = "lblB50";
            lblB50.Size = new Size(343, 30);
            lblB50.TabIndex = 24;
            lblB50.Text = "Mill BULK Sell Price $/m²:";
            // 
            // txtB50
            // 
            txtB50.Location = new Point(1217, 316);
            txtB50.Margin = new Padding(4, 5, 4, 5);
            txtB50.Name = "txtB50";
            txtB50.Size = new Size(98, 31);
            txtB50.TabIndex = 25;
            // 
            // lblB52
            // 
            lblB52.Location = new Point(2, 354);
            lblB52.Margin = new Padding(4, 0, 4, 0);
            lblB52.Name = "lblB52";
            lblB52.Size = new Size(343, 30);
            lblB52.TabIndex = 26;
            lblB52.Text = "Mill NON-Bulk POA Cost $/m²:";
            // 
            // txtB52
            // 
            txtB52.Location = new Point(556, 351);
            txtB52.Margin = new Padding(4, 5, 4, 5);
            txtB52.Name = "txtB52";
            txtB52.Size = new Size(98, 31);
            txtB52.TabIndex = 27;
            // 
            // lblB54
            // 
            lblB54.Location = new Point(788, 354);
            lblB54.Margin = new Padding(4, 0, 4, 0);
            lblB54.Name = "lblB54";
            lblB54.Size = new Size(343, 30);
            lblB54.TabIndex = 28;
            lblB54.Text = "Mill NON-Bulk Sell Price $/m²:";
            // 
            // txtB54
            // 
            txtB54.Location = new Point(1217, 351);
            txtB54.Margin = new Padding(4, 5, 4, 5);
            txtB54.Name = "txtB54";
            txtB54.Size = new Size(98, 31);
            txtB54.TabIndex = 29;
            // 
            // grpAnodisedThresholds
            // 
            grpAnodisedThresholds.Controls.Add(lblAnodisedThresholdCost);
            grpAnodisedThresholds.Controls.Add(txtAnodisedThresholdCost);
            grpAnodisedThresholds.Controls.Add(lblB57);
            grpAnodisedThresholds.Controls.Add(txtB57);
            grpAnodisedThresholds.Controls.Add(lblB58);
            grpAnodisedThresholds.Controls.Add(txtB58);
            grpAnodisedThresholds.Controls.Add(lblThresholdMargin);
            grpAnodisedThresholds.Controls.Add(lblThresholdMarginValue);
            grpAnodisedThresholds.Controls.Add(btnBulkNonBulkRates);
            grpAnodisedThresholds.Location = new Point(4, 1055);
            grpAnodisedThresholds.Margin = new Padding(4, 5, 4, 5);
            grpAnodisedThresholds.Name = "grpAnodisedThresholds";
            grpAnodisedThresholds.Padding = new Padding(4, 5, 4, 5);
            grpAnodisedThresholds.Size = new Size(1443, 99);
            grpAnodisedThresholds.TabIndex = 7;
            grpAnodisedThresholds.TabStop = false;
            grpAnodisedThresholds.Text = "Anodised Thresholds";
            // 
            // lblAnodisedThresholdCost
            // 
            lblAnodisedThresholdCost.Location = new Point(17, 29);
            lblAnodisedThresholdCost.Margin = new Padding(4, 0, 4, 0);
            lblAnodisedThresholdCost.Name = "lblAnodisedThresholdCost";
            lblAnodisedThresholdCost.Size = new Size(343, 30);
            lblAnodisedThresholdCost.TabIndex = 0;
            lblAnodisedThresholdCost.Text = "Finishing Cost Price $/m² (auto):";
            // 
            // txtAnodisedThresholdCost
            // 
            txtAnodisedThresholdCost.Location = new Point(371, 22);
            txtAnodisedThresholdCost.Margin = new Padding(4, 5, 4, 5);
            txtAnodisedThresholdCost.Name = "txtAnodisedThresholdCost";
            txtAnodisedThresholdCost.Size = new Size(113, 31);
            txtAnodisedThresholdCost.TabIndex = 1;
            // 
            // lblB57
            // 
            lblB57.Location = new Point(529, 29);
            lblB57.Margin = new Padding(4, 0, 4, 0);
            lblB57.Name = "lblB57";
            lblB57.Size = new Size(286, 30);
            lblB57.TabIndex = 2;
            lblB57.Text = "Threshold Cost Price $/m² (B57):";
            // 
            // txtB57
            // 
            txtB57.Location = new Point(829, 22);
            txtB57.Margin = new Padding(4, 5, 4, 5);
            txtB57.Name = "txtB57";
            txtB57.Size = new Size(113, 31);
            txtB57.TabIndex = 3;
            // 
            // lblB58
            // 
            lblB58.Location = new Point(17, 66);
            lblB58.Margin = new Padding(4, 0, 4, 0);
            lblB58.Name = "lblB58";
            lblB58.Size = new Size(343, 30);
            lblB58.TabIndex = 4;
            lblB58.Text = "Finishing Sell Price $/m²:";
            // 
            // txtB58
            // 
            txtB58.Location = new Point(371, 59);
            txtB58.Margin = new Padding(4, 5, 4, 5);
            txtB58.Name = "txtB58";
            txtB58.Size = new Size(113, 31);
            txtB58.TabIndex = 5;
            // 
            // lblThresholdMargin
            // 
            lblThresholdMargin.Location = new Point(536, 62);
            lblThresholdMargin.Margin = new Padding(4, 0, 4, 0);
            lblThresholdMargin.Name = "lblThresholdMargin";
            lblThresholdMargin.Size = new Size(343, 30);
            lblThresholdMargin.TabIndex = 6;
            lblThresholdMargin.Text = "Margin %:";
            // 
            // lblThresholdMarginValue
            // 
            lblThresholdMarginValue.Location = new Point(890, 62);
            lblThresholdMarginValue.Margin = new Padding(4, 0, 4, 0);
            lblThresholdMarginValue.Name = "lblThresholdMarginValue";
            lblThresholdMarginValue.Size = new Size(114, 30);
            lblThresholdMarginValue.TabIndex = 7;
            lblThresholdMarginValue.Text = "0.00%";
            // 
            // btnBulkNonBulkRates
            // 
            btnBulkNonBulkRates.Location = new Point(1077, 16);
            btnBulkNonBulkRates.Margin = new Padding(4, 5, 4, 5);
            btnBulkNonBulkRates.Name = "btnBulkNonBulkRates";
            btnBulkNonBulkRates.Size = new Size(221, 43);
            btnBulkNonBulkRates.TabIndex = 8;
            btnBulkNonBulkRates.Text = "Bulk/Non-bulk Rates";
            btnBulkNonBulkRates.Click += btnBulkNonBulkRates_Click;
            // 
            // grpSummary
            // 
            grpSummary.Controls.Add(gridSummary);
            grpSummary.Controls.Add(lblSummaryNote1);
            grpSummary.Controls.Add(lblSummaryNote2);
            grpSummary.Location = new Point(4, 1156);
            grpSummary.Margin = new Padding(4, 5, 4, 5);
            grpSummary.Name = "grpSummary";
            grpSummary.Padding = new Padding(4, 5, 4, 5);
            grpSummary.Size = new Size(1443, 367);
            grpSummary.TabIndex = 8;
            grpSummary.TabStop = false;
            grpSummary.Text = "Quote Summary";
            // 
            // gridSummary
            // 
            gridSummary.AllowUserToAddRows = false;
            gridSummary.AllowUserToDeleteRows = false;
            gridSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSummary.BackgroundColor = SystemColors.Control;
            gridSummary.BorderStyle = BorderStyle.Fixed3D;
            gridSummary.ColumnHeadersHeight = 34;
            gridSummary.Location = new Point(17, 40);
            gridSummary.Margin = new Padding(4, 5, 4, 5);
            gridSummary.Name = "gridSummary";
            gridSummary.ReadOnly = true;
            gridSummary.RowHeadersVisible = false;
            gridSummary.RowHeadersWidth = 62;
            gridSummary.Size = new Size(1400, 258);
            gridSummary.TabIndex = 0;
            // 
            // lblSummaryNote1
            // 
            lblSummaryNote1.AutoSize = true;
            lblSummaryNote1.Location = new Point(17, 308);
            lblSummaryNote1.Margin = new Padding(4, 0, 4, 0);
            lblSummaryNote1.Name = "lblSummaryNote1";
            lblSummaryNote1.Size = new Size(280, 25);
            lblSummaryNote1.TabIndex = 1;
            lblSummaryNote1.Text = "* Margin $ also includes IP charge";
            // 
            // lblSummaryNote2
            // 
            lblSummaryNote2.AutoSize = true;
            lblSummaryNote2.Location = new Point(17, 333);
            lblSummaryNote2.Margin = new Padding(4, 0, 4, 0);
            lblSummaryNote2.Name = "lblSummaryNote2";
            lblSummaryNote2.Size = new Size(574, 25);
            lblSummaryNote2.TabIndex = 2;
            lblSummaryNote2.Text = "* Sundry costs based on sundry costs entered and slot costs estimated";
            // 
            // btnEditSundryItems
            // 
            btnEditSundryItems.Location = new Point(11, 1533);
            btnEditSundryItems.Margin = new Padding(4, 5, 4, 5);
            btnEditSundryItems.Name = "btnEditSundryItems";
            btnEditSundryItems.Size = new Size(186, 50);
            btnEditSundryItems.TabIndex = 9;
            btnEditSundryItems.Text = "Edit Sundry Items";
            btnEditSundryItems.Click += btnEditSundryItems_Click;
            // 
            // btnRecalculateAll
            // 
            btnRecalculateAll.Location = new Point(211, 1533);
            btnRecalculateAll.Margin = new Padding(4, 5, 4, 5);
            btnRecalculateAll.Name = "btnRecalculateAll";
            btnRecalculateAll.Size = new Size(171, 50);
            btnRecalculateAll.TabIndex = 10;
            btnRecalculateAll.Text = "Recalculate All";
            btnRecalculateAll.Click += btnRecalculateAll_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(396, 1529);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(157, 57);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnRefreshSummary
            // 
            btnRefreshSummary.Location = new Point(568, 1529);
            btnRefreshSummary.Margin = new Padding(4, 5, 4, 5);
            btnRefreshSummary.Name = "btnRefreshSummary";
            btnRefreshSummary.Size = new Size(214, 57);
            btnRefreshSummary.TabIndex = 12;
            btnRefreshSummary.Text = "Refresh Summary";
            btnRefreshSummary.Click += btnRefreshSummary_Click;
            // 
            // btnUpdateVisibility
            // 
            btnUpdateVisibility.Location = new Point(796, 1533);
            btnUpdateVisibility.Margin = new Padding(4, 5, 4, 5);
            btnUpdateVisibility.Name = "btnUpdateVisibility";
            btnUpdateVisibility.Size = new Size(207, 50);
            btnUpdateVisibility.TabIndex = 13;
            btnUpdateVisibility.Text = "Update Row Visibility";
            btnUpdateVisibility.Click += btnUpdateVisibility_Click;
            // 
            // grpRowVisibility
            // 
            grpRowVisibility.Controls.Add(lblVisibleRows);
            grpRowVisibility.Controls.Add(lblHiddenRows);
            grpRowVisibility.Location = new Point(11, 1595);
            grpRowVisibility.Margin = new Padding(4, 5, 4, 5);
            grpRowVisibility.Name = "grpRowVisibility";
            grpRowVisibility.Padding = new Padding(4, 5, 4, 5);
            grpRowVisibility.Size = new Size(1443, 133);
            grpRowVisibility.TabIndex = 14;
            grpRowVisibility.TabStop = false;
            grpRowVisibility.Text = "Row Visibility (advanced)";
            // 
            // lblVisibleRows
            // 
            lblVisibleRows.Location = new Point(14, 37);
            lblVisibleRows.Margin = new Padding(4, 0, 4, 0);
            lblVisibleRows.Name = "lblVisibleRows";
            lblVisibleRows.Size = new Size(1407, 33);
            lblVisibleRows.TabIndex = 0;
            lblVisibleRows.Text = "Visible rows: —";
            // 
            // lblHiddenRows
            // 
            lblHiddenRows.Location = new Point(14, 77);
            lblHiddenRows.Margin = new Padding(4, 0, 4, 0);
            lblHiddenRows.Name = "lblHiddenRows";
            lblHiddenRows.Size = new Size(1407, 33);
            lblHiddenRows.TabIndex = 1;
            lblHiddenRows.Text = "Hidden rows: —";
            // 
            // EditPricingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1500, 1170);
            Controls.Add(pnlScroll);
            Margin = new Padding(4, 5, 4, 5);
            Name = "EditPricingForm";
            Text = "Edit Pricing";
            pnlScroll.ResumeLayout(false);
            grpHeader.ResumeLayout(false);
            grpHeader.PerformLayout();
            grpFinishSection.ResumeLayout(false);
            grpFinishSection.PerformLayout();
            grpQuantitiesSection.ResumeLayout(false);
            grpQuantitiesSection.PerformLayout();
            grpMillFinishPricing.ResumeLayout(false);
            grpMillFinishPricing.PerformLayout();
            grpThermalBreakPricing.ResumeLayout(false);
            grpThermalBreakPricing.PerformLayout();
            grpFinishingPricing.ResumeLayout(false);
            grpFinishingPricing.PerformLayout();
            grpAnodisedThresholds.ResumeLayout(false);
            grpAnodisedThresholds.PerformLayout();
            grpSummary.ResumeLayout(false);
            grpSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridSummary).EndInit();
            grpRowVisibility.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Private field declarations ────────────────────────────────────────
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Label lblTitle;

        // Header
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Label lblQuotedBy;
        private System.Windows.Forms.TextBox txtQuotedBy;
        private System.Windows.Forms.Label lblQuoteDate;
        private System.Windows.Forms.DateTimePicker dtpQuoteDate;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblPricingNotes;
        private System.Windows.Forms.TextBox txtPricingNotes;

        // Finish
        private System.Windows.Forms.GroupBox grpFinishSection;
        private System.Windows.Forms.Label lblFinType;
        private System.Windows.Forms.ComboBox cmbFinType;
        private System.Windows.Forms.Label lblFinishGroup;
        private System.Windows.Forms.ComboBox cmbFinishGroup;
        private System.Windows.Forms.Label lblFinishName;
        private System.Windows.Forms.ComboBox cmbFinishName;
        private System.Windows.Forms.Label lblApplicatorGroup;
        private System.Windows.Forms.ComboBox cmbApplicatorGroup;
        private System.Windows.Forms.Label lblFinishApplicator;
        private System.Windows.Forms.ComboBox cmbFinishApplicator;

        // Quantities
        private System.Windows.Forms.GroupBox grpQuantitiesSection;
        private System.Windows.Forms.Label lblTotalKg;
        private System.Windows.Forms.Label lblTotalKgValue;
        private System.Windows.Forms.Label lblTotalFinishingM2;
        private System.Windows.Forms.Label lblTotalFinishingM2Value;
        private System.Windows.Forms.Label lblApNote;
        private System.Windows.Forms.Label lblTotalPieces;
        private System.Windows.Forms.Label lblTotalPiecesValue;
        private System.Windows.Forms.Button btnEditIpCategories;
        private System.Windows.Forms.Button btnResetIpCharges;

        // Mill Finish pricing
        private System.Windows.Forms.GroupBox grpMillFinishPricing;
        private System.Windows.Forms.Label lblSapDiscountHeader;
        private System.Windows.Forms.TextBox txtCustomerSapDiscount;
        private System.Windows.Forms.Label lblMFColHdr;
        private System.Windows.Forms.Label lblMFTypHdr;
        private System.Windows.Forms.Label lblMFMinHdr;
        private System.Windows.Forms.Label lblMFMaxHdr;
        private System.Windows.Forms.Label lblMetalPrice;
        private System.Windows.Forms.TextBox txtMetalPrice;
        private System.Windows.Forms.Label lblMetalPriceMin;
        private System.Windows.Forms.Label lblMetalPriceMax;
        private System.Windows.Forms.Label lblMillFinishMargin;
        private System.Windows.Forms.Label lblMillFinishMarginValue;
        // IP
        private System.Windows.Forms.Label lblIpHdr;
        private System.Windows.Forms.Label lblIPColHdr;
        private System.Windows.Forms.Label lblIPTypHdr;
        private System.Windows.Forms.Label lblIP1Upcharge;
        private System.Windows.Forms.TextBox txtIP1Upcharge;
        private System.Windows.Forms.Label lblIP1Margin;
        private System.Windows.Forms.Label lblIP1MarginValue;
        private System.Windows.Forms.Label lblIP2Upcharge;
        private System.Windows.Forms.TextBox txtIP2Upcharge;
        private System.Windows.Forms.Label lblIP2Margin;
        private System.Windows.Forms.Label lblIP2MarginValue;
        private System.Windows.Forms.Label lblIP3Rate;
        private System.Windows.Forms.TextBox txtIP3Rate;
        private System.Windows.Forms.Label lblIP3Margin;
        private System.Windows.Forms.Label lblIP3MarginValue;

        // Thermal Break
        private System.Windows.Forms.GroupBox grpThermalBreakPricing;
        private System.Windows.Forms.Label lblTBCalcType;
        private System.Windows.Forms.ComboBox cmbTBCalcType;
        private System.Windows.Forms.Label lblTBTypHdr;
        private System.Windows.Forms.Label lblTBMargin;
        private System.Windows.Forms.TextBox txtTBMargin;

        // Finishing pricing
        private System.Windows.Forms.GroupBox grpFinishingPricing;
        private System.Windows.Forms.Label lblPowderPricedAs;
        private System.Windows.Forms.ComboBox cmbPowderPricedAs;
        private System.Windows.Forms.Label lblCostType;
        private System.Windows.Forms.ComboBox cmbCostType;
        private System.Windows.Forms.CheckBox chkCostTypeOverride;
        private System.Windows.Forms.Button btnToggleOverride;
        private System.Windows.Forms.Label lblStdFinishRatesHdr;
        private System.Windows.Forms.Label lblFCColHdr;
        private System.Windows.Forms.Label lblFCTypHdr;
        private System.Windows.Forms.Label lblFCMinHdr;
        private System.Windows.Forms.Label lblFCMaxHdr;
        private System.Windows.Forms.Label lblFinishingCostPricePerM2;
        private System.Windows.Forms.TextBox txtFinishingCostPricePerM2;
        private System.Windows.Forms.Label lblB42;
        private System.Windows.Forms.TextBox txtB42;
        private System.Windows.Forms.Label lblFinishingPrice;
        private System.Windows.Forms.TextBox txtFinishingPrice;
        private System.Windows.Forms.Label lblB44;
        private System.Windows.Forms.TextBox txtB44;
        private System.Windows.Forms.Label lblFinishingMargin;
        private System.Windows.Forms.Label lblFinishingMarginValue;
        private System.Windows.Forms.Label lblBulkHdr;
        private System.Windows.Forms.Label lblB48;
        private System.Windows.Forms.TextBox txtB48;
        private System.Windows.Forms.Label lblB50;
        private System.Windows.Forms.TextBox txtB50;
        private System.Windows.Forms.Label lblB52;
        private System.Windows.Forms.TextBox txtB52;
        private System.Windows.Forms.Label lblB54;
        private System.Windows.Forms.TextBox txtB54;

        // Anodised Thresholds
        private System.Windows.Forms.GroupBox grpAnodisedThresholds;
        private System.Windows.Forms.Label lblAnodisedThresholdCost;
        private System.Windows.Forms.TextBox txtAnodisedThresholdCost;
        private System.Windows.Forms.Label lblB57;
        private System.Windows.Forms.TextBox txtB57;
        private System.Windows.Forms.Label lblB58;
        private System.Windows.Forms.TextBox txtB58;
        private System.Windows.Forms.Label lblThresholdMargin;
        private System.Windows.Forms.Label lblThresholdMarginValue;
        private System.Windows.Forms.Button btnBulkNonBulkRates;

        // Summary
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.DataGridView gridSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMarginDollar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMarginPct;
        private System.Windows.Forms.Label lblSummaryNote1;
        private System.Windows.Forms.Label lblSummaryNote2;

        // Action buttons
        private System.Windows.Forms.Button btnEditSundryItems;
        private System.Windows.Forms.Button btnRecalculateAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdateVisibility;
        private System.Windows.Forms.Button btnRefreshSummary;

        // Row visibility
        private System.Windows.Forms.GroupBox grpRowVisibility;
        private System.Windows.Forms.Label lblVisibleRows;
        private System.Windows.Forms.Label lblHiddenRows;
    }
}
