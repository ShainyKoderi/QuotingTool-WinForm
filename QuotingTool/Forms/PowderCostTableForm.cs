using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the PowderCostTable
    /// (Pricing Matrix rows 27-40 – powder/anodised cost table per vendor/group).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch on "Save".
    /// </summary>
    public partial class PowderCostTableForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly DateTime _effectiveDate;
        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private BindingSource _binding = new();

        public int? SavedBatchId { get; private set; }

        public PowderCostTableForm(DatabaseContext db, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Powder Cost Table";
            ClientSize = new System.Drawing.Size(1050, 460);
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 380,
                AutoGenerateColumns = false,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                RowHeadersVisible = false
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "FinishCategory", HeaderText = "Finish Category", DataPropertyName = "FinishCategory", Width = 110 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Vendor", HeaderText = "Vendor", DataPropertyName = "Vendor", Width = 220 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ApPp", HeaderText = "AP/PP", DataPropertyName = "ApPp", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group1Rate", HeaderText = "Group 1", DataPropertyName = "Group1Rate", Width = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group2Rate", HeaderText = "Group 2", DataPropertyName = "Group2Rate", Width = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group3Rate", HeaderText = "Group 3", DataPropertyName = "Group3Rate", Width = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group4Rate", HeaderText = "Group 4", DataPropertyName = "Group4Rate", Width = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group5Rate", HeaderText = "Group 5", DataPropertyName = "Group5Rate", Width = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Anodised20umRate", HeaderText = "20um Clear", DataPropertyName = "Anodised20umRate", Width = 80 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Anodised25umRate", HeaderText = "25um Clear", DataPropertyName = "Anodised25umRate", Width = 80 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "GroupPOARate", HeaderText = "Group POA", DataPropertyName = "GroupPOARate", Width = 80 });

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(790, 395), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(900, 395), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(grid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void LoadValues(int? pinnedBatchId)
        {
            var rows = _db.GetPowderCostTableByBatch(pinnedBatchId).ToList();
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<PowderCostRow>().ToList();
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertPowderCostTableBatch(rows, _effectiveDate);
        }
    }
}
