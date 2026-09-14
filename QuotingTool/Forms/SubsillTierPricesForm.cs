using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Allows an administrator to view and edit the SubsillTierPrices table
    /// (Pricing Matrix rows 65-68 – SubSill tier prices, SubSill_Tier_Price).
    /// Loads the batch pinned to the current project (or the latest batch for
    /// new projects) and saves a brand new batch on "Save".
    /// </summary>
    public partial class SubsillTierPricesForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly DateTime _effectiveDate;
        private DataGridView grid = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;
        private BindingSource _binding = new();

        public int? SavedBatchId { get; private set; }

        public SubsillTierPricesForm(DatabaseContext db, int? pinnedBatchId, DateTime effectiveDate)
        {
            _db = db;
            _effectiveDate = effectiveDate;
            InitializeComponent();
            LoadValues(pinnedBatchId);
        }

        private void InitializeComponent()
        {
            Text = "Edit Subsill Tier Prices";
            ClientSize = new System.Drawing.Size(560, 340);
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 260,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tier", HeaderText = "Tier", DataPropertyName = "Tier", Width = 60, ReadOnly = true });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "SellPricePerMetre", HeaderText = "Sell Price ($/m)", DataPropertyName = "SellPricePerMetre", Width = 130 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "CostPerMetre", HeaderText = "Cost ($/m)", DataPropertyName = "CostPerMetre", Width = 100 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Notes", HeaderText = "Notes", DataPropertyName = "Notes", Width = 220 });

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(310, 275), Width = 100 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(420, 275), Width = 100 };
            btnSave.Click += BtnSave_Click;

            Controls.Add(grid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void LoadValues(int? pinnedBatchId)
        {
            var rows = _db.GetSubsillTierPricesByBatch(pinnedBatchId).OrderBy(r => r.Tier).ToList();
            if (rows.Count == 0)
            {
                rows = new List<SubsillTierPrice>
                {
                    new() { Tier = 1, SellPricePerMetre = 2.00m, CostPerMetre = 1.25m, Notes = "Standard subsill" },
                    new() { Tier = 2, SellPricePerMetre = 3.00m, CostPerMetre = 2.25m, Notes = "" },
                    new() { Tier = 3, SellPricePerMetre = 5.00m, CostPerMetre = 4.50m, Notes = "" },
                    new() { Tier = 4, SellPricePerMetre = 16.15m, CostPerMetre = 4.52m, Notes = "Panoramic door – 150% market factor applied to sell price" },
                };
            }
            _binding.DataSource = rows;
            grid.DataSource = _binding;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            grid.EndEdit();
            var rows = _binding.List.Cast<SubsillTierPrice>().ToList();
            foreach (var row in rows) row.Id = 0;
            SavedBatchId = _db.InsertSubsillTierPricesBatch(rows, _effectiveDate);
        }
    }
}
