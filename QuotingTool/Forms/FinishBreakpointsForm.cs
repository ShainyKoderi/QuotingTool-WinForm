using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the FinishBreakpoints table
    /// (Pricing Matrix rows 52-60 – finish margin/price breakpoints by m2).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch on "Save".
    /// </summary>
    public partial class FinishBreakpointsForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly DateTime _effectiveDate;
        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private BindingSource _binding = new();

        public int? SavedBatchId { get; private set; }

        public FinishBreakpointsForm(DatabaseContext db, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Finish Breakpoints";
            ClientSize = new System.Drawing.Size(1150, 460);
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 380,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "BreakpointAP", HeaderText = "AP (m2)", DataPropertyName = "BreakpointAP", Width = 65 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "BreakpointPP", HeaderText = "PP (m2)", DataPropertyName = "BreakpointPP", Width = 65 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeA_Typical", HeaderText = "A Typ", DataPropertyName = "RangeA_Typical", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeA_Min", HeaderText = "A Min", DataPropertyName = "RangeA_Min", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeA_Max", HeaderText = "A Max", DataPropertyName = "RangeA_Max", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeB_Typical", HeaderText = "B Typ", DataPropertyName = "RangeB_Typical", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeB_Min", HeaderText = "B Min", DataPropertyName = "RangeB_Min", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeB_Max", HeaderText = "B Max", DataPropertyName = "RangeB_Max", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeC_Typical", HeaderText = "C Typ", DataPropertyName = "RangeC_Typical", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeC_Min", HeaderText = "C Min", DataPropertyName = "RangeC_Min", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeC_Max", HeaderText = "C Max", DataPropertyName = "RangeC_Max", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeD_Typical", HeaderText = "D Typ", DataPropertyName = "RangeD_Typical", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeD_Min", HeaderText = "D Min", DataPropertyName = "RangeD_Min", Width = 60 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RangeD_Max", HeaderText = "D Max", DataPropertyName = "RangeD_Max", Width = 60 });

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(890, 395), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(1000, 395), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(grid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void LoadValues(int? pinnedBatchId)
        {
            var rows = _db.GetFinishBreakpointsByBatch(pinnedBatchId).OrderBy(r => r.BreakpointAP).ToList();
            if (rows.Count == 0)
            {
                rows = new List<FinishBreakpoint>
                {
                    new() { BreakpointAP=0,    BreakpointPP=0,    RangeA_Typical=0.40m, RangeA_Min=0.35m, RangeA_Max=0.45m, RangeB_Typical=0.50m, RangeB_Min=0.45m, RangeB_Max=0.55m, RangeC_Typical=0.40m, RangeC_Min=0.35m, RangeC_Max=0.45m, RangeD_Typical=0.50m, RangeD_Min=0.45m, RangeD_Max=0.55m },
                    new() { BreakpointAP=100,  BreakpointPP=60,   RangeA_Typical=0.25m, RangeA_Min=0.23m, RangeA_Max=0.30m, RangeB_Typical=0.27m, RangeB_Min=0.25m, RangeB_Max=0.32m, RangeC_Typical=0.25m, RangeC_Min=0.23m, RangeC_Max=0.30m, RangeD_Typical=0.30m, RangeD_Min=0.28m, RangeD_Max=0.35m },
                    new() { BreakpointAP=500,  BreakpointPP=270,  RangeA_Typical=0.22m, RangeA_Min=0.20m, RangeA_Max=0.27m, RangeB_Typical=0.24m, RangeB_Min=0.22m, RangeB_Max=0.29m, RangeC_Typical=0.22m, RangeC_Min=0.20m, RangeC_Max=0.27m, RangeD_Typical=0.25m, RangeD_Min=0.23m, RangeD_Max=0.30m },
                    new() { BreakpointAP=700,  BreakpointPP=370,  RangeA_Typical=0.20m, RangeA_Min=0.17m, RangeA_Max=0.25m, RangeB_Typical=0.22m, RangeB_Min=0.19m, RangeB_Max=0.27m, RangeC_Typical=0.20m, RangeC_Min=0.17m, RangeC_Max=0.25m, RangeD_Typical=0.22m, RangeD_Min=0.20m, RangeD_Max=0.27m },
                    new() { BreakpointAP=1000, BreakpointPP=530,  RangeA_Typical=0.17m, RangeA_Min=0.15m, RangeA_Max=0.20m, RangeB_Typical=0.20m, RangeB_Min=0.17m, RangeB_Max=0.23m, RangeC_Typical=0.17m, RangeC_Min=0.15m, RangeC_Max=0.20m, RangeD_Typical=0.20m, RangeD_Min=0.18m, RangeD_Max=0.25m },
                    new() { BreakpointAP=2000, BreakpointPP=1060, RangeA_Typical=0.15m, RangeA_Min=0.135m,RangeA_Max=0.18m, RangeB_Typical=0.18m, RangeB_Min=0.155m,RangeB_Max=0.20m, RangeC_Typical=0.15m, RangeC_Min=0.135m,RangeC_Max=0.18m, RangeD_Typical=0.17m, RangeD_Min=0.15m,  RangeD_Max=0.22m },
                    new() { BreakpointAP=4000, BreakpointPP=2110, RangeA_Typical=0.13m, RangeA_Min=0.12m, RangeA_Max=0.15m, RangeB_Typical=0.16m, RangeB_Min=0.14m, RangeB_Max=0.18m, RangeC_Typical=0.13m, RangeC_Min=0.12m, RangeC_Max=0.15m, RangeD_Typical=0.15m, RangeD_Min=0.13m,  RangeD_Max=0.18m },
                };
            }
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<FinishBreakpoint>().ToList();
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertFinishBreakpointsBatch(rows, _effectiveDate);
        }
    }
}
