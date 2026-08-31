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
            lblTitle = new Label();
            btnFixLengths = new Button();
            btnResetPrices = new Button();
            btnImportQuoteLines = new Button();
            btnExportQuote = new Button();
            btnExportPdf = new Button();
            btnCopyForSap = new Button();
            btnSundryItems = new Button();
            btnDeleteSelected = new Button();
            btnDeleteAll = new Button();
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
            lblTitle.Location = new Point(14, 13);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(217, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quote";
            // 
            // btnFixLengths
            // 
            btnFixLengths.Location = new Point(17, 68);
            btnFixLengths.Margin = new Padding(4, 5, 4, 5);
            btnFixLengths.Name = "btnFixLengths";
            btnFixLengths.Size = new Size(214, 47);
            btnFixLengths.TabIndex = 4;
            btnFixLengths.Text = "Fix Lengths (mm → m)";
            btnFixLengths.Visible = false;
            btnFixLengths.Click += btnFixLengths_Click;
            // 
            // btnResetPrices
            // 
            btnResetPrices.Location = new Point(14, 125);
            btnResetPrices.Margin = new Padding(4, 5, 4, 5);
            btnResetPrices.Name = "btnResetPrices";
            btnResetPrices.Size = new Size(214, 47);
            btnResetPrices.TabIndex = 5;
            btnResetPrices.Text = "Reset Price Formulas";
            btnResetPrices.Visible = false;
            btnResetPrices.Click += btnResetPrices_Click;
            // 
            // btnImportQuoteLines
            // 
            btnImportQuoteLines.Location = new Point(261, 68);
            btnImportQuoteLines.Margin = new Padding(4, 5, 4, 5);
            btnImportQuoteLines.Name = "btnImportQuoteLines";
            btnImportQuoteLines.Size = new Size(171, 47);
            btnImportQuoteLines.TabIndex = 6;
            btnImportQuoteLines.Text = "Import...";
            btnImportQuoteLines.Click += btnImportQuoteLines_Click;
            // 
            // btnExportQuote
            // 
            btnExportQuote.Location = new Point(261, 133);
            btnExportQuote.Margin = new Padding(4, 5, 4, 5);
            btnExportQuote.Name = "btnExportQuote";
            btnExportQuote.Size = new Size(171, 47);
            btnExportQuote.TabIndex = 7;
            btnExportQuote.Text = "Export Quote";
            btnExportQuote.Click += btnExportQuote_Click;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Location = new Point(781, 68);
            btnExportPdf.Margin = new Padding(4, 5, 4, 5);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(171, 47);
            btnExportPdf.TabIndex = 8;
            btnExportPdf.Text = "Print / PDF...";
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // btnCopyForSap
            // 
            btnCopyForSap.Location = new Point(601, 68);
            btnCopyForSap.Margin = new Padding(4, 5, 4, 5);
            btnCopyForSap.Name = "btnCopyForSap";
            btnCopyForSap.Size = new Size(171, 47);
            btnCopyForSap.TabIndex = 9;
            btnCopyForSap.Text = "Copy for SAP";
            btnCopyForSap.Click += btnCopyForSap_Click;
            // 
            // btnSundryItems
            // 
            btnSundryItems.Location = new Point(601, 133);
            btnSundryItems.Margin = new Padding(4, 5, 4, 5);
            btnSundryItems.Name = "btnSundryItems";
            btnSundryItems.Size = new Size(171, 47);
            btnSundryItems.TabIndex = 10;
            btnSundryItems.Text = "Sundry Items";
            btnSundryItems.Click += btnSundryItems_Click;
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.Location = new Point(457, 128);
            btnDeleteSelected.Margin = new Padding(4, 5, 4, 5);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.Size = new Size(136, 47);
            btnDeleteSelected.TabIndex = 13;
            btnDeleteSelected.Text = "Delete Row";
            btnDeleteSelected.Click += btnDeleteSelected_Click;
            // 
            // btnDeleteAll
            // 
            btnDeleteAll.Location = new Point(457, 68);
            btnDeleteAll.Margin = new Padding(4, 5, 4, 5);
            btnDeleteAll.Name = "btnDeleteAll";
            btnDeleteAll.Size = new Size(136, 47);
            btnDeleteAll.TabIndex = 14;
            btnDeleteAll.Text = "Delete All";
            btnDeleteAll.Click += btnDeleteAll_Click;
            // 
            // gridQuote
            // 
            gridQuote.AllowUserToAddRows = false;
            gridQuote.AllowUserToDeleteRows = false;
            gridQuote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridQuote.ColumnHeadersHeight = 34;
            gridQuote.Dock = DockStyle.Fill;
            gridQuote.Location = new Point(0, 200);
            gridQuote.Margin = new Padding(4, 5, 4, 5);
            gridQuote.Name = "gridQuote";
            gridQuote.RowHeadersWidth = 62;
            gridQuote.Size = new Size(1077, 967);
            gridQuote.TabIndex = 0;
            gridQuote.CellValueChanged += gridQuote_CellValueChanged;
            gridQuote.CurrentCellDirtyStateChanged += gridQuote_CurrentCellDirtyStateChanged;
            gridQuote.SelectionChanged += gridQuote_SelectionChanged;
            gridQuote.KeyDown += gridQuote_KeyDown;
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(lblTitle);
            pnlToolbar.Controls.Add(btnFixLengths);
            pnlToolbar.Controls.Add(btnResetPrices);
            pnlToolbar.Controls.Add(btnImportQuoteLines);
            pnlToolbar.Controls.Add(btnExportQuote);
            pnlToolbar.Controls.Add(btnExportPdf);
            pnlToolbar.Controls.Add(btnCopyForSap);
            pnlToolbar.Controls.Add(btnSundryItems);
            pnlToolbar.Controls.Add(btnDeleteSelected);
            pnlToolbar.Controls.Add(btnDeleteAll);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 0);
            pnlToolbar.Margin = new Padding(4, 5, 4, 5);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(1336, 200);
            pnlToolbar.TabIndex = 2;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(lblPriceBreakdown);
            pnlRight.Controls.Add(lblBreakdownTitle);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(1077, 200);
            pnlRight.Margin = new Padding(4, 5, 4, 5);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(17, 20, 17, 20);
            pnlRight.Size = new Size(259, 967);
            pnlRight.TabIndex = 1;
            // 
            // lblPriceBreakdown
            // 
            lblPriceBreakdown.Dock = DockStyle.Fill;
            lblPriceBreakdown.Location = new Point(17, 70);
            lblPriceBreakdown.Margin = new Padding(4, 0, 4, 0);
            lblPriceBreakdown.Name = "lblPriceBreakdown";
            lblPriceBreakdown.Size = new Size(225, 877);
            lblPriceBreakdown.TabIndex = 0;
            lblPriceBreakdown.Text = "Select a quote line to review the calculation.";
            // 
            // lblBreakdownTitle
            // 
            lblBreakdownTitle.Dock = DockStyle.Top;
            lblBreakdownTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBreakdownTitle.Location = new Point(17, 20);
            lblBreakdownTitle.Margin = new Padding(4, 0, 4, 0);
            lblBreakdownTitle.Name = "lblBreakdownTitle";
            lblBreakdownTitle.Size = new Size(225, 50);
            lblBreakdownTitle.TabIndex = 1;
            lblBreakdownTitle.Text = "Price build-up";
            // 
            // QuoteForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1336, 1167);
            Controls.Add(gridQuote);
            Controls.Add(pnlRight);
            Controls.Add(pnlToolbar);
            Margin = new Padding(4, 5, 4, 5);
            Name = "QuoteForm";
            Text = "Quote";
            ((System.ComponentModel.ISupportInitialize)gridQuote).EndInit();
            pnlToolbar.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnFixLengths;
        private System.Windows.Forms.Button btnResetPrices;
        private System.Windows.Forms.Button btnImportQuoteLines;
        private System.Windows.Forms.Button btnExportQuote;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnCopyForSap;
        private System.Windows.Forms.Button btnSundryItems;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.DataGridView gridQuote;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblBreakdownTitle;
        private System.Windows.Forms.Label lblPriceBreakdown;
    }
}
