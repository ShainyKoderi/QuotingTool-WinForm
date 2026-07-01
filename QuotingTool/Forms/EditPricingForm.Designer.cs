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
            this.lblTitle           = new System.Windows.Forms.Label();
            this.lblCostType        = new System.Windows.Forms.Label();
            this.cmbCostType        = new System.Windows.Forms.ComboBox();
            this.lblFinType         = new System.Windows.Forms.Label();
            this.cmbFinType         = new System.Windows.Forms.ComboBox();
            this.chkCostTypeOverride = new System.Windows.Forms.CheckBox();
            this.btnToggleOverride  = new System.Windows.Forms.Button();
            this.lblMetalPrice      = new System.Windows.Forms.Label();
            this.txtMetalPrice      = new System.Windows.Forms.TextBox();
            this.lblFinishingPrice  = new System.Windows.Forms.Label();
            this.txtFinishingPrice  = new System.Windows.Forms.TextBox();
            this.lblB42             = new System.Windows.Forms.Label();
            this.txtB42             = new System.Windows.Forms.TextBox();
            this.lblB44             = new System.Windows.Forms.Label();
            this.txtB44             = new System.Windows.Forms.TextBox();
            this.lblB48             = new System.Windows.Forms.Label();
            this.txtB48             = new System.Windows.Forms.TextBox();
            this.lblB50             = new System.Windows.Forms.Label();
            this.txtB50             = new System.Windows.Forms.TextBox();
            // New fields from .xlsm 3.Edit Pricing
            this.lblB52             = new System.Windows.Forms.Label();
            this.txtB52             = new System.Windows.Forms.TextBox();
            this.lblB54             = new System.Windows.Forms.Label();
            this.txtB54             = new System.Windows.Forms.TextBox();
            this.lblB57             = new System.Windows.Forms.Label();
            this.txtB57             = new System.Windows.Forms.TextBox();
            this.lblB58             = new System.Windows.Forms.Label();
            this.txtB58             = new System.Windows.Forms.TextBox();
            this.lblTBCalcType      = new System.Windows.Forms.Label();
            this.cmbTBCalcType      = new System.Windows.Forms.ComboBox();
            this.lblTBMargin        = new System.Windows.Forms.Label();
            this.txtTBMargin        = new System.Windows.Forms.TextBox();
            this.btnSave            = new System.Windows.Forms.Button();
            this.btnUpdateVisibility = new System.Windows.Forms.Button();
            this.grpRowVisibility   = new System.Windows.Forms.GroupBox();
            this.lblVisibleRows     = new System.Windows.Forms.Label();
            this.lblHiddenRows      = new System.Windows.Forms.Label();

            this.grpRowVisibility.SuspendLayout();
            this.SuspendLayout();

            int col1 = 20, col2 = 230, rowH = 35, startY = 60;

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(col1, 15);
            this.lblTitle.Size = new System.Drawing.Size(350, 28);
            this.lblTitle.Text = "3. Edit Pricing";

            // CostType
            this.lblCostType.AutoSize = true;
            this.lblCostType.Location = new System.Drawing.Point(col1, startY);
            this.lblCostType.Text = "Cost Type (C25):";
            this.cmbCostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostType.Location = new System.Drawing.Point(col2, startY - 3);
            this.cmbCostType.Size = new System.Drawing.Size(280, 23);
            startY += rowH;

            // FinType
            this.lblFinType.AutoSize = true;
            this.lblFinType.Location = new System.Drawing.Point(col1, startY);
            this.lblFinType.Text = "Finish Type (C16):";
            this.cmbFinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinType.Location = new System.Drawing.Point(col2, startY - 3);
            this.cmbFinType.Size = new System.Drawing.Size(280, 23);
            startY += rowH;

            // CostTypeOverride
            this.chkCostTypeOverride.AutoSize = true;
            this.chkCostTypeOverride.Location = new System.Drawing.Point(col1, startY);
            this.chkCostTypeOverride.Text = "Cost Type Override (D25)";
            this.btnToggleOverride.Location = new System.Drawing.Point(col2 + 200, startY - 3);
            this.btnToggleOverride.Size = new System.Drawing.Size(130, 25);
            this.btnToggleOverride.Text = "Toggle Override";
            this.btnToggleOverride.Click += new System.EventHandler(this.btnToggleOverride_Click);
            startY += rowH;

            // MetalPrice (B33)
            this.lblMetalPrice.AutoSize = true;
            this.lblMetalPrice.Location = new System.Drawing.Point(col1, startY);
            this.lblMetalPrice.Text = "Mill Finish Sell Price $/kg (B33):";
            this.txtMetalPrice.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtMetalPrice.Size = new System.Drawing.Size(120, 23);
            startY += rowH;

            // FinishingPrice (B43 – read-only, from Pricing Matrix)
            this.lblFinishingPrice.AutoSize = true;
            this.lblFinishingPrice.Location = new System.Drawing.Point(col1, startY);
            this.lblFinishingPrice.Text = "Finishing Cost Price $/m2 (B43, auto):";
            this.txtFinishingPrice.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtFinishingPrice.Size = new System.Drawing.Size(120, 23);
            this.txtFinishingPrice.ReadOnly = true;
            this.txtFinishingPrice.BackColor = System.Drawing.SystemColors.Control;
            startY += rowH;

            // B42
            this.lblB42.AutoSize = true;
            this.lblB42.Location = new System.Drawing.Point(col1, startY);
            this.lblB42.Text = "POA/Other Vendor Cost $/m2 (B42):";
            this.txtB42.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB42.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B44
            this.lblB44.AutoSize = true;
            this.lblB44.Location = new System.Drawing.Point(col1, startY);
            this.lblB44.Text = "Finishing Sell Price $/m2 (B44):";
            this.txtB44.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB44.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B48
            this.lblB48.AutoSize = true;
            this.lblB48.Location = new System.Drawing.Point(col1, startY);
            this.lblB48.Text = "Mill BULK POA Cost $/m2 (B48):";
            this.txtB48.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB48.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B50
            this.lblB50.AutoSize = true;
            this.lblB50.Location = new System.Drawing.Point(col1, startY);
            this.lblB50.Text = "Mill BULK Sell Price $/m2 (B50):";
            this.txtB50.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB50.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B52 (new from .xlsm)
            this.lblB52.AutoSize = true;
            this.lblB52.Location = new System.Drawing.Point(col1, startY);
            this.lblB52.Text = "Mill NON-Bulk POA Cost $/m2 (B52):";
            this.txtB52.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB52.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B54 (new from .xlsm)
            this.lblB54.AutoSize = true;
            this.lblB54.Location = new System.Drawing.Point(col1, startY);
            this.lblB54.Text = "Mill NON-Bulk Sell Price $/m2 (B54):";
            this.txtB54.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB54.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B57 (new – anodised threshold cost, auto from Pricing Matrix)
            this.lblB57.AutoSize = true;
            this.lblB57.Location = new System.Drawing.Point(col1, startY);
            this.lblB57.Text = "Anodised Threshold Cost $/m2 (B57, auto):";
            this.txtB57.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB57.Size = new System.Drawing.Size(100, 23);
            this.txtB57.ReadOnly = true;
            this.txtB57.BackColor = System.Drawing.SystemColors.Control;
            startY += rowH;

            // B58 (new – anodised threshold sell price)
            this.lblB58.AutoSize = true;
            this.lblB58.Location = new System.Drawing.Point(col1, startY);
            this.lblB58.Text = "Anodised Threshold Sell Price $/m2 (B58):";
            this.txtB58.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB58.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // Thermal Break Calc Type (B36 – new)
            this.lblTBCalcType.AutoSize = true;
            this.lblTBCalcType.Location = new System.Drawing.Point(col1, startY);
            this.lblTBCalcType.Text = "Thermal Break Calc Type (B36):";
            this.cmbTBCalcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTBCalcType.Items.AddRange(new object[] { "Margin", "List less Discount" });
            this.cmbTBCalcType.Location = new System.Drawing.Point(col2, startY - 3);
            this.cmbTBCalcType.Size = new System.Drawing.Size(200, 23);
            startY += rowH;

            // Thermal Break Margin (B37 – new)
            this.lblTBMargin.AutoSize = true;
            this.lblTBMargin.Location = new System.Drawing.Point(col1, startY);
            this.lblTBMargin.Text = "Thermal Break Margin/Discount (B37):";
            this.txtTBMargin.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtTBMargin.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // Buttons
            this.btnSave.Location = new System.Drawing.Point(col1, startY);
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnUpdateVisibility.Location = new System.Drawing.Point(col1 + 115, startY);
            this.btnUpdateVisibility.Size = new System.Drawing.Size(180, 30);
            this.btnUpdateVisibility.Text = "Update Row Visibility";
            this.btnUpdateVisibility.Click += new System.EventHandler(this.btnUpdateVisibility_Click);
            startY += 45;

            // Row visibility group
            this.grpRowVisibility.Location = new System.Drawing.Point(col1, startY);
            this.grpRowVisibility.Size = new System.Drawing.Size(780, 80);
            this.grpRowVisibility.Text = "Row Visibility Result";

            this.lblVisibleRows.AutoSize = false;
            this.lblVisibleRows.Location = new System.Drawing.Point(10, 20);
            this.lblVisibleRows.Size = new System.Drawing.Size(760, 20);
            this.lblVisibleRows.Text = "Visible rows: —";

            this.lblHiddenRows.AutoSize = false;
            this.lblHiddenRows.Location = new System.Drawing.Point(10, 45);
            this.lblHiddenRows.Size = new System.Drawing.Size(760, 20);
            this.lblHiddenRows.Text = "Hidden rows: —";

            this.grpRowVisibility.Controls.Add(this.lblVisibleRows);
            this.grpRowVisibility.Controls.Add(this.lblHiddenRows);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, startY + 120);
            this.AutoScroll = true;
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle,
                this.lblCostType, this.cmbCostType,
                this.lblFinType, this.cmbFinType,
                this.chkCostTypeOverride, this.btnToggleOverride,
                this.lblMetalPrice, this.txtMetalPrice,
                this.lblFinishingPrice, this.txtFinishingPrice,
                this.lblB42, this.txtB42,
                this.lblB44, this.txtB44,
                this.lblB48, this.txtB48,
                this.lblB50, this.txtB50,
                this.lblB52, this.txtB52,
                this.lblB54, this.txtB54,
                this.lblB57, this.txtB57,
                this.lblB58, this.txtB58,
                this.lblTBCalcType, this.cmbTBCalcType,
                this.lblTBMargin, this.txtTBMargin,
                this.btnSave, this.btnUpdateVisibility,
                this.grpRowVisibility
            });
            this.Name = "EditPricingForm";
            this.Text = "Edit Pricing";

            this.grpRowVisibility.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
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
        private System.Windows.Forms.Label lblB42;
        private System.Windows.Forms.TextBox txtB42;
        private System.Windows.Forms.Label lblB44;
        private System.Windows.Forms.TextBox txtB44;
        private System.Windows.Forms.Label lblB48;
        private System.Windows.Forms.TextBox txtB48;
        private System.Windows.Forms.Label lblB50;
        private System.Windows.Forms.TextBox txtB50;
        // New fields from .xlsm
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdateVisibility;
        private System.Windows.Forms.GroupBox grpRowVisibility;
        private System.Windows.Forms.Label lblVisibleRows;
        private System.Windows.Forms.Label lblHiddenRows;
    }
}
