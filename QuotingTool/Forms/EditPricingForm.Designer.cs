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
            grpBulkCategories = new GroupBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label26 = new Label();
            label27 = new Label();
            label28 = new Label();
            label29 = new Label();
            label30 = new Label();
            label31 = new Label();
            label25 = new Label();
            label24 = new Label();
            label23 = new Label();
            label22 = new Label();
            txtB53 = new TextBox();
            lblB53 = new Label();
            txtB52 = new TextBox();
            lblB52 = new Label();
            txtB49 = new TextBox();
            lblB49 = new Label();
            txtB48 = new TextBox();
            lblB48 = new Label();
            txtB50 = new TextBox();
            lblB50 = new Label();
            txtB54 = new TextBox();
            lblB54 = new Label();
            grpIpCategories = new GroupBox();
            btnResetIpCharges = new Button();
            lblIP3MarginValue = new Label();
            txtIP3Rate = new TextBox();
            lblIP3Rate = new Label();
            lblIP2MarginValue = new Label();
            txtIP2Upcharge = new TextBox();
            lblIP2Upcharge = new Label();
            lblIP1MarginValue = new Label();
            lblIP1Margin = new Label();
            btnEditIpCategories = new Button();
            txtIP1Upcharge = new TextBox();
            lblIP1Upcharge = new Label();
            lblIPTypHdr = new Label();
            lblIPColHdr = new Label();
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
            txtFinType = new TextBox();
            lblFinishGroup = new Label();
            txtFinishGroup = new TextBox();
            lblFinishName = new Label();
            txtFinishName = new TextBox();
            lblApplicatorGroup = new Label();
            txtApplicatorGroup = new TextBox();
            lblFinishApplicator = new Label();
            txtFinishApplicator = new TextBox();
            grpQuantitiesSection = new GroupBox();
            lblTotalKg = new Label();
            lblTotalKgValue = new Label();
            lblTotalFinishingM2 = new Label();
            lblTotalFinishingM2Value = new Label();
            lblApNote = new Label();
            lblTotalPieces = new Label();
            lblTotalPiecesValue = new Label();
            grpMillFinishPricing = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblMetalPriceTyp = new Label();
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
            grpThermalBreakPricing = new GroupBox();
            label6 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblTBMinHdr = new Label();
            lblTBMaxHdr = new Label();
            lblTBCalcType = new Label();
            cmbTBCalcType = new ComboBox();
            lblTBTypHdr = new Label();
            lblTBMargin = new Label();
            txtTBMargin = new TextBox();
            grpFinishingPricing = new GroupBox();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label13 = new Label();
            label14 = new Label();
            btnBulkNonBulkRates = new Button();
            label15 = new Label();
            lblPowderPricedAs = new Label();
            cmbPowderPricedAs = new ComboBox();
            lblCostType = new Label();
            cmbCostType = new ComboBox();
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
            lblFinishingMargin = new Label();
            lblFinishingMarginValue = new Label();
            grpAnodisedThresholds = new GroupBox();
            lblAnodisedThresholdCost = new Label();
            txtAnodisedThresholdCost = new TextBox();
            lblB58 = new Label();
            txtB58 = new TextBox();
            lblThresholdMargin = new Label();
            lblThresholdMarginValue = new Label();
            grpSummary = new GroupBox();
            gridSummary = new DataGridView();
            lblSummaryNote1 = new Label();
            lblSummaryNote2 = new Label();
            btnRecalculateAll = new Button();
            btnSave = new Button();
            btnRefreshSummary = new Button();
            pnlScroll.SuspendLayout();
            grpBulkCategories.SuspendLayout();
            grpIpCategories.SuspendLayout();
            grpHeader.SuspendLayout();
            grpFinishSection.SuspendLayout();
            grpQuantitiesSection.SuspendLayout();
            grpMillFinishPricing.SuspendLayout();
            grpThermalBreakPricing.SuspendLayout();
            grpFinishingPricing.SuspendLayout();
            grpAnodisedThresholds.SuspendLayout();
            grpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridSummary).BeginInit();
            SuspendLayout();
            // 
            // pnlScroll
            // 
            pnlScroll.AutoScroll = true;
            pnlScroll.Controls.Add(grpBulkCategories);
            pnlScroll.Controls.Add(grpIpCategories);
            pnlScroll.Controls.Add(lblTitle);
            pnlScroll.Controls.Add(grpHeader);
            pnlScroll.Controls.Add(grpFinishSection);
            pnlScroll.Controls.Add(grpQuantitiesSection);
            pnlScroll.Controls.Add(grpMillFinishPricing);
            pnlScroll.Controls.Add(grpThermalBreakPricing);
            pnlScroll.Controls.Add(grpFinishingPricing);
            pnlScroll.Controls.Add(grpAnodisedThresholds);
            pnlScroll.Controls.Add(grpSummary);
            pnlScroll.Controls.Add(btnRecalculateAll);
            pnlScroll.Controls.Add(btnSave);
            pnlScroll.Controls.Add(btnRefreshSummary);
            pnlScroll.Dock = DockStyle.Fill;
            pnlScroll.Location = new Point(0, 0);
            pnlScroll.Margin = new Padding(4, 5, 4, 5);
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(1500, 1062);
            pnlScroll.TabIndex = 0;
            // 
            // grpBulkCategories
            // 
            grpBulkCategories.Controls.Add(label10);
            grpBulkCategories.Controls.Add(label11);
            grpBulkCategories.Controls.Add(label12);
            grpBulkCategories.Controls.Add(label16);
            grpBulkCategories.Controls.Add(label17);
            grpBulkCategories.Controls.Add(label18);
            grpBulkCategories.Controls.Add(label7);
            grpBulkCategories.Controls.Add(label8);
            grpBulkCategories.Controls.Add(label9);
            grpBulkCategories.Controls.Add(label26);
            grpBulkCategories.Controls.Add(label27);
            grpBulkCategories.Controls.Add(label28);
            grpBulkCategories.Controls.Add(label29);
            grpBulkCategories.Controls.Add(label30);
            grpBulkCategories.Controls.Add(label31);
            grpBulkCategories.Controls.Add(label25);
            grpBulkCategories.Controls.Add(label24);
            grpBulkCategories.Controls.Add(label23);
            grpBulkCategories.Controls.Add(label22);
            grpBulkCategories.Controls.Add(txtB53);
            grpBulkCategories.Controls.Add(lblB53);
            grpBulkCategories.Controls.Add(txtB52);
            grpBulkCategories.Controls.Add(lblB52);
            grpBulkCategories.Controls.Add(txtB49);
            grpBulkCategories.Controls.Add(lblB49);
            grpBulkCategories.Controls.Add(txtB48);
            grpBulkCategories.Controls.Add(lblB48);
            grpBulkCategories.Controls.Add(txtB50);
            grpBulkCategories.Controls.Add(lblB50);
            grpBulkCategories.Controls.Add(txtB54);
            grpBulkCategories.Controls.Add(lblB54);
            grpBulkCategories.Location = new Point(711, 640);
            grpBulkCategories.Margin = new Padding(4, 5, 4, 5);
            grpBulkCategories.Name = "grpBulkCategories";
            grpBulkCategories.Padding = new Padding(4, 5, 4, 5);
            grpBulkCategories.Size = new Size(664, 413);
            grpBulkCategories.TabIndex = 49;
            grpBulkCategories.TabStop = false;
            grpBulkCategories.Text = "Bulk Rates";
            grpBulkCategories.Visible = false;
            // 
            // label10
            // 
            label10.Location = new Point(417, 343);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(79, 30);
            label10.TabIndex = 53;
            label10.Text = "—";
            // 
            // label11
            // 
            label11.Location = new Point(500, 343);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(79, 30);
            label11.TabIndex = 51;
            label11.Text = "—";
            // 
            // label12
            // 
            label12.Location = new Point(579, 343);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(79, 30);
            label12.TabIndex = 52;
            label12.Text = "—";
            // 
            // label16
            // 
            label16.Location = new Point(417, 305);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(79, 30);
            label16.TabIndex = 50;
            label16.Text = "—";
            // 
            // label17
            // 
            label17.Location = new Point(500, 305);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(81, 30);
            label17.TabIndex = 48;
            label17.Text = "—";
            // 
            // label18
            // 
            label18.Location = new Point(579, 305);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(79, 30);
            label18.TabIndex = 49;
            label18.Text = "—";
            // 
            // label7
            // 
            label7.Location = new Point(417, 195);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(79, 30);
            label7.TabIndex = 47;
            label7.Text = "—";
            // 
            // label8
            // 
            label8.Location = new Point(500, 195);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(79, 30);
            label8.TabIndex = 45;
            label8.Text = "—";
            // 
            // label9
            // 
            label9.Location = new Point(579, 195);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(79, 30);
            label9.TabIndex = 46;
            label9.Text = "—";
            // 
            // label26
            // 
            label26.Location = new Point(417, 157);
            label26.Margin = new Padding(4, 0, 4, 0);
            label26.Name = "label26";
            label26.Size = new Size(79, 30);
            label26.TabIndex = 44;
            label26.Text = "—";
            // 
            // label27
            // 
            label27.Location = new Point(500, 157);
            label27.Margin = new Padding(4, 0, 4, 0);
            label27.Name = "label27";
            label27.Size = new Size(81, 30);
            label27.TabIndex = 42;
            label27.Text = "—";
            // 
            // label28
            // 
            label28.Location = new Point(579, 157);
            label28.Margin = new Padding(4, 0, 4, 0);
            label28.Name = "label28";
            label28.Size = new Size(79, 30);
            label28.TabIndex = 43;
            label28.Text = "—";
            // 
            // label29
            // 
            label29.Location = new Point(400, 27);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new Size(100, 30);
            label29.TabIndex = 39;
            label29.Text = "Typical";
            // 
            // label30
            // 
            label30.Location = new Point(499, 27);
            label30.Margin = new Padding(4, 0, 4, 0);
            label30.Name = "label30";
            label30.Size = new Size(53, 30);
            label30.TabIndex = 40;
            label30.Text = "Min";
            // 
            // label31
            // 
            label31.Location = new Point(574, 27);
            label31.Margin = new Padding(4, 0, 4, 0);
            label31.Name = "label31";
            label31.Size = new Size(79, 30);
            label31.TabIndex = 41;
            label31.Text = "Max";
            // 
            // label25
            // 
            label25.Location = new Point(291, 352);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new Size(79, 30);
            label25.TabIndex = 38;
            label25.Text = "—";
            // 
            // label24
            // 
            label24.Location = new Point(291, 195);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(79, 22);
            label24.TabIndex = 37;
            label24.Text = "—";
            // 
            // label23
            // 
            label23.Location = new Point(9, 347);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(241, 30);
            label23.TabIndex = 35;
            label23.Text = "Margin %";
            // 
            // label22
            // 
            label22.Location = new Point(9, 182);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(241, 30);
            label22.TabIndex = 34;
            label22.Text = "Margin %";
            // 
            // txtB53
            // 
            txtB53.Location = new Point(291, 262);
            txtB53.Margin = new Padding(4, 5, 4, 5);
            txtB53.Name = "txtB53";
            txtB53.ReadOnly = true;
            txtB53.Size = new Size(98, 31);
            txtB53.TabIndex = 31;
            // 
            // lblB53
            // 
            lblB53.Location = new Point(9, 262);
            lblB53.Margin = new Padding(4, 0, 4, 0);
            lblB53.Name = "lblB53";
            lblB53.Size = new Size(287, 30);
            lblB53.TabIndex = 30;
            lblB53.Text = "Finishing NON-Bulk Cost Price $/m²:";
            // 
            // txtB52
            // 
            txtB52.Location = new Point(291, 218);
            txtB52.Margin = new Padding(4, 5, 4, 5);
            txtB52.Name = "txtB52";
            txtB52.Size = new Size(98, 31);
            txtB52.TabIndex = 29;
            // 
            // lblB52
            // 
            lblB52.Location = new Point(9, 217);
            lblB52.Margin = new Padding(4, 0, 4, 0);
            lblB52.Name = "lblB52";
            lblB52.Size = new Size(230, 30);
            lblB52.TabIndex = 28;
            lblB52.Text = "Mill NON-Bulk POA Cost $/m²:";
            // 
            // txtB49
            // 
            txtB49.Location = new Point(291, 107);
            txtB49.Margin = new Padding(4, 5, 4, 5);
            txtB49.Name = "txtB49";
            txtB49.ReadOnly = true;
            txtB49.Size = new Size(98, 31);
            txtB49.TabIndex = 25;
            // 
            // lblB49
            // 
            lblB49.Location = new Point(9, 107);
            lblB49.Margin = new Padding(4, 0, 4, 0);
            lblB49.Name = "lblB49";
            lblB49.Size = new Size(287, 30);
            lblB49.TabIndex = 24;
            lblB49.Text = "Finishing BULK Cost Price $/m²:";
            // 
            // txtB48
            // 
            txtB48.Location = new Point(291, 63);
            txtB48.Margin = new Padding(4, 5, 4, 5);
            txtB48.Name = "txtB48";
            txtB48.Size = new Size(98, 31);
            txtB48.TabIndex = 23;
            // 
            // lblB48
            // 
            lblB48.Location = new Point(9, 63);
            lblB48.Margin = new Padding(4, 0, 4, 0);
            lblB48.Name = "lblB48";
            lblB48.Size = new Size(230, 30);
            lblB48.TabIndex = 22;
            lblB48.Text = "Mill BULK POA Cost $/m²:";
            // 
            // txtB50
            // 
            txtB50.Location = new Point(291, 152);
            txtB50.Margin = new Padding(4, 5, 4, 5);
            txtB50.Name = "txtB50";
            txtB50.Size = new Size(98, 31);
            txtB50.TabIndex = 27;
            // 
            // lblB50
            // 
            lblB50.Location = new Point(9, 147);
            lblB50.Margin = new Padding(4, 0, 4, 0);
            lblB50.Name = "lblB50";
            lblB50.Size = new Size(216, 30);
            lblB50.TabIndex = 26;
            lblB50.Text = "Mill BULK Sell Price $/m²:";
            // 
            // txtB54
            // 
            txtB54.Location = new Point(291, 308);
            txtB54.Margin = new Padding(4, 5, 4, 5);
            txtB54.Name = "txtB54";
            txtB54.Size = new Size(98, 31);
            txtB54.TabIndex = 33;
            // 
            // lblB54
            // 
            lblB54.Location = new Point(9, 305);
            lblB54.Margin = new Padding(4, 0, 4, 0);
            lblB54.Name = "lblB54";
            lblB54.Size = new Size(241, 30);
            lblB54.TabIndex = 32;
            lblB54.Text = "Mill NON-Bulk Sell Price $/m²:";
            // 
            // grpIpCategories
            // 
            grpIpCategories.Controls.Add(btnResetIpCharges);
            grpIpCategories.Controls.Add(lblIP3MarginValue);
            grpIpCategories.Controls.Add(txtIP3Rate);
            grpIpCategories.Controls.Add(lblIP3Rate);
            grpIpCategories.Controls.Add(lblIP2MarginValue);
            grpIpCategories.Controls.Add(txtIP2Upcharge);
            grpIpCategories.Controls.Add(lblIP2Upcharge);
            grpIpCategories.Controls.Add(lblIP1MarginValue);
            grpIpCategories.Controls.Add(lblIP1Margin);
            grpIpCategories.Controls.Add(btnEditIpCategories);
            grpIpCategories.Controls.Add(txtIP1Upcharge);
            grpIpCategories.Controls.Add(lblIP1Upcharge);
            grpIpCategories.Controls.Add(lblIPTypHdr);
            grpIpCategories.Controls.Add(lblIPColHdr);
            grpIpCategories.Location = new Point(711, 460);
            grpIpCategories.Margin = new Padding(4, 5, 4, 5);
            grpIpCategories.Name = "grpIpCategories";
            grpIpCategories.Padding = new Padding(4, 5, 4, 5);
            grpIpCategories.Size = new Size(666, 180);
            grpIpCategories.TabIndex = 14;
            grpIpCategories.TabStop = false;
            grpIpCategories.Text = "IP Categories";
            // 
            // btnResetIpCharges
            // 
            btnResetIpCharges.Location = new Point(470, 90);
            btnResetIpCharges.Margin = new Padding(4, 5, 4, 5);
            btnResetIpCharges.Name = "btnResetIpCharges";
            btnResetIpCharges.Size = new Size(166, 43);
            btnResetIpCharges.TabIndex = 8;
            btnResetIpCharges.Text = "Reset IP Charges";
            btnResetIpCharges.Click += btnResetIpCharges_Click;
            // 
            // lblIP3MarginValue
            // 
            lblIP3MarginValue.Location = new Point(377, 137);
            lblIP3MarginValue.Margin = new Padding(4, 0, 4, 0);
            lblIP3MarginValue.Name = "lblIP3MarginValue";
            lblIP3MarginValue.Size = new Size(86, 30);
            lblIP3MarginValue.TabIndex = 26;
            lblIP3MarginValue.Text = "0.00%";
            // 
            // txtIP3Rate
            // 
            txtIP3Rate.Location = new Point(269, 132);
            txtIP3Rate.Margin = new Padding(4, 5, 4, 5);
            txtIP3Rate.Name = "txtIP3Rate";
            txtIP3Rate.Size = new Size(84, 31);
            txtIP3Rate.TabIndex = 24;
            // 
            // lblIP3Rate
            // 
            lblIP3Rate.Location = new Point(11, 137);
            lblIP3Rate.Margin = new Padding(4, 0, 4, 0);
            lblIP3Rate.Name = "lblIP3Rate";
            lblIP3Rate.Size = new Size(236, 30);
            lblIP3Rate.TabIndex = 23;
            lblIP3Rate.Text = "IP3 – Panoramic Fixed $/kg:";
            // 
            // lblIP2MarginValue
            // 
            lblIP2MarginValue.Location = new Point(377, 95);
            lblIP2MarginValue.Margin = new Padding(4, 0, 4, 0);
            lblIP2MarginValue.Name = "lblIP2MarginValue";
            lblIP2MarginValue.Size = new Size(86, 30);
            lblIP2MarginValue.TabIndex = 22;
            lblIP2MarginValue.Text = "0.00%";
            // 
            // txtIP2Upcharge
            // 
            txtIP2Upcharge.Location = new Point(269, 90);
            txtIP2Upcharge.Margin = new Padding(4, 5, 4, 5);
            txtIP2Upcharge.Name = "txtIP2Upcharge";
            txtIP2Upcharge.Size = new Size(84, 31);
            txtIP2Upcharge.TabIndex = 20;
            // 
            // lblIP2Upcharge
            // 
            lblIP2Upcharge.Location = new Point(11, 95);
            lblIP2Upcharge.Margin = new Padding(4, 0, 4, 0);
            lblIP2Upcharge.Name = "lblIP2Upcharge";
            lblIP2Upcharge.Size = new Size(236, 30);
            lblIP2Upcharge.TabIndex = 19;
            lblIP2Upcharge.Text = "IP2 Upcharge $/kg:";
            // 
            // lblIP1MarginValue
            // 
            lblIP1MarginValue.Location = new Point(377, 53);
            lblIP1MarginValue.Margin = new Padding(4, 0, 4, 0);
            lblIP1MarginValue.Name = "lblIP1MarginValue";
            lblIP1MarginValue.Size = new Size(86, 30);
            lblIP1MarginValue.TabIndex = 18;
            lblIP1MarginValue.Text = "0.00%";
            // 
            // lblIP1Margin
            // 
            lblIP1Margin.Location = new Point(377, 23);
            lblIP1Margin.Margin = new Padding(4, 0, 4, 0);
            lblIP1Margin.Name = "lblIP1Margin";
            lblIP1Margin.Size = new Size(86, 30);
            lblIP1Margin.TabIndex = 17;
            lblIP1Margin.Text = "Margin:";
            // 
            // btnEditIpCategories
            // 
            btnEditIpCategories.Location = new Point(470, 45);
            btnEditIpCategories.Margin = new Padding(4, 5, 4, 5);
            btnEditIpCategories.Name = "btnEditIpCategories";
            btnEditIpCategories.Size = new Size(170, 43);
            btnEditIpCategories.TabIndex = 7;
            btnEditIpCategories.Text = "Edit IP Categories";
            btnEditIpCategories.Visible = false;
            btnEditIpCategories.Click += btnEditIpCategories_Click;
            // 
            // txtIP1Upcharge
            // 
            txtIP1Upcharge.Location = new Point(269, 48);
            txtIP1Upcharge.Margin = new Padding(4, 5, 4, 5);
            txtIP1Upcharge.Name = "txtIP1Upcharge";
            txtIP1Upcharge.Size = new Size(84, 31);
            txtIP1Upcharge.TabIndex = 16;
            // 
            // lblIP1Upcharge
            // 
            lblIP1Upcharge.Location = new Point(11, 53);
            lblIP1Upcharge.Margin = new Padding(4, 0, 4, 0);
            lblIP1Upcharge.Name = "lblIP1Upcharge";
            lblIP1Upcharge.Size = new Size(236, 30);
            lblIP1Upcharge.TabIndex = 15;
            lblIP1Upcharge.Text = "IP1 Upcharge $/kg:";
            // 
            // lblIPTypHdr
            // 
            lblIPTypHdr.Location = new Point(269, 23);
            lblIPTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblIPTypHdr.Name = "lblIPTypHdr";
            lblIPTypHdr.Size = new Size(93, 30);
            lblIPTypHdr.TabIndex = 14;
            lblIPTypHdr.Text = "$/kg";
            // 
            // lblIPColHdr
            // 
            lblIPColHdr.Location = new Point(11, 23);
            lblIPColHdr.Margin = new Padding(4, 0, 4, 0);
            lblIPColHdr.Name = "lblIPColHdr";
            lblIPColHdr.Size = new Size(236, 30);
            lblIPColHdr.TabIndex = 13;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(11, 7);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(274, 47);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Edit Pricing";
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
            grpHeader.Location = new Point(11, 53);
            grpHeader.Margin = new Padding(4, 5, 4, 5);
            grpHeader.Name = "grpHeader";
            grpHeader.Padding = new Padding(4, 5, 4, 5);
            grpHeader.Size = new Size(707, 248);
            grpHeader.TabIndex = 1;
            grpHeader.TabStop = false;
            grpHeader.Text = "Project Information";
            // 
            // lblQuotedBy
            // 
            lblQuotedBy.AutoSize = true;
            lblQuotedBy.Location = new Point(17, 35);
            lblQuotedBy.Margin = new Padding(4, 0, 4, 0);
            lblQuotedBy.Name = "lblQuotedBy";
            lblQuotedBy.Size = new Size(101, 25);
            lblQuotedBy.TabIndex = 0;
            lblQuotedBy.Text = "Quoted By:";
            // 
            // txtQuotedBy
            // 
            txtQuotedBy.Location = new Point(134, 27);
            txtQuotedBy.Margin = new Padding(4, 5, 4, 5);
            txtQuotedBy.Name = "txtQuotedBy";
            txtQuotedBy.ReadOnly = true;
            txtQuotedBy.Size = new Size(255, 31);
            txtQuotedBy.TabIndex = 1;
            // 
            // lblQuoteDate
            // 
            lblQuoteDate.AutoSize = true;
            lblQuoteDate.Location = new Point(406, 32);
            lblQuoteDate.Margin = new Padding(4, 0, 4, 0);
            lblQuoteDate.Name = "lblQuoteDate";
            lblQuoteDate.Size = new Size(108, 25);
            lblQuoteDate.TabIndex = 2;
            lblQuoteDate.Text = "Quote Date:";
            // 
            // dtpQuoteDate
            // 
            dtpQuoteDate.Format = DateTimePickerFormat.Short;
            dtpQuoteDate.Location = new Point(514, 27);
            dtpQuoteDate.Margin = new Padding(4, 5, 4, 5);
            dtpQuoteDate.Name = "dtpQuoteDate";
            dtpQuoteDate.Size = new Size(184, 31);
            dtpQuoteDate.TabIndex = 3;
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Location = new Point(17, 77);
            lblProjectName.Margin = new Padding(4, 0, 4, 0);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(70, 25);
            lblProjectName.TabIndex = 4;
            lblProjectName.Text = "Project:";
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(134, 68);
            txtProjectName.Margin = new Padding(4, 5, 4, 5);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.ReadOnly = true;
            txtProjectName.Size = new Size(255, 31);
            txtProjectName.TabIndex = 5;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(17, 118);
            lblCustomerName.Margin = new Padding(4, 0, 4, 0);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(93, 25);
            lblCustomerName.TabIndex = 6;
            lblCustomerName.Text = "Customer:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(134, 108);
            txtCustomerName.Margin = new Padding(4, 5, 4, 5);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.ReadOnly = true;
            txtCustomerName.Size = new Size(255, 31);
            txtCustomerName.TabIndex = 7;
            // 
            // lblPricingNotes
            // 
            lblPricingNotes.AutoSize = true;
            lblPricingNotes.Location = new Point(17, 158);
            lblPricingNotes.Margin = new Padding(4, 0, 4, 0);
            lblPricingNotes.Name = "lblPricingNotes";
            lblPricingNotes.Size = new Size(121, 25);
            lblPricingNotes.TabIndex = 8;
            lblPricingNotes.Text = "Pricing Notes:";
            // 
            // txtPricingNotes
            // 
            txtPricingNotes.Location = new Point(134, 150);
            txtPricingNotes.Margin = new Padding(4, 5, 4, 5);
            txtPricingNotes.Multiline = true;
            txtPricingNotes.Name = "txtPricingNotes";
            txtPricingNotes.Size = new Size(563, 87);
            txtPricingNotes.TabIndex = 9;
            // 
            // grpFinishSection
            // 
            grpFinishSection.Controls.Add(lblFinType);
            grpFinishSection.Controls.Add(txtFinType);
            grpFinishSection.Controls.Add(lblFinishGroup);
            grpFinishSection.Controls.Add(txtFinishGroup);
            grpFinishSection.Controls.Add(lblFinishName);
            grpFinishSection.Controls.Add(txtFinishName);
            grpFinishSection.Controls.Add(lblApplicatorGroup);
            grpFinishSection.Controls.Add(txtApplicatorGroup);
            grpFinishSection.Controls.Add(lblFinishApplicator);
            grpFinishSection.Controls.Add(txtFinishApplicator);
            grpFinishSection.Location = new Point(740, 58);
            grpFinishSection.Margin = new Padding(4, 5, 4, 5);
            grpFinishSection.Name = "grpFinishSection";
            grpFinishSection.Padding = new Padding(4, 5, 4, 5);
            grpFinishSection.Size = new Size(636, 248);
            grpFinishSection.TabIndex = 2;
            grpFinishSection.TabStop = false;
            grpFinishSection.Text = "Finish Selection";
            // 
            // lblFinType
            // 
            lblFinType.AutoSize = true;
            lblFinType.Location = new Point(40, 32);
            lblFinType.Margin = new Padding(4, 0, 4, 0);
            lblFinType.Name = "lblFinType";
            lblFinType.Size = new Size(103, 25);
            lblFinType.TabIndex = 0;
            lblFinType.Text = "Finish Type:";
            // 
            // txtFinType
            // 
            txtFinType.Location = new Point(244, 27);
            txtFinType.Margin = new Padding(4, 5, 4, 5);
            txtFinType.Name = "txtFinType";
            txtFinType.ReadOnly = true;
            txtFinType.Size = new Size(341, 31);
            txtFinType.TabIndex = 1;
            // 
            // lblFinishGroup
            // 
            lblFinishGroup.AutoSize = true;
            lblFinishGroup.Location = new Point(40, 75);
            lblFinishGroup.Margin = new Padding(4, 0, 4, 0);
            lblFinishGroup.Name = "lblFinishGroup";
            lblFinishGroup.Size = new Size(116, 25);
            lblFinishGroup.TabIndex = 2;
            lblFinishGroup.Text = "Finish Group:";
            // 
            // txtFinishGroup
            // 
            txtFinishGroup.Location = new Point(244, 72);
            txtFinishGroup.Margin = new Padding(4, 5, 4, 5);
            txtFinishGroup.Name = "txtFinishGroup";
            txtFinishGroup.ReadOnly = true;
            txtFinishGroup.Size = new Size(341, 31);
            txtFinishGroup.TabIndex = 3;
            // 
            // lblFinishName
            // 
            lblFinishName.AutoSize = true;
            lblFinishName.Location = new Point(40, 118);
            lblFinishName.Margin = new Padding(4, 0, 4, 0);
            lblFinishName.Name = "lblFinishName";
            lblFinishName.Size = new Size(113, 25);
            lblFinishName.TabIndex = 4;
            lblFinishName.Text = "Finish Name:";
            // 
            // txtFinishName
            // 
            txtFinishName.Location = new Point(244, 115);
            txtFinishName.Margin = new Padding(4, 5, 4, 5);
            txtFinishName.Name = "txtFinishName";
            txtFinishName.ReadOnly = true;
            txtFinishName.Size = new Size(341, 31);
            txtFinishName.TabIndex = 5;
            // 
            // lblApplicatorGroup
            // 
            lblApplicatorGroup.AutoSize = true;
            lblApplicatorGroup.Location = new Point(40, 162);
            lblApplicatorGroup.Margin = new Padding(4, 0, 4, 0);
            lblApplicatorGroup.Name = "lblApplicatorGroup";
            lblApplicatorGroup.Size = new Size(203, 25);
            lblApplicatorGroup.TabIndex = 6;
            lblApplicatorGroup.Text = "Applicator Finish Group:";
            // 
            // txtApplicatorGroup
            // 
            txtApplicatorGroup.Location = new Point(244, 158);
            txtApplicatorGroup.Margin = new Padding(4, 5, 4, 5);
            txtApplicatorGroup.Name = "txtApplicatorGroup";
            txtApplicatorGroup.ReadOnly = true;
            txtApplicatorGroup.Size = new Size(341, 31);
            txtApplicatorGroup.TabIndex = 7;
            // 
            // lblFinishApplicator
            // 
            lblFinishApplicator.AutoSize = true;
            lblFinishApplicator.Location = new Point(40, 208);
            lblFinishApplicator.Margin = new Padding(4, 0, 4, 0);
            lblFinishApplicator.Name = "lblFinishApplicator";
            lblFinishApplicator.Size = new Size(148, 25);
            lblFinishApplicator.TabIndex = 8;
            lblFinishApplicator.Text = "Finish Applicator:";
            // 
            // txtFinishApplicator
            // 
            txtFinishApplicator.Location = new Point(244, 202);
            txtFinishApplicator.Margin = new Padding(4, 5, 4, 5);
            txtFinishApplicator.Name = "txtFinishApplicator";
            txtFinishApplicator.ReadOnly = true;
            txtFinishApplicator.Size = new Size(341, 31);
            txtFinishApplicator.TabIndex = 9;
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
            grpQuantitiesSection.Location = new Point(11, 312);
            grpQuantitiesSection.Margin = new Padding(4, 5, 4, 5);
            grpQuantitiesSection.Name = "grpQuantitiesSection";
            grpQuantitiesSection.Padding = new Padding(4, 5, 4, 5);
            grpQuantitiesSection.Size = new Size(300, 150);
            grpQuantitiesSection.TabIndex = 3;
            grpQuantitiesSection.TabStop = false;
            grpQuantitiesSection.Text = "Quantities";
            // 
            // lblTotalKg
            // 
            lblTotalKg.AutoSize = true;
            lblTotalKg.Location = new Point(14, 27);
            lblTotalKg.Margin = new Padding(4, 0, 4, 0);
            lblTotalKg.Name = "lblTotalKg";
            lblTotalKg.Size = new Size(78, 25);
            lblTotalKg.TabIndex = 0;
            lblTotalKg.Text = "Total kg:";
            // 
            // lblTotalKgValue
            // 
            lblTotalKgValue.Location = new Point(166, 27);
            lblTotalKgValue.Margin = new Padding(4, 0, 4, 0);
            lblTotalKgValue.Name = "lblTotalKgValue";
            lblTotalKgValue.Size = new Size(93, 33);
            lblTotalKgValue.TabIndex = 1;
            lblTotalKgValue.Text = "0.00";
            // 
            // lblTotalFinishingM2
            // 
            lblTotalFinishingM2.AutoSize = true;
            lblTotalFinishingM2.Location = new Point(14, 62);
            lblTotalFinishingM2.Margin = new Padding(4, 0, 4, 0);
            lblTotalFinishingM2.Name = "lblTotalFinishingM2";
            lblTotalFinishingM2.Size = new Size(156, 25);
            lblTotalFinishingM2.TabIndex = 2;
            lblTotalFinishingM2.Text = "Total Finishing m²:";
            // 
            // lblTotalFinishingM2Value
            // 
            lblTotalFinishingM2Value.Location = new Point(166, 62);
            lblTotalFinishingM2Value.Margin = new Padding(4, 0, 4, 0);
            lblTotalFinishingM2Value.Name = "lblTotalFinishingM2Value";
            lblTotalFinishingM2Value.Size = new Size(64, 33);
            lblTotalFinishingM2Value.TabIndex = 3;
            lblTotalFinishingM2Value.Text = "0.00";
            // 
            // lblApNote
            // 
            lblApNote.AutoSize = true;
            lblApNote.Location = new Point(224, 70);
            lblApNote.Margin = new Padding(4, 0, 4, 0);
            lblApNote.Name = "lblApNote";
            lblApNote.Size = new Size(44, 25);
            lblApNote.TabIndex = 4;
            lblApNote.Text = "(AP)";
            // 
            // lblTotalPieces
            // 
            lblTotalPieces.AutoSize = true;
            lblTotalPieces.Location = new Point(14, 95);
            lblTotalPieces.Margin = new Padding(4, 0, 4, 0);
            lblTotalPieces.Name = "lblTotalPieces";
            lblTotalPieces.Size = new Size(106, 25);
            lblTotalPieces.TabIndex = 5;
            lblTotalPieces.Text = "Total Pieces:";
            // 
            // lblTotalPiecesValue
            // 
            lblTotalPiecesValue.Location = new Point(166, 95);
            lblTotalPiecesValue.Margin = new Padding(4, 0, 4, 0);
            lblTotalPiecesValue.Name = "lblTotalPiecesValue";
            lblTotalPiecesValue.Size = new Size(86, 33);
            lblTotalPiecesValue.TabIndex = 6;
            lblTotalPiecesValue.Text = "0";
            // 
            // grpMillFinishPricing
            // 
            grpMillFinishPricing.Controls.Add(label1);
            grpMillFinishPricing.Controls.Add(label2);
            grpMillFinishPricing.Controls.Add(label3);
            grpMillFinishPricing.Controls.Add(lblMetalPriceTyp);
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
            grpMillFinishPricing.Location = new Point(320, 308);
            grpMillFinishPricing.Margin = new Padding(4, 5, 4, 5);
            grpMillFinishPricing.Name = "grpMillFinishPricing";
            grpMillFinishPricing.Padding = new Padding(4, 5, 4, 5);
            grpMillFinishPricing.Size = new Size(1056, 150);
            grpMillFinishPricing.TabIndex = 4;
            grpMillFinishPricing.TabStop = false;
            grpMillFinishPricing.Text = "Mill Finish Pricing";
            // 
            // label1
            // 
            label1.Location = new Point(714, 93);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(79, 30);
            label1.TabIndex = 15;
            label1.Text = "—";
            // 
            // label2
            // 
            label2.Location = new Point(823, 93);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(79, 30);
            label2.TabIndex = 13;
            label2.Text = "—";
            // 
            // label3
            // 
            label3.Location = new Point(937, 93);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(79, 30);
            label3.TabIndex = 14;
            label3.Text = "—";
            // 
            // lblMetalPriceTyp
            // 
            lblMetalPriceTyp.Location = new Point(714, 63);
            lblMetalPriceTyp.Margin = new Padding(4, 0, 4, 0);
            lblMetalPriceTyp.Name = "lblMetalPriceTyp";
            lblMetalPriceTyp.Size = new Size(79, 30);
            lblMetalPriceTyp.TabIndex = 12;
            lblMetalPriceTyp.Text = "—";
            // 
            // lblSapDiscountHeader
            // 
            lblSapDiscountHeader.AutoSize = true;
            lblSapDiscountHeader.Location = new Point(30, 38);
            lblSapDiscountHeader.Margin = new Padding(4, 0, 4, 0);
            lblSapDiscountHeader.Name = "lblSapDiscountHeader";
            lblSapDiscountHeader.Size = new Size(225, 25);
            lblSapDiscountHeader.TabIndex = 0;
            lblSapDiscountHeader.Text = "Customer SAP Discount %:";
            // 
            // txtCustomerSapDiscount
            // 
            txtCustomerSapDiscount.Location = new Point(251, 32);
            txtCustomerSapDiscount.Margin = new Padding(4, 5, 4, 5);
            txtCustomerSapDiscount.Name = "txtCustomerSapDiscount";
            txtCustomerSapDiscount.Size = new Size(113, 31);
            txtCustomerSapDiscount.TabIndex = 1;
            // 
            // lblMFColHdr
            // 
            lblMFColHdr.Location = new Point(580, 32);
            lblMFColHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFColHdr.Name = "lblMFColHdr";
            lblMFColHdr.Size = new Size(73, 32);
            lblMFColHdr.TabIndex = 4;
            lblMFColHdr.Text = "$/kg";
            // 
            // lblMFTypHdr
            // 
            lblMFTypHdr.Location = new Point(710, 32);
            lblMFTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFTypHdr.Name = "lblMFTypHdr";
            lblMFTypHdr.Size = new Size(100, 30);
            lblMFTypHdr.TabIndex = 3;
            lblMFTypHdr.Text = "Typical";
            // 
            // lblMFMinHdr
            // 
            lblMFMinHdr.Location = new Point(823, 32);
            lblMFMinHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFMinHdr.Name = "lblMFMinHdr";
            lblMFMinHdr.Size = new Size(79, 30);
            lblMFMinHdr.TabIndex = 4;
            lblMFMinHdr.Text = "Min";
            // 
            // lblMFMaxHdr
            // 
            lblMFMaxHdr.Location = new Point(937, 32);
            lblMFMaxHdr.Margin = new Padding(4, 0, 4, 0);
            lblMFMaxHdr.Name = "lblMFMaxHdr";
            lblMFMaxHdr.Size = new Size(79, 30);
            lblMFMaxHdr.TabIndex = 5;
            lblMFMaxHdr.Text = "Max";
            // 
            // lblMetalPrice
            // 
            lblMetalPrice.Location = new Point(383, 68);
            lblMetalPrice.Margin = new Padding(4, 0, 4, 0);
            lblMetalPrice.Name = "lblMetalPrice";
            lblMetalPrice.Size = new Size(174, 30);
            lblMetalPrice.TabIndex = 6;
            lblMetalPrice.Text = "Mill Finish Sell Price ";
            // 
            // txtMetalPrice
            // 
            txtMetalPrice.Location = new Point(566, 62);
            txtMetalPrice.Margin = new Padding(4, 5, 4, 5);
            txtMetalPrice.Name = "txtMetalPrice";
            txtMetalPrice.Size = new Size(98, 31);
            txtMetalPrice.TabIndex = 7;
            // 
            // lblMetalPriceMin
            // 
            lblMetalPriceMin.Location = new Point(823, 63);
            lblMetalPriceMin.Margin = new Padding(4, 0, 4, 0);
            lblMetalPriceMin.Name = "lblMetalPriceMin";
            lblMetalPriceMin.Size = new Size(79, 30);
            lblMetalPriceMin.TabIndex = 8;
            lblMetalPriceMin.Text = "—";
            // 
            // lblMetalPriceMax
            // 
            lblMetalPriceMax.Location = new Point(937, 63);
            lblMetalPriceMax.Margin = new Padding(4, 0, 4, 0);
            lblMetalPriceMax.Name = "lblMetalPriceMax";
            lblMetalPriceMax.Size = new Size(79, 30);
            lblMetalPriceMax.TabIndex = 9;
            lblMetalPriceMax.Text = "—";
            // 
            // lblMillFinishMargin
            // 
            lblMillFinishMargin.Location = new Point(383, 103);
            lblMillFinishMargin.Margin = new Padding(4, 0, 4, 0);
            lblMillFinishMargin.Name = "lblMillFinishMargin";
            lblMillFinishMargin.Size = new Size(100, 30);
            lblMillFinishMargin.TabIndex = 10;
            lblMillFinishMargin.Text = "Margin %";
            // 
            // lblMillFinishMarginValue
            // 
            lblMillFinishMarginValue.Location = new Point(566, 103);
            lblMillFinishMarginValue.Margin = new Padding(4, 0, 4, 0);
            lblMillFinishMarginValue.Name = "lblMillFinishMarginValue";
            lblMillFinishMarginValue.Size = new Size(100, 30);
            lblMillFinishMarginValue.TabIndex = 11;
            lblMillFinishMarginValue.Text = "0.00%";
            // 
            // grpThermalBreakPricing
            // 
            grpThermalBreakPricing.Controls.Add(label6);
            grpThermalBreakPricing.Controls.Add(label4);
            grpThermalBreakPricing.Controls.Add(label5);
            grpThermalBreakPricing.Controls.Add(lblTBMinHdr);
            grpThermalBreakPricing.Controls.Add(lblTBMaxHdr);
            grpThermalBreakPricing.Controls.Add(lblTBCalcType);
            grpThermalBreakPricing.Controls.Add(cmbTBCalcType);
            grpThermalBreakPricing.Controls.Add(lblTBTypHdr);
            grpThermalBreakPricing.Controls.Add(lblTBMargin);
            grpThermalBreakPricing.Controls.Add(txtTBMargin);
            grpThermalBreakPricing.Location = new Point(11, 460);
            grpThermalBreakPricing.Margin = new Padding(4, 5, 4, 5);
            grpThermalBreakPricing.Name = "grpThermalBreakPricing";
            grpThermalBreakPricing.Padding = new Padding(4, 5, 4, 5);
            grpThermalBreakPricing.Size = new Size(683, 127);
            grpThermalBreakPricing.TabIndex = 5;
            grpThermalBreakPricing.TabStop = false;
            grpThermalBreakPricing.Text = "Thermal Break Pricing";
            // 
            // label6
            // 
            label6.Location = new Point(440, 67);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(66, 30);
            label6.TabIndex = 12;
            label6.Text = "—";
            // 
            // label4
            // 
            label4.Location = new Point(514, 67);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(79, 30);
            label4.TabIndex = 10;
            label4.Text = "—";
            // 
            // label5
            // 
            label5.Location = new Point(587, 67);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(69, 30);
            label5.TabIndex = 11;
            label5.Text = "—";
            // 
            // lblTBMinHdr
            // 
            lblTBMinHdr.Location = new Point(514, 32);
            lblTBMinHdr.Margin = new Padding(4, 0, 4, 0);
            lblTBMinHdr.Name = "lblTBMinHdr";
            lblTBMinHdr.Size = new Size(79, 30);
            lblTBMinHdr.TabIndex = 6;
            lblTBMinHdr.Text = "Min";
            // 
            // lblTBMaxHdr
            // 
            lblTBMaxHdr.Location = new Point(587, 32);
            lblTBMaxHdr.Margin = new Padding(4, 0, 4, 0);
            lblTBMaxHdr.Name = "lblTBMaxHdr";
            lblTBMaxHdr.Size = new Size(50, 30);
            lblTBMaxHdr.TabIndex = 7;
            lblTBMaxHdr.Text = "Max";
            // 
            // lblTBCalcType
            // 
            lblTBCalcType.Location = new Point(17, 27);
            lblTBCalcType.Margin = new Padding(4, 0, 4, 0);
            lblTBCalcType.Name = "lblTBCalcType";
            lblTBCalcType.Size = new Size(194, 30);
            lblTBCalcType.TabIndex = 0;
            lblTBCalcType.Text = "Thermal Break Method:";
            // 
            // cmbTBCalcType
            // 
            cmbTBCalcType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTBCalcType.Location = new Point(220, 23);
            cmbTBCalcType.Margin = new Padding(4, 5, 4, 5);
            cmbTBCalcType.Name = "cmbTBCalcType";
            cmbTBCalcType.Size = new Size(194, 33);
            cmbTBCalcType.TabIndex = 1;
            // 
            // lblTBTypHdr
            // 
            lblTBTypHdr.Location = new Point(436, 32);
            lblTBTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblTBTypHdr.Name = "lblTBTypHdr";
            lblTBTypHdr.Size = new Size(70, 30);
            lblTBTypHdr.TabIndex = 2;
            lblTBTypHdr.Text = "Typical";
            // 
            // lblTBMargin
            // 
            lblTBMargin.Location = new Point(50, 72);
            lblTBMargin.Margin = new Padding(4, 0, 4, 0);
            lblTBMargin.Name = "lblTBMargin";
            lblTBMargin.Size = new Size(140, 30);
            lblTBMargin.TabIndex = 3;
            lblTBMargin.Text = "Enter Margin %:";
            // 
            // txtTBMargin
            // 
            txtTBMargin.Location = new Point(220, 67);
            txtTBMargin.Margin = new Padding(4, 5, 4, 5);
            txtTBMargin.Name = "txtTBMargin";
            txtTBMargin.Size = new Size(98, 31);
            txtTBMargin.TabIndex = 4;
            // 
            // grpFinishingPricing
            // 
            grpFinishingPricing.Controls.Add(label19);
            grpFinishingPricing.Controls.Add(label20);
            grpFinishingPricing.Controls.Add(label21);
            grpFinishingPricing.Controls.Add(label13);
            grpFinishingPricing.Controls.Add(label14);
            grpFinishingPricing.Controls.Add(btnBulkNonBulkRates);
            grpFinishingPricing.Controls.Add(label15);
            grpFinishingPricing.Controls.Add(lblPowderPricedAs);
            grpFinishingPricing.Controls.Add(cmbPowderPricedAs);
            grpFinishingPricing.Controls.Add(lblCostType);
            grpFinishingPricing.Controls.Add(cmbCostType);
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
            grpFinishingPricing.Controls.Add(lblFinishingMargin);
            grpFinishingPricing.Controls.Add(lblFinishingMarginValue);
            grpFinishingPricing.Location = new Point(11, 592);
            grpFinishingPricing.Margin = new Padding(4, 5, 4, 5);
            grpFinishingPricing.Name = "grpFinishingPricing";
            grpFinishingPricing.Padding = new Padding(4, 5, 4, 5);
            grpFinishingPricing.Size = new Size(683, 313);
            grpFinishingPricing.TabIndex = 6;
            grpFinishingPricing.TabStop = false;
            grpFinishingPricing.Text = "Powder / Finishing Pricing";
            // 
            // label19
            // 
            label19.Location = new Point(364, 270);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(79, 30);
            label19.TabIndex = 48;
            label19.Text = "—";
            // 
            // label20
            // 
            label20.Location = new Point(483, 270);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(79, 30);
            label20.TabIndex = 46;
            label20.Text = "—";
            // 
            // label21
            // 
            label21.Location = new Point(586, 270);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(79, 30);
            label21.TabIndex = 47;
            label21.Text = "—";
            // 
            // label13
            // 
            label13.Location = new Point(364, 238);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(79, 30);
            label13.TabIndex = 42;
            label13.Text = "—";
            // 
            // label14
            // 
            label14.Location = new Point(483, 238);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(79, 30);
            label14.TabIndex = 40;
            label14.Text = "—";
            // 
            // btnBulkNonBulkRates
            // 
            btnBulkNonBulkRates.Location = new Point(469, 30);
            btnBulkNonBulkRates.Margin = new Padding(4, 5, 4, 5);
            btnBulkNonBulkRates.Name = "btnBulkNonBulkRates";
            btnBulkNonBulkRates.Size = new Size(206, 43);
            btnBulkNonBulkRates.TabIndex = 8;
            btnBulkNonBulkRates.Text = "Bulk/Non-bulk Rates";
            btnBulkNonBulkRates.Click += btnBulkNonBulkRates_Click;
            // 
            // label15
            // 
            label15.Location = new Point(586, 238);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(79, 30);
            label15.TabIndex = 41;
            label15.Text = "—";
            // 
            // lblPowderPricedAs
            // 
            lblPowderPricedAs.AutoSize = true;
            lblPowderPricedAs.Location = new Point(7, 82);
            lblPowderPricedAs.Margin = new Padding(4, 0, 4, 0);
            lblPowderPricedAs.Name = "lblPowderPricedAs";
            lblPowderPricedAs.Size = new Size(150, 25);
            lblPowderPricedAs.TabIndex = 0;
            lblPowderPricedAs.Text = "Powder Priced as:";
            // 
            // cmbPowderPricedAs
            // 
            cmbPowderPricedAs.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPowderPricedAs.Location = new Point(164, 75);
            cmbPowderPricedAs.Margin = new Padding(4, 5, 4, 5);
            cmbPowderPricedAs.Name = "cmbPowderPricedAs";
            cmbPowderPricedAs.Size = new Size(284, 33);
            cmbPowderPricedAs.TabIndex = 1;
            // 
            // lblCostType
            // 
            lblCostType.AutoSize = true;
            lblCostType.Location = new Point(6, 37);
            lblCostType.Margin = new Padding(4, 0, 4, 0);
            lblCostType.Name = "lblCostType";
            lblCostType.Size = new Size(94, 25);
            lblCostType.TabIndex = 2;
            lblCostType.Text = "Cost Type:";
            // 
            // cmbCostType
            // 
            cmbCostType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCostType.Location = new Point(141, 30);
            cmbCostType.Margin = new Padding(4, 5, 4, 5);
            cmbCostType.Name = "cmbCostType";
            cmbCostType.Size = new Size(313, 33);
            cmbCostType.TabIndex = 3;
            // 
            // lblFCColHdr
            // 
            lblFCColHdr.Location = new Point(254, 120);
            lblFCColHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCColHdr.Name = "lblFCColHdr";
            lblFCColHdr.Size = new Size(94, 30);
            lblFCColHdr.TabIndex = 7;
            lblFCColHdr.Text = "$/m²";
            // 
            // lblFCTypHdr
            // 
            lblFCTypHdr.Location = new Point(370, 120);
            lblFCTypHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCTypHdr.Name = "lblFCTypHdr";
            lblFCTypHdr.Size = new Size(100, 30);
            lblFCTypHdr.TabIndex = 8;
            lblFCTypHdr.Text = "Typical";
            // 
            // lblFCMinHdr
            // 
            lblFCMinHdr.Location = new Point(483, 120);
            lblFCMinHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCMinHdr.Name = "lblFCMinHdr";
            lblFCMinHdr.Size = new Size(79, 30);
            lblFCMinHdr.TabIndex = 9;
            lblFCMinHdr.Text = "Min";
            // 
            // lblFCMaxHdr
            // 
            lblFCMaxHdr.Location = new Point(586, 120);
            lblFCMaxHdr.Margin = new Padding(4, 0, 4, 0);
            lblFCMaxHdr.Name = "lblFCMaxHdr";
            lblFCMaxHdr.Size = new Size(79, 30);
            lblFCMaxHdr.TabIndex = 10;
            lblFCMaxHdr.Text = "Max";
            // 
            // lblFinishingCostPricePerM2
            // 
            lblFinishingCostPricePerM2.Location = new Point(4, 195);
            lblFinishingCostPricePerM2.Margin = new Padding(4, 0, 4, 0);
            lblFinishingCostPricePerM2.Name = "lblFinishingCostPricePerM2";
            lblFinishingCostPricePerM2.Size = new Size(241, 30);
            lblFinishingCostPricePerM2.TabIndex = 11;
            lblFinishingCostPricePerM2.Text = "Finishing Cost Price $/m²";
            // 
            // txtFinishingCostPricePerM2
            // 
            txtFinishingCostPricePerM2.Location = new Point(254, 195);
            txtFinishingCostPricePerM2.Margin = new Padding(4, 5, 4, 5);
            txtFinishingCostPricePerM2.Name = "txtFinishingCostPricePerM2";
            txtFinishingCostPricePerM2.ReadOnly = true;
            txtFinishingCostPricePerM2.Size = new Size(98, 31);
            txtFinishingCostPricePerM2.TabIndex = 12;
            // 
            // lblB42
            // 
            lblB42.Location = new Point(7, 153);
            lblB42.Margin = new Padding(4, 0, 4, 0);
            lblB42.Name = "lblB42";
            lblB42.Size = new Size(241, 30);
            lblB42.TabIndex = 13;
            lblB42.Text = "POA/Other Vendor Cost $/m²";
            // 
            // txtB42
            // 
            txtB42.Location = new Point(254, 155);
            txtB42.Margin = new Padding(4, 5, 4, 5);
            txtB42.Name = "txtB42";
            txtB42.Size = new Size(98, 31);
            txtB42.TabIndex = 14;
            // 
            // lblFinishingPrice
            // 
            lblFinishingPrice.Location = new Point(4, 233);
            lblFinishingPrice.Margin = new Padding(4, 0, 4, 0);
            lblFinishingPrice.Name = "lblFinishingPrice";
            lblFinishingPrice.Size = new Size(241, 30);
            lblFinishingPrice.TabIndex = 15;
            lblFinishingPrice.Text = "Finishing Sell Price $/m²";
            // 
            // txtFinishingPrice
            // 
            txtFinishingPrice.Location = new Point(254, 233);
            txtFinishingPrice.Margin = new Padding(4, 5, 4, 5);
            txtFinishingPrice.Name = "txtFinishingPrice";
            txtFinishingPrice.Size = new Size(98, 31);
            txtFinishingPrice.TabIndex = 16;
            // 
            // lblFinishingMargin
            // 
            lblFinishingMargin.Location = new Point(4, 272);
            lblFinishingMargin.Margin = new Padding(4, 0, 4, 0);
            lblFinishingMargin.Name = "lblFinishingMargin";
            lblFinishingMargin.Size = new Size(241, 30);
            lblFinishingMargin.TabIndex = 19;
            lblFinishingMargin.Text = "Margin %";
            // 
            // lblFinishingMarginValue
            // 
            lblFinishingMarginValue.Location = new Point(254, 277);
            lblFinishingMarginValue.Margin = new Padding(4, 0, 4, 0);
            lblFinishingMarginValue.Name = "lblFinishingMarginValue";
            lblFinishingMarginValue.Size = new Size(100, 30);
            lblFinishingMarginValue.TabIndex = 20;
            lblFinishingMarginValue.Text = "0.00%";
            // 
            // grpAnodisedThresholds
            // 
            grpAnodisedThresholds.Controls.Add(lblAnodisedThresholdCost);
            grpAnodisedThresholds.Controls.Add(txtAnodisedThresholdCost);
            grpAnodisedThresholds.Controls.Add(lblB58);
            grpAnodisedThresholds.Controls.Add(txtB58);
            grpAnodisedThresholds.Controls.Add(lblThresholdMargin);
            grpAnodisedThresholds.Controls.Add(lblThresholdMarginValue);
            grpAnodisedThresholds.Location = new Point(11, 912);
            grpAnodisedThresholds.Margin = new Padding(4, 5, 4, 5);
            grpAnodisedThresholds.Name = "grpAnodisedThresholds";
            grpAnodisedThresholds.Padding = new Padding(4, 5, 4, 5);
            grpAnodisedThresholds.Size = new Size(683, 142);
            grpAnodisedThresholds.TabIndex = 7;
            grpAnodisedThresholds.TabStop = false;
            grpAnodisedThresholds.Text = "Anodised Thresholds";
            // 
            // lblAnodisedThresholdCost
            // 
            lblAnodisedThresholdCost.Location = new Point(17, 28);
            lblAnodisedThresholdCost.Margin = new Padding(4, 0, 4, 0);
            lblAnodisedThresholdCost.Name = "lblAnodisedThresholdCost";
            lblAnodisedThresholdCost.Size = new Size(261, 30);
            lblAnodisedThresholdCost.TabIndex = 0;
            lblAnodisedThresholdCost.Text = "Finishing Cost Price $/m² (auto):";
            // 
            // txtAnodisedThresholdCost
            // 
            txtAnodisedThresholdCost.Location = new Point(284, 23);
            txtAnodisedThresholdCost.Margin = new Padding(4, 5, 4, 5);
            txtAnodisedThresholdCost.Name = "txtAnodisedThresholdCost";
            txtAnodisedThresholdCost.ReadOnly = true;
            txtAnodisedThresholdCost.Size = new Size(113, 31);
            txtAnodisedThresholdCost.TabIndex = 1;
            // 
            // lblB58
            // 
            lblB58.Location = new Point(17, 72);
            lblB58.Margin = new Padding(4, 0, 4, 0);
            lblB58.Name = "lblB58";
            lblB58.Size = new Size(261, 30);
            lblB58.TabIndex = 4;
            lblB58.Text = "Finishing Sell Price $/m²:";
            // 
            // txtB58
            // 
            txtB58.Location = new Point(284, 65);
            txtB58.Margin = new Padding(4, 5, 4, 5);
            txtB58.Name = "txtB58";
            txtB58.Size = new Size(113, 31);
            txtB58.TabIndex = 5;
            // 
            // lblThresholdMargin
            // 
            lblThresholdMargin.Location = new Point(139, 108);
            lblThresholdMargin.Margin = new Padding(4, 0, 4, 0);
            lblThresholdMargin.Name = "lblThresholdMargin";
            lblThresholdMargin.Size = new Size(110, 30);
            lblThresholdMargin.TabIndex = 6;
            lblThresholdMargin.Text = "Margin %:";
            // 
            // lblThresholdMarginValue
            // 
            lblThresholdMarginValue.Location = new Point(284, 108);
            lblThresholdMarginValue.Margin = new Padding(4, 0, 4, 0);
            lblThresholdMarginValue.Name = "lblThresholdMarginValue";
            lblThresholdMarginValue.Size = new Size(90, 30);
            lblThresholdMarginValue.TabIndex = 7;
            lblThresholdMarginValue.Text = "0.00%";
            // 
            // grpSummary
            // 
            grpSummary.Controls.Add(gridSummary);
            grpSummary.Controls.Add(lblSummaryNote1);
            grpSummary.Controls.Add(lblSummaryNote2);
            grpSummary.Location = new Point(11, 1063);
            grpSummary.Margin = new Padding(4, 5, 4, 5);
            grpSummary.Name = "grpSummary";
            grpSummary.Padding = new Padding(4, 5, 4, 5);
            grpSummary.Size = new Size(1364, 357);
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
            gridSummary.Size = new Size(1326, 285);
            gridSummary.TabIndex = 0;
            // 
            // lblSummaryNote1
            // 
            lblSummaryNote1.AutoSize = true;
            lblSummaryNote1.Location = new Point(17, 330);
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
            // btnRecalculateAll
            // 
            btnRecalculateAll.Location = new Point(616, 7);
            btnRecalculateAll.Margin = new Padding(4, 5, 4, 5);
            btnRecalculateAll.Name = "btnRecalculateAll";
            btnRecalculateAll.Size = new Size(171, 47);
            btnRecalculateAll.TabIndex = 10;
            btnRecalculateAll.Text = "Recalculate All";
            btnRecalculateAll.Click += btnRecalculateAll_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.Highlight;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(801, 7);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(157, 50);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnRefreshSummary
            // 
            btnRefreshSummary.Location = new Point(393, 5);
            btnRefreshSummary.Margin = new Padding(4, 5, 4, 5);
            btnRefreshSummary.Name = "btnRefreshSummary";
            btnRefreshSummary.Size = new Size(214, 48);
            btnRefreshSummary.TabIndex = 12;
            btnRefreshSummary.Text = "Refresh Summary";
            btnRefreshSummary.Click += btnRefreshSummary_Click;
            // 
            // EditPricingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1500, 1062);
            Controls.Add(pnlScroll);
            Margin = new Padding(4, 5, 4, 5);
            Name = "EditPricingForm";
            Text = "Edit Pricing";
            pnlScroll.ResumeLayout(false);
            grpBulkCategories.ResumeLayout(false);
            grpBulkCategories.PerformLayout();
            grpIpCategories.ResumeLayout(false);
            grpIpCategories.PerformLayout();
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
        private System.Windows.Forms.TextBox txtFinType;
        private System.Windows.Forms.Label lblFinishGroup;
        private System.Windows.Forms.TextBox txtFinishGroup;
        private System.Windows.Forms.Label lblFinishName;
        private System.Windows.Forms.TextBox txtFinishName;
        private System.Windows.Forms.Label lblApplicatorGroup;
        private System.Windows.Forms.TextBox txtApplicatorGroup;
        private System.Windows.Forms.Label lblFinishApplicator;
        private System.Windows.Forms.TextBox txtFinishApplicator;

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
        private System.Windows.Forms.Label lblFinishingMargin;
        private System.Windows.Forms.Label lblFinishingMarginValue;
        private System.Windows.Forms.Label lblB48;
        private System.Windows.Forms.TextBox txtB48;
        private System.Windows.Forms.Label lblB49;
        private System.Windows.Forms.TextBox txtB49;
        private System.Windows.Forms.Label lblB50;
        private System.Windows.Forms.TextBox txtB50;
        private System.Windows.Forms.Label lblB52;
        private System.Windows.Forms.TextBox txtB52;
        private System.Windows.Forms.Label lblB53;
        private System.Windows.Forms.TextBox txtB53;
        private System.Windows.Forms.Label lblB54;
        private System.Windows.Forms.TextBox txtB54;

        // Anodised Thresholds
        private System.Windows.Forms.GroupBox grpAnodisedThresholds;
        private System.Windows.Forms.Label lblAnodisedThresholdCost;
        private System.Windows.Forms.TextBox txtAnodisedThresholdCost;
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
        private System.Windows.Forms.Button btnRecalculateAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefreshSummary;
        private GroupBox grpIpCategories;
        private Label lblTBMinHdr;
        private Label lblTBMaxHdr;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblMetalPriceTyp;
        private Label label6;
        private Label label4;
        private Label label5;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label13;
        private Label label14;
        private Label label15;
        private GroupBox grpBulkCategories;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label label25;
        private Label label24;
        private Label label23;
        private Label label22;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label16;
        private Label label17;
        private Label label18;
    }
}
