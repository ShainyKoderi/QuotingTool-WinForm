namespace QuotingTool.Forms
{
    partial class MillFinishQuoteForm
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
            btnExportPdf = new Button();
            gridQuote = new DataGridView();
            pnlToolbar = new Panel();
            pnlRight = new Panel();
            lblPriceBreakdown = new Label();
            lblBreakdownTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)gridQuote).BeginInit();
            pnlToolbar.SuspendLayout();
            pnlRight.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkSlateGray;
            lblTitle.Location = new Point(10, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(565, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Mill Finish Alternative Quote  (read-only – finishing cost = $0)";
            // 
            // btnExportPdf
            // 
            btnExportPdf.Location = new Point(10, 50);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(150, 28);
            btnExportPdf.TabIndex = 1;
            btnExportPdf.Text = "Export Mill Finish PDF...";
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // gridQuote
            // 
            gridQuote.AllowUserToAddRows = false;
            gridQuote.AllowUserToDeleteRows = false;
            gridQuote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridQuote.Dock = DockStyle.Fill;
            gridQuote.Location = new Point(0, 90);
            gridQuote.Name = "gridQuote";
            gridQuote.ReadOnly = true;
            gridQuote.Size = new Size(754, 610);
            gridQuote.TabIndex = 0;
            gridQuote.SelectionChanged += gridQuote_SelectionChanged;
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(lblTitle);
            pnlToolbar.Controls.Add(btnExportPdf);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 0);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(935, 90);
            pnlToolbar.TabIndex = 2;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(lblPriceBreakdown);
            pnlRight.Controls.Add(lblBreakdownTitle);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(754, 90);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(181, 610);
            pnlRight.TabIndex = 1;
            // 
            // lblPriceBreakdown
            // 
            lblPriceBreakdown.Dock = DockStyle.Fill;
            lblPriceBreakdown.Location = new Point(12, 42);
            lblPriceBreakdown.Name = "lblPriceBreakdown";
            lblPriceBreakdown.Size = new Size(157, 556);
            lblPriceBreakdown.TabIndex = 0;
            lblPriceBreakdown.Text = "Select a quote line to review the Mill Finish calculation.";
            // 
            // lblBreakdownTitle
            // 
            lblBreakdownTitle.Dock = DockStyle.Top;
            lblBreakdownTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBreakdownTitle.Location = new Point(12, 12);
            lblBreakdownTitle.Name = "lblBreakdownTitle";
            lblBreakdownTitle.Size = new Size(157, 30);
            lblBreakdownTitle.TabIndex = 1;
            lblBreakdownTitle.Text = "MF Price build-up";
            // 
            // MillFinishQuoteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(935, 700);
            Controls.Add(gridQuote);
            Controls.Add(pnlRight);
            Controls.Add(pnlToolbar);
            Name = "MillFinishQuoteForm";
            Text = "Mill Finish Alternative Quote";
            ((System.ComponentModel.ISupportInitialize)gridQuote).EndInit();
            pnlToolbar.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.DataGridView gridQuote;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblBreakdownTitle;
        private System.Windows.Forms.Label lblPriceBreakdown;
    }
}
