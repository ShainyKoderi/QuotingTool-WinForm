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
            lblTitle = new System.Windows.Forms.Label();
            lblPageSelector = new System.Windows.Forms.Label();
            cmbPageSelector = new System.Windows.Forms.ComboBox();
            lblPrintArea = new System.Windows.Forms.Label();
            btnFixLengths = new System.Windows.Forms.Button();
            btnResetPrices = new System.Windows.Forms.Button();
            btnExportQuote = new System.Windows.Forms.Button();
            btnCopyForSap = new System.Windows.Forms.Button();
            btnSundryItems = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            btnAddRow = new System.Windows.Forms.Button();
            btnDeleteSelected = new System.Windows.Forms.Button();
            gridQuote = new System.Windows.Forms.DataGridView();
            pnlToolbar = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)gridQuote).BeginInit();
            pnlToolbar.SuspendLayout();
            SuspendLayout();

            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 8);
            lblTitle.Size = new System.Drawing.Size(300, 30);
            lblTitle.Text = "2. Quote";

            lblPageSelector.AutoSize = true;
            lblPageSelector.Location = new System.Drawing.Point(10, 48);
            lblPageSelector.Text = "Active pages:";

            cmbPageSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbPageSelector.Items.AddRange(new object[] { "1 page", "2 pages", "3 pages", "4 pages", "5 pages", "6 pages" });
            cmbPageSelector.Location = new System.Drawing.Point(110, 45);
            cmbPageSelector.Size = new System.Drawing.Size(120, 23);
            cmbPageSelector.SelectedIndex = 0;
            cmbPageSelector.SelectedIndexChanged += new System.EventHandler(cmbPageSelector_SelectedIndexChanged);

            lblPrintArea.AutoSize = true;
            lblPrintArea.Location = new System.Drawing.Point(250, 48);
            lblPrintArea.Text = "Print area: —";

            btnFixLengths.Location = new System.Drawing.Point(10, 80);
            btnFixLengths.Size = new System.Drawing.Size(150, 28);
            btnFixLengths.Text = "Fix Lengths (mm → m)";
            btnFixLengths.Click += new System.EventHandler(btnFixLengths_Click);

            btnResetPrices.Location = new System.Drawing.Point(170, 80);
            btnResetPrices.Size = new System.Drawing.Size(150, 28);
            btnResetPrices.Text = "Reset Price Formulas";
            btnResetPrices.Click += new System.EventHandler(btnResetPrices_Click);

            btnExportQuote.Location = new System.Drawing.Point(330, 80);
            btnExportQuote.Size = new System.Drawing.Size(120, 28);
            btnExportQuote.Text = "Export Quote";
            btnExportQuote.Click += new System.EventHandler(btnExportQuote_Click);

            btnCopyForSap.Location = new System.Drawing.Point(460, 80);
            btnCopyForSap.Size = new System.Drawing.Size(120, 28);
            btnCopyForSap.Text = "Copy for SAP";
            btnCopyForSap.Click += new System.EventHandler(btnCopyForSap_Click);

            btnSundryItems.Location = new System.Drawing.Point(590, 80);
            btnSundryItems.Size = new System.Drawing.Size(120, 28);
            btnSundryItems.Text = "Sundry Items";
            btnSundryItems.Click += new System.EventHandler(btnSundryItems_Click);

            btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(720, 77);
            btnSave.Size = new System.Drawing.Size(120, 34);
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += new System.EventHandler(btnSave_Click);

            btnAddRow.Location = new System.Drawing.Point(850, 80);
            btnAddRow.Size = new System.Drawing.Size(120, 28);
            btnAddRow.Text = "Add Row";
            btnAddRow.Click += new System.EventHandler(btnAddRow_Click);

            btnDeleteSelected.Location = new System.Drawing.Point(980, 80);
            btnDeleteSelected.Size = new System.Drawing.Size(120, 28);
            btnDeleteSelected.Text = "Delete Row";
            btnDeleteSelected.Click += new System.EventHandler(btnDeleteSelected_Click);

            pnlToolbar.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTitle, lblPageSelector, cmbPageSelector, lblPrintArea,
                btnFixLengths, btnResetPrices, btnExportQuote, btnCopyForSap, btnSundryItems, btnSave,
                btnAddRow, btnDeleteSelected
            });
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Height = 120;

            gridQuote.AllowUserToAddRows = false;
            gridQuote.AllowUserToDeleteRows = false;
            gridQuote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridQuote.Dock = System.Windows.Forms.DockStyle.Fill;
            gridQuote.CurrentCellDirtyStateChanged += new System.EventHandler(gridQuote_CurrentCellDirtyStateChanged);
            gridQuote.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(gridQuote_CellValueChanged);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1150, 700);
            Controls.Add(gridQuote);
            Controls.Add(pnlToolbar);
            Text = "Quote";
            ((System.ComponentModel.ISupportInitialize)gridQuote).EndInit();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPageSelector;
        private System.Windows.Forms.ComboBox cmbPageSelector;
        private System.Windows.Forms.Label lblPrintArea;
        private System.Windows.Forms.Button btnFixLengths;
        private System.Windows.Forms.Button btnResetPrices;
        private System.Windows.Forms.Button btnExportQuote;
        private System.Windows.Forms.Button btnCopyForSap;
        private System.Windows.Forms.Button btnSundryItems;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.DataGridView gridQuote;
        private System.Windows.Forms.Panel pnlToolbar;
    }
}
