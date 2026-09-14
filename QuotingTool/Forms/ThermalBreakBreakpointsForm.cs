using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the ThermalBreakBreakpoints table
    /// (Pricing Matrix rows 16-21 – thermal break margin breakpoints, Table_TB_BP).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch on "Save".
    /// </summary>
    public partial class ThermalBreakBreakpointsForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly DateTime _effectiveDate;
        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private BindingSource _binding = new();

        public int? SavedBatchId { get; private set; }

        public ThermalBreakBreakpointsForm(DatabaseContext db, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Thermal Break Breakpoints";
            ClientSize = new System.Drawing.Size(820, 420);
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 340,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "BreakpointKg", HeaderText = "Breakpoint (kg)", DataPropertyName = "BreakpointKg", Width = 100 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MarginTyp", HeaderText = "Margin Typ", DataPropertyName = "MarginTyp", Width = 90 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MarginMin", HeaderText = "Margin Min", DataPropertyName = "MarginMin", Width = 90 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MarginMax", HeaderText = "Margin Max", DataPropertyName = "MarginMax", Width = 90 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ListTyp", HeaderText = "List Typ ($/m)", DataPropertyName = "ListTyp", Width = 100 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ListMin", HeaderText = "List Min ($/m)", DataPropertyName = "ListMin", Width = 100 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ListMax", HeaderText = "List Max ($/m)", DataPropertyName = "ListMax", Width = 100 });

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
            var rows = _db.GetThermalBreakBreakpointsByBatch(pinnedBatchId).OrderBy(r => r.BreakpointKg).ToList();
            if (rows.Count == 0)
            {
                rows = new List<ThermalBreakBreakpoint>
                {
                    new() { BreakpointKg = 1000,  MarginTyp = 0.40m, MarginMin = 0.38m, MarginMax = 0.43m, ListTyp = 0.47m, ListMin = 0.44m, ListMax = 0.50m },
                    new() { BreakpointKg = 2000,  MarginTyp = 0.35m, MarginMin = 0.33m, MarginMax = 0.38m, ListTyp = 0.52m, ListMin = 0.48m, ListMax = 0.54m },
                    new() { BreakpointKg = 3000,  MarginTyp = 0.32m, MarginMin = 0.30m, MarginMax = 0.35m, ListTyp = 0.54m, ListMin = 0.50m, ListMax = 0.56m },
                    new() { BreakpointKg = 5000,  MarginTyp = 0.29m, MarginMin = 0.27m, MarginMax = 0.32m, ListTyp = 0.56m, ListMin = 0.52m, ListMax = 0.58m },
                    new() { BreakpointKg = 7000,  MarginTyp = 0.27m, MarginMin = 0.25m, MarginMax = 0.30m, ListTyp = 0.58m, ListMin = 0.53m, ListMax = 0.59m },
                    new() { BreakpointKg = 10000, MarginTyp = 0.25m, MarginMin = 0.23m, MarginMax = 0.28m, ListTyp = 0.59m, ListMin = 0.54m, ListMax = 0.60m },
                };
            }
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<ThermalBreakBreakpoint>().ToList();
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertThermalBreakBreakpointsBatch(rows, _effectiveDate);
        }
    }
}
