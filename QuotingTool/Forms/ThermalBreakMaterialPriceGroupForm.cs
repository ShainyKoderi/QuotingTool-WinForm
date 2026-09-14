using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the ThermalBreakMaterialPriceGroup table
    /// (TB_MPG named range – material price group $/kg rate lookup used by the
    /// "List less Discount" branch of the AH column PDB formula).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch on "Save".
    /// </summary>
    public partial class ThermalBreakMaterialPriceGroupForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly DateTime _effectiveDate;
        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private BindingSource _binding = new();

        public int? SavedBatchId { get; private set; }

        public ThermalBreakMaterialPriceGroupForm(DatabaseContext db, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Thermal Break Material Price Group (TB_MPG)";
            ClientSize = new System.Drawing.Size(500, 420);
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

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "GroupCode", HeaderText = "Group Code", DataPropertyName = "GroupCode", Width = 150 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RatePerKg", HeaderText = "Rate ($/kg)", DataPropertyName = "RatePerKg", Width = 150 });

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(240, 355), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(350, 355), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(grid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void LoadValues(int? pinnedBatchId)
        {
            var rows = _db.GetThermalBreakMaterialPriceGroupsByBatch(pinnedBatchId).OrderBy(r => r.GroupCode).ToList();
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<ThermalBreakMaterialPriceGroup>()
                .Where(r => !string.IsNullOrWhiteSpace(r.GroupCode))
                .ToList();
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertThermalBreakMaterialPriceGroupBatch(rows, _effectiveDate);
        }
    }
}
