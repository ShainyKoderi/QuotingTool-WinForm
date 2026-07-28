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
            grpMillFinishPricing = new GroupBox();
            lblSapDiscountHeader = new Label();
            txtCustomerSapDiscount = new TextBox();
            lblDeliveryCharge = new Label();
            txtDeliveryCharge = new TextBox();
            lblMFColHdr = new Label();
            lblMFTypHdr = new Label();
            lblMFMinHdr = new Label();
            lblMFMaxHdr = new Label();
            lblMetalPrice = new Label();
            btnResetIpCharges = new Button();
            btnEditIpCategories = new Button();
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
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(1050, 637);
            pnlScroll.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Location = new Point(8, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 28);
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
            grpHeader.Location = new Point(8, 32);
            grpHeader.Name = "grpHeader";
            grpHeader.Size = new Size(495, 139);
            grpHeader.TabIndex = 1;
            grpHeader.TabStop = false;
            grpHeader.Text = "Project Information";
            // 
            // lblQuotedBy
            // 
            lblQuotedBy.AutoSize = true;
            lblQuotedBy.Location = new Point(12, 22);
            lblQuotedBy.Name = "lblQuotedBy";
            lblQuotedBy.Size = new Size(66, 15);
            lblQuotedBy.TabIndex = 0;
            lblQuotedBy.Text = "Quoted By:";
            // 
            // txtQuotedBy
            // 
            txtQuotedBy.Location = new Point(94, 17);
            txtQuotedBy.Name = "txtQuotedBy";
            txtQuotedBy.Size = new Size(180, 23);
            txtQuotedBy.TabIndex = 1;
            // 
            // lblQuoteDate
            // 
            lblQuoteDate.AutoSize = true;
            lblQuoteDate.Location = new Point(284, 19);
            lblQuoteDate.Name = "lblQuoteDate";
            lblQuoteDate.Size = new Size(70, 15);
            lblQuoteDate.TabIndex = 2;
            lblQuoteDate.Text = "Quote Date:";
            // 
            // dtpQuoteDate
            // 
            dtpQuoteDate.Format = DateTimePickerFormat.Short;
            dtpQuoteDate.Location = new Point(360, 16);
            dtpQuoteDate.Name = "dtpQuoteDate";
            dtpQuoteDate.Size = new Size(130, 23);
            dtpQuoteDate.TabIndex = 3;
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Location = new Point(12, 43);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(47, 15);
            lblProjectName.TabIndex = 4;
            lblProjectName.Text = "Project:";
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(94, 38);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(180, 23);
            txtProjectName.TabIndex = 5;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(12, 65);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(62, 15);
            lblCustomerName.TabIndex = 6;
            lblCustomerName.Text = "Customer:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(94, 59);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(180, 23);
            txtCustomerName.TabIndex = 7;
            // 
            // lblPricingNotes
            // 
            lblPricingNotes.AutoSize = true;
            lblPricingNotes.Location = new Point(12, 86);
            lblPricingNotes.Name = "lblPricingNotes";
            lblPricingNotes.Size = new Size(81, 15);
            lblPricingNotes.TabIndex = 8;
            lblPricingNotes.Text = "Pricing Notes:";
            // 
            // txtPricingNotes
            // 
            txtPricingNotes.Location = new Point(94, 81);
            txtPricingNotes.Multiline = true;
            txtPricingNotes.Name = "txtPricingNotes";
            txtPricingNotes.Size = new Size(395, 54);
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
            grpFinishSection.Location = new Point(518, 32);
            grpFinishSection.Name = "grpFinishSection";
            grpFinishSection.Size = new Size(433, 139);
            grpFinishSection.TabIndex = 2;
            grpFinishSection.TabStop = false;
            grpFinishSection.Text = "Finish Selection";
            // 
            // lblFinType
            // 
            lblFinType.AutoSize = true;
            lblFinType.Location = new Point(12, 19);
            lblFinType.Name = "lblFinType";
            lblFinType.Size = new Size(69, 15);
            lblFinType.TabIndex = 0;
            lblFinType.Text = "Finish Type:";
            // 
            // cmbFinType
            // 
            cmbFinType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinType.Location = new Point(155, 16);
            cmbFinType.Name = "cmbFinType";
            cmbFinType.Size = new Size(240, 23);
            cmbFinType.TabIndex = 1;
            // 
            // lblFinishGroup
            // 
            lblFinishGroup.AutoSize = true;
            lblFinishGroup.Location = new Point(12, 40);
            lblFinishGroup.Name = "lblFinishGroup";
            lblFinishGroup.Size = new Size(77, 15);
            lblFinishGroup.TabIndex = 2;
            lblFinishGroup.Text = "Finish Group:";
            // 
            // cmbFinishGroup
            // 
            cmbFinishGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinishGroup.Location = new Point(155, 38);
            cmbFinishGroup.Name = "cmbFinishGroup";
            cmbFinishGroup.Size = new Size(240, 23);
            cmbFinishGroup.TabIndex = 3;
            // 
            // lblFinishName
            // 
            lblFinishName.AutoSize = true;
            lblFinishName.Location = new Point(12, 63);
            lblFinishName.Name = "lblFinishName";
            lblFinishName.Size = new Size(76, 15);
            lblFinishName.TabIndex = 4;
            lblFinishName.Text = "Finish Name:";
            // 
            // cmbFinishName
            // 
            cmbFinishName.Location = new Point(155, 61);
            cmbFinishName.Name = "cmbFinishName";
            cmbFinishName.Size = new Size(240, 23);
            cmbFinishName.TabIndex = 5;
            // 
            // lblApplicatorGroup
            // 
            lblApplicatorGroup.AutoSize = true;
            lblApplicatorGroup.Location = new Point(12, 85);
            lblApplicatorGroup.Name = "lblApplicatorGroup";
            lblApplicatorGroup.Size = new Size(135, 15);
            lblApplicatorGroup.TabIndex = 6;
            lblApplicatorGroup.Text = "Applicator Finish Group:";
            // 
            // cmbApplicatorGroup
            // 
            cmbApplicatorGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbApplicatorGroup.Location = new Point(155, 83);
            cmbApplicatorGroup.Name = "cmbApplicatorGroup";
            cmbApplicatorGroup.Size = new Size(240, 23);
            cmbApplicatorGroup.TabIndex = 7;
            // 
            // lblFinishApplicator
            // 
            lblFinishApplicator.AutoSize = true;
            lblFinishApplicator.Location = new Point(12, 108);
            lblFinishApplicator.Name = "lblFinishApplicator";
            lblFinishApplicator.Size = new Size(99, 15);
            lblFinishApplicator.TabIndex = 8;
            lblFinishApplicator.Text = "Finish Applicator:";
            // 
            // cmbFinishApplicator
            // 
            cmbFinishApplicator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFinishApplicator.Location = new Point(155, 106);
            cmbFinishApplicator.Name = "cmbFinishApplicator";
            cmbFinishApplicator.Size = new Size(240, 23);
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
            grpQuantitiesSection.Location = new Point(8, 173);
            grpQuantitiesSection.Name = "grpQuantitiesSection";
            grpQuantitiesSection.Size = new Size(944, 47);
            grpQuantitiesSection.TabIndex = 3;
            grpQuantitiesSection.TabStop = false;
            grpQuantitiesSection.Text = "Quantities";
            // 
            // lblTotalKg
            // 
            lblTotalKg.AutoSize = true;
            lblTotalKg.Location = new Point(10, 16);
            lblTotalKg.Name = "lblTotalKg";
            lblTotalKg.Size = new Size(52, 15);
            lblTotalKg.TabIndex = 0;
            lblTotalKg.Text = "Total kg:";
            // 
            // lblTotalKgValue
            // 
            lblTotalKgValue.Location = new Point(90, 16);
            lblTotalKgValue.Name = "lblTotalKgValue";
            lblTotalKgValue.Size = new Size(80, 20);
            lblTotalKgValue.TabIndex = 1;
            lblTotalKgValue.Text = "0.00";
            // 
            // lblTotalFinishingM2
            // 
            lblTotalFinishingM2.AutoSize = true;
            lblTotalFinishingM2.Location = new Point(190, 16);
            lblTotalFinishingM2.Name = "lblTotalFinishingM2";
            lblTotalFinishingM2.Size = new Size(105, 15);
            lblTotalFinishingM2.TabIndex = 2;
            lblTotalFinishingM2.Text = "Total Finishing m²:";
            // 
            // lblTotalFinishingM2Value
            // 
            lblTotalFinishingM2Value.Location = new Point(321, 16);
            lblTotalFinishingM2Value.Name = "lblTotalFinishingM2Value";
            lblTotalFinishingM2Value.Size = new Size(69, 20);
            lblTotalFinishingM2Value.TabIndex = 3;
            lblTotalFinishingM2Value.Text = "0.00";
            // 
            // lblApNote
            // 
            lblApNote.AutoSize = true;
            lblApNote.Location = new Point(387, 20);
            lblApNote.Name = "lblApNote";
            lblApNote.Size = new Size(30, 15);
            lblApNote.TabIndex = 4;
            lblApNote.Text = "(AP)";
            // 
            // lblTotalPieces
            // 
            lblTotalPieces.AutoSize = true;
            lblTotalPieces.Location = new Point(451, 16);
            lblTotalPieces.Name = "lblTotalPieces";
            lblTotalPieces.Size = new Size(72, 15);
            lblTotalPieces.TabIndex = 5;
            lblTotalPieces.Text = "Total Pieces:";
            // 
            // lblTotalPiecesValue
            // 
            lblTotalPiecesValue.Location = new Point(551, 16);
            lblTotalPiecesValue.Name = "lblTotalPiecesValue";
            lblTotalPiecesValue.Size = new Size(60, 20);
            lblTotalPiecesValue.TabIndex = 6;
            lblTotalPiecesValue.Text = "0";
            // 
            // grpMillFinishPricing
            // 
            grpMillFinishPricing.Controls.Add(lblSapDiscountHeader);
            grpMillFinishPricing.Controls.Add(txtCustomerSapDiscount);
            grpMillFinishPricing.Controls.Add(lblDeliveryCharge);
            grpMillFinishPricing.Controls.Add(txtDeliveryCharge);
            grpMillFinishPricing.Controls.Add(lblMFColHdr);
            grpMillFinishPricing.Controls.Add(lblMFTypHdr);
            grpMillFinishPricing.Controls.Add(lblMFMinHdr);
            grpMillFinishPricing.Controls.Add(lblMFMaxHdr);
            grpMillFinishPricing.Controls.Add(lblMetalPrice);
            grpMillFinishPricing.Controls.Add(btnResetIpCharges);
            grpMillFinishPricing.Controls.Add(btnEditIpCategories);
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
            grpMillFinishPricing.Location = new Point(8, 222);
            grpMillFinishPricing.Name = "grpMillFinishPricing";
            grpMillFinishPricing.Size = new Size(944, 116);
            grpMillFinishPricing.TabIndex = 4;
            grpMillFinishPricing.TabStop = false;
            grpMillFinishPricing.Text = "Mill Finish Pricing";
            // 
            // lblSapDiscountHeader
            // 
            lblSapDiscountHeader.AutoSize = true;
            lblSapDiscountHeader.Location = new Point(12, 17);
            lblSapDiscountHeader.Name = "lblSapDiscountHeader";
            lblSapDiscountHeader.Size = new Size(149, 15);
            lblSapDiscountHeader.TabIndex = 0;
            lblSapDiscountHeader.Text = "Customer SAP Discount %:";
            // 
            // txtCustomerSapDiscount
            // 
            txtCustomerSapDiscount.Location = new Point(180, 15);
            txtCustomerSapDiscount.Name = "txtCustomerSapDiscount";
            txtCustomerSapDiscount.Size = new Size(80, 23);
            txtCustomerSapDiscount.TabIndex = 1;
            // 
            // lblDeliveryCharge
            // 
            lblDeliveryCharge.AutoSize = true;
            lblDeliveryCharge.Location = new Point(281, 17);
            lblDeliveryCharge.Name = "lblDeliveryCharge";
            lblDeliveryCharge.Size = new Size(93, 15);
            lblDeliveryCharge.TabIndex = 2;
            lblDeliveryCharge.Text = "Delivery Charge:";
            // 
            // txtDeliveryCharge
            // 
            txtDeliveryCharge.Location = new Point(380, 15);
            txtDeliveryCharge.Name = "txtDeliveryCharge";
            txtDeliveryCharge.Size = new Size(80, 23);
            txtDeliveryCharge.TabIndex = 3;
            // 
            // lblMFColHdr
            // 
            lblMFColHdr.Location = new Point(12, 35);
            lblMFColHdr.Name = "lblMFColHdr";
            lblMFColHdr.Size = new Size(240, 18);
            lblMFColHdr.TabIndex = 4;
            // 
            // lblMFTypHdr
            // 
            lblMFTypHdr.Location = new Point(281, 35);
            lblMFTypHdr.Name = "lblMFTypHdr";
            lblMFTypHdr.Size = new Size(70, 18);
            lblMFTypHdr.TabIndex = 3;
            lblMFTypHdr.Text = "Typical";
            // 
            // lblMFMinHdr
            // 
            lblMFMinHdr.Location = new Point(360, 35);
            lblMFMinHdr.Name = "lblMFMinHdr";
            lblMFMinHdr.Size = new Size(55, 18);
            lblMFMinHdr.TabIndex = 4;
            lblMFMinHdr.Text = "Min";
            // 
            // lblMFMaxHdr
            // 
            lblMFMaxHdr.Location = new Point(440, 35);
            lblMFMaxHdr.Name = "lblMFMaxHdr";
            lblMFMaxHdr.Size = new Size(55, 18);
            lblMFMaxHdr.TabIndex = 5;
            lblMFMaxHdr.Text = "Max";
            // 
            // lblMetalPrice
            // 
            lblMetalPrice.Location = new Point(12, 53);
            lblMetalPrice.Name = "lblMetalPrice";
            lblMetalPrice.Size = new Size(200, 18);
            lblMetalPrice.TabIndex = 6;
            lblMetalPrice.Text = "Mill Finish Sell Price $/kg";
            // 
            // btnResetIpCharges
            // 
            btnResetIpCharges.Location = new Point(816, 8);
            btnResetIpCharges.Name = "btnResetIpCharges";
            btnResetIpCharges.Size = new Size(120, 26);
            btnResetIpCharges.TabIndex = 8;
            btnResetIpCharges.Text = "Reset IP Charges";
            btnResetIpCharges.Click += btnResetIpCharges_Click;
            // 
            // btnEditIpCategories
            // 
            btnEditIpCategories.Location = new Point(679, 8);
            btnEditIpCategories.Name = "btnEditIpCategories";
            btnEditIpCategories.Size = new Size(131, 26);
            btnEditIpCategories.TabIndex = 7;
            btnEditIpCategories.Text = "Edit IP Categories";
            btnEditIpCategories.Click += btnEditIpCategories_Click;
            // 
            // txtMetalPrice
            // 
            txtMetalPrice.Location = new Point(281, 52);
            txtMetalPrice.Name = "txtMetalPrice";
            txtMetalPrice.Size = new Size(70, 23);
            txtMetalPrice.TabIndex = 7;
            // 
            // lblMetalPriceMin
            // 
            lblMetalPriceMin.Location = new Point(360, 53);
            lblMetalPriceMin.Name = "lblMetalPriceMin";
            lblMetalPriceMin.Size = new Size(55, 18);
            lblMetalPriceMin.TabIndex = 8;
            lblMetalPriceMin.Text = "—";
            // 
            // lblMetalPriceMax
            // 
            lblMetalPriceMax.Location = new Point(440, 53);
            lblMetalPriceMax.Name = "lblMetalPriceMax";
            lblMetalPriceMax.Size = new Size(55, 18);
            lblMetalPriceMax.TabIndex = 9;
            lblMetalPriceMax.Text = "—";
            // 
            // lblMillFinishMargin
            // 
            lblMillFinishMargin.Location = new Point(12, 74);
            lblMillFinishMargin.Name = "lblMillFinishMargin";
            lblMillFinishMargin.Size = new Size(200, 18);
            lblMillFinishMargin.TabIndex = 10;
            lblMillFinishMargin.Text = "Margin %";
            // 
            // lblMillFinishMarginValue
            // 
            lblMillFinishMarginValue.Location = new Point(281, 74);
            lblMillFinishMarginValue.Name = "lblMillFinishMarginValue";
            lblMillFinishMarginValue.Size = new Size(70, 18);
            lblMillFinishMarginValue.TabIndex = 11;
            lblMillFinishMarginValue.Text = "0.00%";
            // 
            // lblIpHdr
            // 
            lblIpHdr.AutoSize = true;
            lblIpHdr.Location = new Point(570, 17);
            lblIpHdr.Name = "lblIpHdr";
            lblIpHdr.Size = new Size(76, 15);
            lblIpHdr.TabIndex = 12;
            lblIpHdr.Text = "IP Upcharges";
            // 
            // lblIPColHdr
            // 
            lblIPColHdr.Location = new Point(570, 31);
            lblIPColHdr.Name = "lblIPColHdr";
            lblIPColHdr.Size = new Size(165, 18);
            lblIPColHdr.TabIndex = 13;
            // 
            // lblIPTypHdr
            // 
            lblIPTypHdr.Location = new Point(750, 31);
            lblIPTypHdr.Name = "lblIPTypHdr";
            lblIPTypHdr.Size = new Size(65, 18);
            lblIPTypHdr.TabIndex = 14;
            lblIPTypHdr.Text = "$/kg";
            // 
            // lblIP1Upcharge
            // 
            lblIP1Upcharge.Location = new Point(570, 49);
            lblIP1Upcharge.Name = "lblIP1Upcharge";
            lblIP1Upcharge.Size = new Size(165, 18);
            lblIP1Upcharge.TabIndex = 15;
            lblIP1Upcharge.Text = "IP1 Upcharge $/kg:";
            // 
            // txtIP1Upcharge
            // 
            txtIP1Upcharge.Location = new Point(750, 46);
            txtIP1Upcharge.Name = "txtIP1Upcharge";
            txtIP1Upcharge.Size = new Size(60, 23);
            txtIP1Upcharge.TabIndex = 16;
            // 
            // lblIP1Margin
            // 
            lblIP1Margin.Location = new Point(816, 49);
            lblIP1Margin.Name = "lblIP1Margin";
            lblIP1Margin.Size = new Size(60, 18);
            lblIP1Margin.TabIndex = 17;
            lblIP1Margin.Text = "Margin:";
            // 
            // lblIP1MarginValue
            // 
            lblIP1MarginValue.Location = new Point(875, 49);
            lblIP1MarginValue.Name = "lblIP1MarginValue";
            lblIP1MarginValue.Size = new Size(60, 18);
            lblIP1MarginValue.TabIndex = 18;
            lblIP1MarginValue.Text = "0.00%";
            // 
            // lblIP2Upcharge
            // 
            lblIP2Upcharge.Location = new Point(570, 70);
            lblIP2Upcharge.Name = "lblIP2Upcharge";
            lblIP2Upcharge.Size = new Size(165, 18);
            lblIP2Upcharge.TabIndex = 19;
            lblIP2Upcharge.Text = "IP2 Upcharge $/kg:";
            // 
            // txtIP2Upcharge
            // 
            txtIP2Upcharge.Location = new Point(750, 67);
            txtIP2Upcharge.Name = "txtIP2Upcharge";
            txtIP2Upcharge.Size = new Size(60, 23);
            txtIP2Upcharge.TabIndex = 20;
            // 
            // lblIP2Margin
            // 
            lblIP2Margin.Location = new Point(816, 70);
            lblIP2Margin.Name = "lblIP2Margin";
            lblIP2Margin.Size = new Size(60, 18);
            lblIP2Margin.TabIndex = 21;
            lblIP2Margin.Text = "Margin:";
            // 
            // lblIP2MarginValue
            // 
            lblIP2MarginValue.Location = new Point(875, 70);
            lblIP2MarginValue.Name = "lblIP2MarginValue";
            lblIP2MarginValue.Size = new Size(60, 18);
            lblIP2MarginValue.TabIndex = 22;
            lblIP2MarginValue.Text = "0.00%";
            // 
            // lblIP3Rate
            // 
            lblIP3Rate.Location = new Point(570, 91);
            lblIP3Rate.Name = "lblIP3Rate";
            lblIP3Rate.Size = new Size(165, 18);
            lblIP3Rate.TabIndex = 23;
            lblIP3Rate.Text = "IP3 – Panoramic Fixed $/kg:";
            // 
            // txtIP3Rate
            // 
            txtIP3Rate.Location = new Point(750, 88);
            txtIP3Rate.Name = "txtIP3Rate";
            txtIP3Rate.Size = new Size(60, 23);
            txtIP3Rate.TabIndex = 24;
            // 
            // lblIP3Margin
            // 
            lblIP3Margin.Location = new Point(816, 91);
            lblIP3Margin.Name = "lblIP3Margin";
            lblIP3Margin.Size = new Size(60, 18);
            lblIP3Margin.TabIndex = 25;
            lblIP3Margin.Text = "Margin:";
            // 
            // lblIP3MarginValue
            // 
            lblIP3MarginValue.Location = new Point(875, 91);
            lblIP3MarginValue.Name = "lblIP3MarginValue";
            lblIP3MarginValue.Size = new Size(60, 18);
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
            grpThermalBreakPricing.Location = new Point(9, 337);
            grpThermalBreakPricing.Name = "grpThermalBreakPricing";
            grpThermalBreakPricing.Size = new Size(942, 56);
            grpThermalBreakPricing.TabIndex = 5;
            grpThermalBreakPricing.TabStop = false;
            grpThermalBreakPricing.Text = "Thermal Break Pricing";
            // 
            // lblTBCalcType
            // 
            lblTBCalcType.Location = new Point(12, 18);
            lblTBCalcType.Name = "lblTBCalcType";
            lblTBCalcType.Size = new Size(143, 18);
            lblTBCalcType.TabIndex = 0;
            lblTBCalcType.Text = "Thermal Break Method:";
            // 
            // cmbTBCalcType
            // 
            cmbTBCalcType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTBCalcType.Location = new Point(160, 16);
            cmbTBCalcType.Name = "cmbTBCalcType";
            cmbTBCalcType.Size = new Size(200, 23);
            cmbTBCalcType.TabIndex = 1;
            // 
            // lblTBTypHdr
            // 
            lblTBTypHdr.Location = new Point(571, 11);
            lblTBTypHdr.Name = "lblTBTypHdr";
            lblTBTypHdr.Size = new Size(70, 18);
            lblTBTypHdr.TabIndex = 2;
            lblTBTypHdr.Text = "Typical";
            // 
            // lblTBMargin
            // 
            lblTBMargin.Location = new Point(375, 18);
            lblTBMargin.Name = "lblTBMargin";
            lblTBMargin.Size = new Size(190, 18);
            lblTBMargin.TabIndex = 3;
            lblTBMargin.Text = "Enter Margin %:";
            // 
            // txtTBMargin
            // 
            txtTBMargin.Location = new Point(571, 30);
            txtTBMargin.Name = "txtTBMargin";
            txtTBMargin.Size = new Size(70, 23);
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
            grpFinishingPricing.Location = new Point(8, 395);
            grpFinishingPricing.Name = "grpFinishingPricing";
            grpFinishingPricing.Size = new Size(950, 248);
            grpFinishingPricing.TabIndex = 6;
            grpFinishingPricing.TabStop = false;
            grpFinishingPricing.Text = "Powder / Finishing Pricing";
            // 
            // lblPowderPricedAs
            // 
            lblPowderPricedAs.AutoSize = true;
            lblPowderPricedAs.Location = new Point(12, 16);
            lblPowderPricedAs.Name = "lblPowderPricedAs";
            lblPowderPricedAs.Size = new Size(100, 15);
            lblPowderPricedAs.TabIndex = 0;
            lblPowderPricedAs.Text = "Powder Priced as:";
            // 
            // cmbPowderPricedAs
            // 
            cmbPowderPricedAs.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPowderPricedAs.Location = new Point(160, 12);
            cmbPowderPricedAs.Name = "cmbPowderPricedAs";
            cmbPowderPricedAs.Size = new Size(200, 23);
            cmbPowderPricedAs.TabIndex = 1;
            // 
            // lblCostType
            // 
            lblCostType.AutoSize = true;
            lblCostType.Location = new Point(375, 16);
            lblCostType.Name = "lblCostType";
            lblCostType.Size = new Size(62, 15);
            lblCostType.TabIndex = 2;
            lblCostType.Text = "Cost Type:";
            // 
            // cmbCostType
            // 
            cmbCostType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCostType.Location = new Point(475, 12);
            cmbCostType.Name = "cmbCostType";
            cmbCostType.Size = new Size(220, 23);
            cmbCostType.TabIndex = 3;
            // 
            // chkCostTypeOverride
            // 
            chkCostTypeOverride.AutoSize = true;
            chkCostTypeOverride.Location = new Point(710, 16);
            chkCostTypeOverride.Name = "chkCostTypeOverride";
            chkCostTypeOverride.Size = new Size(71, 19);
            chkCostTypeOverride.TabIndex = 4;
            chkCostTypeOverride.Text = "Override";
            // 
            // btnToggleOverride
            // 
            btnToggleOverride.Location = new Point(790, 11);
            btnToggleOverride.Name = "btnToggleOverride";
            btnToggleOverride.Size = new Size(120, 26);
            btnToggleOverride.TabIndex = 5;
            btnToggleOverride.Text = "Toggle Override";
            btnToggleOverride.Click += btnToggleOverride_Click;
            // 
            // lblStdFinishRatesHdr
            // 
            lblStdFinishRatesHdr.AutoSize = true;
            lblStdFinishRatesHdr.Location = new Point(1, 35);
            lblStdFinishRatesHdr.Name = "lblStdFinishRatesHdr";
            lblStdFinishRatesHdr.Size = new Size(358, 15);
            lblStdFinishRatesHdr.TabIndex = 6;
            lblStdFinishRatesHdr.Text = "STANDARD (non-mill) FINISHING RATES (powdercoat or anodised)";
            // 
            // lblFCColHdr
            // 
            lblFCColHdr.Location = new Point(1, 50);
            lblFCColHdr.Name = "lblFCColHdr";
            lblFCColHdr.Size = new Size(280, 18);
            lblFCColHdr.TabIndex = 7;
            // 
            // lblFCTypHdr
            // 
            lblFCTypHdr.Location = new Point(389, 50);
            lblFCTypHdr.Name = "lblFCTypHdr";
            lblFCTypHdr.Size = new Size(70, 18);
            lblFCTypHdr.TabIndex = 8;
            lblFCTypHdr.Text = "Typical";
            // 
            // lblFCMinHdr
            // 
            lblFCMinHdr.Location = new Point(468, 50);
            lblFCMinHdr.Name = "lblFCMinHdr";
            lblFCMinHdr.Size = new Size(55, 18);
            lblFCMinHdr.TabIndex = 9;
            lblFCMinHdr.Text = "Min";
            // 
            // lblFCMaxHdr
            // 
            lblFCMaxHdr.Location = new Point(540, 50);
            lblFCMaxHdr.Name = "lblFCMaxHdr";
            lblFCMaxHdr.Size = new Size(55, 18);
            lblFCMaxHdr.TabIndex = 10;
            lblFCMaxHdr.Text = "Max";
            // 
            // lblFinishingCostPricePerM2
            // 
            lblFinishingCostPricePerM2.Location = new Point(1, 71);
            lblFinishingCostPricePerM2.Name = "lblFinishingCostPricePerM2";
            lblFinishingCostPricePerM2.Size = new Size(280, 18);
            lblFinishingCostPricePerM2.TabIndex = 11;
            lblFinishingCostPricePerM2.Text = "Finishing Cost Price $/m²";
            // 
            // txtFinishingCostPricePerM2
            // 
            txtFinishingCostPricePerM2.Location = new Point(389, 68);
            txtFinishingCostPricePerM2.Name = "txtFinishingCostPricePerM2";
            txtFinishingCostPricePerM2.Size = new Size(70, 23);
            txtFinishingCostPricePerM2.TabIndex = 12;
            // 
            // lblB42
            // 
            lblB42.Location = new Point(1, 92);
            lblB42.Name = "lblB42";
            lblB42.Size = new Size(280, 18);
            lblB42.TabIndex = 13;
            lblB42.Text = "POA/Other Vendor Cost $/m²";
            // 
            // txtB42
            // 
            txtB42.Location = new Point(389, 90);
            txtB42.Name = "txtB42";
            txtB42.Size = new Size(70, 23);
            txtB42.TabIndex = 14;
            // 
            // lblFinishingPrice
            // 
            lblFinishingPrice.Location = new Point(1, 115);
            lblFinishingPrice.Name = "lblFinishingPrice";
            lblFinishingPrice.Size = new Size(280, 18);
            lblFinishingPrice.TabIndex = 15;
            lblFinishingPrice.Text = "Finishing Sell Price $/m²";
            // 
            // txtFinishingPrice
            // 
            txtFinishingPrice.Location = new Point(389, 112);
            txtFinishingPrice.Name = "txtFinishingPrice";
            txtFinishingPrice.Size = new Size(70, 23);
            txtFinishingPrice.TabIndex = 16;
            // 
            // lblB44
            // 
            lblB44.Location = new Point(1, 136);
            lblB44.Name = "lblB44";
            lblB44.Size = new Size(280, 18);
            lblB44.TabIndex = 17;
            lblB44.Text = "Finishing Sell Price override $/m²";
            // 
            // txtB44
            // 
            txtB44.Location = new Point(389, 134);
            txtB44.Name = "txtB44";
            txtB44.Size = new Size(70, 23);
            txtB44.TabIndex = 18;
            // 
            // lblFinishingMargin
            // 
            lblFinishingMargin.Location = new Point(1, 156);
            lblFinishingMargin.Name = "lblFinishingMargin";
            lblFinishingMargin.Size = new Size(280, 18);
            lblFinishingMargin.TabIndex = 19;
            lblFinishingMargin.Text = "Margin %";
            // 
            // lblFinishingMarginValue
            // 
            lblFinishingMarginValue.Location = new Point(389, 156);
            lblFinishingMarginValue.Name = "lblFinishingMarginValue";
            lblFinishingMarginValue.Size = new Size(70, 18);
            lblFinishingMarginValue.TabIndex = 20;
            lblFinishingMarginValue.Text = "0.00%";
            // 
            // lblBulkHdr
            // 
            lblBulkHdr.AutoSize = true;
            lblBulkHdr.Location = new Point(1, 174);
            lblBulkHdr.Name = "lblBulkHdr";
            lblBulkHdr.Size = new Size(168, 15);
            lblBulkHdr.TabIndex = 21;
            lblBulkHdr.Text = "Bulk / Non-bulk Rates (AP/PP)";
            // 
            // lblB48
            // 
            lblB48.Location = new Point(1, 191);
            lblB48.Name = "lblB48";
            lblB48.Size = new Size(240, 18);
            lblB48.TabIndex = 22;
            lblB48.Text = "Mill BULK POA Cost $/m²:";
            // 
            // txtB48
            // 
            txtB48.Location = new Point(389, 190);
            txtB48.Name = "txtB48";
            txtB48.Size = new Size(70, 23);
            txtB48.TabIndex = 23;
            // 
            // lblB50
            // 
            lblB50.Location = new Point(552, 191);
            lblB50.Name = "lblB50";
            lblB50.Size = new Size(240, 18);
            lblB50.TabIndex = 24;
            lblB50.Text = "Mill BULK Sell Price $/m²:";
            // 
            // txtB50
            // 
            txtB50.Location = new Point(852, 190);
            txtB50.Name = "txtB50";
            txtB50.Size = new Size(70, 23);
            txtB50.TabIndex = 25;
            // 
            // lblB52
            // 
            lblB52.Location = new Point(1, 212);
            lblB52.Name = "lblB52";
            lblB52.Size = new Size(240, 18);
            lblB52.TabIndex = 26;
            lblB52.Text = "Mill NON-Bulk POA Cost $/m²:";
            // 
            // txtB52
            // 
            txtB52.Location = new Point(389, 211);
            txtB52.Name = "txtB52";
            txtB52.Size = new Size(70, 23);
            txtB52.TabIndex = 27;
            // 
            // lblB54
            // 
            lblB54.Location = new Point(552, 212);
            lblB54.Name = "lblB54";
            lblB54.Size = new Size(240, 18);
            lblB54.TabIndex = 28;
            lblB54.Text = "Mill NON-Bulk Sell Price $/m²:";
            // 
            // txtB54
            // 
            txtB54.Location = new Point(852, 211);
            txtB54.Name = "txtB54";
            txtB54.Size = new Size(70, 23);
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
            grpAnodisedThresholds.Location = new Point(3, 639);
            grpAnodisedThresholds.Name = "grpAnodisedThresholds";
            grpAnodisedThresholds.Size = new Size(955, 59);
            grpAnodisedThresholds.TabIndex = 7;
            grpAnodisedThresholds.TabStop = false;
            grpAnodisedThresholds.Text = "Anodised Thresholds";
            // 
            // lblAnodisedThresholdCost
            // 
            lblAnodisedThresholdCost.Location = new Point(12, 17);
            lblAnodisedThresholdCost.Name = "lblAnodisedThresholdCost";
            lblAnodisedThresholdCost.Size = new Size(240, 18);
            lblAnodisedThresholdCost.TabIndex = 0;
            lblAnodisedThresholdCost.Text = "Finishing Cost Price $/m² (auto):";
            // 
            // txtAnodisedThresholdCost
            // 
            txtAnodisedThresholdCost.Location = new Point(260, 13);
            txtAnodisedThresholdCost.Name = "txtAnodisedThresholdCost";
            txtAnodisedThresholdCost.Size = new Size(80, 23);
            txtAnodisedThresholdCost.TabIndex = 1;
            // 
            // lblB57
            // 
            lblB57.Location = new Point(370, 17);
            lblB57.Name = "lblB57";
            lblB57.Size = new Size(200, 18);
            lblB57.TabIndex = 2;
            lblB57.Text = "Threshold Cost Price $/m² (B57):";
            // 
            // txtB57
            // 
            txtB57.Location = new Point(580, 13);
            txtB57.Name = "txtB57";
            txtB57.Size = new Size(80, 23);
            txtB57.TabIndex = 3;
            // 
            // lblB58
            // 
            lblB58.Location = new Point(12, 40);
            lblB58.Name = "lblB58";
            lblB58.Size = new Size(240, 18);
            lblB58.TabIndex = 4;
            lblB58.Text = "Finishing Sell Price $/m²:";
            // 
            // txtB58
            // 
            txtB58.Location = new Point(260, 35);
            txtB58.Name = "txtB58";
            txtB58.Size = new Size(80, 23);
            txtB58.TabIndex = 5;
            // 
            // lblThresholdMargin
            // 
            lblThresholdMargin.Location = new Point(375, 37);
            lblThresholdMargin.Name = "lblThresholdMargin";
            lblThresholdMargin.Size = new Size(240, 18);
            lblThresholdMargin.TabIndex = 6;
            lblThresholdMargin.Text = "Margin %:";
            // 
            // lblThresholdMarginValue
            // 
            lblThresholdMarginValue.Location = new Point(623, 37);
            lblThresholdMarginValue.Name = "lblThresholdMarginValue";
            lblThresholdMarginValue.Size = new Size(80, 18);
            lblThresholdMarginValue.TabIndex = 7;
            lblThresholdMarginValue.Text = "0.00%";
            // 
            // btnBulkNonBulkRates
            // 
            btnBulkNonBulkRates.Location = new Point(754, 10);
            btnBulkNonBulkRates.Name = "btnBulkNonBulkRates";
            btnBulkNonBulkRates.Size = new Size(155, 26);
            btnBulkNonBulkRates.TabIndex = 8;
            btnBulkNonBulkRates.Text = "Bulk/Non-bulk Rates";
            btnBulkNonBulkRates.Click += btnBulkNonBulkRates_Click;
            // 
            // grpSummary
            // 
            grpSummary.Controls.Add(gridSummary);
            grpSummary.Controls.Add(lblSummaryNote1);
            grpSummary.Controls.Add(lblSummaryNote2);
            grpSummary.Location = new Point(3, 700);
            grpSummary.Name = "grpSummary";
            grpSummary.Size = new Size(955, 220);
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
            gridSummary.Location = new Point(12, 24);
            gridSummary.Name = "gridSummary";
            gridSummary.ReadOnly = true;
            gridSummary.RowHeadersVisible = false;
            gridSummary.RowHeadersWidth = 62;
            gridSummary.Size = new Size(928, 155);
            gridSummary.TabIndex = 0;
            // 
            // lblSummaryNote1
            // 
            lblSummaryNote1.AutoSize = true;
            lblSummaryNote1.Location = new Point(12, 185);
            lblSummaryNote1.Name = "lblSummaryNote1";
            lblSummaryNote1.Size = new Size(185, 15);
            lblSummaryNote1.TabIndex = 1;
            lblSummaryNote1.Text = "* Margin $ also includes IP charge";
            // 
            // lblSummaryNote2
            // 
            lblSummaryNote2.AutoSize = true;
            lblSummaryNote2.Location = new Point(12, 200);
            lblSummaryNote2.Name = "lblSummaryNote2";
            lblSummaryNote2.Size = new Size(375, 15);
            lblSummaryNote2.TabIndex = 2;
            lblSummaryNote2.Text = "* Sundry costs based on sundry costs entered and slot costs estimated";
            // 
            // btnEditSundryItems
            // 
            btnEditSundryItems.Location = new Point(8, 926);
            btnEditSundryItems.Name = "btnEditSundryItems";
            btnEditSundryItems.Size = new Size(130, 30);
            btnEditSundryItems.TabIndex = 9;
            btnEditSundryItems.Text = "Edit Sundry Items";
            btnEditSundryItems.Click += btnEditSundryItems_Click;
            // 
            // btnRecalculateAll
            // 
            btnRecalculateAll.Location = new Point(148, 926);
            btnRecalculateAll.Name = "btnRecalculateAll";
            btnRecalculateAll.Size = new Size(120, 30);
            btnRecalculateAll.TabIndex = 10;
            btnRecalculateAll.Text = "Recalculate All";
            btnRecalculateAll.Click += btnRecalculateAll_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(277, 923);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 34);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnRefreshSummary
            // 
            btnRefreshSummary.Location = new Point(398, 923);
            btnRefreshSummary.Name = "btnRefreshSummary";
            btnRefreshSummary.Size = new Size(150, 34);
            btnRefreshSummary.TabIndex = 12;
            btnRefreshSummary.Text = "Refresh Summary";
            btnRefreshSummary.Click += btnRefreshSummary_Click;
            // 
            // btnUpdateVisibility
            // 
            btnUpdateVisibility.Location = new Point(557, 926);
            btnUpdateVisibility.Name = "btnUpdateVisibility";
            btnUpdateVisibility.Size = new Size(145, 30);
            btnUpdateVisibility.TabIndex = 13;
            btnUpdateVisibility.Text = "Update Row Visibility";
            btnUpdateVisibility.Click += btnUpdateVisibility_Click;
            // 
            // grpRowVisibility
            // 
            grpRowVisibility.Controls.Add(lblVisibleRows);
            grpRowVisibility.Controls.Add(lblHiddenRows);
            grpRowVisibility.Location = new Point(8, 963);
            grpRowVisibility.Name = "grpRowVisibility";
            grpRowVisibility.Size = new Size(950, 80);
            grpRowVisibility.TabIndex = 14;
            grpRowVisibility.TabStop = false;
            grpRowVisibility.Text = "Row Visibility (advanced)";
            // 
            // lblVisibleRows
            // 
            lblVisibleRows.Location = new Point(10, 22);
            lblVisibleRows.Name = "lblVisibleRows";
            lblVisibleRows.Size = new Size(985, 20);
            lblVisibleRows.TabIndex = 0;
            lblVisibleRows.Text = "Visible rows: —";
            // 
            // lblHiddenRows
            // 
            lblHiddenRows.Location = new Point(10, 46);
            lblHiddenRows.Name = "lblHiddenRows";
            lblHiddenRows.Size = new Size(925, 20);
            lblHiddenRows.TabIndex = 1;
            lblHiddenRows.Text = "Hidden rows: —";
            // 
            // EditPricingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 637);
            Controls.Add(pnlScroll);
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
        private System.Windows.Forms.Label lblDeliveryCharge;
        private System.Windows.Forms.TextBox txtDeliveryCharge;
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
