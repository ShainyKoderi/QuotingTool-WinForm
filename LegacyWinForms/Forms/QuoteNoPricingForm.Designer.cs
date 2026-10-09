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
            pnlButtons = new Panel();
            label1 = new Label();
            btnPrint = new Button();
            btnSavePdf = new Button();
            pnlContent = new Panel();
            lblTitle = new Label();
            lblCustomer = new Label();
            lblCustomerValue = new Label();
            lblProject = new Label();
            lblProjectValue = new Label();
            lblQuoteDate = new Label();
            lblQuoteDateValue = new Label();
            lblValidity = new Label();
            lblValidityValue = new Label();
            lblFinish = new Label();
            lblFinishValue = new Label();
            lblQuotedBy = new Label();
            lblQuotedByValue = new Label();
            gridQuoteNoPricing = new DataGridView();
            pnlButtons.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridQuoteNoPricing).BeginInit();
            SuspendLayout();
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(label1);
            pnlButtons.Controls.Add(btnPrint);
            pnlButtons.Controls.Add(btnSavePdf);
            pnlButtons.Dock = DockStyle.Top;
            pnlButtons.Location = new Point(0, 0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(900, 83);
            pnlButtons.TabIndex = 1;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(12, 8);
            label1.Name = "label1";
            label1.Size = new Size(300, 30);
            label1.TabIndex = 4;
            label1.Text = "4b. Quote no Pricing";
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(12, 49);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(100, 28);
            btnPrint.TabIndex = 0;
            btnPrint.Text = "Print";
            btnPrint.Click += btnPrint_Click;
            // 
            // btnSavePdf
            // 
            btnSavePdf.Location = new Point(118, 49);
            btnSavePdf.Name = "btnSavePdf";
            btnSavePdf.Size = new Size(100, 28);
            btnSavePdf.TabIndex = 1;
            btnSavePdf.Text = "Save as PDF";
            btnSavePdf.Click += btnSavePdf_Click;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(lblTitle);
            pnlContent.Controls.Add(lblCustomer);
            pnlContent.Controls.Add(lblCustomerValue);
            pnlContent.Controls.Add(lblProject);
            pnlContent.Controls.Add(lblProjectValue);
            pnlContent.Controls.Add(lblQuoteDate);
            pnlContent.Controls.Add(lblQuoteDateValue);
            pnlContent.Controls.Add(lblValidity);
            pnlContent.Controls.Add(lblValidityValue);
            pnlContent.Controls.Add(lblFinish);
            pnlContent.Controls.Add(lblFinishValue);
            pnlContent.Controls.Add(lblQuotedBy);
            pnlContent.Controls.Add(lblQuotedByValue);
            pnlContent.Controls.Add(gridQuoteNoPricing);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 83);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(900, 567);
            pnlContent.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(343, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUOTATION - NOT ITEMISED";
            // 
            // lblCustomer
            // 
            lblCustomer.Location = new Point(0, 0);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(100, 23);
            lblCustomer.TabIndex = 1;
            // 
            // lblCustomerValue
            // 
            lblCustomerValue.Location = new Point(0, 0);
            lblCustomerValue.Name = "lblCustomerValue";
            lblCustomerValue.Size = new Size(100, 23);
            lblCustomerValue.TabIndex = 2;
            // 
            // lblProject
            // 
            lblProject.Location = new Point(0, 0);
            lblProject.Name = "lblProject";
            lblProject.Size = new Size(100, 23);
            lblProject.TabIndex = 3;
            // 
            // lblProjectValue
            // 
            lblProjectValue.Location = new Point(0, 0);
            lblProjectValue.Name = "lblProjectValue";
            lblProjectValue.Size = new Size(100, 23);
            lblProjectValue.TabIndex = 4;
            // 
            // lblQuoteDate
            // 
            lblQuoteDate.Location = new Point(0, 0);
            lblQuoteDate.Name = "lblQuoteDate";
            lblQuoteDate.Size = new Size(100, 23);
            lblQuoteDate.TabIndex = 5;
            // 
            // lblQuoteDateValue
            // 
            lblQuoteDateValue.Location = new Point(0, 0);
            lblQuoteDateValue.Name = "lblQuoteDateValue";
            lblQuoteDateValue.Size = new Size(100, 23);
            lblQuoteDateValue.TabIndex = 6;
            // 
            // lblValidity
            // 
            lblValidity.Location = new Point(0, 0);
            lblValidity.Name = "lblValidity";
            lblValidity.Size = new Size(100, 23);
            lblValidity.TabIndex = 7;
            // 
            // lblValidityValue
            // 
            lblValidityValue.Location = new Point(0, 0);
            lblValidityValue.Name = "lblValidityValue";
            lblValidityValue.Size = new Size(100, 23);
            lblValidityValue.TabIndex = 8;
            // 
            // lblFinish
            // 
            lblFinish.Location = new Point(0, 0);
            lblFinish.Name = "lblFinish";
            lblFinish.Size = new Size(100, 23);
            lblFinish.TabIndex = 9;
            // 
            // lblFinishValue
            // 
            lblFinishValue.Location = new Point(0, 0);
            lblFinishValue.Name = "lblFinishValue";
            lblFinishValue.Size = new Size(100, 23);
            lblFinishValue.TabIndex = 10;
            // 
            // lblQuotedBy
            // 
            lblQuotedBy.Location = new Point(0, 0);
            lblQuotedBy.Name = "lblQuotedBy";
            lblQuotedBy.Size = new Size(100, 23);
            lblQuotedBy.TabIndex = 11;
            // 
            // lblQuotedByValue
            // 
            lblQuotedByValue.Location = new Point(0, 0);
            lblQuotedByValue.Name = "lblQuotedByValue";
            lblQuotedByValue.Size = new Size(100, 23);
            lblQuotedByValue.TabIndex = 12;
            // 
            // gridQuoteNoPricing
            // 
            gridQuoteNoPricing.AllowUserToAddRows = false;
            gridQuoteNoPricing.AllowUserToDeleteRows = false;
            gridQuoteNoPricing.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridQuoteNoPricing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridQuoteNoPricing.Location = new Point(3, 55);
            gridQuoteNoPricing.Name = "gridQuoteNoPricing";
            gridQuoteNoPricing.ReadOnly = true;
            gridQuoteNoPricing.Size = new Size(885, 434);
            gridQuoteNoPricing.TabIndex = 13;
            // 
            // QuoteNoPricingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 650);
            Controls.Add(pnlContent);
            Controls.Add(pnlButtons);
            Name = "QuoteNoPricingForm";
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
        private Label label1;
    }
}
