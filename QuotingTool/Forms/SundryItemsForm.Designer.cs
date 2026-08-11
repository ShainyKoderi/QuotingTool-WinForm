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
            gridSundryItems = new DataGridView();
            pnlButtons = new Panel();
            btnEdit26Plus = new Button();
            btnDeleteRow = new Button();
            btnDeleteAll = new Button();
            btnCloseSave = new Button();
            ((System.ComponentModel.ISupportInitialize)gridSundryItems).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // gridSundryItems
            // 
            gridSundryItems.AllowUserToAddRows = false;
            gridSundryItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSundryItems.Dock = DockStyle.Fill;
            gridSundryItems.Location = new Point(0, 52);
            gridSundryItems.Name = "gridSundryItems";
            gridSundryItems.Size = new Size(900, 448);
            gridSundryItems.TabIndex = 0;
            gridSundryItems.CellValueChanged += gridSundryItems_CellValueChanged;
            gridSundryItems.CurrentCellDirtyStateChanged += gridSundryItems_CurrentCellDirtyStateChanged;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnEdit26Plus);
            pnlButtons.Controls.Add(btnDeleteRow);
            pnlButtons.Controls.Add(btnDeleteAll);
            pnlButtons.Controls.Add(btnCloseSave);
            pnlButtons.Dock = DockStyle.Top;
            pnlButtons.Location = new Point(0, 0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(900, 52);
            pnlButtons.TabIndex = 1;
            // 
            // btnEdit26Plus
            // 
            btnEdit26Plus.Location = new Point(12, 12);
            btnEdit26Plus.Name = "btnEdit26Plus";
            btnEdit26Plus.Size = new Size(120, 28);
            btnEdit26Plus.TabIndex = 0;
            btnEdit26Plus.Text = "Add Item +";
            btnEdit26Plus.Click += btnEdit26Plus_Click;
            // 
            // btnDeleteRow – g. Delete selected row
            // 
            btnDeleteRow.Location = new Point(138, 12);
            btnDeleteRow.Name = "btnDeleteRow";
            btnDeleteRow.Size = new Size(110, 28);
            btnDeleteRow.TabIndex = 1;
            btnDeleteRow.Text = "Delete Row";
            btnDeleteRow.Click += btnDeleteRow_Click;
            // 
            // btnDeleteAll – g. Delete all rows
            // 
            btnDeleteAll.Location = new Point(254, 12);
            btnDeleteAll.Name = "btnDeleteAll";
            btnDeleteAll.Size = new Size(110, 28);
            btnDeleteAll.TabIndex = 2;
            btnDeleteAll.Text = "Delete All";
            btnDeleteAll.Click += btnDeleteAll_Click;
            // 
            // btnCloseSave
            // 
            btnCloseSave.BackColor = Color.FromArgb(0, 120, 215);
            btnCloseSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCloseSave.ForeColor = Color.White;
            btnCloseSave.Location = new Point(370, 9);
            btnCloseSave.Name = "btnCloseSave";
            btnCloseSave.Size = new Size(120, 34);
            btnCloseSave.TabIndex = 3;
            btnCloseSave.Text = "Save & Close";
            btnCloseSave.UseVisualStyleBackColor = false;
            btnCloseSave.Click += btnCloseSave_Click;
            // 
            // SundryItemsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(gridSundryItems);
            Controls.Add(pnlButtons);
            Name = "SundryItemsForm";
            Text = "Edit Sundry Items";
            ((System.ComponentModel.ISupportInitialize)gridSundryItems).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView gridSundryItems;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnEdit26Plus;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnCloseSave;
    }
}
