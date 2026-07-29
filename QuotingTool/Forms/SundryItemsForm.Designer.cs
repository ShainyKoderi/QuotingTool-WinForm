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
            btnEdit26Plus = new System.Windows.Forms.Button();
            btnCloseSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)gridSundryItems).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();

            gridSundryItems.AllowUserToAddRows = false;
            gridSundryItems.AutoGenerateColumns = false;
            gridSundryItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridSundryItems.Dock = System.Windows.Forms.DockStyle.Fill;
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "SectionCode", HeaderText = "Section", Name = "SectionCode", FillWeight = 90, ReadOnly = true });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", Name = "Description", FillWeight = 180 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "MassKgPerM", HeaderText = "Mass kg/m", Name = "MassKgPerM", FillWeight = 80 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Ap", HeaderText = "AP", Name = "Ap", FillWeight = 70 });
            gridSundryItems.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Pp", HeaderText = "PP", Name = "Pp", FillWeight = 70 });
            gridSundryItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(gridSundryItems_CellValueChanged);
            gridSundryItems.CurrentCellDirtyStateChanged += new System.EventHandler(gridSundryItems_CurrentCellDirtyStateChanged);

            pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[] { btnEdit26Plus, btnCloseSave });
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            pnlButtons.Height = 52;

            btnEdit26Plus.Location = new System.Drawing.Point(12, 12);
            btnEdit26Plus.Size = new System.Drawing.Size(130, 28);
            btnEdit26Plus.Text = "Edit Items 26+";
            btnEdit26Plus.Click += new System.EventHandler(btnEdit26Plus_Click);

            btnCloseSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnCloseSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCloseSave.ForeColor = System.Drawing.Color.White;
            btnCloseSave.Location = new System.Drawing.Point(148, 9);
            btnCloseSave.Size = new System.Drawing.Size(120, 34);
            btnCloseSave.Text = "Close";
            btnCloseSave.UseVisualStyleBackColor = false;
            btnCloseSave.Click += new System.EventHandler(btnCloseSave_Click);

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
        private System.Windows.Forms.Button btnEdit26Plus;
        private System.Windows.Forms.Button btnCloseSave;
    }
}
