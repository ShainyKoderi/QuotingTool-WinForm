using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the MetalBreakpoints table
    /// (Pricing Matrix rows 8-13 – metal cost breakpoints, Table_Metal_BP).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch (sharing one BatchId from the
    /// shared PricingBatchSeq sequence) on "Save", linking the project to that
    /// new batch.
    /// </summary>
    public partial class MetalBreakpointsForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly int _projectId;
        private readonly DateTime _effectiveDate;

        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        public int? SavedBatchId { get; private set; }

        public MetalBreakpointsForm(DatabaseContext db, int projectId, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _projectId = projectId;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Metal Breakpoints";
            ClientSize = new System.Drawing.Size(760, 420);
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

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "BreakpointKg", HeaderText = "Breakpoint (kg)", DataPropertyName = "BreakpointKg", Width = 110 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Spread", HeaderText = "Spread ($/kg)", DataPropertyName = "Spread", Width = 110 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TypicalPrice", HeaderText = "Typical ($/kg) [calc]", DataPropertyName = "TypicalPrice", Width = 130, ReadOnly = true, DefaultCellStyle = { BackColor = System.Drawing.SystemColors.Control } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MinPrice", HeaderText = "Min ($/kg) [calc]", DataPropertyName = "MinPrice", Width = 120, ReadOnly = true, DefaultCellStyle = { BackColor = System.Drawing.SystemColors.Control } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaxPrice", HeaderText = "Max ($/kg) [calc]", DataPropertyName = "MaxPrice", Width = 120, ReadOnly = true, DefaultCellStyle = { BackColor = System.Drawing.SystemColors.Control } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MinFactor", HeaderText = "Min Factor", DataPropertyName = "MinFactor", Width = 90 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaxFactor", HeaderText = "Max Factor", DataPropertyName = "MaxFactor", Width = 90 });

            grid.CellEndEdit += Grid_CellEndEdit;

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(500, 355), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(610, 355), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(grid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private BindingSource _binding = new();

        private void LoadValues(int? pinnedBatchId)
        {
            var rows = _db.GetMetalBreakpointsByBatch(pinnedBatchId).OrderBy(r => r.BreakpointKg).ToList();
            if (rows.Count == 0)
            {
                rows = new List<MetalBreakpoint>
                {
                    new() { BreakpointKg = 1,     Spread = 3.50m, MinFactor = 0.35m, MaxFactor = 0.50m },
                    new() { BreakpointKg = 2000,  Spread = 1.42m, MinFactor = 0.35m, MaxFactor = 0.50m },
                    new() { BreakpointKg = 3000,  Spread = 1.37m, MinFactor = 0.35m, MaxFactor = 0.50m },
                    new() { BreakpointKg = 5000,  Spread = 1.32m, MinFactor = 0.35m, MaxFactor = 0.50m },
                    new() { BreakpointKg = 7000,  Spread = 1.12m, MinFactor = 0.15m, MaxFactor = 0.50m },
                    new() { BreakpointKg = 10000, Spread = 1.02m, MinFactor = 0.15m, MaxFactor = 0.50m },
                };
            }
            RecalculateAll(rows);
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void Grid_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            RecalculateAll(_binding.List.Cast<MetalBreakpoint>().ToList());
            grid.Refresh();
        }

        private void RecalculateAll(List<MetalBreakpoint> rows)
        {
            var pc = _db.GetLatestPricingConstants();
            var metalCostPerKg = pc?.MetalCostPerKg ?? 8.73m;
            foreach (var row in rows)
            {
                row.TypicalPrice = metalCostPerKg + row.Spread;
                row.MinPrice = row.TypicalPrice - row.MinFactor;
                row.MaxPrice = row.TypicalPrice + row.MaxFactor;
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<MetalBreakpoint>().ToList();
            RecalculateAll(rows);
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertMetalBreakpointsBatch(rows, _effectiveDate);
        }
    }
}
