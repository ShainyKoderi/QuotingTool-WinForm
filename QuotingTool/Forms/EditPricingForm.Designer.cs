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
            // ── Instantiate all controls ──────────────────────────────────────
            pnlScroll                  = new System.Windows.Forms.Panel();
            lblTitle                   = new System.Windows.Forms.Label();
            grpHeader                  = new System.Windows.Forms.GroupBox();
            lblQuotedBy                = new System.Windows.Forms.Label();
            txtQuotedBy                = new System.Windows.Forms.TextBox();
            lblQuoteDate               = new System.Windows.Forms.Label();
            dtpQuoteDate               = new System.Windows.Forms.DateTimePicker();
            lblProjectName             = new System.Windows.Forms.Label();
            txtProjectName             = new System.Windows.Forms.TextBox();
            lblCustomerName            = new System.Windows.Forms.Label();
            txtCustomerName            = new System.Windows.Forms.TextBox();
            lblPricingNotes            = new System.Windows.Forms.Label();
            txtPricingNotes            = new System.Windows.Forms.TextBox();
            grpFinishSection           = new System.Windows.Forms.GroupBox();
            lblFinType                 = new System.Windows.Forms.Label();
            cmbFinType                 = new System.Windows.Forms.ComboBox();
            lblFinishGroup             = new System.Windows.Forms.Label();
            cmbFinishGroup             = new System.Windows.Forms.ComboBox();
            lblFinishName              = new System.Windows.Forms.Label();
            cmbFinishName              = new System.Windows.Forms.ComboBox();
            lblApplicatorGroup         = new System.Windows.Forms.Label();
            cmbApplicatorGroup         = new System.Windows.Forms.ComboBox();
            lblFinishApplicator        = new System.Windows.Forms.Label();
            cmbFinishApplicator        = new System.Windows.Forms.ComboBox();
            grpQuantitiesSection       = new System.Windows.Forms.GroupBox();
            lblTotalKg                 = new System.Windows.Forms.Label();
            lblTotalKgValue            = new System.Windows.Forms.Label();
            lblTotalFinishingM2        = new System.Windows.Forms.Label();
            lblTotalFinishingM2Value   = new System.Windows.Forms.Label();
            lblApNote                  = new System.Windows.Forms.Label();
            lblTotalPieces             = new System.Windows.Forms.Label();
            lblTotalPiecesValue        = new System.Windows.Forms.Label();
            btnEditIpCategories        = new System.Windows.Forms.Button();
            btnResetIpCharges          = new System.Windows.Forms.Button();
            grpMillFinishPricing       = new System.Windows.Forms.GroupBox();
            lblSapDiscountHeader       = new System.Windows.Forms.Label();
            txtCustomerSapDiscount     = new System.Windows.Forms.TextBox();
            lblMFColHdr                = new System.Windows.Forms.Label();
            lblMFTypHdr                = new System.Windows.Forms.Label();
            lblMFMinHdr                = new System.Windows.Forms.Label();
            lblMFMaxHdr                = new System.Windows.Forms.Label();
            lblMetalPrice              = new System.Windows.Forms.Label();
            txtMetalPrice              = new System.Windows.Forms.TextBox();
            lblMetalPriceMin           = new System.Windows.Forms.Label();
            lblMetalPriceMax           = new System.Windows.Forms.Label();
            lblMillFinishMargin        = new System.Windows.Forms.Label();
            lblMillFinishMarginValue   = new System.Windows.Forms.Label();
            lblIpHdr                   = new System.Windows.Forms.Label();
            lblIPColHdr                = new System.Windows.Forms.Label();
            lblIPTypHdr                = new System.Windows.Forms.Label();
            lblIP1Upcharge             = new System.Windows.Forms.Label();
            txtIP1Upcharge             = new System.Windows.Forms.TextBox();
            lblIP1Margin               = new System.Windows.Forms.Label();
            lblIP1MarginValue          = new System.Windows.Forms.Label();
            lblIP2Upcharge             = new System.Windows.Forms.Label();
            txtIP2Upcharge             = new System.Windows.Forms.TextBox();
            lblIP2Margin               = new System.Windows.Forms.Label();
            lblIP2MarginValue          = new System.Windows.Forms.Label();
            lblIP3Rate                 = new System.Windows.Forms.Label();
            txtIP3Rate                 = new System.Windows.Forms.TextBox();
            lblIP3Margin               = new System.Windows.Forms.Label();
            lblIP3MarginValue          = new System.Windows.Forms.Label();
            grpThermalBreakPricing     = new System.Windows.Forms.GroupBox();
            lblTBCalcType              = new System.Windows.Forms.Label();
            cmbTBCalcType              = new System.Windows.Forms.ComboBox();
            lblTBTypHdr                = new System.Windows.Forms.Label();
            lblTBMargin                = new System.Windows.Forms.Label();
            txtTBMargin                = new System.Windows.Forms.TextBox();
            grpFinishingPricing        = new System.Windows.Forms.GroupBox();
            lblPowderPricedAs          = new System.Windows.Forms.Label();
            cmbPowderPricedAs          = new System.Windows.Forms.ComboBox();
            lblCostType                = new System.Windows.Forms.Label();
            cmbCostType                = new System.Windows.Forms.ComboBox();
            chkCostTypeOverride        = new System.Windows.Forms.CheckBox();
            btnToggleOverride          = new System.Windows.Forms.Button();
            lblStdFinishRatesHdr       = new System.Windows.Forms.Label();
            lblFCColHdr                = new System.Windows.Forms.Label();
            lblFCTypHdr                = new System.Windows.Forms.Label();
            lblFCMinHdr                = new System.Windows.Forms.Label();
            lblFCMaxHdr                = new System.Windows.Forms.Label();
            lblFinishingCostPricePerM2 = new System.Windows.Forms.Label();
            txtFinishingCostPricePerM2 = new System.Windows.Forms.TextBox();
            lblB42                     = new System.Windows.Forms.Label();
            txtB42                     = new System.Windows.Forms.TextBox();
            lblFinishingPrice          = new System.Windows.Forms.Label();
            txtFinishingPrice          = new System.Windows.Forms.TextBox();
            lblB44                     = new System.Windows.Forms.Label();
            txtB44                     = new System.Windows.Forms.TextBox();
            lblFinishingMargin         = new System.Windows.Forms.Label();
            lblFinishingMarginValue    = new System.Windows.Forms.Label();
            lblBulkHdr                 = new System.Windows.Forms.Label();
            lblB48                     = new System.Windows.Forms.Label();
            txtB48                     = new System.Windows.Forms.TextBox();
            lblB50                     = new System.Windows.Forms.Label();
            txtB50                     = new System.Windows.Forms.TextBox();
            lblB52                     = new System.Windows.Forms.Label();
            txtB52                     = new System.Windows.Forms.TextBox();
            lblB54                     = new System.Windows.Forms.Label();
            txtB54                     = new System.Windows.Forms.TextBox();
            grpAnodisedThresholds      = new System.Windows.Forms.GroupBox();
            lblAnodisedThresholdCost   = new System.Windows.Forms.Label();
            txtAnodisedThresholdCost   = new System.Windows.Forms.TextBox();
            lblB57                     = new System.Windows.Forms.Label();
            txtB57                     = new System.Windows.Forms.TextBox();
            lblB58                     = new System.Windows.Forms.Label();
            txtB58                     = new System.Windows.Forms.TextBox();
            lblThresholdMargin         = new System.Windows.Forms.Label();
            lblThresholdMarginValue    = new System.Windows.Forms.Label();
            btnBulkNonBulkRates        = new System.Windows.Forms.Button();
            grpSummary                 = new System.Windows.Forms.GroupBox();
            gridSummary                = new System.Windows.Forms.DataGridView();
            colCategory                = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotal                   = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotalValue              = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMarginDollar            = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMarginPct               = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblSummaryNote1            = new System.Windows.Forms.Label();
            lblSummaryNote2            = new System.Windows.Forms.Label();
            btnEditSundryItems         = new System.Windows.Forms.Button();
            btnRecalculateAll          = new System.Windows.Forms.Button();
            btnSave                    = new System.Windows.Forms.Button();
            btnUpdateVisibility        = new System.Windows.Forms.Button();
            btnRefreshSummary          = new System.Windows.Forms.Button();
            grpRowVisibility           = new System.Windows.Forms.GroupBox();
            lblVisibleRows             = new System.Windows.Forms.Label();
            lblHiddenRows              = new System.Windows.Forms.Label();

            // ── SuspendLayout ─────────────────────────────────────────────────
            ((System.ComponentModel.ISupportInitialize)gridSummary).BeginInit();
            grpHeader.SuspendLayout();
            grpFinishSection.SuspendLayout();
            grpQuantitiesSection.SuspendLayout();
            grpMillFinishPricing.SuspendLayout();
            grpThermalBreakPricing.SuspendLayout();
            grpFinishingPricing.SuspendLayout();
            grpAnodisedThresholds.SuspendLayout();
            grpSummary.SuspendLayout();
            grpRowVisibility.SuspendLayout();
            pnlScroll.SuspendLayout();
            SuspendLayout();

            // ── lblTitle ─────────────────────────────────────────────────────
            lblTitle.Location = new System.Drawing.Point(18, 12);
            lblTitle.Size     = new System.Drawing.Size(400, 28);
            lblTitle.Text     = "3. Edit Pricing";

            // ── grpHeader (y=46, h=180) ──────────────────────────────────────
            grpHeader.Location = new System.Drawing.Point(18, 46);
            grpHeader.Size     = new System.Drawing.Size(1010, 180);
            grpHeader.Text     = "Project Information";
            lblQuotedBy.AutoSize = true;  lblQuotedBy.Location  = new System.Drawing.Point(12, 26);   lblQuotedBy.Text  = "Quoted By:";
            txtQuotedBy.Location = new System.Drawing.Point(150, 24); txtQuotedBy.Size = new System.Drawing.Size(180, 23);
            lblQuoteDate.AutoSize = true; lblQuoteDate.Location = new System.Drawing.Point(360, 26);  lblQuoteDate.Text = "Quote Date:";
            dtpQuoteDate.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpQuoteDate.Location = new System.Drawing.Point(460, 24); dtpQuoteDate.Size = new System.Drawing.Size(130, 23);
            lblProjectName.AutoSize = true;  lblProjectName.Location  = new System.Drawing.Point(12, 54);   lblProjectName.Text  = "Project:";
            txtProjectName.Location = new System.Drawing.Point(150, 52); txtProjectName.Size = new System.Drawing.Size(450, 23);
            lblCustomerName.AutoSize = true; lblCustomerName.Location = new System.Drawing.Point(12, 82);   lblCustomerName.Text = "Customer:";
            txtCustomerName.Location = new System.Drawing.Point(150, 80); txtCustomerName.Size = new System.Drawing.Size(450, 23);
            lblPricingNotes.AutoSize = true; lblPricingNotes.Location = new System.Drawing.Point(12, 110);  lblPricingNotes.Text = "Pricing Notes:";
            txtPricingNotes.Multiline = true; txtPricingNotes.Location = new System.Drawing.Point(150, 108); txtPricingNotes.Size = new System.Drawing.Size(820, 54);
            grpHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblQuotedBy, txtQuotedBy, lblQuoteDate, dtpQuoteDate,
                lblProjectName, txtProjectName, lblCustomerName, txtCustomerName,
                lblPricingNotes, txtPricingNotes });

            // ── grpFinishSection (y=232, h=175) ─────────────────────────────
            grpFinishSection.Location = new System.Drawing.Point(18, 232);
            grpFinishSection.Size     = new System.Drawing.Size(1010, 175);
            grpFinishSection.Text     = "Finish Selection";
            lblFinType.AutoSize = true;       lblFinType.Location       = new System.Drawing.Point(12, 26);  lblFinType.Text       = "Finish Type:";
            cmbFinType.DropDownStyle          = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFinType.Location               = new System.Drawing.Point(200, 24); cmbFinType.Size = new System.Drawing.Size(240, 23);
            lblFinishGroup.AutoSize = true;   lblFinishGroup.Location   = new System.Drawing.Point(12, 54);  lblFinishGroup.Text   = "Finish Group:";
            cmbFinishGroup.DropDownStyle      = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFinishGroup.Location           = new System.Drawing.Point(200, 52); cmbFinishGroup.Size = new System.Drawing.Size(240, 23);
            lblFinishName.AutoSize = true;    lblFinishName.Location    = new System.Drawing.Point(12, 82);  lblFinishName.Text    = "Finish Name:";
            cmbFinishName.DropDownStyle       = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbFinishName.Location            = new System.Drawing.Point(200, 80); cmbFinishName.Size = new System.Drawing.Size(760, 23);
            lblApplicatorGroup.AutoSize = true; lblApplicatorGroup.Location = new System.Drawing.Point(12, 110); lblApplicatorGroup.Text = "Applicator Finish Group:";
            cmbApplicatorGroup.DropDownStyle  = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbApplicatorGroup.Location       = new System.Drawing.Point(200, 108); cmbApplicatorGroup.Size = new System.Drawing.Size(240, 23);
            lblFinishApplicator.AutoSize = true; lblFinishApplicator.Location = new System.Drawing.Point(12, 138); lblFinishApplicator.Text = "Finish Applicator:";
            cmbFinishApplicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFinishApplicator.Location      = new System.Drawing.Point(200, 136); cmbFinishApplicator.Size = new System.Drawing.Size(400, 23);
            grpFinishSection.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblFinType, cmbFinType, lblFinishGroup, cmbFinishGroup,
                lblFinishName, cmbFinishName, lblApplicatorGroup, cmbApplicatorGroup,
                lblFinishApplicator, cmbFinishApplicator });

            // ── grpQuantitiesSection (y=413, h=80) ──────────────────────────
            grpQuantitiesSection.Location = new System.Drawing.Point(18, 413);
            grpQuantitiesSection.Size     = new System.Drawing.Size(1010, 80);
            grpQuantitiesSection.Text     = "Quantities";
            lblTotalKg.AutoSize = true;        lblTotalKg.Location         = new System.Drawing.Point(12, 28);  lblTotalKg.Text         = "Total kg:";
            lblTotalKgValue.Location            = new System.Drawing.Point(92, 28);  lblTotalKgValue.Size         = new System.Drawing.Size(80, 20);  lblTotalKgValue.Text         = "0.00";
            lblTotalFinishingM2.AutoSize = true; lblTotalFinishingM2.Location = new System.Drawing.Point(192, 28); lblTotalFinishingM2.Text = "Total Finishing m\u00B2:";
            lblTotalFinishingM2Value.Location   = new System.Drawing.Point(322, 28); lblTotalFinishingM2Value.Size = new System.Drawing.Size(80, 20);  lblTotalFinishingM2Value.Text = "0.00";
            lblApNote.AutoSize = true;          lblApNote.Location          = new System.Drawing.Point(412, 31); lblApNote.Text          = "(AP)";
            lblTotalPieces.AutoSize = true;     lblTotalPieces.Location     = new System.Drawing.Point(452, 28); lblTotalPieces.Text     = "Total Pieces:";
            lblTotalPiecesValue.Location        = new System.Drawing.Point(552, 28); lblTotalPiecesValue.Size     = new System.Drawing.Size(60, 20);  lblTotalPiecesValue.Text     = "0";
            btnEditIpCategories.Location        = new System.Drawing.Point(690, 26); btnEditIpCategories.Size     = new System.Drawing.Size(145, 26); btnEditIpCategories.Text     = "Edit IP Categories";
            btnResetIpCharges.Location          = new System.Drawing.Point(845, 26); btnResetIpCharges.Size       = new System.Drawing.Size(140, 26); btnResetIpCharges.Text       = "Reset IP Charges";
            grpQuantitiesSection.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTotalKg, lblTotalKgValue, lblTotalFinishingM2, lblTotalFinishingM2Value, lblApNote,
                lblTotalPieces, lblTotalPiecesValue, btnEditIpCategories, btnResetIpCharges });

            // ── grpMillFinishPricing (y=499, h=165) ─────────────────────────
            grpMillFinishPricing.Location = new System.Drawing.Point(18, 499);
            grpMillFinishPricing.Size     = new System.Drawing.Size(1010, 165);
            grpMillFinishPricing.Text     = "Mill Finish Pricing";
            lblSapDiscountHeader.AutoSize = true; lblSapDiscountHeader.Location = new System.Drawing.Point(12, 26);  lblSapDiscountHeader.Text = "Customer SAP Discount %:";
            txtCustomerSapDiscount.Location = new System.Drawing.Point(180, 24); txtCustomerSapDiscount.Size = new System.Drawing.Size(80, 23);
            lblMFColHdr.AutoSize  = false; lblMFColHdr.Location  = new System.Drawing.Point(12,  53); lblMFColHdr.Size  = new System.Drawing.Size(240, 18); lblMFColHdr.Text  = "";
            lblMFTypHdr.AutoSize  = false; lblMFTypHdr.Location  = new System.Drawing.Point(370, 53); lblMFTypHdr.Size  = new System.Drawing.Size(70, 18);  lblMFTypHdr.Text  = "Typical";
            lblMFMinHdr.AutoSize  = false; lblMFMinHdr.Location  = new System.Drawing.Point(450, 53); lblMFMinHdr.Size  = new System.Drawing.Size(55, 18);  lblMFMinHdr.Text  = "Min";
            lblMFMaxHdr.AutoSize  = false; lblMFMaxHdr.Location  = new System.Drawing.Point(530, 53); lblMFMaxHdr.Size  = new System.Drawing.Size(55, 18);  lblMFMaxHdr.Text  = "Max";
            lblMetalPrice.AutoSize = false; lblMetalPrice.Location = new System.Drawing.Point(12, 75); lblMetalPrice.Size = new System.Drawing.Size(200, 18); lblMetalPrice.Text = "Mill Finish Sell Price $/kg";
            txtMetalPrice.Location = new System.Drawing.Point(370, 73); txtMetalPrice.Size = new System.Drawing.Size(70, 23);
            lblMetalPriceMin.AutoSize = false; lblMetalPriceMin.Location = new System.Drawing.Point(450, 75); lblMetalPriceMin.Size = new System.Drawing.Size(55, 18); lblMetalPriceMin.Text = "\u2014";
            lblMetalPriceMax.AutoSize = false; lblMetalPriceMax.Location = new System.Drawing.Point(530, 75); lblMetalPriceMax.Size = new System.Drawing.Size(55, 18); lblMetalPriceMax.Text = "\u2014";
            lblMillFinishMargin.AutoSize = false; lblMillFinishMargin.Location = new System.Drawing.Point(12, 102); lblMillFinishMargin.Size = new System.Drawing.Size(200, 18); lblMillFinishMargin.Text = "Margin %";
            lblMillFinishMarginValue.AutoSize = false; lblMillFinishMarginValue.Location = new System.Drawing.Point(370, 102); lblMillFinishMarginValue.Size = new System.Drawing.Size(70, 18); lblMillFinishMarginValue.Text = "0.00%";
            lblIpHdr.AutoSize = true;     lblIpHdr.Location    = new System.Drawing.Point(620, 26); lblIpHdr.Text    = "IP Upcharges";
            lblIPColHdr.AutoSize = false; lblIPColHdr.Location = new System.Drawing.Point(620, 48); lblIPColHdr.Size = new System.Drawing.Size(165, 18); lblIPColHdr.Text = "";
            lblIPTypHdr.AutoSize = false; lblIPTypHdr.Location = new System.Drawing.Point(800, 48); lblIPTypHdr.Size = new System.Drawing.Size(65, 18);  lblIPTypHdr.Text = "$/kg";
            lblIP1Upcharge.AutoSize = false; lblIP1Upcharge.Location = new System.Drawing.Point(620, 70); lblIP1Upcharge.Size = new System.Drawing.Size(165, 18); lblIP1Upcharge.Text = "IP1 Upcharge $/kg:";
            txtIP1Upcharge.Location = new System.Drawing.Point(800, 68); txtIP1Upcharge.Size = new System.Drawing.Size(60, 23);
            lblIP1Margin.AutoSize = false; lblIP1Margin.Location = new System.Drawing.Point(870, 70); lblIP1Margin.Size = new System.Drawing.Size(60, 18); lblIP1Margin.Text = "Margin:";
            lblIP1MarginValue.AutoSize = false; lblIP1MarginValue.Location = new System.Drawing.Point(925, 70); lblIP1MarginValue.Size = new System.Drawing.Size(60, 18); lblIP1MarginValue.Text = "0.00%";
            lblIP2Upcharge.AutoSize = false; lblIP2Upcharge.Location = new System.Drawing.Point(620, 97); lblIP2Upcharge.Size = new System.Drawing.Size(165, 18); lblIP2Upcharge.Text = "IP2 Upcharge $/kg:";
            txtIP2Upcharge.Location = new System.Drawing.Point(800, 95); txtIP2Upcharge.Size = new System.Drawing.Size(60, 23);
            lblIP2Margin.AutoSize = false; lblIP2Margin.Location = new System.Drawing.Point(870, 97); lblIP2Margin.Size = new System.Drawing.Size(60, 18); lblIP2Margin.Text = "Margin:";
            lblIP2MarginValue.AutoSize = false; lblIP2MarginValue.Location = new System.Drawing.Point(925, 97); lblIP2MarginValue.Size = new System.Drawing.Size(60, 18); lblIP2MarginValue.Text = "0.00%";
            lblIP3Rate.AutoSize = false; lblIP3Rate.Location = new System.Drawing.Point(620, 124); lblIP3Rate.Size = new System.Drawing.Size(165, 18); lblIP3Rate.Text = "IP3 \u2013 Panoramic Fixed $/kg:";
            txtIP3Rate.Location = new System.Drawing.Point(800, 122); txtIP3Rate.Size = new System.Drawing.Size(60, 23);
            lblIP3Margin.AutoSize = false; lblIP3Margin.Location = new System.Drawing.Point(870, 124); lblIP3Margin.Size = new System.Drawing.Size(60, 18); lblIP3Margin.Text = "Margin:";
            lblIP3MarginValue.AutoSize = false; lblIP3MarginValue.Location = new System.Drawing.Point(925, 124); lblIP3MarginValue.Size = new System.Drawing.Size(60, 18); lblIP3MarginValue.Text = "0.00%";
            grpMillFinishPricing.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblSapDiscountHeader, txtCustomerSapDiscount,
                lblMFColHdr, lblMFTypHdr, lblMFMinHdr, lblMFMaxHdr,
                lblMetalPrice, txtMetalPrice, lblMetalPriceMin, lblMetalPriceMax,
                lblMillFinishMargin, lblMillFinishMarginValue,
                lblIpHdr, lblIPColHdr, lblIPTypHdr,
                lblIP1Upcharge, txtIP1Upcharge, lblIP1Margin, lblIP1MarginValue,
                lblIP2Upcharge, txtIP2Upcharge, lblIP2Margin, lblIP2MarginValue,
                lblIP3Rate, txtIP3Rate, lblIP3Margin, lblIP3MarginValue });

            // ── grpThermalBreakPricing (y=670, h=105) ───────────────────────
            grpThermalBreakPricing.Location = new System.Drawing.Point(18, 670);
            grpThermalBreakPricing.Size     = new System.Drawing.Size(1010, 105);
            grpThermalBreakPricing.Text     = "Thermal Break Pricing";
            lblTBCalcType.AutoSize = false; lblTBCalcType.Location = new System.Drawing.Point(12, 28); lblTBCalcType.Size = new System.Drawing.Size(220, 18); lblTBCalcType.Text = "Thermal Break Method:";
            cmbTBCalcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbTBCalcType.Location = new System.Drawing.Point(270, 24); cmbTBCalcType.Size = new System.Drawing.Size(200, 23);
            lblTBTypHdr.AutoSize = false; lblTBTypHdr.Location = new System.Drawing.Point(360, 57); lblTBTypHdr.Size = new System.Drawing.Size(70, 18); lblTBTypHdr.Text = "Typical";
            lblTBMargin.AutoSize = false; lblTBMargin.Location = new System.Drawing.Point(12, 79); lblTBMargin.Size = new System.Drawing.Size(220, 18); lblTBMargin.Text = "Enter Margin %:";
            txtTBMargin.Location = new System.Drawing.Point(360, 77); txtTBMargin.Size = new System.Drawing.Size(70, 23);
            grpThermalBreakPricing.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTBCalcType, cmbTBCalcType, lblTBTypHdr, lblTBMargin, txtTBMargin });

            // ── grpFinishingPricing (y=781, h=320) ──────────────────────────
            grpFinishingPricing.Location = new System.Drawing.Point(18, 781);
            grpFinishingPricing.Size     = new System.Drawing.Size(1010, 320);
            grpFinishingPricing.Text     = "Powder / Finishing Pricing";
            lblPowderPricedAs.AutoSize = true; lblPowderPricedAs.Location = new System.Drawing.Point(12, 28); lblPowderPricedAs.Text = "Powder Priced as:";
            cmbPowderPricedAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbPowderPricedAs.Location = new System.Drawing.Point(160, 24); cmbPowderPricedAs.Size = new System.Drawing.Size(200, 23);
            lblCostType.AutoSize = true; lblCostType.Location = new System.Drawing.Point(375, 28); lblCostType.Text = "Cost Type:";
            cmbCostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCostType.Location = new System.Drawing.Point(475, 24); cmbCostType.Size = new System.Drawing.Size(220, 23);
            chkCostTypeOverride.AutoSize = true; chkCostTypeOverride.Location = new System.Drawing.Point(710, 28); chkCostTypeOverride.Text = "Override";
            btnToggleOverride.Location = new System.Drawing.Point(800, 24); btnToggleOverride.Size = new System.Drawing.Size(120, 26); btnToggleOverride.Text = "Toggle Override";
            lblStdFinishRatesHdr.AutoSize = true; lblStdFinishRatesHdr.Location = new System.Drawing.Point(12, 57); lblStdFinishRatesHdr.Text = "STANDARD (non-mill) FINISHING RATES (powdercoat or anodised)";
            lblFCColHdr.AutoSize = false; lblFCColHdr.Location = new System.Drawing.Point(12, 77);  lblFCColHdr.Size = new System.Drawing.Size(280, 18); lblFCColHdr.Text = "";
            lblFCTypHdr.AutoSize = false; lblFCTypHdr.Location = new System.Drawing.Point(400, 77); lblFCTypHdr.Size = new System.Drawing.Size(70, 18);  lblFCTypHdr.Text = "Typical";
            lblFCMinHdr.AutoSize = false; lblFCMinHdr.Location = new System.Drawing.Point(478, 77); lblFCMinHdr.Size = new System.Drawing.Size(55, 18);  lblFCMinHdr.Text = "Min";
            lblFCMaxHdr.AutoSize = false; lblFCMaxHdr.Location = new System.Drawing.Point(550, 77); lblFCMaxHdr.Size = new System.Drawing.Size(55, 18);  lblFCMaxHdr.Text = "Max";
            lblFinishingCostPricePerM2.AutoSize = false; lblFinishingCostPricePerM2.Location = new System.Drawing.Point(12, 97);  lblFinishingCostPricePerM2.Size = new System.Drawing.Size(280, 18); lblFinishingCostPricePerM2.Text = "Finishing Cost Price $/m\u00B2";
            txtFinishingCostPricePerM2.Location = new System.Drawing.Point(400, 95); txtFinishingCostPricePerM2.Size = new System.Drawing.Size(70, 23);
            lblB42.AutoSize = false; lblB42.Location = new System.Drawing.Point(12, 124); lblB42.Size = new System.Drawing.Size(280, 18); lblB42.Text = "POA/Other Vendor Cost $/m\u00B2";
            txtB42.Location = new System.Drawing.Point(400, 122); txtB42.Size = new System.Drawing.Size(70, 23);
            lblFinishingPrice.AutoSize = false; lblFinishingPrice.Location = new System.Drawing.Point(12, 151); lblFinishingPrice.Size = new System.Drawing.Size(280, 18); lblFinishingPrice.Text = "Finishing Sell Price $/m\u00B2";
            txtFinishingPrice.Location = new System.Drawing.Point(400, 149); txtFinishingPrice.Size = new System.Drawing.Size(70, 23);
            lblB44.AutoSize = false; lblB44.Location = new System.Drawing.Point(12, 178); lblB44.Size = new System.Drawing.Size(280, 18); lblB44.Text = "Finishing Sell Price override $/m\u00B2";
            txtB44.Location = new System.Drawing.Point(400, 176); txtB44.Size = new System.Drawing.Size(70, 23);
            lblFinishingMargin.AutoSize = false; lblFinishingMargin.Location = new System.Drawing.Point(12, 205); lblFinishingMargin.Size = new System.Drawing.Size(280, 18); lblFinishingMargin.Text = "Margin %";
            lblFinishingMarginValue.AutoSize = false; lblFinishingMarginValue.Location = new System.Drawing.Point(400, 205); lblFinishingMarginValue.Size = new System.Drawing.Size(70, 18); lblFinishingMarginValue.Text = "0.00%";
            lblBulkHdr.AutoSize = true; lblBulkHdr.Location = new System.Drawing.Point(12, 240); lblBulkHdr.Text = "Bulk / Non-bulk Rates (AP/PP)";
            lblB48.AutoSize = false; lblB48.Location = new System.Drawing.Point(12, 260);  lblB48.Size = new System.Drawing.Size(240, 18); lblB48.Text = "Mill BULK POA Cost $/m\u00B2:";
            txtB48.Location = new System.Drawing.Point(400, 258); txtB48.Size = new System.Drawing.Size(70, 23);
            lblB50.AutoSize = false; lblB50.Location = new System.Drawing.Point(562, 260); lblB50.Size = new System.Drawing.Size(240, 18); lblB50.Text = "Mill BULK Sell Price $/m\u00B2:";
            txtB50.Location = new System.Drawing.Point(950, 258); txtB50.Size = new System.Drawing.Size(70, 23);
            lblB52.AutoSize = false; lblB52.Location = new System.Drawing.Point(12, 287);  lblB52.Size = new System.Drawing.Size(240, 18); lblB52.Text = "Mill NON-Bulk POA Cost $/m\u00B2:";
            txtB52.Location = new System.Drawing.Point(400, 285); txtB52.Size = new System.Drawing.Size(70, 23);
            lblB54.AutoSize = false; lblB54.Location = new System.Drawing.Point(562, 287); lblB54.Size = new System.Drawing.Size(240, 18); lblB54.Text = "Mill NON-Bulk Sell Price $/m\u00B2:";
            txtB54.Location = new System.Drawing.Point(950, 285); txtB54.Size = new System.Drawing.Size(70, 23);
            grpFinishingPricing.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblPowderPricedAs, cmbPowderPricedAs, lblCostType, cmbCostType, chkCostTypeOverride, btnToggleOverride,
                lblStdFinishRatesHdr, lblFCColHdr, lblFCTypHdr, lblFCMinHdr, lblFCMaxHdr,
                lblFinishingCostPricePerM2, txtFinishingCostPricePerM2, lblB42, txtB42,
                lblFinishingPrice, txtFinishingPrice, lblB44, txtB44,
                lblFinishingMargin, lblFinishingMarginValue, lblBulkHdr,
                lblB48, txtB48, lblB50, txtB50, lblB52, txtB52, lblB54, txtB54 });

            // ── grpAnodisedThresholds (y=1107, h=135) ───────────────────────
            grpAnodisedThresholds.Location = new System.Drawing.Point(18, 1107);
            grpAnodisedThresholds.Size     = new System.Drawing.Size(1010, 135);
            grpAnodisedThresholds.Text     = "Anodised Thresholds";
            lblAnodisedThresholdCost.AutoSize = false; lblAnodisedThresholdCost.Location = new System.Drawing.Point(12, 28); lblAnodisedThresholdCost.Size = new System.Drawing.Size(240, 18); lblAnodisedThresholdCost.Text = "Finishing Cost Price $/m\u00B2 (auto):";
            txtAnodisedThresholdCost.Location = new System.Drawing.Point(260, 24); txtAnodisedThresholdCost.Size = new System.Drawing.Size(80, 23);
            lblB57.AutoSize = false; lblB57.Location = new System.Drawing.Point(370, 28); lblB57.Size = new System.Drawing.Size(200, 18); lblB57.Text = "Threshold Cost Price $/m\u00B2 (B57):";
            txtB57.Location = new System.Drawing.Point(580, 24); txtB57.Size = new System.Drawing.Size(80, 23);
            lblB58.AutoSize = false; lblB58.Location = new System.Drawing.Point(12, 55); lblB58.Size = new System.Drawing.Size(240, 18); lblB58.Text = "Finishing Sell Price $/m\u00B2:";
            txtB58.Location = new System.Drawing.Point(260, 51); txtB58.Size = new System.Drawing.Size(80, 23);
            lblThresholdMargin.AutoSize = false; lblThresholdMargin.Location = new System.Drawing.Point(12, 82); lblThresholdMargin.Size = new System.Drawing.Size(240, 18); lblThresholdMargin.Text = "Margin %:";
            lblThresholdMarginValue.AutoSize = false; lblThresholdMarginValue.Location = new System.Drawing.Point(260, 82); lblThresholdMarginValue.Size = new System.Drawing.Size(80, 18); lblThresholdMarginValue.Text = "0.00%";
            btnBulkNonBulkRates.Location = new System.Drawing.Point(562, 80); btnBulkNonBulkRates.Size = new System.Drawing.Size(155, 26); btnBulkNonBulkRates.Text = "Bulk/Non-bulk Rates";
            grpAnodisedThresholds.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblAnodisedThresholdCost, txtAnodisedThresholdCost, lblB57, txtB57,
                lblB58, txtB58, lblThresholdMargin, lblThresholdMarginValue, btnBulkNonBulkRates });

            // ── grpSummary (y=1248, h=220) ───────────────────────────────────
            grpSummary.Location = new System.Drawing.Point(18, 1248);
            grpSummary.Size     = new System.Drawing.Size(1010, 220);
            grpSummary.Text     = "Quote Summary";
            gridSummary.AllowUserToAddRows    = false;
            gridSummary.AllowUserToDeleteRows = false;
            gridSummary.AutoGenerateColumns   = false;
            gridSummary.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridSummary.BackgroundColor       = System.Drawing.SystemColors.Control;
            gridSummary.BorderStyle           = System.Windows.Forms.BorderStyle.Fixed3D;
            gridSummary.Location              = new System.Drawing.Point(12, 24);
            gridSummary.ReadOnly              = true;
            gridSummary.RowHeadersVisible     = false;
            gridSummary.Size                  = new System.Drawing.Size(980, 155);
            colCategory.DataPropertyName = "Category";     colCategory.HeaderText    = "Category";     colCategory.FillWeight    = 25;
            colTotal.DataPropertyName    = "TotalCost";    colTotal.HeaderText       = "Total (Cost)"; colTotal.FillWeight       = 18; colTotal.DefaultCellStyle.Format    = "C2"; colTotal.DefaultCellStyle.Alignment    = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colTotalValue.DataPropertyName = "TotalSellValue"; colTotalValue.HeaderText = "Total Value"; colTotalValue.FillWeight  = 18; colTotalValue.DefaultCellStyle.Format = "C2"; colTotalValue.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colMarginDollar.DataPropertyName = "MarginDollar"; colMarginDollar.HeaderText = "Margin $"; colMarginDollar.FillWeight = 18; colMarginDollar.DefaultCellStyle.Format = "C2"; colMarginDollar.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colMarginPct.DataPropertyName = "MarginPct";   colMarginPct.HeaderText   = "Margin %";    colMarginPct.FillWeight   = 12; colMarginPct.DefaultCellStyle.Format    = "F2"; colMarginPct.DefaultCellStyle.Alignment    = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            gridSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colCategory, colTotal, colTotalValue, colMarginDollar, colMarginPct });
            lblSummaryNote1.AutoSize = true; lblSummaryNote1.Location = new System.Drawing.Point(12, 185); lblSummaryNote1.Text = "* Margin $ also includes IP charge";
            lblSummaryNote2.AutoSize = true; lblSummaryNote2.Location = new System.Drawing.Point(12, 200); lblSummaryNote2.Text = "* Sundry costs based on sundry costs entered and slot costs estimated";
            grpSummary.Controls.AddRange(new System.Windows.Forms.Control[] { gridSummary, lblSummaryNote1, lblSummaryNote2 });

            // ── Action buttons (y=1474) ──────────────────────────────────────
            btnEditSundryItems.Location  = new System.Drawing.Point(18,  1474); btnEditSundryItems.Size  = new System.Drawing.Size(130, 30); btnEditSundryItems.Text  = "Edit Sundry Items";
            btnRecalculateAll.Location   = new System.Drawing.Point(158, 1474); btnRecalculateAll.Size   = new System.Drawing.Size(120, 30); btnRecalculateAll.Text   = "Recalculate All";
            btnSave.Location             = new System.Drawing.Point(288, 1472); btnSave.Size             = new System.Drawing.Size(110, 34); btnSave.Text             = "Save";
            btnRefreshSummary.Location   = new System.Drawing.Point(408, 1472); btnRefreshSummary.Size   = new System.Drawing.Size(150, 34); btnRefreshSummary.Text   = "Refresh Summary";
            btnUpdateVisibility.Location = new System.Drawing.Point(568, 1474); btnUpdateVisibility.Size = new System.Drawing.Size(145, 30); btnUpdateVisibility.Text = "Update Row Visibility";

            // ── grpRowVisibility (y=1516, h=80) ─────────────────────────────
            grpRowVisibility.Location = new System.Drawing.Point(18, 1516);
            grpRowVisibility.Size     = new System.Drawing.Size(1010, 80);
            grpRowVisibility.Text     = "Row Visibility (advanced)";
            lblVisibleRows.AutoSize = false; lblVisibleRows.Location = new System.Drawing.Point(10, 22); lblVisibleRows.Size = new System.Drawing.Size(985, 20); lblVisibleRows.Text = "Visible rows: \u2014";
            lblHiddenRows.AutoSize  = false; lblHiddenRows.Location  = new System.Drawing.Point(10, 46); lblHiddenRows.Size  = new System.Drawing.Size(985, 20); lblHiddenRows.Text  = "Hidden rows: \u2014";
            grpRowVisibility.Controls.AddRange(new System.Windows.Forms.Control[] { lblVisibleRows, lblHiddenRows });

            // ── pnlScroll ────────────────────────────────────────────────────
            pnlScroll.AutoScroll = true;
            pnlScroll.Dock       = System.Windows.Forms.DockStyle.Fill;
            pnlScroll.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle,
                grpHeader, grpFinishSection, grpQuantitiesSection,
                grpMillFinishPricing, grpThermalBreakPricing, grpFinishingPricing,
                grpAnodisedThresholds, grpSummary,
                btnEditSundryItems, btnRecalculateAll, btnSave, btnRefreshSummary, btnUpdateVisibility,
                grpRowVisibility });

            // ── Event hookups ─────────────────────────────────────────────────
            btnEditIpCategories.Click += new System.EventHandler(btnEditIpCategories_Click);
            btnResetIpCharges.Click   += new System.EventHandler(btnResetIpCharges_Click);
            btnToggleOverride.Click   += new System.EventHandler(btnToggleOverride_Click);
            btnBulkNonBulkRates.Click += new System.EventHandler(btnBulkNonBulkRates_Click);
            btnEditSundryItems.Click  += new System.EventHandler(btnEditSundryItems_Click);
            btnRecalculateAll.Click   += new System.EventHandler(btnRecalculateAll_Click);
            btnSave.Click             += new System.EventHandler(btnSave_Click);
            btnRefreshSummary.Click   += new System.EventHandler(btnRefreshSummary_Click);
            btnUpdateVisibility.Click += new System.EventHandler(btnUpdateVisibility_Click);

            // ── Form ─────────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(1050, 760);
            Controls.Add(pnlScroll);
            Name = "EditPricingForm";
            Text = "Edit Pricing";

            ((System.ComponentModel.ISupportInitialize)gridSummary).EndInit();
            grpHeader.ResumeLayout(false);            grpHeader.PerformLayout();
            grpFinishSection.ResumeLayout(false);     grpFinishSection.PerformLayout();
            grpQuantitiesSection.ResumeLayout(false); grpQuantitiesSection.PerformLayout();
            grpMillFinishPricing.ResumeLayout(false); grpMillFinishPricing.PerformLayout();
            grpThermalBreakPricing.ResumeLayout(false); grpThermalBreakPricing.PerformLayout();
            grpFinishingPricing.ResumeLayout(false);  grpFinishingPricing.PerformLayout();
            grpAnodisedThresholds.ResumeLayout(false); grpAnodisedThresholds.PerformLayout();
            grpSummary.ResumeLayout(false);           grpSummary.PerformLayout();
            grpRowVisibility.ResumeLayout(false);
            pnlScroll.ResumeLayout(false);
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
