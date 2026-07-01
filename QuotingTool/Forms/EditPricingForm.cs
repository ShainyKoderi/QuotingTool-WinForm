using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Tab 3 – Edit Pricing
    /// Fields: Cost Type, Fin Type, Cost Type Override, Metal Price, Finishing Price,
    /// Prices B42/B44/B48/B50. Shows row visibility result.
    /// </summary>
    public partial class EditPricingForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly ShowHideBulkService _showHide;
        private readonly EditPricingRowVisibilityService _rowVis;
        private int _projectId;

        private static readonly string[] CostTypeOptions =
        {
            "0 – Standard",
            "1 – POA Colour",
            "2 – Custom Supplier",
            "4 – Bremer Mill Standard",
            "5 – Bremer Mill POA"
        };
        private static readonly int[] CostTypeValues = { 0, 1, 2, 4, 5 };

        public EditPricingForm(DatabaseContext db)
        {
            _db       = db;
            _showHide = new ShowHideBulkService(db);
            _rowVis   = new EditPricingRowVisibilityService(db);
            InitializeComponent();

            foreach (var opt in CostTypeOptions)
                cmbCostType.Items.Add(opt);
            cmbCostType.SelectedIndex = 0;

            cmbFinType.Items.Add("0 – Not Powder Coat");
            cmbFinType.Items.Add("1 – Powder Coat");
            cmbFinType.SelectedIndex = 0;
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };

            // Map CostType value to combo index
            int ctIndex = Array.IndexOf(CostTypeValues, ep.CostType);
            cmbCostType.SelectedIndex = ctIndex >= 0 ? ctIndex : 0;

            cmbFinType.SelectedIndex = ep.FinType == 1 ? 1 : 0;
            chkCostTypeOverride.Checked = ep.CostTypeOverride;
            txtMetalPrice.Text     = ep.MetalPrice.ToString("F4");
            txtFinishingPrice.Text = ep.FinishingPrice.ToString("F4");
            txtB42.Text = ep.Price_B42.ToString("F2");
            txtB44.Text = ep.Price_B44.ToString("F2");
            txtB48.Text = ep.Price_B48.ToString("F2");
            txtB50.Text = ep.Price_B50.ToString("F2");
        }

        public void SavePricing()
        {
            if (_projectId <= 0) return;
            var ep = _db.GetEditPricing(_projectId) ?? new EditPricing { ProjectId = _projectId };

            ep.CostType        = CostTypeValues[Math.Max(0, cmbCostType.SelectedIndex)];
            ep.FinType         = cmbFinType.SelectedIndex;
            ep.CostTypeOverride = chkCostTypeOverride.Checked;
            ep.MetalPrice      = ParseDecimal(txtMetalPrice.Text);
            ep.FinishingPrice  = ParseDecimal(txtFinishingPrice.Text);
            ep.Price_B42 = ParseDecimal(txtB42.Text);
            ep.Price_B44 = ParseDecimal(txtB44.Text);
            ep.Price_B48 = ParseDecimal(txtB48.Text);
            ep.Price_B50 = ParseDecimal(txtB50.Text);

            _db.UpsertEditPricing(ep);
            MessageBox.Show("Pricing saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e) => SavePricing();

        private void btnToggleOverride_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            bool newValue = _showHide.ToggleCostTypeOverride(_projectId);
            chkCostTypeOverride.Checked = newValue;
            UpdateRowVisibility();
        }

        private void btnUpdateVisibility_Click(object sender, EventArgs e)
        {
            SavePricing();
            UpdateRowVisibility();
        }

        private void UpdateRowVisibility()
        {
            if (_projectId <= 0) return;
            var result = _rowVis.GetVisibleRows(_projectId);
            result.VisibleRows.Sort();
            result.HiddenRows.Sort();
            lblVisibleRows.Text = $"Visible rows: {string.Join(", ", result.VisibleRows)}";
            lblHiddenRows.Text  = $"Hidden rows: {string.Join(", ", result.HiddenRows)}";
        }

        private static decimal ParseDecimal(string text)
            => decimal.TryParse(text, out decimal v) ? v : 0m;
    }
}
