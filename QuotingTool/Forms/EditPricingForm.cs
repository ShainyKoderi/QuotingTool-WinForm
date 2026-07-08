using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class EditPricingForm : Form
    {
        private const decimal DefaultIP1Upcharge = 1.00m;
        private const decimal DefaultIP2Upcharge = 2.00m;
        private const decimal DefaultIP3Rate = 25.85m;
        private const decimal DefaultAnodisedThresholdCost = 10.65m;

        private readonly DatabaseContext _db;
        private readonly ShowHideBulkService _showHide;
        private readonly EditPricingRowVisibilityService _rowVis;
        private readonly QuoteSummaryService _quoteSummaryService;
        private readonly PivotRefreshService _pivotRefresh;
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
            _db = db;
            _showHide = new ShowHideBulkService(db);
            _rowVis = new EditPricingRowVisibilityService(db);
            _quoteSummaryService = new QuoteSummaryService(db);
            _pivotRefresh = new PivotRefreshService(db);
            InitializeComponent();

            cmbCostType.Items.AddRange(CostTypeOptions);
            cmbFinType.Items.AddRange(new object[] { "0 – Not Powder Coat", "1 – Powder Coat" });
            cmbTBCalcType.Items.AddRange(new object[] { "Margin", "List less Discount" });
            if (cmbCostType.SelectedIndex < 0) cmbCostType.SelectedIndex = 0;
            if (cmbFinType.SelectedIndex < 0) cmbFinType.SelectedIndex = 0;
            if (cmbTBCalcType.SelectedIndex < 0) cmbTBCalcType.SelectedIndex = 0;
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };
            var project = _db.GetProject(projectId) ?? new Project();

            txtPricingNotes.Text = ep.PricingNotes;
            int ctIndex = Array.IndexOf(CostTypeValues, ep.CostType);
            cmbCostType.SelectedIndex = ctIndex >= 0 ? ctIndex : 0;
            cmbFinType.SelectedIndex = ep.FinType == 1 ? 1 : 0;
            chkCostTypeOverride.Checked = ep.CostTypeOverride;
            txtMetalPrice.Text = ep.MetalPrice.ToString("F4");
            txtFinishingPrice.Text = ep.FinishingPrice.ToString("F4");
            txtFinishingCostPricePerM2.Text = ep.FinishingCostPricePerM2.ToString("F4");
            txtB42.Text = ep.Price_B42.ToString("F2");
            txtB44.Text = ep.Price_B44.ToString("F2");
            txtB48.Text = ep.Price_B48.ToString("F2");
            txtB50.Text = ep.Price_B50.ToString("F2");
            txtB52.Text = ep.Price_B52.ToString("F2");
            txtB54.Text = ep.Price_B54.ToString("F2");
            txtB57.Text = ep.Price_B57.ToString("F4");
            txtB58.Text = ep.Price_B58.ToString("F2");
            txtCustomerSapDiscount.Text = project.CustomerSapDiscount.ToString("F2");
            txtIP1Upcharge.Text = ep.IP1UpchargePerKg.ToString("F2");
            txtIP2Upcharge.Text = ep.IP2UpchargePerKg.ToString("F2");
            txtIP3Rate.Text = ep.IP3PanoramicFixedKgRate.ToString("F2");
            txtAnodisedThresholdCost.Text = ep.AnodisedThresholdCostPerM2.ToString("F2");
            int tbIdx = cmbTBCalcType.FindStringExact(ep.ThermalBreakCalcType);
            cmbTBCalcType.SelectedIndex = tbIdx >= 0 ? tbIdx : 0;
            txtTBMargin.Text = ep.ThermalBreakMargin.ToString("F4");
            UpdateDerivedFields(saveChanges: false);
        }

        public void SavePricing()
        {
            if (_projectId <= 0) return;
            UpdateDerivedFields(saveChanges: true);
            MessageBox.Show("Pricing saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e) => SavePricing();

        private void btnToggleOverride_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            chkCostTypeOverride.Checked = _showHide.ToggleCostTypeOverride(_projectId);
            UpdateRowVisibility();
        }

        private void btnUpdateVisibility_Click(object sender, EventArgs e)
        {
            UpdateDerivedFields(saveChanges: true);
            UpdateRowVisibility();
        }

        private void btnEditIpCategories_Click(object sender, EventArgs e)
        {
            MessageBox.Show("IP Categories: IP1=Standard upcharge, IP2=Premium upcharge, IP3=Panoramic door fixed rate",
                "IP Categories", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnResetIpCharges_Click(object sender, EventArgs e)
        {
            txtIP1Upcharge.Text = DefaultIP1Upcharge.ToString("F2");
            txtIP2Upcharge.Text = DefaultIP2Upcharge.ToString("F2");
            txtIP3Rate.Text = DefaultIP3Rate.ToString("F2");
            SavePricing();
        }

        private void btnBulkNonBulkRates_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bulk rates apply when total finishing area > 100m². Non-bulk rates apply below this threshold.",
                "Bulk/Non-bulk Rates", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditSundryItems_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            using var form = new SundryItemsForm(_db);
            form.LoadProject(_projectId);
            form.ShowDialog(this);
            UpdateDerivedFields(saveChanges: false);
        }

        private void btnRecalculateAll_Click(object sender, EventArgs e) => UpdateDerivedFields(saveChanges: true);

        private void btnRefreshSummary_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) { MessageBox.Show("No project open."); return; }
            var summary = _pivotRefresh.RefreshSummary(_projectId);
            UpdateDerivedFields(saveChanges: true);
            MessageBox.Show(
                $"Summary refreshed.\nTotal rows: {summary.TotalRows}\nGrand total price: {summary.GrandTotalPrice:C}",
                "Refresh Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDerivedFields(bool saveChanges)
        {
            if (_projectId <= 0) return;

            var ep = _db.GetEditPricing(_projectId) ?? new EditPricing { ProjectId = _projectId };
            var project = _db.GetProject(_projectId) ?? new Project { Id = _projectId };
            var lines = _db.GetQuoteLines(_projectId).Where(l => !string.IsNullOrWhiteSpace(l.ExtrusionCode) || !string.IsNullOrWhiteSpace(l.Description)).ToList();

            ep.PricingNotes = txtPricingNotes.Text.Trim();
            ep.CostType = CostTypeValues[Math.Max(0, cmbCostType.SelectedIndex)];
            ep.FinType = cmbFinType.SelectedIndex;
            ep.CostTypeOverride = chkCostTypeOverride.Checked;
            ep.MetalPrice = ParseDecimal(txtMetalPrice.Text);
            ep.FinishingPrice = ParseDecimal(txtFinishingPrice.Text);
            ep.FinishingCostPricePerM2 = ParseDecimal(txtFinishingCostPricePerM2.Text);
            ep.Price_B42 = ParseDecimal(txtB42.Text);
            ep.Price_B44 = ParseDecimal(txtB44.Text);
            ep.Price_B48 = ParseDecimal(txtB48.Text);
            ep.Price_B50 = ParseDecimal(txtB50.Text);
            ep.Price_B52 = ParseDecimal(txtB52.Text);
            ep.Price_B54 = ParseDecimal(txtB54.Text);
            ep.Price_B57 = ParseDecimal(txtB57.Text);
            ep.Price_B58 = ParseDecimal(txtB58.Text);
            ep.ThermalBreakCalcType = cmbTBCalcType.SelectedItem?.ToString() ?? "Margin";
            ep.ThermalBreakMargin = ParseDecimal(txtTBMargin.Text);
            ep.IP1UpchargePerKg = ParseDecimal(txtIP1Upcharge.Text, DefaultIP1Upcharge);
            ep.IP2UpchargePerKg = ParseDecimal(txtIP2Upcharge.Text, DefaultIP2Upcharge);
            ep.IP3PanoramicFixedKgRate = ParseDecimal(txtIP3Rate.Text, DefaultIP3Rate);
            ep.AnodisedThresholdCostPerM2 = ParseDecimal(txtAnodisedThresholdCost.Text, DefaultAnodisedThresholdCost);
            if (ep.FinishingPrice == 0m && ep.Price_B44 > 0m)
                ep.FinishingPrice = ep.Price_B44;
            if (ep.FinishingCostPricePerM2 == 0m && ep.Price_B57 > 0m)
                ep.FinishingCostPricePerM2 = ep.Price_B57;

            var totalLineValue = lines.Sum(l => l.TotalPrice);
            ep.TotalKg = ep.MetalPrice > 0m
                ? totalLineValue / ep.MetalPrice
                : lines.Sum(l => l.Quantity * l.LengthM);
            ep.TotalFinishingM2 = lines.Sum(l => l.Quantity * l.LengthM);
            ep.TotalPieces = (int)Math.Round(lines.Sum(l => l.Quantity), MidpointRounding.AwayFromZero);

            project.CustomerSapDiscount = ParseDecimal(txtCustomerSapDiscount.Text);

            ep.IP1MarginPct = CalculateMarginPct(ep.MetalPrice + ep.IP1UpchargePerKg, ep.MetalPrice);
            ep.IP2MarginPct = CalculateMarginPct(ep.MetalPrice + ep.IP2UpchargePerKg, ep.MetalPrice);
            ep.IP3MarginPct = CalculateMarginPct(ep.IP3PanoramicFixedKgRate, ep.MetalPrice);
            ep.FinishingMarginPct = FormulaCalculationService.CalculateFinishingMarginPct(ep.FinishingPrice, ep.FinishingCostPricePerM2, ep.Price_B42, ep.CostType) * 100m;
            ep.ThermalBreakMarginPct = FormulaCalculationService.CalculateThresholdMarginPct(ep.Price_B58, ep.AnodisedThresholdCostPerM2) * 100m;

            lblTotalKgValue.Text = ep.TotalKg.ToString("N2");
            lblTotalFinishingM2Value.Text = ep.TotalFinishingM2.ToString("N2");
            lblTotalPiecesValue.Text = ep.TotalPieces.ToString();
            lblIP1MarginValue.Text = $"{ep.IP1MarginPct:N2}%";
            lblIP2MarginValue.Text = $"{ep.IP2MarginPct:N2}%";
            lblIP3MarginValue.Text = $"{ep.IP3MarginPct:N2}%";
            lblThresholdMarginValue.Text = $"{ep.ThermalBreakMarginPct:N2}%";

            if (saveChanges)
            {
                _db.UpdateProject(project);
                _db.UpsertEditPricing(ep);
            }

            var summary = _quoteSummaryService.Calculate(_projectId);
            ep.MillFinishMarginPct = summary.MillFinishMarginPct;
            ep.FinishingMarginPct = summary.FinishingMarginPct;
            lblMillFinishMarginValue.Text = $"{summary.MillFinishMarginPct:N2}%";
            lblFinishingMarginValue.Text = $"{summary.FinishingMarginPct:N2}%";

            if (saveChanges)
                _db.UpsertEditPricing(ep);

            gridSummary.DataSource = new List<SummaryRow>
            {
                new() { Category = "Mill Finish", Total = summary.MillFinishTotal, MarginDollar = summary.MillFinishMarginDollar, MarginPct = summary.MillFinishMarginPct },
                new() { Category = "Thermal Break Metal", Total = summary.ThermalBreakMetalTotal, MarginDollar = summary.ThermalBreakMarginDollar, MarginPct = summary.ThermalBreakMarginPct },
                new() { Category = "Slotting/Sundry", Total = summary.SlottingSundryTotal, MarginDollar = summary.SlottingSundryMarginDollar, MarginPct = summary.SlottingSundryMarginPct },
                new() { Category = "Finishing", Total = summary.FinishingTotal, MarginDollar = summary.FinishingMarginDollar, MarginPct = summary.FinishingMarginPct },
                new() { Category = "Total", Total = summary.GrandTotal, MarginDollar = summary.GrandMarginDollar, MarginPct = summary.GrandMarginPct }
            };

            UpdateRowVisibility();
        }

        private void UpdateRowVisibility()
        {
            if (_projectId <= 0) return;
            var result = _rowVis.GetVisibleRows(_projectId);
            result.VisibleRows.Sort();
            result.HiddenRows.Sort();
            lblVisibleRows.Text = $"Visible rows: {string.Join(", ", result.VisibleRows)}";
            lblHiddenRows.Text = $"Hidden rows: {string.Join(", ", result.HiddenRows)}";
        }

        private static decimal ParseDecimal(string text, decimal fallback = 0m)
            => decimal.TryParse(text, out decimal value) ? value : fallback;

        private static decimal CalculateMarginPct(decimal sell, decimal cost)
            => sell > 0m ? (sell - cost) / sell * 100m : 0m;

        private class SummaryRow
        {
            public string Category { get; set; } = string.Empty;
            public decimal Total { get; set; }
            public decimal MarginDollar { get; set; }
            public decimal MarginPct { get; set; }
        }
    }
}
