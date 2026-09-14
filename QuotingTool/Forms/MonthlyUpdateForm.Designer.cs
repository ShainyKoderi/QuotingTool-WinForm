namespace QuotingTool.Forms
{
    partial class MonthlyUpdateForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblRevDate = new Label();
            dtpRevisionDate = new DateTimePicker();
            lblLMERate = new Label();
            txtLMERate = new TextBox();
            lblBillet = new Label();
            txtBilletPremium = new TextBox();
            btnClose = new Button();
            btnEditPricingConstants = new Button();
            btnEditMetalBreakpoints = new Button();
            btnEditThermalBreakBreakpoints = new Button();
            btnEditPowderCostTable = new Button();
            btnEditFinishBreakpoints = new Button();
            btnEditSubsillTierPrices = new Button();
            btnEditThermalBreakCost = new Button();
            btnEditThermalBreakMaterialPriceGroup = new Button();
            lblStatus = new Label();
            grpMatrix = new GroupBox();
            gridPricingMatrix = new DataGridView();
            pnlTop = new Panel();
            grpMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPricingMatrix).BeginInit();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = " Monthly Update";
            // 
            // lblRevDate
            // 
            lblRevDate.AutoSize = true;
            lblRevDate.Location = new Point(10, 55);
            lblRevDate.Name = "lblRevDate";
            lblRevDate.Size = new Size(81, 15);
            lblRevDate.TabIndex = 1;
            lblRevDate.Text = "Revision Date:";
            // 
            // dtpRevisionDate
            // 
            dtpRevisionDate.Format = DateTimePickerFormat.Short;
            dtpRevisionDate.Location = new Point(97, 55);
            dtpRevisionDate.Name = "dtpRevisionDate";
            dtpRevisionDate.Size = new Size(150, 23);
            dtpRevisionDate.TabIndex = 2;
            // 
            // lblLMERate
            // 
            lblLMERate.AutoSize = true;
            lblLMERate.Location = new Point(10, 90);
            lblLMERate.Name = "lblLMERate";
            lblLMERate.Size = new Size(59, 15);
            lblLMERate.TabIndex = 3;
            lblLMERate.Text = "LME Rate:";
            // 
            // txtLMERate
            // 
            txtLMERate.Location = new Point(97, 90);
            txtLMERate.Name = "txtLMERate";
            txtLMERate.Size = new Size(150, 23);
            txtLMERate.TabIndex = 4;
            // 
            // lblBillet
            // 
            lblBillet.AutoSize = true;
            lblBillet.Location = new Point(10, 125);
            lblBillet.Name = "lblBillet";
            lblBillet.Size = new Size(88, 15);
            lblBillet.TabIndex = 5;
            lblBillet.Text = "Billet Premium:";
            // 
            // txtBilletPremium
            // 
            txtBilletPremium.Location = new Point(97, 125);
            txtBilletPremium.Name = "txtBilletPremium";
            txtBilletPremium.Size = new Size(150, 23);
            txtBilletPremium.TabIndex = 6;
            // 
            // btnClose
            // 
            btnClose.BackColor = SystemColors.Highlight;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(257, 55);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 30);
            btnClose.TabIndex = 8;
            btnClose.Text = "Save";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnEditPricingConstants
            // 
            btnEditPricingConstants.Location = new Point(427, 55);
            btnEditPricingConstants.Name = "btnEditPricingConstants";
            btnEditPricingConstants.Size = new Size(160, 30);
            btnEditPricingConstants.TabIndex = 9;
            btnEditPricingConstants.Text = "Edit Pricing Constants";
            btnEditPricingConstants.Visible = false;
            btnEditPricingConstants.Click += btnEditPricingConstants_Click;
            // 
            // btnEditMetalBreakpoints
            // 
            btnEditMetalBreakpoints.Location = new Point(597, 55);
            btnEditMetalBreakpoints.Name = "btnEditMetalBreakpoints";
            btnEditMetalBreakpoints.Size = new Size(160, 30);
            btnEditMetalBreakpoints.TabIndex = 11;
            btnEditMetalBreakpoints.Text = "Edit Metal Breakpoints";
            btnEditMetalBreakpoints.Visible = false;
            btnEditMetalBreakpoints.Click += btnEditMetalBreakpoints_Click;
            // 
            // btnEditThermalBreakBreakpoints
            // 
            btnEditThermalBreakBreakpoints.Location = new Point(427, 90);
            btnEditThermalBreakBreakpoints.Name = "btnEditThermalBreakBreakpoints";
            btnEditThermalBreakBreakpoints.Size = new Size(160, 30);
            btnEditThermalBreakBreakpoints.TabIndex = 12;
            btnEditThermalBreakBreakpoints.Text = "Edit Thermal Break BPs";
            btnEditThermalBreakBreakpoints.Visible = false;
            btnEditThermalBreakBreakpoints.Click += btnEditThermalBreakBreakpoints_Click;
            // 
            // btnEditPowderCostTable
            // 
            btnEditPowderCostTable.Location = new Point(597, 90);
            btnEditPowderCostTable.Name = "btnEditPowderCostTable";
            btnEditPowderCostTable.Size = new Size(160, 30);
            btnEditPowderCostTable.TabIndex = 13;
            btnEditPowderCostTable.Text = "Edit Powder Cost Table";
            btnEditPowderCostTable.Visible = false;
            btnEditPowderCostTable.Click += btnEditPowderCostTable_Click;
            // 
            // btnEditFinishBreakpoints
            // 
            btnEditFinishBreakpoints.Location = new Point(427, 125);
            btnEditFinishBreakpoints.Name = "btnEditFinishBreakpoints";
            btnEditFinishBreakpoints.Size = new Size(160, 30);
            btnEditFinishBreakpoints.TabIndex = 14;
            btnEditFinishBreakpoints.Text = "Edit Finish Breakpoints";
            btnEditFinishBreakpoints.Visible = false;
            btnEditFinishBreakpoints.Click += btnEditFinishBreakpoints_Click;
            // 
            // btnEditSubsillTierPrices
            // 
            btnEditSubsillTierPrices.Location = new Point(597, 125);
            btnEditSubsillTierPrices.Name = "btnEditSubsillTierPrices";
            btnEditSubsillTierPrices.Size = new Size(160, 30);
            btnEditSubsillTierPrices.TabIndex = 15;
            btnEditSubsillTierPrices.Text = "Edit Subsill Tier Prices";
            btnEditSubsillTierPrices.Visible = false;
            btnEditSubsillTierPrices.Click += btnEditSubsillTierPrices_Click;
            // 
            // btnEditThermalBreakCost
            // 
            btnEditThermalBreakCost.Location = new Point(427, 160);
            btnEditThermalBreakCost.Name = "btnEditThermalBreakCost";
            btnEditThermalBreakCost.Size = new Size(160, 30);
            btnEditThermalBreakCost.TabIndex = 16;
            btnEditThermalBreakCost.Text = "Edit Thermal Break Cost";
            btnEditThermalBreakCost.Visible = false;
            btnEditThermalBreakCost.Click += btnEditThermalBreakCost_Click;
            // 
            // btnEditThermalBreakMaterialPriceGroup
            // 
            btnEditThermalBreakMaterialPriceGroup.Location = new Point(597, 160);
            btnEditThermalBreakMaterialPriceGroup.Name = "btnEditThermalBreakMaterialPriceGroup";
            btnEditThermalBreakMaterialPriceGroup.Size = new Size(160, 30);
            btnEditThermalBreakMaterialPriceGroup.TabIndex = 17;
            btnEditThermalBreakMaterialPriceGroup.Text = "Edit TB Material Price Grp";
            btnEditThermalBreakMaterialPriceGroup.Visible = false;
            btnEditThermalBreakMaterialPriceGroup.Click += btnEditThermalBreakMaterialPriceGroup_Click;
            // 
            // lblStatus
            // 
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Location = new Point(12, 198);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(750, 20);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Status: ready";
            // 
            // grpMatrix
            // 
            grpMatrix.Controls.Add(gridPricingMatrix);
            grpMatrix.Dock = DockStyle.Fill;
            grpMatrix.Location = new Point(0, 193);
            grpMatrix.Name = "grpMatrix";
            grpMatrix.Size = new Size(1000, 507);
            grpMatrix.TabIndex = 0;
            grpMatrix.TabStop = false;
            grpMatrix.Text = "Pricing Matrix (editable)";
            // 
            // gridPricingMatrix
            // 
            gridPricingMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPricingMatrix.Dock = DockStyle.Fill;
            gridPricingMatrix.Location = new Point(3, 19);
            gridPricingMatrix.Name = "gridPricingMatrix";
            gridPricingMatrix.Size = new Size(994, 505);
            gridPricingMatrix.TabIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblRevDate);
            pnlTop.Controls.Add(dtpRevisionDate);
            pnlTop.Controls.Add(lblLMERate);
            pnlTop.Controls.Add(txtLMERate);
            pnlTop.Controls.Add(lblBillet);
            pnlTop.Controls.Add(txtBilletPremium);
            pnlTop.Controls.Add(btnClose);
            pnlTop.Controls.Add(btnEditPricingConstants);
            pnlTop.Controls.Add(btnEditMetalBreakpoints);
            pnlTop.Controls.Add(btnEditThermalBreakBreakpoints);
            pnlTop.Controls.Add(btnEditPowderCostTable);
            pnlTop.Controls.Add(btnEditFinishBreakpoints);
            pnlTop.Controls.Add(btnEditSubsillTierPrices);
            pnlTop.Controls.Add(btnEditThermalBreakCost);
            pnlTop.Controls.Add(btnEditThermalBreakMaterialPriceGroup);
            pnlTop.Controls.Add(lblStatus);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1000, 223);
            pnlTop.TabIndex = 1;
            // 
            // MonthlyUpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(grpMatrix);
            Controls.Add(pnlTop);
            Name = "MonthlyUpdateForm";
            Text = "Monthly Update";
            grpMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridPricingMatrix).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRevDate;
        private System.Windows.Forms.DateTimePicker dtpRevisionDate;
        private System.Windows.Forms.Label lblLMERate;
        private System.Windows.Forms.TextBox txtLMERate;
        private System.Windows.Forms.Label lblBillet;
        private System.Windows.Forms.TextBox txtBilletPremium;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEditPricingConstants;
        private System.Windows.Forms.Button btnEditMetalBreakpoints;
        private System.Windows.Forms.Button btnEditThermalBreakBreakpoints;
        private System.Windows.Forms.Button btnEditPowderCostTable;
        private System.Windows.Forms.Button btnEditFinishBreakpoints;
        private System.Windows.Forms.Button btnEditSubsillTierPrices;
        private System.Windows.Forms.Button btnEditThermalBreakCost;
        private System.Windows.Forms.Button btnEditThermalBreakMaterialPriceGroup;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpMatrix;
        private System.Windows.Forms.DataGridView gridPricingMatrix;
        private System.Windows.Forms.Panel pnlTop;
    }
}
