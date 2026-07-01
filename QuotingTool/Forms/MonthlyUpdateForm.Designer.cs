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
            this.lblTitle        = new System.Windows.Forms.Label();
            this.lblRevDate      = new System.Windows.Forms.Label();
            this.dtpRevisionDate = new System.Windows.Forms.DateTimePicker();
            this.lblLMERate      = new System.Windows.Forms.Label();
            this.txtLMERate      = new System.Windows.Forms.TextBox();
            this.lblBillet       = new System.Windows.Forms.Label();
            this.txtBilletPremium = new System.Windows.Forms.TextBox();
            this.btnOpen         = new System.Windows.Forms.Button();
            this.btnClose        = new System.Windows.Forms.Button();
            this.lblStatus       = new System.Windows.Forms.Label();
            this.grpMatrix       = new System.Windows.Forms.GroupBox();
            this.gridPricingMatrix = new System.Windows.Forms.DataGridView();
            this.pnlTop          = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)this.gridPricingMatrix).BeginInit();
            this.grpMatrix.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Size = new System.Drawing.Size(400, 30);
            this.lblTitle.Text = "5. Monthly Update";

            // Revision Date
            this.lblRevDate.AutoSize = true;
            this.lblRevDate.Location = new System.Drawing.Point(10, 55);
            this.lblRevDate.Text = "Revision Date:";

            this.dtpRevisionDate.Location = new System.Drawing.Point(160, 52);
            this.dtpRevisionDate.Name = "dtpRevisionDate";
            this.dtpRevisionDate.Size = new System.Drawing.Size(200, 23);
            this.dtpRevisionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // LME Rate
            this.lblLMERate.AutoSize = true;
            this.lblLMERate.Location = new System.Drawing.Point(10, 90);
            this.lblLMERate.Text = "LME Rate:";

            this.txtLMERate.Location = new System.Drawing.Point(160, 87);
            this.txtLMERate.Name = "txtLMERate";
            this.txtLMERate.Size = new System.Drawing.Size(150, 23);

            // Billet Premium
            this.lblBillet.AutoSize = true;
            this.lblBillet.Location = new System.Drawing.Point(10, 125);
            this.lblBillet.Text = "Billet Premium:";

            this.txtBilletPremium.Location = new System.Drawing.Point(160, 122);
            this.txtBilletPremium.Name = "txtBilletPremium";
            this.txtBilletPremium.Size = new System.Drawing.Size(150, 23);

            // Buttons
            this.btnOpen.Location = new System.Drawing.Point(10, 165);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(200, 30);
            this.btnOpen.Text = "Open (Unlock Maintenance)";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);

            this.btnClose.Location = new System.Drawing.Point(220, 165);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 30);
            this.btnClose.Text = "Close (Lock & Save)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Status label
            this.lblStatus.AutoSize = false;
            this.lblStatus.Location = new System.Drawing.Point(10, 205);
            this.lblStatus.Size = new System.Drawing.Size(750, 20);
            this.lblStatus.Text = "Status: ready";
            this.lblStatus.ForeColor = System.Drawing.Color.DarkBlue;

            // pnlTop
            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblRevDate, this.dtpRevisionDate,
                this.lblLMERate, this.txtLMERate,
                this.lblBillet, this.txtBilletPremium,
                this.btnOpen, this.btnClose, this.lblStatus
            });
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 240;
            this.pnlTop.Name = "pnlTop";

            // Pricing Matrix groupbox
            this.grpMatrix.Text = "Pricing Matrix (editable)";
            this.grpMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMatrix.Name = "grpMatrix";

            this.gridPricingMatrix.AllowUserToAddRows = true;
            this.gridPricingMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPricingMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPricingMatrix.Name = "gridPricingMatrix";

            this.grpMatrix.Controls.Add(this.gridPricingMatrix);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.grpMatrix);
            this.Controls.Add(this.pnlTop);
            this.Name = "MonthlyUpdateForm";
            this.Text = "Monthly Update";

            ((System.ComponentModel.ISupportInitialize)this.gridPricingMatrix).EndInit();
            this.grpMatrix.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRevDate;
        private System.Windows.Forms.DateTimePicker dtpRevisionDate;
        private System.Windows.Forms.Label lblLMERate;
        private System.Windows.Forms.TextBox txtLMERate;
        private System.Windows.Forms.Label lblBillet;
        private System.Windows.Forms.TextBox txtBilletPremium;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpMatrix;
        private System.Windows.Forms.DataGridView gridPricingMatrix;
        private System.Windows.Forms.Panel pnlTop;
    }
}
