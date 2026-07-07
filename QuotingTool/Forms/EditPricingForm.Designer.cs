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
            pnlScroll = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblPricingNotes = new System.Windows.Forms.Label();
            txtPricingNotes = new System.Windows.Forms.TextBox();
            lblCostType = new System.Windows.Forms.Label();
            cmbCostType = new System.Windows.Forms.ComboBox();
            lblFinType = new System.Windows.Forms.Label();
            cmbFinType = new System.Windows.Forms.ComboBox();
            chkCostTypeOverride = new System.Windows.Forms.CheckBox();
            btnToggleOverride = new System.Windows.Forms.Button();
            lblMetalPrice = new System.Windows.Forms.Label();
            txtMetalPrice = new System.Windows.Forms.TextBox();
            lblFinishingPrice = new System.Windows.Forms.Label();
            txtFinishingPrice = new System.Windows.Forms.TextBox();
            lblFinishingCostPricePerM2 = new System.Windows.Forms.Label();
            txtFinishingCostPricePerM2 = new System.Windows.Forms.TextBox();
            lblB42 = new System.Windows.Forms.Label();
            txtB42 = new System.Windows.Forms.TextBox();
            lblB44 = new System.Windows.Forms.Label();
            txtB44 = new System.Windows.Forms.TextBox();
            lblB48 = new System.Windows.Forms.Label();
            txtB48 = new System.Windows.Forms.TextBox();
            lblB50 = new System.Windows.Forms.Label();
            txtB50 = new System.Windows.Forms.TextBox();
            lblB52 = new System.Windows.Forms.Label();
            txtB52 = new System.Windows.Forms.TextBox();
            lblB54 = new System.Windows.Forms.Label();
            txtB54 = new System.Windows.Forms.TextBox();
            lblB57 = new System.Windows.Forms.Label();
            txtB57 = new System.Windows.Forms.TextBox();
            lblB58 = new System.Windows.Forms.Label();
            txtB58 = new System.Windows.Forms.TextBox();
            lblTBCalcType = new System.Windows.Forms.Label();
            cmbTBCalcType = new System.Windows.Forms.ComboBox();
            lblTBMargin = new System.Windows.Forms.Label();
            txtTBMargin = new System.Windows.Forms.TextBox();
            grpTotals = new System.Windows.Forms.GroupBox();
            lblTotalKg = new System.Windows.Forms.Label();
            lblTotalKgValue = new System.Windows.Forms.Label();
            lblTotalFinishingM2 = new System.Windows.Forms.Label();
            lblTotalFinishingM2Value = new System.Windows.Forms.Label();
            lblTotalPieces = new System.Windows.Forms.Label();
            lblTotalPiecesValue = new System.Windows.Forms.Label();
            grpIp = new System.Windows.Forms.GroupBox();
            lblCustomerSapDiscount = new System.Windows.Forms.Label();
            txtCustomerSapDiscount = new System.Windows.Forms.TextBox();
            lblIP1Upcharge = new System.Windows.Forms.Label();
            txtIP1Upcharge = new System.Windows.Forms.TextBox();
            lblIP1Margin = new System.Windows.Forms.Label();
            lblIP1MarginValue = new System.Windows.Forms.Label();
            lblIP2Upcharge = new System.Windows.Forms.Label();
            txtIP2Upcharge = new System.Windows.Forms.TextBox();
            lblIP2Margin = new System.Windows.Forms.Label();
            lblIP2MarginValue = new System.Windows.Forms.Label();
            lblIP3Rate = new System.Windows.Forms.Label();
            txtIP3Rate = new System.Windows.Forms.TextBox();
            lblIP3Margin = new System.Windows.Forms.Label();
            lblIP3MarginValue = new System.Windows.Forms.Label();
            btnEditIpCategories = new System.Windows.Forms.Button();
            btnResetIpCharges = new System.Windows.Forms.Button();
            grpMargins = new System.Windows.Forms.GroupBox();
            lblMillFinishMargin = new System.Windows.Forms.Label();
            lblMillFinishMarginValue = new System.Windows.Forms.Label();
            lblFinishingMargin = new System.Windows.Forms.Label();
            lblFinishingMarginValue = new System.Windows.Forms.Label();
            lblThresholdMargin = new System.Windows.Forms.Label();
            lblThresholdMarginValue = new System.Windows.Forms.Label();
            lblAnodisedThresholdCost = new System.Windows.Forms.Label();
            txtAnodisedThresholdCost = new System.Windows.Forms.TextBox();
            grpSummary = new System.Windows.Forms.GroupBox();
            gridSummary = new System.Windows.Forms.DataGridView();
            lblSummaryNote1 = new System.Windows.Forms.Label();
            lblSummaryNote2 = new System.Windows.Forms.Label();
            btnBulkNonBulkRates = new System.Windows.Forms.Button();
            btnEditSundryItems = new System.Windows.Forms.Button();
            btnRecalculateAll = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            btnUpdateVisibility = new System.Windows.Forms.Button();
            grpRowVisibility = new System.Windows.Forms.GroupBox();
            lblVisibleRows = new System.Windows.Forms.Label();
            lblHiddenRows = new System.Windows.Forms.Label();
            grpTotals.SuspendLayout();
            grpIp.SuspendLayout();
            grpMargins.SuspendLayout();
            grpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridSummary).BeginInit();
            grpRowVisibility.SuspendLayout();
            pnlScroll.SuspendLayout();
            SuspendLayout();

            pnlScroll.AutoScroll = true;
            pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(pnlScroll);

            int x1 = 20, x2 = 280, w = 140, h = 30, y = 15;

            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(x1, y);
            lblTitle.Size = new System.Drawing.Size(350, 28);
            lblTitle.Text = "3. Edit Pricing";
            pnlScroll.Controls.Add(lblTitle);
            y += 40;

            lblPricingNotes.AutoSize = true;
            lblPricingNotes.Location = new System.Drawing.Point(x1, y + 3);
            lblPricingNotes.Text = "Pricing Notes:";
            txtPricingNotes.Location = new System.Drawing.Point(x2, y);
            txtPricingNotes.Multiline = true;
            txtPricingNotes.Size = new System.Drawing.Size(320, 60);
            pnlScroll.Controls.Add(lblPricingNotes);
            pnlScroll.Controls.Add(txtPricingNotes);
            y += 70;

            AddComboRow(lblCostType, cmbCostType, "Cost Type:", x1, x2, 280, ref y, h, pnlScroll, true);
            AddComboRow(lblFinType, cmbFinType, "Finish Type:", x1, x2, 280, ref y, h, pnlScroll, true);

            chkCostTypeOverride.AutoSize = true;
            chkCostTypeOverride.Location = new System.Drawing.Point(x1, y + 3);
            chkCostTypeOverride.Text = "Cost Type Override";
            btnToggleOverride.Location = new System.Drawing.Point(x2, y);
            btnToggleOverride.Size = new System.Drawing.Size(140, 26);
            btnToggleOverride.Text = "Toggle Override";
            btnToggleOverride.Click += new System.EventHandler(btnToggleOverride_Click);
            pnlScroll.Controls.Add(chkCostTypeOverride);
            pnlScroll.Controls.Add(btnToggleOverride);
            y += h;

            AddTextRow(lblMetalPrice, txtMetalPrice, "Mill Finish Price $/kg:", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblFinishingPrice, txtFinishingPrice, "Finishing Price $/m2:", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblFinishingCostPricePerM2, txtFinishingCostPricePerM2, "Finishing Cost Price $/m2:", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB42, txtB42, "POA/Other Vendor Cost (B42):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB44, txtB44, "Finishing Sell Price (B44):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB48, txtB48, "Mill BULK POA Cost (B48):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB50, txtB50, "Mill BULK Sell Price (B50):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB52, txtB52, "Mill NON-Bulk POA Cost (B52):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB54, txtB54, "Mill NON-Bulk Sell Price (B54):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB57, txtB57, "Anodised Threshold Cost (B57):", x1, x2, w, ref y, h, pnlScroll);
            AddTextRow(lblB58, txtB58, "Anodised Threshold Sell Price (B58):", x1, x2, w, ref y, h, pnlScroll);
            AddComboRow(lblTBCalcType, cmbTBCalcType, "Thermal Break Calc Type:", x1, x2, 200, ref y, h, pnlScroll, true);
            AddTextRow(lblTBMargin, txtTBMargin, "Thermal Break Margin/Discount:", x1, x2, w, ref y, h, pnlScroll);
            y += 5;

            grpTotals.Location = new System.Drawing.Point(x1, y);
            grpTotals.Size = new System.Drawing.Size(430, 110);
            grpTotals.Text = "Calculated Totals";
            ConfigureValueRow(lblTotalKg, lblTotalKgValue, "Total kg:", 15, 180, 28, grpTotals);
            ConfigureValueRow(lblTotalFinishingM2, lblTotalFinishingM2Value, "Total Finishing m2:", 15, 180, 53, grpTotals);
            ConfigureValueRow(lblTotalPieces, lblTotalPiecesValue, "Total Pieces:", 15, 180, 78, grpTotals);
            pnlScroll.Controls.Add(grpTotals);

            grpIp.Location = new System.Drawing.Point(470, y);
            grpIp.Size = new System.Drawing.Size(450, 190);
            grpIp.Text = "IP Upcharges";
            ConfigureTextRow(lblCustomerSapDiscount, txtCustomerSapDiscount, "Customer SAP Discount %:", 15, 210, 90, 28, grpIp);
            ConfigureTextRow(lblIP1Upcharge, txtIP1Upcharge, "IP1 Upcharge $/kg:", 15, 210, 90, 58, grpIp);
            ConfigureValueRow(lblIP1Margin, lblIP1MarginValue, "IP1 Margin %:", 320, 390, 58, grpIp);
            ConfigureTextRow(lblIP2Upcharge, txtIP2Upcharge, "IP2 Upcharge $/kg:", 15, 210, 90, 88, grpIp);
            ConfigureValueRow(lblIP2Margin, lblIP2MarginValue, "IP2 Margin %:", 320, 390, 88, grpIp);
            ConfigureTextRow(lblIP3Rate, txtIP3Rate, "IP3 Panoramic Fixed $/kg:", 15, 210, 90, 118, grpIp);
            ConfigureValueRow(lblIP3Margin, lblIP3MarginValue, "IP3 Margin %:", 320, 390, 118, grpIp);
            btnEditIpCategories.Location = new System.Drawing.Point(15, 150);
            btnEditIpCategories.Size = new System.Drawing.Size(150, 28);
            btnEditIpCategories.Text = "Edit IP Categories";
            btnEditIpCategories.Click += new System.EventHandler(btnEditIpCategories_Click);
            btnResetIpCharges.Location = new System.Drawing.Point(175, 150);
            btnResetIpCharges.Size = new System.Drawing.Size(120, 28);
            btnResetIpCharges.Text = "Reset IP Charges";
            btnResetIpCharges.Click += new System.EventHandler(btnResetIpCharges_Click);
            grpIp.Controls.Add(btnEditIpCategories);
            grpIp.Controls.Add(btnResetIpCharges);
            pnlScroll.Controls.Add(grpIp);
            y += 200;

            grpMargins.Location = new System.Drawing.Point(x1, y);
            grpMargins.Size = new System.Drawing.Size(430, 130);
            grpMargins.Text = "Margin %";
            ConfigureValueRow(lblMillFinishMargin, lblMillFinishMarginValue, "Mill Finish Margin %:", 15, 210, 28, grpMargins);
            ConfigureValueRow(lblFinishingMargin, lblFinishingMarginValue, "Finishing Margin %:", 15, 210, 58, grpMargins);
            ConfigureValueRow(lblThresholdMargin, lblThresholdMarginValue, "Threshold Margin %:", 15, 210, 88, grpMargins);
            lblAnodisedThresholdCost.AutoSize = true;
            lblAnodisedThresholdCost.Location = new System.Drawing.Point(15, 110);
            lblAnodisedThresholdCost.Text = "Threshold Cost Price $/m2:";
            txtAnodisedThresholdCost.Location = new System.Drawing.Point(210, 106);
            txtAnodisedThresholdCost.Size = new System.Drawing.Size(90, 23);
            grpMargins.Controls.Add(lblAnodisedThresholdCost);
            grpMargins.Controls.Add(txtAnodisedThresholdCost);
            pnlScroll.Controls.Add(grpMargins);

            grpSummary.Location = new System.Drawing.Point(470, y);
            grpSummary.Size = new System.Drawing.Size(520, 250);
            grpSummary.Text = "Quote Summary (rows 61-66)";
            gridSummary.AutoGenerateColumns = false;
            gridSummary.AllowUserToAddRows = false;
            gridSummary.AllowUserToDeleteRows = false;
            gridSummary.ReadOnly = true;
            gridSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridSummary.Location = new System.Drawing.Point(12, 24);
            gridSummary.Size = new System.Drawing.Size(495, 150);
            gridSummary.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Category" });
            gridSummary.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Total $" });
            gridSummary.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "MarginDollar", HeaderText = "Margin $" });
            gridSummary.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "MarginPct", HeaderText = "Margin %" });
            lblSummaryNote1.AutoSize = true;
            lblSummaryNote1.Location = new System.Drawing.Point(12, 185);
            lblSummaryNote1.Text = "* Margin $ also includes IP charge";
            lblSummaryNote2.AutoSize = true;
            lblSummaryNote2.Location = new System.Drawing.Point(12, 205);
            lblSummaryNote2.Text = "* Sundry costs based on sundry costs entered and slot costs estimated";
            grpSummary.Controls.Add(gridSummary);
            grpSummary.Controls.Add(lblSummaryNote1);
            grpSummary.Controls.Add(lblSummaryNote2);
            pnlScroll.Controls.Add(grpSummary);
            y += 265;

            btnBulkNonBulkRates.Location = new System.Drawing.Point(x1, y);
            btnBulkNonBulkRates.Size = new System.Drawing.Size(150, 30);
            btnBulkNonBulkRates.Text = "Bulk/Non-bulk Rates";
            btnBulkNonBulkRates.Click += new System.EventHandler(btnBulkNonBulkRates_Click);
            btnEditSundryItems.Location = new System.Drawing.Point(x1 + 160, y);
            btnEditSundryItems.Size = new System.Drawing.Size(120, 30);
            btnEditSundryItems.Text = "Edit Sundry Items";
            btnEditSundryItems.Click += new System.EventHandler(btnEditSundryItems_Click);
            btnRecalculateAll.Location = new System.Drawing.Point(x1 + 290, y);
            btnRecalculateAll.Size = new System.Drawing.Size(120, 30);
            btnRecalculateAll.Text = "Recalculate All";
            btnRecalculateAll.Click += new System.EventHandler(btnRecalculateAll_Click);
            btnSave.Location = new System.Drawing.Point(x1 + 420, y);
            btnSave.Size = new System.Drawing.Size(90, 30);
            btnSave.Text = "Save";
            btnSave.Click += new System.EventHandler(btnSave_Click);
            btnUpdateVisibility.Location = new System.Drawing.Point(x1 + 520, y);
            btnUpdateVisibility.Size = new System.Drawing.Size(140, 30);
            btnUpdateVisibility.Text = "Update Row Visibility";
            btnUpdateVisibility.Click += new System.EventHandler(btnUpdateVisibility_Click);
            pnlScroll.Controls.Add(btnBulkNonBulkRates);
            pnlScroll.Controls.Add(btnEditSundryItems);
            pnlScroll.Controls.Add(btnRecalculateAll);
            pnlScroll.Controls.Add(btnSave);
            pnlScroll.Controls.Add(btnUpdateVisibility);
            y += 45;

            grpRowVisibility.Location = new System.Drawing.Point(x1, y);
            grpRowVisibility.Size = new System.Drawing.Size(970, 85);
            grpRowVisibility.Text = "Row Visibility Result";
            lblVisibleRows.AutoSize = false;
            lblVisibleRows.Location = new System.Drawing.Point(10, 24);
            lblVisibleRows.Size = new System.Drawing.Size(950, 20);
            lblVisibleRows.Text = "Visible rows: —";
            lblHiddenRows.AutoSize = false;
            lblHiddenRows.Location = new System.Drawing.Point(10, 50);
            lblHiddenRows.Size = new System.Drawing.Size(950, 20);
            lblHiddenRows.Text = "Hidden rows: —";
            grpRowVisibility.Controls.Add(lblVisibleRows);
            grpRowVisibility.Controls.Add(lblHiddenRows);
            pnlScroll.Controls.Add(grpRowVisibility);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1050, 760);
            Name = "EditPricingForm";
            Text = "Edit Pricing";
            grpTotals.ResumeLayout(false);
            grpTotals.PerformLayout();
            grpIp.ResumeLayout(false);
            grpIp.PerformLayout();
            grpMargins.ResumeLayout(false);
            grpMargins.PerformLayout();
            grpSummary.ResumeLayout(false);
            grpSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridSummary).EndInit();
            grpRowVisibility.ResumeLayout(false);
            pnlScroll.ResumeLayout(false);
            pnlScroll.PerformLayout();
            ResumeLayout(false);
        }

        private static void AddTextRow(System.Windows.Forms.Label label, System.Windows.Forms.TextBox textBox, string text, int lx, int tx, int tw, ref int y, int h, System.Windows.Forms.Control parent)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(lx, y + 3);
            label.Text = text;
            textBox.Location = new System.Drawing.Point(tx, y);
            textBox.Size = new System.Drawing.Size(tw, 23);
            parent.Controls.Add(label);
            parent.Controls.Add(textBox);
            y += h;
        }

        private static void AddComboRow(System.Windows.Forms.Label label, System.Windows.Forms.ComboBox comboBox, string text, int lx, int tx, int tw, ref int y, int h, System.Windows.Forms.Control parent, bool dropDownList)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(lx, y + 3);
            label.Text = text;
            comboBox.DropDownStyle = dropDownList ? System.Windows.Forms.ComboBoxStyle.DropDownList : System.Windows.Forms.ComboBoxStyle.DropDown;
            comboBox.Location = new System.Drawing.Point(tx, y);
            comboBox.Size = new System.Drawing.Size(tw, 23);
            parent.Controls.Add(label);
            parent.Controls.Add(comboBox);
            y += h;
        }

        private static void ConfigureValueRow(System.Windows.Forms.Label label, System.Windows.Forms.Label value, string text, int lx, int vx, int y, System.Windows.Forms.Control parent)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(lx, y);
            label.Text = text;
            value.AutoSize = true;
            value.Location = new System.Drawing.Point(vx, y);
            value.Text = "0.00";
            parent.Controls.Add(label);
            parent.Controls.Add(value);
        }

        private static void ConfigureTextRow(System.Windows.Forms.Label label, System.Windows.Forms.TextBox textBox, string text, int lx, int tx, int tw, int y, System.Windows.Forms.Control parent)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(lx, y + 3);
            label.Text = text;
            textBox.Location = new System.Drawing.Point(tx, y);
            textBox.Size = new System.Drawing.Size(tw, 23);
            parent.Controls.Add(label);
            parent.Controls.Add(textBox);
        }

        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPricingNotes;
        private System.Windows.Forms.TextBox txtPricingNotes;
        private System.Windows.Forms.Label lblCostType;
        private System.Windows.Forms.ComboBox cmbCostType;
        private System.Windows.Forms.Label lblFinType;
        private System.Windows.Forms.ComboBox cmbFinType;
        private System.Windows.Forms.CheckBox chkCostTypeOverride;
        private System.Windows.Forms.Button btnToggleOverride;
        private System.Windows.Forms.Label lblMetalPrice;
        private System.Windows.Forms.TextBox txtMetalPrice;
        private System.Windows.Forms.Label lblFinishingPrice;
        private System.Windows.Forms.TextBox txtFinishingPrice;
        private System.Windows.Forms.Label lblFinishingCostPricePerM2;
        private System.Windows.Forms.TextBox txtFinishingCostPricePerM2;
        private System.Windows.Forms.Label lblB42;
        private System.Windows.Forms.TextBox txtB42;
        private System.Windows.Forms.Label lblB44;
        private System.Windows.Forms.TextBox txtB44;
        private System.Windows.Forms.Label lblB48;
        private System.Windows.Forms.TextBox txtB48;
        private System.Windows.Forms.Label lblB50;
        private System.Windows.Forms.TextBox txtB50;
        private System.Windows.Forms.Label lblB52;
        private System.Windows.Forms.TextBox txtB52;
        private System.Windows.Forms.Label lblB54;
        private System.Windows.Forms.TextBox txtB54;
        private System.Windows.Forms.Label lblB57;
        private System.Windows.Forms.TextBox txtB57;
        private System.Windows.Forms.Label lblB58;
        private System.Windows.Forms.TextBox txtB58;
        private System.Windows.Forms.Label lblTBCalcType;
        private System.Windows.Forms.ComboBox cmbTBCalcType;
        private System.Windows.Forms.Label lblTBMargin;
        private System.Windows.Forms.TextBox txtTBMargin;
        private System.Windows.Forms.GroupBox grpTotals;
        private System.Windows.Forms.Label lblTotalKg;
        private System.Windows.Forms.Label lblTotalKgValue;
        private System.Windows.Forms.Label lblTotalFinishingM2;
        private System.Windows.Forms.Label lblTotalFinishingM2Value;
        private System.Windows.Forms.Label lblTotalPieces;
        private System.Windows.Forms.Label lblTotalPiecesValue;
        private System.Windows.Forms.GroupBox grpIp;
        private System.Windows.Forms.Label lblCustomerSapDiscount;
        private System.Windows.Forms.TextBox txtCustomerSapDiscount;
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
        private System.Windows.Forms.Button btnEditIpCategories;
        private System.Windows.Forms.Button btnResetIpCharges;
        private System.Windows.Forms.GroupBox grpMargins;
        private System.Windows.Forms.Label lblMillFinishMargin;
        private System.Windows.Forms.Label lblMillFinishMarginValue;
        private System.Windows.Forms.Label lblFinishingMargin;
        private System.Windows.Forms.Label lblFinishingMarginValue;
        private System.Windows.Forms.Label lblThresholdMargin;
        private System.Windows.Forms.Label lblThresholdMarginValue;
        private System.Windows.Forms.Label lblAnodisedThresholdCost;
        private System.Windows.Forms.TextBox txtAnodisedThresholdCost;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.DataGridView gridSummary;
        private System.Windows.Forms.Label lblSummaryNote1;
        private System.Windows.Forms.Label lblSummaryNote2;
        private System.Windows.Forms.Button btnBulkNonBulkRates;
        private System.Windows.Forms.Button btnEditSundryItems;
        private System.Windows.Forms.Button btnRecalculateAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdateVisibility;
        private System.Windows.Forms.GroupBox grpRowVisibility;
        private System.Windows.Forms.Label lblVisibleRows;
        private System.Windows.Forms.Label lblHiddenRows;
    }
}
