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
            // ── Declare all controls ──────────────────────────────────────────
            pnlScroll                 = new System.Windows.Forms.Panel();
            lblTitle                  = new System.Windows.Forms.Label();

            // Header group
            grpHeader                 = new System.Windows.Forms.GroupBox();
            lblQuotedBy               = new System.Windows.Forms.Label();
            txtQuotedBy               = new System.Windows.Forms.TextBox();
            lblQuoteDate              = new System.Windows.Forms.Label();
            dtpQuoteDate              = new System.Windows.Forms.DateTimePicker();
            lblProjectName            = new System.Windows.Forms.Label();
            txtProjectName            = new System.Windows.Forms.TextBox();
            lblCustomerName           = new System.Windows.Forms.Label();
            txtCustomerName           = new System.Windows.Forms.TextBox();
            lblPricingNotes           = new System.Windows.Forms.Label();
            txtPricingNotes           = new System.Windows.Forms.TextBox();

            // Finish group
            grpFinishSection          = new System.Windows.Forms.GroupBox();
            lblFinType                = new System.Windows.Forms.Label();
            cmbFinType                = new System.Windows.Forms.ComboBox();
            lblFinishGroup            = new System.Windows.Forms.Label();
            cmbFinishGroup            = new System.Windows.Forms.ComboBox();
            lblFinishName             = new System.Windows.Forms.Label();
            cmbFinishName             = new System.Windows.Forms.ComboBox();
            lblApplicatorGroup        = new System.Windows.Forms.Label();
            cmbApplicatorGroup        = new System.Windows.Forms.ComboBox();
            lblFinishApplicator       = new System.Windows.Forms.Label();
            cmbFinishApplicator       = new System.Windows.Forms.ComboBox();

            // Quantities group
            grpQuantitiesSection      = new System.Windows.Forms.GroupBox();
            lblTotalKg                = new System.Windows.Forms.Label();
            lblTotalKgValue           = new System.Windows.Forms.Label();
            lblTotalFinishingM2       = new System.Windows.Forms.Label();
            lblTotalFinishingM2Value  = new System.Windows.Forms.Label();
            lblApNote                 = new System.Windows.Forms.Label();
            lblTotalPieces            = new System.Windows.Forms.Label();
            lblTotalPiecesValue       = new System.Windows.Forms.Label();
            btnEditIpCategories       = new System.Windows.Forms.Button();
            btnResetIpCharges         = new System.Windows.Forms.Button();

            // Mill Finish pricing group (left)
            grpMillFinishPricing      = new System.Windows.Forms.GroupBox();
            lblSapDiscountHeader      = new System.Windows.Forms.Label();
            txtCustomerSapDiscount    = new System.Windows.Forms.TextBox();
            lblMFColHdr               = new System.Windows.Forms.Label();
            lblMFTypHdr               = new System.Windows.Forms.Label();
            lblMFMinHdr               = new System.Windows.Forms.Label();
            lblMFMaxHdr               = new System.Windows.Forms.Label();
            lblMetalPrice             = new System.Windows.Forms.Label();
            txtMetalPrice             = new System.Windows.Forms.TextBox();
            lblMetalPriceMin          = new System.Windows.Forms.Label();
            lblMetalPriceMax          = new System.Windows.Forms.Label();
            lblMillFinishMargin       = new System.Windows.Forms.Label();
            lblMillFinishMarginValue  = new System.Windows.Forms.Label();
            // IP Upcharges (right side of same group)
            lblIpHdr                  = new System.Windows.Forms.Label();
            lblIPColHdr               = new System.Windows.Forms.Label();
            lblIPTypHdr               = new System.Windows.Forms.Label();
            lblIP1Upcharge            = new System.Windows.Forms.Label();
            txtIP1Upcharge            = new System.Windows.Forms.TextBox();
            lblIP1Margin              = new System.Windows.Forms.Label();
            lblIP1MarginValue         = new System.Windows.Forms.Label();
            lblIP2Upcharge            = new System.Windows.Forms.Label();
            txtIP2Upcharge            = new System.Windows.Forms.TextBox();
            lblIP2Margin              = new System.Windows.Forms.Label();
            lblIP2MarginValue         = new System.Windows.Forms.Label();
            lblIP3Rate                = new System.Windows.Forms.Label();
            txtIP3Rate                = new System.Windows.Forms.TextBox();
            lblIP3Margin              = new System.Windows.Forms.Label();
            lblIP3MarginValue         = new System.Windows.Forms.Label();

            // Thermal Break pricing group
            grpThermalBreakPricing    = new System.Windows.Forms.GroupBox();
            lblTBCalcType             = new System.Windows.Forms.Label();
            cmbTBCalcType             = new System.Windows.Forms.ComboBox();
            lblTBTypHdr               = new System.Windows.Forms.Label();
            lblTBMargin               = new System.Windows.Forms.Label();
            txtTBMargin               = new System.Windows.Forms.TextBox();

            // Powder / Finishing pricing group
            grpFinishingPricing       = new System.Windows.Forms.GroupBox();
            lblPowderPricedAs         = new System.Windows.Forms.Label();
            cmbPowderPricedAs         = new System.Windows.Forms.ComboBox();
            lblCostType               = new System.Windows.Forms.Label();
            cmbCostType               = new System.Windows.Forms.ComboBox();
            chkCostTypeOverride       = new System.Windows.Forms.CheckBox();
            btnToggleOverride         = new System.Windows.Forms.Button();
            lblStdFinishRatesHdr      = new System.Windows.Forms.Label();
            lblFCColHdr               = new System.Windows.Forms.Label();
            lblFCTypHdr               = new System.Windows.Forms.Label();
            lblFCMinHdr               = new System.Windows.Forms.Label();
            lblFCMaxHdr               = new System.Windows.Forms.Label();
            lblFinishingCostPricePerM2 = new System.Windows.Forms.Label();
            txtFinishingCostPricePerM2 = new System.Windows.Forms.TextBox();
            lblB42                    = new System.Windows.Forms.Label();
            txtB42                    = new System.Windows.Forms.TextBox();
            lblFinishingPrice         = new System.Windows.Forms.Label();
            txtFinishingPrice         = new System.Windows.Forms.TextBox();
            lblB44                    = new System.Windows.Forms.Label();
            txtB44                    = new System.Windows.Forms.TextBox();
            lblFinishingMargin        = new System.Windows.Forms.Label();
            lblFinishingMarginValue   = new System.Windows.Forms.Label();
            // Bulk / Non-bulk sub-section
            lblBulkHdr                = new System.Windows.Forms.Label();
            lblB48                    = new System.Windows.Forms.Label();
            txtB48                    = new System.Windows.Forms.TextBox();
            lblB50                    = new System.Windows.Forms.Label();
            txtB50                    = new System.Windows.Forms.TextBox();
            lblB52                    = new System.Windows.Forms.Label();
            txtB52                    = new System.Windows.Forms.TextBox();
            lblB54                    = new System.Windows.Forms.Label();
            txtB54                    = new System.Windows.Forms.TextBox();

            // Anodised Thresholds group
            grpAnodisedThresholds     = new System.Windows.Forms.GroupBox();
            lblAnodisedThresholdCost  = new System.Windows.Forms.Label();
            txtAnodisedThresholdCost  = new System.Windows.Forms.TextBox();
            lblB57                    = new System.Windows.Forms.Label();
            txtB57                    = new System.Windows.Forms.TextBox();
            lblB58                    = new System.Windows.Forms.Label();
            txtB58                    = new System.Windows.Forms.TextBox();
            lblThresholdMargin        = new System.Windows.Forms.Label();
            lblThresholdMarginValue   = new System.Windows.Forms.Label();
            btnBulkNonBulkRates       = new System.Windows.Forms.Button();

            // Summary group
            grpSummary                = new System.Windows.Forms.GroupBox();
            gridSummary               = new System.Windows.Forms.DataGridView();
            colCategory               = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotal                  = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotalValue             = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMarginDollar           = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMarginPct              = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblSummaryNote1           = new System.Windows.Forms.Label();
            lblSummaryNote2           = new System.Windows.Forms.Label();

            // Action buttons
            btnEditSundryItems        = new System.Windows.Forms.Button();
            btnRecalculateAll         = new System.Windows.Forms.Button();
            btnSave                   = new System.Windows.Forms.Button();
            btnUpdateVisibility       = new System.Windows.Forms.Button();
            btnRefreshSummary         = new System.Windows.Forms.Button();

            // Row visibility group
            grpRowVisibility          = new System.Windows.Forms.GroupBox();
            lblVisibleRows            = new System.Windows.Forms.Label();
            lblHiddenRows             = new System.Windows.Forms.Label();

            // ── Begin layout suspend ──────────────────────────────────────────
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
            pnlScroll.SuspendLayout();
            SuspendLayout();

            // ── Helper colour for GroupBox title text (painted via ForeColor) ─
            var oliveFont  = BoldFont(10F);
            var labelFont  = RegFont(9F);
            var headerFont = BoldFont(9F);
            var colHdrFont = BoldFont(8.5F);

            // ─────────────────────────────────────────────────────────────────
            // pnlScroll
            // ─────────────────────────────────────────────────────────────────
            pnlScroll.AutoScroll = true;
            pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;

            // ─────────────────────────────────────────────────────────────────
            // lblTitle
            // ─────────────────────────────────────────────────────────────────
            lblTitle.Font     = BoldFont(13F);
            lblTitle.ForeColor = ClrOlive;
            lblTitle.Location = new System.Drawing.Point(18, 12);
            lblTitle.Size     = new System.Drawing.Size(400, 28);
            lblTitle.Text     = "3. Edit Pricing";

            // ═════════════════════════════════════════════════════════════════
            // GROUP 1 – Project Information (header)
            // ═════════════════════════════════════════════════════════════════
            int gY = 46;
            grpHeader.Font        = oliveFont;
            grpHeader.ForeColor   = ClrOlive;
            grpHeader.Location    = new System.Drawing.Point(18, gY);
            grpHeader.Size        = new System.Drawing.Size(1010, 180);
            grpHeader.Text        = "Project Information";

            int lx = 12, cx = 150, row = 26, rh = 28;
            // Row 1: Quoted By / Quote Date
            lblQuotedBy.AutoSize  = true;
            lblQuotedBy.Font      = labelFont;
            lblQuotedBy.Location  = new System.Drawing.Point(lx, row);
            lblQuotedBy.Text      = "Quoted By:";
            txtQuotedBy.BackColor = ClrInputBg;
            txtQuotedBy.Font      = RegFont();
            txtQuotedBy.Location  = new System.Drawing.Point(cx, row - 2);
            txtQuotedBy.Size      = new System.Drawing.Size(180, 23);
            lblQuoteDate.AutoSize = true;
            lblQuoteDate.Font     = labelFont;
            lblQuoteDate.Location = new System.Drawing.Point(360, row);
            lblQuoteDate.Text     = "Quote Date:";
            dtpQuoteDate.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpQuoteDate.Location = new System.Drawing.Point(460, row - 2);
            dtpQuoteDate.Size     = new System.Drawing.Size(130, 23);

            // Row 2: Project
            row += rh;
            lblProjectName.AutoSize  = true;
            lblProjectName.Font      = labelFont;
            lblProjectName.Location  = new System.Drawing.Point(lx, row);
            lblProjectName.Text      = "Project:";
            txtProjectName.BackColor = ClrInputBg;
            txtProjectName.Font      = RegFont();
            txtProjectName.Location  = new System.Drawing.Point(cx, row - 2);
            txtProjectName.Size      = new System.Drawing.Size(450, 23);

            // Row 3: Customer
            row += rh;
            lblCustomerName.AutoSize  = true;
            lblCustomerName.Font      = labelFont;
            lblCustomerName.Location  = new System.Drawing.Point(lx, row);
            lblCustomerName.Text      = "Customer:";
            txtCustomerName.BackColor = ClrInputBg;
            txtCustomerName.Font      = RegFont();
            txtCustomerName.Location  = new System.Drawing.Point(cx, row - 2);
            txtCustomerName.Size      = new System.Drawing.Size(450, 23);

            // Row 4: Pricing Notes
            row += rh;
            lblPricingNotes.AutoSize = true;
            lblPricingNotes.Font     = labelFont;
            lblPricingNotes.Location = new System.Drawing.Point(lx, row);
            lblPricingNotes.Text     = "Pricing Notes:";
            txtPricingNotes.BackColor    = ClrInputBg;
            txtPricingNotes.Font         = RegFont();
            txtPricingNotes.Multiline    = true;
            txtPricingNotes.Location     = new System.Drawing.Point(cx, row - 2);
            txtPricingNotes.Size         = new System.Drawing.Size(820, 54);

            grpHeader.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblQuotedBy, txtQuotedBy, lblQuoteDate, dtpQuoteDate,
                lblProjectName, txtProjectName, lblCustomerName, txtCustomerName,
                lblPricingNotes, txtPricingNotes
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 2 – Finish Selection
            // ═════════════════════════════════════════════════════════════════
            gY += 186;
            grpFinishSection.Font      = oliveFont;
            grpFinishSection.ForeColor = ClrOlive;
            grpFinishSection.Location  = new System.Drawing.Point(18, gY);
            grpFinishSection.Size      = new System.Drawing.Size(1010, 175);
            grpFinishSection.Text      = "Finish Selection";

            int fLx = 12, fCx = 200, fCw = 240, fRow = 26, fRh = 28;

            lblFinType.AutoSize = true;
            lblFinType.Font     = labelFont;
            lblFinType.Location = new System.Drawing.Point(fLx, fRow);
            lblFinType.Text     = "Finish Type:";
            cmbFinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFinType.Font          = RegFont();
            cmbFinType.Location      = new System.Drawing.Point(fCx, fRow - 2);
            cmbFinType.Size          = new System.Drawing.Size(fCw, 23);
            cmbFinType.BackColor     = ClrInputBg;

            fRow += fRh;
            lblFinishGroup.AutoSize = true;
            lblFinishGroup.Font     = labelFont;
            lblFinishGroup.Location = new System.Drawing.Point(fLx, fRow);
            lblFinishGroup.Text     = "Finish Group:";
            cmbFinishGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFinishGroup.Font          = RegFont();
            cmbFinishGroup.Location      = new System.Drawing.Point(fCx, fRow - 2);
            cmbFinishGroup.Size          = new System.Drawing.Size(fCw, 23);
            cmbFinishGroup.BackColor     = ClrInputBg;

            fRow += fRh;
            lblFinishName.AutoSize = true;
            lblFinishName.Font     = labelFont;
            lblFinishName.Location = new System.Drawing.Point(fLx, fRow);
            lblFinishName.Text     = "Finish Name:";
            cmbFinishName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cmbFinishName.Font          = RegFont();
            cmbFinishName.Location      = new System.Drawing.Point(fCx, fRow - 2);
            cmbFinishName.Size          = new System.Drawing.Size(760, 23);
            cmbFinishName.BackColor     = ClrInputBg;

            fRow += fRh;
            lblApplicatorGroup.AutoSize = true;
            lblApplicatorGroup.Font     = labelFont;
            lblApplicatorGroup.Location = new System.Drawing.Point(fLx, fRow);
            lblApplicatorGroup.Text     = "Applicator Finish Group:";
            cmbApplicatorGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbApplicatorGroup.Font          = RegFont();
            cmbApplicatorGroup.Location      = new System.Drawing.Point(fCx, fRow - 2);
            cmbApplicatorGroup.Size          = new System.Drawing.Size(fCw, 23);
            cmbApplicatorGroup.BackColor     = ClrInputBg;

            fRow += fRh;
            lblFinishApplicator.AutoSize = true;
            lblFinishApplicator.Font     = labelFont;
            lblFinishApplicator.Location = new System.Drawing.Point(fLx, fRow);
            lblFinishApplicator.Text     = "Finish Applicator:";
            cmbFinishApplicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFinishApplicator.Font          = RegFont();
            cmbFinishApplicator.Location      = new System.Drawing.Point(fCx, fRow - 2);
            cmbFinishApplicator.Size          = new System.Drawing.Size(fCw + 160, 23);
            cmbFinishApplicator.BackColor     = ClrInputBg;

            grpFinishSection.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblFinType, cmbFinType, lblFinishGroup, cmbFinishGroup,
                lblFinishName, cmbFinishName, lblApplicatorGroup, cmbApplicatorGroup,
                lblFinishApplicator, cmbFinishApplicator
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 3 – Quantities
            // ═════════════════════════════════════════════════════════════════
            gY += 181;
            grpQuantitiesSection.Font      = oliveFont;
            grpQuantitiesSection.ForeColor = ClrOlive;
            grpQuantitiesSection.Location  = new System.Drawing.Point(18, gY);
            grpQuantitiesSection.Size      = new System.Drawing.Size(1010, 80);
            grpQuantitiesSection.Text      = "Quantities";

            int qY = 28, qX = 12;
            lblTotalKg.AutoSize   = true;
            lblTotalKg.Font       = labelFont;
            lblTotalKg.Location   = new System.Drawing.Point(qX, qY);
            lblTotalKg.Text       = "Total kg:";
            lblTotalKgValue.Font       = headerFont;
            lblTotalKgValue.Location   = new System.Drawing.Point(qX + 80, qY);
            lblTotalKgValue.Size       = new System.Drawing.Size(80, 20);
            lblTotalKgValue.Text       = "0.00";
            lblTotalKgValue.BackColor  = ClrReadOnly;

            lblTotalFinishingM2.AutoSize   = true;
            lblTotalFinishingM2.Font       = labelFont;
            lblTotalFinishingM2.Location   = new System.Drawing.Point(qX + 180, qY);
            lblTotalFinishingM2.Text       = "Total Finishing m²:";
            lblTotalFinishingM2Value.Font       = headerFont;
            lblTotalFinishingM2Value.Location   = new System.Drawing.Point(qX + 310, qY);
            lblTotalFinishingM2Value.Size       = new System.Drawing.Size(80, 20);
            lblTotalFinishingM2Value.Text       = "0.00";
            lblTotalFinishingM2Value.BackColor  = ClrReadOnly;

            lblApNote.AutoSize  = true;
            lblApNote.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            lblApNote.ForeColor = System.Drawing.Color.Gray;
            lblApNote.Location  = new System.Drawing.Point(qX + 400, qY + 3);
            lblApNote.Text      = "(AP)";

            lblTotalPieces.AutoSize   = true;
            lblTotalPieces.Font       = labelFont;
            lblTotalPieces.Location   = new System.Drawing.Point(qX + 440, qY);
            lblTotalPieces.Text       = "Total Pieces:";
            lblTotalPiecesValue.Font       = headerFont;
            lblTotalPiecesValue.Location   = new System.Drawing.Point(qX + 540, qY);
            lblTotalPiecesValue.Size       = new System.Drawing.Size(60, 20);
            lblTotalPiecesValue.Text       = "0";
            lblTotalPiecesValue.BackColor  = ClrReadOnly;

            btnEditIpCategories.Location  = new System.Drawing.Point(690, qY - 2);
            btnEditIpCategories.Size      = new System.Drawing.Size(145, 26);
            btnEditIpCategories.Text      = "Edit IP Categories";
            btnEditIpCategories.Click    += new System.EventHandler(btnEditIpCategories_Click);

            btnResetIpCharges.Location  = new System.Drawing.Point(845, qY - 2);
            btnResetIpCharges.Size      = new System.Drawing.Size(140, 26);
            btnResetIpCharges.Text      = "Reset IP Charges";
            btnResetIpCharges.Click    += new System.EventHandler(btnResetIpCharges_Click);

            grpQuantitiesSection.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTotalKg, lblTotalKgValue,
                lblTotalFinishingM2, lblTotalFinishingM2Value, lblApNote,
                lblTotalPieces, lblTotalPiecesValue,
                btnEditIpCategories, btnResetIpCharges
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 4 – Mill Finish Pricing (left) + IP Upcharges (right)
            // ═════════════════════════════════════════════════════════════════
            gY += 86;
            grpMillFinishPricing.Font      = oliveFont;
            grpMillFinishPricing.ForeColor = ClrOlive;
            grpMillFinishPricing.Location  = new System.Drawing.Point(18, gY);
            grpMillFinishPricing.Size      = new System.Drawing.Size(1010, 165);
            grpMillFinishPricing.Text      = "Mill Finish Pricing";

            // Left section: SAP discount + pricing table
            int mfLx = 12, mfCx = 290, mfTypX = 370, mfMinX = 450, mfMaxX = 530;
            int mfRow = 26, mfRh = 27;

            lblSapDiscountHeader.AutoSize  = true;
            lblSapDiscountHeader.Font      = labelFont;
            lblSapDiscountHeader.Location  = new System.Drawing.Point(mfLx, mfRow);
            lblSapDiscountHeader.Text      = "Customer SAP Discount %:";
            txtCustomerSapDiscount.BackColor = ClrInputBg;
            txtCustomerSapDiscount.Font      = RegFont();
            txtCustomerSapDiscount.Location  = new System.Drawing.Point(mfCx - 110, mfRow - 2);
            txtCustomerSapDiscount.Size      = new System.Drawing.Size(80, 23);

            // Column headers
            mfRow += mfRh;
            lblMFColHdr.AutoSize  = false;
            lblMFColHdr.Font      = colHdrFont;
            lblMFColHdr.ForeColor = ClrOlive;
            lblMFColHdr.Location  = new System.Drawing.Point(mfLx, mfRow);
            lblMFColHdr.Size      = new System.Drawing.Size(240, 18);
            lblMFColHdr.Text      = "";

            lblMFTypHdr.AutoSize  = false;
            lblMFTypHdr.Font      = colHdrFont;
            lblMFTypHdr.ForeColor = ClrOlive;
            lblMFTypHdr.Location  = new System.Drawing.Point(mfTypX, mfRow);
            lblMFTypHdr.Size      = new System.Drawing.Size(70, 18);
            lblMFTypHdr.Text      = "Typical";

            lblMFMinHdr.AutoSize  = false;
            lblMFMinHdr.Font      = colHdrFont;
            lblMFMinHdr.ForeColor = ClrOlive;
            lblMFMinHdr.Location  = new System.Drawing.Point(mfMinX, mfRow);
            lblMFMinHdr.Size      = new System.Drawing.Size(55, 18);
            lblMFMinHdr.Text      = "Min";

            lblMFMaxHdr.AutoSize  = false;
            lblMFMaxHdr.Font      = colHdrFont;
            lblMFMaxHdr.ForeColor = ClrOlive;
            lblMFMaxHdr.Location  = new System.Drawing.Point(mfMaxX, mfRow);
            lblMFMaxHdr.Size      = new System.Drawing.Size(55, 18);
            lblMFMaxHdr.Text      = "Max";

            // Row: Mill Finish Sell Price
            mfRow += 20;
            lblMetalPrice.AutoSize  = false;
            lblMetalPrice.Font      = labelFont;
            lblMetalPrice.Location  = new System.Drawing.Point(mfLx, mfRow + 2);
            lblMetalPrice.Size      = new System.Drawing.Size(200, 18);
            lblMetalPrice.Text      = "Mill Finish Sell Price $/kg";
            txtMetalPrice.BackColor = ClrInputBg;
            txtMetalPrice.Font      = RegFont();
            txtMetalPrice.Location  = new System.Drawing.Point(mfTypX, mfRow);
            txtMetalPrice.Size      = new System.Drawing.Size(70, 23);

            lblMetalPriceMin.AutoSize  = false;
            lblMetalPriceMin.BackColor = ClrReadOnly;
            lblMetalPriceMin.Font      = RegFont();
            lblMetalPriceMin.Location  = new System.Drawing.Point(mfMinX, mfRow + 2);
            lblMetalPriceMin.Size      = new System.Drawing.Size(55, 18);
            lblMetalPriceMin.Text      = "—";

            lblMetalPriceMax.AutoSize  = false;
            lblMetalPriceMax.BackColor = ClrReadOnly;
            lblMetalPriceMax.Font      = RegFont();
            lblMetalPriceMax.Location  = new System.Drawing.Point(mfMaxX, mfRow + 2);
            lblMetalPriceMax.Size      = new System.Drawing.Size(55, 18);
            lblMetalPriceMax.Text      = "—";

            // Row: Margin %
            mfRow += mfRh;
            lblMillFinishMargin.AutoSize  = false;
            lblMillFinishMargin.Font      = labelFont;
            lblMillFinishMargin.Location  = new System.Drawing.Point(mfLx, mfRow + 2);
            lblMillFinishMargin.Size      = new System.Drawing.Size(200, 18);
            lblMillFinishMargin.Text      = "Margin %";
            lblMillFinishMarginValue.AutoSize  = false;
            lblMillFinishMarginValue.BackColor = ClrReadOnly;
            lblMillFinishMarginValue.Font      = headerFont;
            lblMillFinishMarginValue.ForeColor = System.Drawing.Color.DarkGreen;
            lblMillFinishMarginValue.Location  = new System.Drawing.Point(mfTypX, mfRow + 2);
            lblMillFinishMarginValue.Size      = new System.Drawing.Size(70, 18);
            lblMillFinishMarginValue.Text      = "0.00%";

            // Right section: IP Upcharges
            int ipX = 620, ipCx = 800, ipMrgX = 870, ipRow = 26, ipRh = 27;

            lblIpHdr.AutoSize  = true;
            lblIpHdr.Font      = BoldFont(9F);
            lblIpHdr.ForeColor = ClrOlive;
            lblIpHdr.Location  = new System.Drawing.Point(ipX, ipRow);
            lblIpHdr.Text      = "IP Upcharges";

            ipRow += 22;
            lblIPColHdr.AutoSize  = false;
            lblIPColHdr.Font      = colHdrFont;
            lblIPColHdr.ForeColor = ClrOlive;
            lblIPColHdr.Location  = new System.Drawing.Point(ipX, ipRow);
            lblIPColHdr.Size      = new System.Drawing.Size(165, 18);
            lblIPColHdr.Text      = "";

            lblIPTypHdr.AutoSize  = false;
            lblIPTypHdr.Font      = colHdrFont;
            lblIPTypHdr.ForeColor = ClrOlive;
            lblIPTypHdr.Location  = new System.Drawing.Point(ipCx, ipRow);
            lblIPTypHdr.Size      = new System.Drawing.Size(65, 18);
            lblIPTypHdr.Text      = "$/kg";

            // IP1
            ipRow += 20;
            lblIP1Upcharge.AutoSize  = false;
            lblIP1Upcharge.Font      = labelFont;
            lblIP1Upcharge.Location  = new System.Drawing.Point(ipX, ipRow + 2);
            lblIP1Upcharge.Size      = new System.Drawing.Size(165, 18);
            lblIP1Upcharge.Text      = "IP1 Upcharge $/kg:";
            txtIP1Upcharge.BackColor = ClrInputBg;
            txtIP1Upcharge.Font      = RegFont();
            txtIP1Upcharge.Location  = new System.Drawing.Point(ipCx, ipRow);
            txtIP1Upcharge.Size      = new System.Drawing.Size(60, 23);
            lblIP1Margin.AutoSize  = false;
            lblIP1Margin.Font      = labelFont;
            lblIP1Margin.Location  = new System.Drawing.Point(ipMrgX, ipRow + 2);
            lblIP1Margin.Size      = new System.Drawing.Size(60, 18);
            lblIP1Margin.Text      = "Margin:";
            lblIP1MarginValue.AutoSize  = false;
            lblIP1MarginValue.BackColor = ClrReadOnly;
            lblIP1MarginValue.Font      = RegFont();
            lblIP1MarginValue.Location  = new System.Drawing.Point(ipMrgX + 55, ipRow + 2);
            lblIP1MarginValue.Size      = new System.Drawing.Size(60, 18);
            lblIP1MarginValue.Text      = "0.00%";

            // IP2
            ipRow += ipRh;
            lblIP2Upcharge.AutoSize  = false;
            lblIP2Upcharge.Font      = labelFont;
            lblIP2Upcharge.Location  = new System.Drawing.Point(ipX, ipRow + 2);
            lblIP2Upcharge.Size      = new System.Drawing.Size(165, 18);
            lblIP2Upcharge.Text      = "IP2 Upcharge $/kg:";
            txtIP2Upcharge.BackColor = ClrInputBg;
            txtIP2Upcharge.Font      = RegFont();
            txtIP2Upcharge.Location  = new System.Drawing.Point(ipCx, ipRow);
            txtIP2Upcharge.Size      = new System.Drawing.Size(60, 23);
            lblIP2Margin.AutoSize  = false;
            lblIP2Margin.Font      = labelFont;
            lblIP2Margin.Location  = new System.Drawing.Point(ipMrgX, ipRow + 2);
            lblIP2Margin.Size      = new System.Drawing.Size(60, 18);
            lblIP2Margin.Text      = "Margin:";
            lblIP2MarginValue.AutoSize  = false;
            lblIP2MarginValue.BackColor = ClrReadOnly;
            lblIP2MarginValue.Font      = RegFont();
            lblIP2MarginValue.Location  = new System.Drawing.Point(ipMrgX + 55, ipRow + 2);
            lblIP2MarginValue.Size      = new System.Drawing.Size(60, 18);
            lblIP2MarginValue.Text      = "0.00%";

            // IP3
            ipRow += ipRh;
            lblIP3Rate.AutoSize  = false;
            lblIP3Rate.Font      = labelFont;
            lblIP3Rate.Location  = new System.Drawing.Point(ipX, ipRow + 2);
            lblIP3Rate.Size      = new System.Drawing.Size(165, 18);
            lblIP3Rate.Text      = "IP3 – Panoramic Fixed $/kg:";
            txtIP3Rate.BackColor = ClrInputBg;
            txtIP3Rate.Font      = RegFont();
            txtIP3Rate.Location  = new System.Drawing.Point(ipCx, ipRow);
            txtIP3Rate.Size      = new System.Drawing.Size(60, 23);
            lblIP3Margin.AutoSize  = false;
            lblIP3Margin.Font      = labelFont;
            lblIP3Margin.Location  = new System.Drawing.Point(ipMrgX, ipRow + 2);
            lblIP3Margin.Size      = new System.Drawing.Size(60, 18);
            lblIP3Margin.Text      = "Margin:";
            lblIP3MarginValue.AutoSize  = false;
            lblIP3MarginValue.BackColor = ClrReadOnly;
            lblIP3MarginValue.Font      = RegFont();
            lblIP3MarginValue.Location  = new System.Drawing.Point(ipMrgX + 55, ipRow + 2);
            lblIP3MarginValue.Size      = new System.Drawing.Size(60, 18);
            lblIP3MarginValue.Text      = "0.00%";

            grpMillFinishPricing.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblSapDiscountHeader, txtCustomerSapDiscount,
                lblMFColHdr, lblMFTypHdr, lblMFMinHdr, lblMFMaxHdr,
                lblMetalPrice, txtMetalPrice, lblMetalPriceMin, lblMetalPriceMax,
                lblMillFinishMargin, lblMillFinishMarginValue,
                lblIpHdr, lblIPColHdr, lblIPTypHdr,
                lblIP1Upcharge, txtIP1Upcharge, lblIP1Margin, lblIP1MarginValue,
                lblIP2Upcharge, txtIP2Upcharge, lblIP2Margin, lblIP2MarginValue,
                lblIP3Rate, txtIP3Rate, lblIP3Margin, lblIP3MarginValue
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 5 – Thermal Break Pricing
            // ═════════════════════════════════════════════════════════════════
            gY += 171;
            grpThermalBreakPricing.Font      = oliveFont;
            grpThermalBreakPricing.ForeColor = ClrOlive;
            grpThermalBreakPricing.Location  = new System.Drawing.Point(18, gY);
            grpThermalBreakPricing.Size      = new System.Drawing.Size(1010, 105);
            grpThermalBreakPricing.Text      = "Thermal Break Pricing";

            int tbLx = 12, tbCx = 270, tbTypX = 360, tbRow = 26, tbRh = 27;

            lblTBCalcType.AutoSize  = false;
            lblTBCalcType.Font      = labelFont;
            lblTBCalcType.Location  = new System.Drawing.Point(tbLx, tbRow + 2);
            lblTBCalcType.Size      = new System.Drawing.Size(220, 18);
            lblTBCalcType.Text      = "Thermal Break Method:";
            cmbTBCalcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbTBCalcType.BackColor     = ClrInputBg;
            cmbTBCalcType.Font          = RegFont();
            cmbTBCalcType.Location      = new System.Drawing.Point(tbCx, tbRow - 2);
            cmbTBCalcType.Size          = new System.Drawing.Size(200, 23);

            tbRow += tbRh + 4;
            lblTBTypHdr.AutoSize  = false;
            lblTBTypHdr.Font      = colHdrFont;
            lblTBTypHdr.ForeColor = ClrOlive;
            lblTBTypHdr.Location  = new System.Drawing.Point(tbTypX, tbRow);
            lblTBTypHdr.Size      = new System.Drawing.Size(70, 18);
            lblTBTypHdr.Text      = "Typical";

            tbRow += 20;
            lblTBMargin.AutoSize  = false;
            lblTBMargin.Font      = labelFont;
            lblTBMargin.Location  = new System.Drawing.Point(tbLx, tbRow + 2);
            lblTBMargin.Size      = new System.Drawing.Size(220, 18);
            lblTBMargin.Text      = "Enter Margin %:";
            txtTBMargin.BackColor = ClrInputBg;
            txtTBMargin.Font      = RegFont();
            txtTBMargin.Location  = new System.Drawing.Point(tbTypX, tbRow);
            txtTBMargin.Size      = new System.Drawing.Size(70, 23);

            grpThermalBreakPricing.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTBCalcType, cmbTBCalcType,
                lblTBTypHdr,
                lblTBMargin, txtTBMargin
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 6 – Powder / Finishing Pricing
            // ═════════════════════════════════════════════════════════════════
            gY += 111;
            grpFinishingPricing.Font      = oliveFont;
            grpFinishingPricing.ForeColor = ClrOlive;
            grpFinishingPricing.Location  = new System.Drawing.Point(18, gY);
            grpFinishingPricing.Size      = new System.Drawing.Size(1010, 320);
            grpFinishingPricing.Text      = "Powder / Finishing Pricing";

            int fcLx = 12, fcCx = 290, fcTypX = 400, fcMinX = 478, fcMaxX = 550;
            int fcRow = 26, fcRh = 27;

            // Row 1: Powder Priced as / Cost Type
            lblPowderPricedAs.AutoSize = true;
            lblPowderPricedAs.Font     = labelFont;
            lblPowderPricedAs.Location = new System.Drawing.Point(fcLx, fcRow + 2);
            lblPowderPricedAs.Text     = "Powder Priced as:";
            cmbPowderPricedAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbPowderPricedAs.BackColor     = ClrInputBg;
            cmbPowderPricedAs.Font          = RegFont();
            cmbPowderPricedAs.Location      = new System.Drawing.Point(fcCx - 130, fcRow - 2);
            cmbPowderPricedAs.Size          = new System.Drawing.Size(200, 23);

            lblCostType.AutoSize = true;
            lblCostType.Font     = labelFont;
            lblCostType.Location = new System.Drawing.Point(fcCx + 85, fcRow + 2);
            lblCostType.Text     = "Cost Type:";
            cmbCostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCostType.BackColor     = ClrInputBg;
            cmbCostType.Font          = RegFont();
            cmbCostType.Location      = new System.Drawing.Point(fcCx + 185, fcRow - 2);
            cmbCostType.Size          = new System.Drawing.Size(220, 23);

            chkCostTypeOverride.AutoSize = true;
            chkCostTypeOverride.Font     = labelFont;
            chkCostTypeOverride.Location = new System.Drawing.Point(fcCx + 420, fcRow + 2);
            chkCostTypeOverride.Text     = "Override";

            btnToggleOverride.Location  = new System.Drawing.Point(fcCx + 510, fcRow - 2);
            btnToggleOverride.Size      = new System.Drawing.Size(120, 26);
            btnToggleOverride.Text      = "Toggle Override";
            btnToggleOverride.Click    += new System.EventHandler(btnToggleOverride_Click);

            // Section header
            fcRow += fcRh + 4;
            lblStdFinishRatesHdr.AutoSize  = true;
            lblStdFinishRatesHdr.Font      = BoldFont(8.5F);
            lblStdFinishRatesHdr.ForeColor = ClrOlive;
            lblStdFinishRatesHdr.Location  = new System.Drawing.Point(fcLx, fcRow);
            lblStdFinishRatesHdr.Text      = "STANDARD (non-mill) FINISHING RATES (powdercoat or anodised)";

            // Column headers
            fcRow += 20;
            lblFCColHdr.AutoSize  = false;
            lblFCColHdr.Font      = colHdrFont;
            lblFCColHdr.ForeColor = ClrOlive;
            lblFCColHdr.Location  = new System.Drawing.Point(fcLx, fcRow);
            lblFCColHdr.Size      = new System.Drawing.Size(280, 18);
            lblFCColHdr.Text      = "";

            lblFCTypHdr.AutoSize  = false;
            lblFCTypHdr.Font      = colHdrFont;
            lblFCTypHdr.ForeColor = ClrOlive;
            lblFCTypHdr.Location  = new System.Drawing.Point(fcTypX, fcRow);
            lblFCTypHdr.Size      = new System.Drawing.Size(70, 18);
            lblFCTypHdr.Text      = "Typical";

            lblFCMinHdr.AutoSize  = false;
            lblFCMinHdr.Font      = colHdrFont;
            lblFCMinHdr.ForeColor = ClrOlive;
            lblFCMinHdr.Location  = new System.Drawing.Point(fcMinX, fcRow);
            lblFCMinHdr.Size      = new System.Drawing.Size(55, 18);
            lblFCMinHdr.Text      = "Min";

            lblFCMaxHdr.AutoSize  = false;
            lblFCMaxHdr.Font      = colHdrFont;
            lblFCMaxHdr.ForeColor = ClrOlive;
            lblFCMaxHdr.Location  = new System.Drawing.Point(fcMaxX, fcRow);
            lblFCMaxHdr.Size      = new System.Drawing.Size(55, 18);
            lblFCMaxHdr.Text      = "Max";

            // Row: Finishing Cost Price $/m² (computed)
            fcRow += 20;
            lblFinishingCostPricePerM2.AutoSize  = false;
            lblFinishingCostPricePerM2.Font      = labelFont;
            lblFinishingCostPricePerM2.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblFinishingCostPricePerM2.Size      = new System.Drawing.Size(280, 18);
            lblFinishingCostPricePerM2.Text      = "Finishing Cost Price $/m²";
            txtFinishingCostPricePerM2.BackColor = ClrInputBg;
            txtFinishingCostPricePerM2.Font      = RegFont();
            txtFinishingCostPricePerM2.Location  = new System.Drawing.Point(fcTypX, fcRow);
            txtFinishingCostPricePerM2.Size      = new System.Drawing.Size(70, 23);

            // Row: POA/Other Vendor Cost $/m² (Price_B42 backing field – visible)
            fcRow += fcRh;
            lblB42.AutoSize  = false;
            lblB42.Font      = labelFont;
            lblB42.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblB42.Size      = new System.Drawing.Size(280, 18);
            lblB42.Text      = "POA/Other Vendor Cost $/m²";
            txtB42.BackColor = ClrInputBg;
            txtB42.Font      = RegFont();
            txtB42.Location  = new System.Drawing.Point(fcTypX, fcRow);
            txtB42.Size      = new System.Drawing.Size(70, 23);

            // Row: Finishing Sell Price $/m²
            fcRow += fcRh;
            lblFinishingPrice.AutoSize  = false;
            lblFinishingPrice.Font      = labelFont;
            lblFinishingPrice.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblFinishingPrice.Size      = new System.Drawing.Size(280, 18);
            lblFinishingPrice.Text      = "Finishing Sell Price $/m²";
            txtFinishingPrice.BackColor = ClrInputBg;
            txtFinishingPrice.Font      = RegFont();
            txtFinishingPrice.Location  = new System.Drawing.Point(fcTypX, fcRow);
            txtFinishingPrice.Size      = new System.Drawing.Size(70, 23);

            // Row: Finishing Sell Price override $/m² (Price_B44 – for POA/custom scenarios)
            fcRow += fcRh;
            lblB44.AutoSize  = false;
            lblB44.Font      = labelFont;
            lblB44.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblB44.Size      = new System.Drawing.Size(280, 18);
            lblB44.Text      = "Finishing Sell Price override $/m²";
            txtB44.BackColor = ClrInputBg;
            txtB44.Font      = RegFont();
            txtB44.Location  = new System.Drawing.Point(fcTypX, fcRow);
            txtB44.Size      = new System.Drawing.Size(70, 23);

            // Row: Margin %
            fcRow += fcRh;
            lblFinishingMargin.AutoSize  = false;
            lblFinishingMargin.Font      = labelFont;
            lblFinishingMargin.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblFinishingMargin.Size      = new System.Drawing.Size(280, 18);
            lblFinishingMargin.Text      = "Margin %";
            lblFinishingMarginValue.AutoSize  = false;
            lblFinishingMarginValue.BackColor = ClrReadOnly;
            lblFinishingMarginValue.Font      = headerFont;
            lblFinishingMarginValue.ForeColor = System.Drawing.Color.DarkGreen;
            lblFinishingMarginValue.Location  = new System.Drawing.Point(fcTypX, fcRow + 2);
            lblFinishingMarginValue.Size      = new System.Drawing.Size(70, 18);
            lblFinishingMarginValue.Text      = "0.00%";

            // ── Bulk / Non-bulk sub-section ────────────────────────────────────
            fcRow += fcRh + 8;
            lblBulkHdr.AutoSize  = true;
            lblBulkHdr.Font      = BoldFont(8.5F);
            lblBulkHdr.ForeColor = ClrOlive;
            lblBulkHdr.Location  = new System.Drawing.Point(fcLx, fcRow);
            lblBulkHdr.Text      = "Bulk / Non-bulk Rates (AP/PP)";

            fcRow += 20;
            lblB48.AutoSize  = false;
            lblB48.Font      = labelFont;
            lblB48.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblB48.Size      = new System.Drawing.Size(240, 18);
            lblB48.Text      = "Mill BULK POA Cost $/m²:";
            txtB48.BackColor = ClrInputBg;
            txtB48.Font      = RegFont();
            txtB48.Location  = new System.Drawing.Point(fcTypX, fcRow);
            txtB48.Size      = new System.Drawing.Size(70, 23);

            lblB50.AutoSize  = false;
            lblB50.Font      = labelFont;
            lblB50.Location  = new System.Drawing.Point(fcLx + 550, fcRow + 2);
            lblB50.Size      = new System.Drawing.Size(240, 18);
            lblB50.Text      = "Mill BULK Sell Price $/m²:";
            txtB50.BackColor = ClrInputBg;
            txtB50.Font      = RegFont();
            txtB50.Location  = new System.Drawing.Point(fcTypX + 550, fcRow);
            txtB50.Size      = new System.Drawing.Size(70, 23);

            fcRow += fcRh;
            lblB52.AutoSize  = false;
            lblB52.Font      = labelFont;
            lblB52.Location  = new System.Drawing.Point(fcLx, fcRow + 2);
            lblB52.Size      = new System.Drawing.Size(240, 18);
            lblB52.Text      = "Mill NON-Bulk POA Cost $/m²:";
            txtB52.BackColor = ClrInputBg;
            txtB52.Font      = RegFont();
            txtB52.Location  = new System.Drawing.Point(fcTypX, fcRow);
            txtB52.Size      = new System.Drawing.Size(70, 23);

            lblB54.AutoSize  = false;
            lblB54.Font      = labelFont;
            lblB54.Location  = new System.Drawing.Point(fcLx + 550, fcRow + 2);
            lblB54.Size      = new System.Drawing.Size(240, 18);
            lblB54.Text      = "Mill NON-Bulk Sell Price $/m²:";
            txtB54.BackColor = ClrInputBg;
            txtB54.Font      = RegFont();
            txtB54.Location  = new System.Drawing.Point(fcTypX + 550, fcRow);
            txtB54.Size      = new System.Drawing.Size(70, 23);

            grpFinishingPricing.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblPowderPricedAs, cmbPowderPricedAs,
                lblCostType, cmbCostType, chkCostTypeOverride, btnToggleOverride,
                lblStdFinishRatesHdr,
                lblFCColHdr, lblFCTypHdr, lblFCMinHdr, lblFCMaxHdr,
                lblFinishingCostPricePerM2, txtFinishingCostPricePerM2,
                lblB42, txtB42,
                lblFinishingPrice, txtFinishingPrice,
                lblB44, txtB44,
                lblFinishingMargin, lblFinishingMarginValue,
                lblBulkHdr,
                lblB48, txtB48, lblB50, txtB50,
                lblB52, txtB52, lblB54, txtB54
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 7 – Anodised Thresholds
            // ═════════════════════════════════════════════════════════════════
            gY += 326;
            grpAnodisedThresholds.Font      = oliveFont;
            grpAnodisedThresholds.ForeColor = ClrOlive;
            grpAnodisedThresholds.Location  = new System.Drawing.Point(18, gY);
            grpAnodisedThresholds.Size      = new System.Drawing.Size(1010, 135);
            grpAnodisedThresholds.Text      = "Anodised Thresholds";

            int atLx = 12, atCx = 260, atRow = 26, atRh = 27;

            lblAnodisedThresholdCost.AutoSize  = false;
            lblAnodisedThresholdCost.Font      = labelFont;
            lblAnodisedThresholdCost.Location  = new System.Drawing.Point(atLx, atRow + 2);
            lblAnodisedThresholdCost.Size      = new System.Drawing.Size(240, 18);
            lblAnodisedThresholdCost.Text      = "Finishing Cost Price $/m² (auto):";
            txtAnodisedThresholdCost.BackColor = ClrInputBg;
            txtAnodisedThresholdCost.Font      = RegFont();
            txtAnodisedThresholdCost.Location  = new System.Drawing.Point(atCx, atRow - 2);
            txtAnodisedThresholdCost.Size      = new System.Drawing.Size(80, 23);

            lblB57.AutoSize  = false;
            lblB57.Font      = labelFont;
            lblB57.Location  = new System.Drawing.Point(atCx + 110, atRow + 2);
            lblB57.Size      = new System.Drawing.Size(200, 18);
            lblB57.Text      = "Threshold Cost Price $/m² (B57):";
            txtB57.BackColor = ClrInputBg;
            txtB57.Font      = RegFont();
            txtB57.Location  = new System.Drawing.Point(atCx + 320, atRow - 2);
            txtB57.Size      = new System.Drawing.Size(80, 23);

            atRow += atRh;
            lblB58.AutoSize  = false;
            lblB58.Font      = labelFont;
            lblB58.Location  = new System.Drawing.Point(atLx, atRow + 2);
            lblB58.Size      = new System.Drawing.Size(240, 18);
            lblB58.Text      = "Finishing Sell Price $/m²:";
            txtB58.BackColor = ClrInputBg;
            txtB58.Font      = RegFont();
            txtB58.Location  = new System.Drawing.Point(atCx, atRow - 2);
            txtB58.Size      = new System.Drawing.Size(80, 23);

            atRow += atRh;
            lblThresholdMargin.AutoSize  = false;
            lblThresholdMargin.Font      = labelFont;
            lblThresholdMargin.Location  = new System.Drawing.Point(atLx, atRow + 2);
            lblThresholdMargin.Size      = new System.Drawing.Size(240, 18);
            lblThresholdMargin.Text      = "Margin %:";
            lblThresholdMarginValue.AutoSize  = false;
            lblThresholdMarginValue.BackColor = ClrReadOnly;
            lblThresholdMarginValue.Font      = headerFont;
            lblThresholdMarginValue.ForeColor = System.Drawing.Color.DarkGreen;
            lblThresholdMarginValue.Location  = new System.Drawing.Point(atCx, atRow + 2);
            lblThresholdMarginValue.Size      = new System.Drawing.Size(80, 18);
            lblThresholdMarginValue.Text      = "0.00%";

            btnBulkNonBulkRates.Location = new System.Drawing.Point(atLx + 550, atRow - 2);
            btnBulkNonBulkRates.Size     = new System.Drawing.Size(155, 26);
            btnBulkNonBulkRates.Text     = "Bulk/Non-bulk Rates";
            btnBulkNonBulkRates.Click   += new System.EventHandler(btnBulkNonBulkRates_Click);

            grpAnodisedThresholds.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblAnodisedThresholdCost, txtAnodisedThresholdCost,
                lblB57, txtB57,
                lblB58, txtB58,
                lblThresholdMargin, lblThresholdMarginValue,
                btnBulkNonBulkRates
            });

            // ═════════════════════════════════════════════════════════════════
            // GROUP 8 – Quote Summary
            // ═════════════════════════════════════════════════════════════════
            gY += 141;
            grpSummary.Font      = oliveFont;
            grpSummary.ForeColor = ClrOlive;
            grpSummary.Location  = new System.Drawing.Point(18, gY);
            grpSummary.Size      = new System.Drawing.Size(1010, 220);
            grpSummary.Text      = "Quote Summary";

            gridSummary.AllowUserToAddRows    = false;
            gridSummary.AllowUserToDeleteRows = false;
            gridSummary.AutoGenerateColumns   = false;
            gridSummary.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridSummary.BackgroundColor       = System.Drawing.SystemColors.Control;
            gridSummary.BorderStyle           = System.Windows.Forms.BorderStyle.Fixed3D;
            gridSummary.ColumnHeadersDefaultCellStyle.BackColor = ClrOliveRow;
            gridSummary.ColumnHeadersDefaultCellStyle.Font      = colHdrFont;
            gridSummary.ColumnHeadersDefaultCellStyle.ForeColor = ClrOlive;
            gridSummary.Location              = new System.Drawing.Point(12, 24);
            gridSummary.ReadOnly              = true;
            gridSummary.RowHeadersVisible     = false;
            gridSummary.Size                  = new System.Drawing.Size(980, 155);

            colCategory.DataPropertyName = "Category";
            colCategory.HeaderText       = "Category";
            colCategory.FillWeight       = 25;
            colCategory.DefaultCellStyle.Font = headerFont;

            colTotal.DataPropertyName = "TotalCost";
            colTotal.HeaderText       = "Total (Cost)";
            colTotal.FillWeight       = 18;
            colTotal.DefaultCellStyle.Format    = "C2";
            colTotal.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            colTotalValue.DataPropertyName = "TotalSellValue";
            colTotalValue.HeaderText       = "Total Value";
            colTotalValue.FillWeight       = 18;
            colTotalValue.DefaultCellStyle.Format    = "C2";
            colTotalValue.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            colMarginDollar.DataPropertyName = "MarginDollar";
            colMarginDollar.HeaderText       = "Margin $";
            colMarginDollar.FillWeight       = 18;
            colMarginDollar.DefaultCellStyle.Format    = "C2";
            colMarginDollar.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            colMarginPct.DataPropertyName = "MarginPct";
            colMarginPct.HeaderText       = "Margin %";
            colMarginPct.FillWeight       = 12;
            colMarginPct.DefaultCellStyle.Format    = "F2";
            colMarginPct.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            gridSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                { colCategory, colTotal, colTotalValue, colMarginDollar, colMarginPct });

            lblSummaryNote1.AutoSize  = true;
            lblSummaryNote1.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            lblSummaryNote1.ForeColor = System.Drawing.Color.Gray;
            lblSummaryNote1.Location  = new System.Drawing.Point(12, 185);
            lblSummaryNote1.Text      = "* Margin $ also includes IP charge";

            lblSummaryNote2.AutoSize  = true;
            lblSummaryNote2.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            lblSummaryNote2.ForeColor = System.Drawing.Color.Gray;
            lblSummaryNote2.Location  = new System.Drawing.Point(12, 200);
            lblSummaryNote2.Text      = "* Sundry costs based on sundry costs entered and slot costs estimated";

            grpSummary.Controls.AddRange(new System.Windows.Forms.Control[]
                { gridSummary, lblSummaryNote1, lblSummaryNote2 });

            // ─────────────────────────────────────────────────────────────────
            // Action buttons row
            // ─────────────────────────────────────────────────────────────────
            gY += 226;
            int btnX = 18;

            btnEditSundryItems.Location = new System.Drawing.Point(btnX, gY);
            btnEditSundryItems.Size     = new System.Drawing.Size(130, 30);
            btnEditSundryItems.Text     = "Edit Sundry Items";
            btnEditSundryItems.Click   += new System.EventHandler(btnEditSundryItems_Click);
            btnX += 140;

            btnRecalculateAll.Location = new System.Drawing.Point(btnX, gY);
            btnRecalculateAll.Size     = new System.Drawing.Size(120, 30);
            btnRecalculateAll.Text     = "Recalculate All";
            btnRecalculateAll.Click   += new System.EventHandler(btnRecalculateAll_Click);
            btnX += 130;

            btnSave.BackColor          = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSave.Font               = BoldFont();
            btnSave.ForeColor          = System.Drawing.Color.White;
            btnSave.Location           = new System.Drawing.Point(btnX, gY - 2);
            btnSave.Size               = new System.Drawing.Size(110, 34);
            btnSave.Text               = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click             += new System.EventHandler(btnSave_Click);
            btnX += 120;

            btnRefreshSummary.BackColor = System.Drawing.Color.FromArgb(0, 128, 64);
            btnRefreshSummary.Font      = BoldFont();
            btnRefreshSummary.ForeColor = System.Drawing.Color.White;
            btnRefreshSummary.Location  = new System.Drawing.Point(btnX, gY - 2);
            btnRefreshSummary.Size      = new System.Drawing.Size(150, 34);
            btnRefreshSummary.Text      = "Refresh Summary";
            btnRefreshSummary.UseVisualStyleBackColor = false;
            btnRefreshSummary.Click    += new System.EventHandler(btnRefreshSummary_Click);
            btnX += 160;

            btnUpdateVisibility.Location = new System.Drawing.Point(btnX, gY);
            btnUpdateVisibility.Size     = new System.Drawing.Size(145, 30);
            btnUpdateVisibility.Text     = "Update Row Visibility";
            btnUpdateVisibility.Click   += new System.EventHandler(btnUpdateVisibility_Click);

            // ─────────────────────────────────────────────────────────────────
            // Row visibility group (developer/admin panel)
            // ─────────────────────────────────────────────────────────────────
            gY += 42;
            grpRowVisibility.Font      = RegFont(9F);
            grpRowVisibility.ForeColor = System.Drawing.Color.Gray;
            grpRowVisibility.Location  = new System.Drawing.Point(18, gY);
            grpRowVisibility.Size      = new System.Drawing.Size(1010, 80);
            grpRowVisibility.Text      = "Row Visibility (advanced)";

            lblVisibleRows.AutoSize = false;
            lblVisibleRows.Font     = RegFont(8F);
            lblVisibleRows.Location = new System.Drawing.Point(10, 22);
            lblVisibleRows.Size     = new System.Drawing.Size(985, 20);
            lblVisibleRows.Text     = "Visible rows: —";

            lblHiddenRows.AutoSize = false;
            lblHiddenRows.Font     = RegFont(8F);
            lblHiddenRows.Location = new System.Drawing.Point(10, 46);
            lblHiddenRows.Size     = new System.Drawing.Size(985, 20);
            lblHiddenRows.Text     = "Hidden rows: —";

            grpRowVisibility.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblVisibleRows, lblHiddenRows });

            // ─────────────────────────────────────────────────────────────────
            // pnlScroll – add all top-level controls
            // ─────────────────────────────────────────────────────────────────
            pnlScroll.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTitle,
                grpHeader,
                grpFinishSection,
                grpQuantitiesSection,
                grpMillFinishPricing,
                grpThermalBreakPricing,
                grpFinishingPricing,
                grpAnodisedThresholds,
                grpSummary,
                btnEditSundryItems,
                btnRecalculateAll,
                btnSave,
                btnRefreshSummary,
                btnUpdateVisibility,
                grpRowVisibility
            });

            // ─────────────────────────────────────────────────────────────────
            // Form settings
            // ─────────────────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(1050, 760);
            Controls.Add(pnlScroll);
            Name  = "EditPricingForm";
            Text  = "Edit Pricing";

            ((System.ComponentModel.ISupportInitialize)gridSummary).EndInit();
            grpAnodisedThresholds.ResumeLayout(false);
            grpAnodisedThresholds.PerformLayout();
            grpSummary.ResumeLayout(false);
            grpSummary.PerformLayout();
            grpFinishingPricing.ResumeLayout(false);
            grpFinishingPricing.PerformLayout();
            grpThermalBreakPricing.ResumeLayout(false);
            grpThermalBreakPricing.PerformLayout();
            grpMillFinishPricing.ResumeLayout(false);
            grpMillFinishPricing.PerformLayout();
            grpQuantitiesSection.ResumeLayout(false);
            grpQuantitiesSection.PerformLayout();
            grpFinishSection.ResumeLayout(false);
            grpFinishSection.PerformLayout();
            grpHeader.ResumeLayout(false);
            grpHeader.PerformLayout();
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
