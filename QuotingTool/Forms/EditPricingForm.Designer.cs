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
            this.btnSave            = new System.Windows.Forms.Button();
            this.btnUpdateVisibility = new System.Windows.Forms.Button();
            this.grpRowVisibility   = new System.Windows.Forms.GroupBox();
            this.lblVisibleRows     = new System.Windows.Forms.Label();
            this.lblHiddenRows      = new System.Windows.Forms.Label();

            this.grpRowVisibility.SuspendLayout();
            this.SuspendLayout();

            int col1 = 20, col2 = 200, rowH = 35, startY = 60;

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(col1, 15);
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.Text = "3. Edit Pricing";

            // CostType
            this.lblCostType.AutoSize = true;
            this.lblCostType.Location = new System.Drawing.Point(col1, startY);
            this.lblCostType.Text = "Cost Type (C25):";

            this.cmbCostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostType.Location = new System.Drawing.Point(col2, startY - 3);
            this.cmbCostType.Name = "cmbCostType";
            this.cmbCostType.Size = new System.Drawing.Size(250, 23);
            startY += rowH;

            // FinType
            this.lblFinType.AutoSize = true;
            this.lblFinType.Location = new System.Drawing.Point(col1, startY);
            this.lblFinType.Text = "Finish Type (C16):";

            this.cmbFinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinType.Location = new System.Drawing.Point(col2, startY - 3);
            this.cmbFinType.Name = "cmbFinType";
            this.cmbFinType.Size = new System.Drawing.Size(250, 23);
            startY += rowH;

            // CostTypeOverride
            this.chkCostTypeOverride.AutoSize = true;
            this.chkCostTypeOverride.Location = new System.Drawing.Point(col1, startY);
            this.chkCostTypeOverride.Name = "chkCostTypeOverride";
            this.chkCostTypeOverride.Text = "Cost Type Override (D25) – show all pricing rows";

            this.btnToggleOverride.Location = new System.Drawing.Point(col2 + 200, startY - 3);
            this.btnToggleOverride.Name = "btnToggleOverride";
            this.btnToggleOverride.Size = new System.Drawing.Size(120, 25);
            this.btnToggleOverride.Text = "Toggle Override";
            this.btnToggleOverride.Click += new System.EventHandler(this.btnToggleOverride_Click);
            startY += rowH;

            // MetalPrice
            this.lblMetalPrice.AutoSize = true;
            this.lblMetalPrice.Location = new System.Drawing.Point(col1, startY);
            this.lblMetalPrice.Text = "Metal Price (B33):";

            this.txtMetalPrice.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtMetalPrice.Name = "txtMetalPrice";
            this.txtMetalPrice.Size = new System.Drawing.Size(120, 23);
            startY += rowH;

            // FinishingPrice
            this.lblFinishingPrice.AutoSize = true;
            this.lblFinishingPrice.Location = new System.Drawing.Point(col1, startY);
            this.lblFinishingPrice.Text = "Finishing Price (B43):";

            this.txtFinishingPrice.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtFinishingPrice.Name = "txtFinishingPrice";
            this.txtFinishingPrice.Size = new System.Drawing.Size(120, 23);
            startY += rowH;

            // B42
            this.lblB42.AutoSize = true;
            this.lblB42.Location = new System.Drawing.Point(col1, startY);
            this.lblB42.Text = "Price B42 (default 99):";
            this.txtB42.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB42.Name = "txtB42";
            this.txtB42.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B44
            this.lblB44.AutoSize = true;
            this.lblB44.Location = new System.Drawing.Point(col1, startY);
            this.lblB44.Text = "Price B44 (default 99):";
            this.txtB44.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB44.Name = "txtB44";
            this.txtB44.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B48
            this.lblB48.AutoSize = true;
            this.lblB48.Location = new System.Drawing.Point(col1, startY);
            this.lblB48.Text = "Price B48 (default 99):";
            this.txtB48.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB48.Name = "txtB48";
            this.txtB48.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // B50
            this.lblB50.AutoSize = true;
            this.lblB50.Location = new System.Drawing.Point(col1, startY);
            this.lblB50.Text = "Price B50 (default 99):";
            this.txtB50.Location = new System.Drawing.Point(col2, startY - 3);
            this.txtB50.Name = "txtB50";
            this.txtB50.Size = new System.Drawing.Size(100, 23);
            startY += rowH;

            // Buttons
            this.btnSave.Location = new System.Drawing.Point(col1, startY);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnUpdateVisibility.Location = new System.Drawing.Point(col1 + 115, startY);
            this.btnUpdateVisibility.Name = "btnUpdateVisibility";
            this.btnUpdateVisibility.Size = new System.Drawing.Size(180, 30);
            this.btnUpdateVisibility.Text = "Update Row Visibility";
            this.btnUpdateVisibility.Click += new System.EventHandler(this.btnUpdateVisibility_Click);
            startY += 45;

            // Row visibility group
            this.grpRowVisibility.Location = new System.Drawing.Point(col1, startY);
            this.grpRowVisibility.Name = "grpRowVisibility";
            this.grpRowVisibility.Size = new System.Drawing.Size(750, 80);
            this.grpRowVisibility.Text = "Row Visibility Result";

            this.lblVisibleRows.AutoSize = false;
            this.lblVisibleRows.Location = new System.Drawing.Point(10, 20);
            this.lblVisibleRows.Size = new System.Drawing.Size(730, 20);
            this.lblVisibleRows.Text = "Visible rows: —";

            this.lblHiddenRows.AutoSize = false;
            this.lblHiddenRows.Location = new System.Drawing.Point(10, 45);
            this.lblHiddenRows.Size = new System.Drawing.Size(730, 20);
            this.lblHiddenRows.Text = "Hidden rows: —";

            this.grpRowVisibility.Controls.Add(this.lblVisibleRows);
            this.grpRowVisibility.Controls.Add(this.lblHiddenRows);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, startY + 120);
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdateVisibility;
        private System.Windows.Forms.GroupBox grpRowVisibility;
        private System.Windows.Forms.Label lblVisibleRows;
        private System.Windows.Forms.Label lblHiddenRows;
    }
}
