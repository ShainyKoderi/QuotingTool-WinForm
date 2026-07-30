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
            dtpRevisionDate.Location = new Point(160, 52);
            dtpRevisionDate.Name = "dtpRevisionDate";
            dtpRevisionDate.Size = new Size(200, 23);
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
            txtLMERate.Location = new Point(160, 87);
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
            txtBilletPremium.Location = new Point(160, 122);
            txtBilletPremium.Name = "txtBilletPremium";
            txtBilletPremium.Size = new Size(150, 23);
            txtBilletPremium.TabIndex = 6;
            // 
            // btnClose
            // 
            btnClose.BackColor = SystemColors.Highlight;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(490, 45);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 30);
            btnClose.TabIndex = 8;
            btnClose.Text = "Save";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // lblStatus
            // 
            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Location = new Point(12, 148);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(750, 20);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Status: ready";
            // 
            // grpMatrix
            // 
            grpMatrix.Controls.Add(gridPricingMatrix);
            grpMatrix.Dock = DockStyle.Fill;
            grpMatrix.Location = new Point(0, 173);
            grpMatrix.Name = "grpMatrix";
            grpMatrix.Size = new Size(1000, 527);
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
            pnlTop.Controls.Add(lblStatus);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1000, 173);
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
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpMatrix;
        private System.Windows.Forms.DataGridView gridPricingMatrix;
        private System.Windows.Forms.Panel pnlTop;
    }
}
