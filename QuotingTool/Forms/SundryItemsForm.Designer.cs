namespace QuotingTool.Forms
{
    partial class SundryItemsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gridSundryItems = new System.Windows.Forms.DataGridView();
            pnlButtons = new System.Windows.Forms.Panel();
            btnAddRow = new System.Windows.Forms.Button();
            btnDeleteSelected = new System.Windows.Forms.Button();
            btnSaveAll = new System.Windows.Forms.Button();
            btnRecalculate = new System.Windows.Forms.Button();
            lblSundryTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)gridSundryItems).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();

            gridSundryItems.AllowUserToAddRows = false;
            gridSundryItems.AutoGenerateColumns = false;
            gridSundryItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridSundryItems.Dock = System.Windows.Forms.DockStyle.Fill;
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "LineNumber", HeaderText = "Line#", Name = "LineNumber", FillWeight = 60 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", Name = "Description", FillWeight = 180 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty", Name = "Quantity", FillWeight = 70 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Unit Price ($)", Name = "UnitPrice", FillWeight = 90 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Total ($)", Name = "TotalPrice", FillWeight = 90, ReadOnly = true });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Category", Name = "Category", FillWeight = 90 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Notes", Name = "Notes", FillWeight = 140 });
            gridSundryItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(gridSundryItems_CellValueChanged);
            gridSundryItems.CurrentCellDirtyStateChanged += new System.EventHandler(gridSundryItems_CurrentCellDirtyStateChanged);

            pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] { btnAddRow, btnDeleteSelected, btnSaveAll, btnRecalculate, lblSundryTotal });
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            pnlButtons.Height = 52;

            btnAddRow.Location = new System.Drawing.Point(12, 12);
            btnAddRow.Size = new System.Drawing.Size(100, 28);
            btnAddRow.Text = "Add Row";
            btnAddRow.Click += new System.EventHandler(btnAddRow_Click);

            btnDeleteSelected.Location = new System.Drawing.Point(118, 12);
            btnDeleteSelected.Size = new System.Drawing.Size(120, 28);
            btnDeleteSelected.Text = "Delete Selected";
            btnDeleteSelected.Click += new System.EventHandler(btnDeleteSelected_Click);

            btnSaveAll.Location = new System.Drawing.Point(244, 12);
            btnSaveAll.Size = new System.Drawing.Size(100, 28);
            btnSaveAll.Text = "Save All";
            btnSaveAll.Click += new System.EventHandler(btnSaveAll_Click);

            btnRecalculate.Location = new System.Drawing.Point(350, 12);
            btnRecalculate.Size = new System.Drawing.Size(100, 28);
            btnRecalculate.Text = "Recalculate";
            btnRecalculate.Click += new System.EventHandler(btnRecalculate_Click);

            lblSundryTotal.AutoSize = true;
            lblSundryTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblSundryTotal.Location = new System.Drawing.Point(470, 17);
            lblSundryTotal.Text = "Sundry Total: $0.00";

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 500);
            Controls.Add(gridSundryItems);
            Controls.Add(pnlButtons);
            Text = "Edit Sundry Items";
            ((System.ComponentModel.ISupportInitialize)gridSundryItems).EndInit();
            pnlButtons.ResumeLayout(false);
            pnlButtons.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView gridSundryItems;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnRecalculate;
        private System.Windows.Forms.Label lblSundryTotal;
    }
}
