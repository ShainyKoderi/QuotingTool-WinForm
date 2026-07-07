namespace QuotingTool.Forms
{
    partial class QuoteNoPricingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlButtons = new System.Windows.Forms.Panel();
            btnPrint = new System.Windows.Forms.Button();
            btnSavePdf = new System.Windows.Forms.Button();
            pnlContent = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblCustomer = new System.Windows.Forms.Label();
            lblCustomerValue = new System.Windows.Forms.Label();
            lblProject = new System.Windows.Forms.Label();
            lblProjectValue = new System.Windows.Forms.Label();
            lblQuoteDate = new System.Windows.Forms.Label();
            lblQuoteDateValue = new System.Windows.Forms.Label();
            lblValidity = new System.Windows.Forms.Label();
            lblValidityValue = new System.Windows.Forms.Label();
            lblFinish = new System.Windows.Forms.Label();
            lblFinishValue = new System.Windows.Forms.Label();
            lblQuotedBy = new System.Windows.Forms.Label();
            lblQuotedByValue = new System.Windows.Forms.Label();
            gridQuoteNoPricing = new System.Windows.Forms.DataGridView();
            pnlButtons.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridQuoteNoPricing).BeginInit();
            SuspendLayout();

            pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] { btnPrint, btnSavePdf });
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            pnlButtons.Height = 48;

            btnPrint.Location = new System.Drawing.Point(12, 10);
            btnPrint.Size = new System.Drawing.Size(100, 28);
            btnPrint.Text = "Print";
            btnPrint.Click += new System.EventHandler(btnPrint_Click);

            btnSavePdf.Location = new System.Drawing.Point(118, 10);
            btnSavePdf.Size = new System.Drawing.Size(100, 28);
            btnSavePdf.Text = "Save as PDF";
            btnSavePdf.Click += new System.EventHandler(btnSavePdf_Click);

            pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlContent.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTitle, lblCustomer, lblCustomerValue, lblProject, lblProjectValue, lblQuoteDate,
                lblQuoteDateValue, lblValidity, lblValidityValue, lblFinish, lblFinishValue,
                lblQuotedBy, lblQuotedByValue, gridQuoteNoPricing
            });

            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.Text = "QUOTATION - NOT ITEMISED";

            ConfigureField(lblCustomer, lblCustomerValue, "Customer:", 20, 140, 70);
            ConfigureField(lblProject, lblProjectValue, "Project:", 20, 140, 98);
            ConfigureField(lblQuoteDate, lblQuoteDateValue, "Quote Date:", 420, 540, 70);
            ConfigureField(lblValidity, lblValidityValue, "Validity:", 420, 540, 98);
            ConfigureField(lblFinish, lblFinishValue, "Finish:", 20, 140, 126);
            ConfigureField(lblQuotedBy, lblQuotedByValue, "Quoted By:", 420, 540, 126);

            gridQuoteNoPricing.AutoGenerateColumns = false;
            gridQuoteNoPricing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridQuoteNoPricing.Location = new System.Drawing.Point(20, 170);
            gridQuoteNoPricing.Size = new System.Drawing.Size(840, 420);
            gridQuoteNoPricing.ReadOnly = true;
            gridQuoteNoPricing.AllowUserToAddRows = false;
            gridQuoteNoPricing.AllowUserToDeleteRows = false;
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "LineNumber", HeaderText = "Ln", FillWeight = 50 });
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "ExtrusionCode", HeaderText = "Section", FillWeight = 90 });
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", FillWeight = 220 });
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Alloy", HeaderText = "Alloy", FillWeight = 70 });
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Temper", HeaderText = "Temper", FillWeight = 70 });
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "LengthM", HeaderText = "Length (m)", FillWeight = 80 });
            gridQuoteNoPricing.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty", FillWeight = 60 });

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 650);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Text = "Quote No Pricing";
            pnlButtons.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridQuoteNoPricing).EndInit();
            ResumeLayout(false);
        }

        private static void ConfigureField(System.Windows.Forms.Label label, System.Windows.Forms.Label value, string text, int lx, int vx, int y)
        {
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label.Location = new System.Drawing.Point(lx, y);
            label.Text = text;
            value.AutoSize = true;
            value.Location = new System.Drawing.Point(vx, y);
        }

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSavePdf;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblCustomerValue;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Label lblProjectValue;
        private System.Windows.Forms.Label lblQuoteDate;
        private System.Windows.Forms.Label lblQuoteDateValue;
        private System.Windows.Forms.Label lblValidity;
        private System.Windows.Forms.Label lblValidityValue;
        private System.Windows.Forms.Label lblFinish;
        private System.Windows.Forms.Label lblFinishValue;
        private System.Windows.Forms.Label lblQuotedBy;
        private System.Windows.Forms.Label lblQuotedByValue;
        private System.Windows.Forms.DataGridView gridQuoteNoPricing;
    }
}
