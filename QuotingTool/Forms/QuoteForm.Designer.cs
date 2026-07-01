namespace QuotingTool.Forms
{
    partial class QuoteForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle          = new System.Windows.Forms.Label();
            this.lblPageSelector   = new System.Windows.Forms.Label();
            this.cmbPageSelector   = new System.Windows.Forms.ComboBox();
            this.lblPrintArea      = new System.Windows.Forms.Label();
            this.btnFixLengths     = new System.Windows.Forms.Button();
            this.btnResetPrices    = new System.Windows.Forms.Button();
            this.btnExportQuote    = new System.Windows.Forms.Button();
            this.gridQuote         = new System.Windows.Forms.DataGridView();
            this.pnlToolbar        = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)this.gridQuote).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.Text = "2. Quote";

            // lblPageSelector
            this.lblPageSelector.AutoSize = true;
            this.lblPageSelector.Location = new System.Drawing.Point(10, 48);
            this.lblPageSelector.Text = "Active pages:";

            // cmbPageSelector
            this.cmbPageSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSelector.Items.AddRange(new object[] { "1 page", "2 pages", "3 pages", "4 pages", "5 pages", "6 pages" });
            this.cmbPageSelector.Location = new System.Drawing.Point(110, 45);
            this.cmbPageSelector.Name = "cmbPageSelector";
            this.cmbPageSelector.Size = new System.Drawing.Size(120, 23);
            this.cmbPageSelector.SelectedIndex = 0;
            this.cmbPageSelector.SelectedIndexChanged += new System.EventHandler(this.cmbPageSelector_SelectedIndexChanged);

            // lblPrintArea
            this.lblPrintArea.AutoSize = true;
            this.lblPrintArea.Location = new System.Drawing.Point(250, 48);
            this.lblPrintArea.Name = "lblPrintArea";
            this.lblPrintArea.Text = "Print area: —";

            // Buttons
            this.btnFixLengths.Location = new System.Drawing.Point(10, 80);
            this.btnFixLengths.Name = "btnFixLengths";
            this.btnFixLengths.Size = new System.Drawing.Size(160, 28);
            this.btnFixLengths.Text = "Fix Lengths (mm → m)";
            this.btnFixLengths.Click += new System.EventHandler(this.btnFixLengths_Click);

            this.btnResetPrices.Location = new System.Drawing.Point(180, 80);
            this.btnResetPrices.Name = "btnResetPrices";
            this.btnResetPrices.Size = new System.Drawing.Size(160, 28);
            this.btnResetPrices.Text = "Reset Price Formulas";
            this.btnResetPrices.Click += new System.EventHandler(this.btnResetPrices_Click);

            this.btnExportQuote.Location = new System.Drawing.Point(350, 80);
            this.btnExportQuote.Name = "btnExportQuote";
            this.btnExportQuote.Size = new System.Drawing.Size(160, 28);
            this.btnExportQuote.Text = "Export Quote Data";
            this.btnExportQuote.Click += new System.EventHandler(this.btnExportQuote_Click);

            // pnlToolbar
            this.pnlToolbar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblPageSelector, this.cmbPageSelector, this.lblPrintArea,
                this.btnFixLengths, this.btnResetPrices, this.btnExportQuote
            });
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 120;
            this.pnlToolbar.Name = "pnlToolbar";

            // gridQuote
            this.gridQuote.AllowUserToAddRows = false;
            this.gridQuote.AllowUserToDeleteRows = false;
            this.gridQuote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridQuote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridQuote.Name = "gridQuote";
            this.gridQuote.ReadOnly = false;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 700);
            this.Controls.Add(this.gridQuote);
            this.Controls.Add(this.pnlToolbar);
            this.Name = "QuoteForm";
            this.Text = "Quote";

            ((System.ComponentModel.ISupportInitialize)this.gridQuote).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPageSelector;
        private System.Windows.Forms.ComboBox cmbPageSelector;
        private System.Windows.Forms.Label lblPrintArea;
        private System.Windows.Forms.Button btnFixLengths;
        private System.Windows.Forms.Button btnResetPrices;
        private System.Windows.Forms.Button btnExportQuote;
        private System.Windows.Forms.DataGridView gridQuote;
        private System.Windows.Forms.Panel pnlToolbar;
    }
}
