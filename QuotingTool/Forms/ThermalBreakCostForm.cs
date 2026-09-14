using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the ThermalBreakCost table (Table_TB_Cost –
    /// per-section thermal-break (PLS/PDB) metal cost reference table used by the
    /// 2.Quote!AH column formula).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch on "Save".
    /// </summary>
    public partial class ThermalBreakCostForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly DateTime _effectiveDate;
        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private BindingSource _binding = new();

        public int? SavedBatchId { get; private set; }

        public ThermalBreakCostForm(DatabaseContext db, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Thermal Break Cost (Table_TB_Cost)";
            ClientSize = new System.Drawing.Size(820, 420);
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 340,
                AutoGenerateColumns = false,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                RowHeadersVisible = false
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Section", HeaderText = "Section", DataPropertyName = "Section", Width = 130 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MetalCost", HeaderText = "Metal Cost ($)", DataPropertyName = "MetalCost", Width = 130 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReferenceLengthM", HeaderText = "Reference Length (m)", DataPropertyName = "ReferenceLengthM", Width = 150 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaterialPriceGroupCode", HeaderText = "Material Price Group Code", DataPropertyName = "MaterialPriceGroupCode", Width = 170 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MetalSellPrice", HeaderText = "Metal Sell Price ($)", DataPropertyName = "MetalSellPrice", Width = 140 });

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(560, 355), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(670, 355), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(grid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void LoadValues(int? pinnedBatchId)
        {
            var rows = _db.GetThermalBreakCostsByBatch(pinnedBatchId).OrderBy(r => r.Section).ToList();
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<ThermalBreakCost>()
                .Where(r => !string.IsNullOrWhiteSpace(r.Section))
                .ToList();
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertThermalBreakCostBatch(rows, _effectiveDate);
        }
    }
}
