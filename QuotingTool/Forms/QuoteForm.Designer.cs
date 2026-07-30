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
            lblPageSelector = new Label();
            cmbPageSelector = new ComboBox();
            lblPrintArea = new Label();
            btnFixLengths = new Button();
            btnResetPrices = new Button();
            btnImportQuoteLines = new Button();
            btnExportQuote = new Button();
            btnExportPdf = new Button();
            btnCopyForSap = new Button();
            btnSundryItems = new Button();
            btnSave = new Button();
            btnAddRow = new Button();
            btnDeleteSelected = new Button();
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
            lblTitle.Location = new Point(10, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quote";
            // 
            // lblPageSelector
            // 
            lblPageSelector.AutoSize = true;
            lblPageSelector.Location = new Point(10, 48);
            lblPageSelector.Name = "lblPageSelector";
            lblPageSelector.Size = new Size(76, 15);
            lblPageSelector.TabIndex = 1;
            lblPageSelector.Text = "Pages (auto):";
            // 
            // cmbPageSelector
            // 
            cmbPageSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSelector.Enabled = false;
            cmbPageSelector.Items.AddRange(new object[] { "1 page", "2 pages", "3 pages", "4 pages", "5 pages", "6 pages" });
            cmbPageSelector.Location = new Point(110, 45);
            cmbPageSelector.Name = "cmbPageSelector";
            cmbPageSelector.Size = new Size(120, 23);
            cmbPageSelector.TabIndex = 2;
            cmbPageSelector.SelectedIndexChanged += cmbPageSelector_SelectedIndexChanged;
            // 
            // lblPrintArea
            // 
            lblPrintArea.AutoSize = true;
            lblPrintArea.Location = new Point(250, 48);
            lblPrintArea.Name = "lblPrintArea";
            lblPrintArea.Size = new Size(75, 15);
            lblPrintArea.TabIndex = 3;
            lblPrintArea.Text = "Print area: —";
            // 
            // btnFixLengths
            // 
            btnFixLengths.Location = new Point(10, 80);
            btnFixLengths.Name = "btnFixLengths";
            btnFixLengths.Size = new Size(150, 28);
            btnFixLengths.TabIndex = 4;
            btnFixLengths.Text = "Fix Lengths (mm → m)";
            btnFixLengths.Click += btnFixLengths_Click;
            // 
            // btnResetPrices
            // 
            btnResetPrices.Location = new Point(170, 80);
            btnResetPrices.Name = "btnResetPrices";
            btnResetPrices.Size = new Size(150, 28);
            btnResetPrices.TabIndex = 5;
            btnResetPrices.Text = "Reset Price Formulas";
            btnResetPrices.Click += btnResetPrices_Click;
            // 
            // btnImportQuoteLines
            // 
            btnImportQuoteLines.Location = new Point(427, 41);
            btnImportQuoteLines.Name = "btnImportQuoteLines";
            btnImportQuoteLines.Size = new Size(120, 28);
            btnImportQuoteLines.TabIndex = 6;
            btnImportQuoteLines.Text = "Import...";
            btnImportQuoteLines.Click += btnImportQuoteLines_Click;
            // 
            // btnExportQuote
            // 
            btnExportQuote.Location = new Point(553, 41);
            btnExportQuote.Name = "btnExportQuote";
            btnExportQuote.Size = new Size(120, 28);
            btnExportQuote.TabIndex = 7;
            btnExportQuote.Text = "Export Quote";
            btnExportQuote.Click += btnExportQuote_Click;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Location = new Point(679, 41);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(120, 28);
            btnExportPdf.TabIndex = 8;
            btnExportPdf.Text = "Print / PDF...";
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // btnCopyForSap
            // 
            btnCopyForSap.Location = new Point(528, 80);
            btnCopyForSap.Name = "btnCopyForSap";
            btnCopyForSap.Size = new Size(120, 28);
            btnCopyForSap.TabIndex = 9;
            btnCopyForSap.Text = "Copy for SAP";
            btnCopyForSap.Click += btnCopyForSap_Click;
            // 
            // btnSundryItems
            // 
            btnSundryItems.Location = new Point(654, 80);
            btnSundryItems.Name = "btnSundryItems";
            btnSundryItems.Size = new Size(120, 28);
            btnSundryItems.TabIndex = 10;
            btnSundryItems.Text = "Sundry Items";
            btnSundryItems.Click += btnSundryItems_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(793, 77);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 34);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnAddRow
            // 
            btnAddRow.Location = new Point(326, 80);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.Size = new Size(95, 28);
            btnAddRow.TabIndex = 12;
            btnAddRow.Text = "Add Row";
            btnAddRow.Click += btnAddRow_Click;
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.Location = new Point(427, 80);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.Size = new Size(95, 28);
            btnDeleteSelected.TabIndex = 13;
            btnDeleteSelected.Text = "Delete Row";
            btnDeleteSelected.Click += btnDeleteSelected_Click;
            // 
            // gridQuote
            // 
            gridQuote.AllowUserToAddRows = false;
            gridQuote.AllowUserToDeleteRows = false;
            gridQuote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridQuote.Dock = DockStyle.Fill;
            gridQuote.Location = new Point(0, 120);
            gridQuote.Name = "gridQuote";
            gridQuote.Size = new Size(754, 580);
            gridQuote.TabIndex = 0;
            gridQuote.CellValueChanged += gridQuote_CellValueChanged;
            gridQuote.CurrentCellDirtyStateChanged += gridQuote_CurrentCellDirtyStateChanged;
            gridQuote.SelectionChanged += gridQuote_SelectionChanged;
            gridQuote.KeyDown += gridQuote_KeyDown;
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(lblTitle);
            pnlToolbar.Controls.Add(lblPageSelector);
            pnlToolbar.Controls.Add(cmbPageSelector);
            pnlToolbar.Controls.Add(lblPrintArea);
            pnlToolbar.Controls.Add(btnFixLengths);
            pnlToolbar.Controls.Add(btnResetPrices);
            pnlToolbar.Controls.Add(btnImportQuoteLines);
            pnlToolbar.Controls.Add(btnExportQuote);
            pnlToolbar.Controls.Add(btnExportPdf);
            pnlToolbar.Controls.Add(btnCopyForSap);
            pnlToolbar.Controls.Add(btnSundryItems);
            pnlToolbar.Controls.Add(btnSave);
            pnlToolbar.Controls.Add(btnAddRow);
            pnlToolbar.Controls.Add(btnDeleteSelected);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 0);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(935, 120);
            pnlToolbar.TabIndex = 2;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(lblPriceBreakdown);
            pnlRight.Controls.Add(lblBreakdownTitle);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(754, 120);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(181, 580);
            pnlRight.TabIndex = 1;
            // 
            // lblPriceBreakdown
            // 
            lblPriceBreakdown.Dock = DockStyle.Fill;
            lblPriceBreakdown.Location = new Point(12, 42);
            lblPriceBreakdown.Name = "lblPriceBreakdown";
            lblPriceBreakdown.Size = new Size(157, 526);
            lblPriceBreakdown.TabIndex = 0;
            lblPriceBreakdown.Text = "Select a quote line to review the calculation.";
            // 
            // lblBreakdownTitle
            // 
            lblBreakdownTitle.Dock = DockStyle.Top;
            lblBreakdownTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBreakdownTitle.Location = new Point(12, 12);
            lblBreakdownTitle.Name = "lblBreakdownTitle";
            lblBreakdownTitle.Size = new Size(157, 30);
            lblBreakdownTitle.TabIndex = 1;
            lblBreakdownTitle.Text = "Price build-up";
            // 
            // QuoteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(935, 700);
            Controls.Add(gridQuote);
            Controls.Add(pnlRight);
            Controls.Add(pnlToolbar);
            Name = "QuoteForm";
            Text = "Quote";
            ((System.ComponentModel.ISupportInitialize)gridQuote).EndInit();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            pnlRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPageSelector;
        private System.Windows.Forms.ComboBox cmbPageSelector;
        private System.Windows.Forms.Label lblPrintArea;
        private System.Windows.Forms.Button btnFixLengths;
        private System.Windows.Forms.Button btnResetPrices;
        private System.Windows.Forms.Button btnImportQuoteLines;
        private System.Windows.Forms.Button btnExportQuote;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnCopyForSap;
        private System.Windows.Forms.Button btnSundryItems;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.DataGridView gridQuote;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblBreakdownTitle;
        private System.Windows.Forms.Label lblPriceBreakdown;
    }
}
