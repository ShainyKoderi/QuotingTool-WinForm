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
        private static readonly System.Drawing.Color ClrOlive = System.Drawing.Color.FromArgb(107, 127, 41);
        private static readonly System.Drawing.Color ClrOliveRow = System.Drawing.Color.FromArgb(226, 239, 218);
        private static readonly System.Drawing.Color ClrInputBg = System.Drawing.Color.LightYellow;
        private static readonly System.Drawing.Color ClrReadOnly = System.Drawing.Color.FromArgb(242, 242, 242);
        private static readonly System.Drawing.Color ClrTotals = System.Drawing.Color.FromArgb(255, 199, 206);
        private static readonly System.Drawing.Color ClrHdr = System.Drawing.Color.FromArgb(198, 224, 180);

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

        // Finish type options matching the Excel Validation sheet FinishType category.
        private static readonly string[] FinishTypeOptions =
            { "Mill Finish", "Powder Coat", "Anodise" };

        // Standard colour/finish groups matching ColourGroups.GroupName values.
        private static readonly string[] FinishGroupOptions =
            { "", "GROUP 1", "GROUP 2", "GROUP 3", "GROUP 4", "GROUP 5", "GROUP POA" };

        // Applicators from the PCApplicator validation category.
        private static readonly string[] ApplicatorOptions =
        {
            "",
            "Capral P&P - Campbellfield",
            "Capral P&P - Bremer",
            "AkzoNobel",
            "Dulux",
            "Tiger",
            "Interpon",
            "Orica",
            "Other",
            "AkzoNobel – POA",
            "Dulux – POA",
            "Tiger – POA",
            "Interpon – POA",
            "Orica – POA"
        };

        // "Powder Priced as" options (AP = Anodised Perimeter, PP = Production Perimeter).
        private static readonly string[] PowderPricedAsOptions =
            { "Anodised Perimeter (AP)", "Production Perimeter (PP)" };

        public EditPricingForm(DatabaseContext db)
        {
            _db = db;
            _showHide = new ShowHideBulkService(db);
            _rowVis = new EditPricingRowVisibilityService(db);
            _quoteSummaryService = new QuoteSummaryService(db);
            _pivotRefresh = new PivotRefreshService(db);
            InitializeComponent();

            // Finish Type combo: shown in grpFinishSection
            cmbFinType.Items.AddRange(FinishTypeOptions);

            // Finish Group
            cmbFinishGroup.Items.AddRange(FinishGroupOptions);

            // Finish Name: free-text ComboBox; populate long desc from ColourGroups at runtime.
            // See LoadProject() – items added after DB is available.

            // Applicator dropdowns
            cmbApplicatorGroup.Items.AddRange(FinishGroupOptions);
            cmbFinishApplicator.Items.AddRange(ApplicatorOptions);

            // Powder priced as
            cmbPowderPricedAs.Items.AddRange(PowderPricedAsOptions);

            // Cost type (for Finishing section)
            cmbCostType.Items.AddRange(CostTypeOptions);

            // Thermal Break method
            cmbTBCalcType.Items.AddRange(new object[] { "Margin", "List less Discount" });

            // Ensure valid initial selections
            if (cmbFinType.SelectedIndex < 0) cmbFinType.SelectedIndex = 0;
            if (cmbFinishGroup.SelectedIndex < 0) cmbFinishGroup.SelectedIndex = 0;
            if (cmbApplicatorGroup.SelectedIndex < 0) cmbApplicatorGroup.SelectedIndex = 0;
            if (cmbFinishApplicator.SelectedIndex < 0) cmbFinishApplicator.SelectedIndex = 0;
            if (cmbPowderPricedAs.SelectedIndex < 0) cmbPowderPricedAs.SelectedIndex = 0;
            if (cmbCostType.SelectedIndex < 0) cmbCostType.SelectedIndex = 0;
            if (cmbTBCalcType.SelectedIndex < 0) cmbTBCalcType.SelectedIndex = 0;
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };
            var project = _db.GetProject(projectId) ?? new Project();

            // ── Header section ──────────────────────────────────────────────
            txtQuotedBy.Text = project.QuotedBy;
            dtpQuoteDate.Value = project.RevisionDate ?? DateTime.Today;
            txtProjectName.Text = project.ProjectName;
            txtCustomerName.Text = project.CustomerName;
            txtPricingNotes.Text = ep.PricingNotes;

            // ── Finish section ───────────────────────────────────────────────
            // Populate Finish Name ComboBox from DB on first load (or refresh).
            if (cmbFinishName.Items.Count == 0)
            {
                cmbFinishName.Items.Add(string.Empty);
                foreach (var cg in _db.GetColourGroups())
                    if (!string.IsNullOrWhiteSpace(cg.LongDesc))
                        cmbFinishName.Items.Add(cg.LongDesc);
            }

            SetComboIndex(cmbFinType, project.FinishType, FinishTypeOptions[0]);
            SetComboIndex(cmbFinishGroup, project.FinishGroup, string.Empty);
            SetComboText(cmbFinishName, project.FinishName);
            SetComboIndex(cmbApplicatorGroup, project.FinishGroup, string.Empty);
            SetComboIndex(cmbFinishApplicator, project.FinishApplicator, string.Empty);

            // ── Mill Finish / IP Upcharges ────────────────────────────────────
            txtCustomerSapDiscount.Text = project.CustomerSapDiscount.ToString("F2");
            txtMetalPrice.Text = ep.MetalPrice.ToString("F4");
            txtIP1Upcharge.Text = ep.IP1UpchargePerKg.ToString("F2");
            txtIP2Upcharge.Text = ep.IP2UpchargePerKg.ToString("F2");
            txtIP3Rate.Text = ep.IP3PanoramicFixedKgRate.ToString("F2");

            // ── Thermal Break ──────────────────────────────────────────────────
            int tbIdx = cmbTBCalcType.FindStringExact(ep.ThermalBreakCalcType);
            cmbTBCalcType.SelectedIndex = tbIdx >= 0 ? tbIdx : 0;
            txtTBMargin.Text = ep.ThermalBreakMargin.ToString("F4");

            // ── Powder / Finishing pricing ─────────────────────────────────────
            int ctIndex = Array.IndexOf(CostTypeValues, ep.CostType);
            cmbCostType.SelectedIndex = ctIndex >= 0 ? ctIndex : 0;
            chkCostTypeOverride.Checked = ep.CostTypeOverride;
            // PowderPricedAs: no stored preference yet; always defaults to first option (AP).
            cmbPowderPricedAs.SelectedIndex = 0;
            txtFinishingCostPricePerM2.Text = ep.FinishingCostPricePerM2.ToString("F4");
            txtB42.Text = ep.Price_B42.ToString("F2");
            txtFinishingPrice.Text = ep.FinishingPrice.ToString("F4");
            txtB44.Text = ep.Price_B44.ToString("F2");
            txtB48.Text = ep.Price_B48.ToString("F2");
            txtB50.Text = ep.Price_B50.ToString("F2");
            txtB52.Text = ep.Price_B52.ToString("F2");
            txtB54.Text = ep.Price_B54.ToString("F2");

            // ── Anodised Thresholds ────────────────────────────────────────────
            txtAnodisedThresholdCost.Text = ep.AnodisedThresholdCostPerM2.ToString("F2");
            txtB57.Text = ep.Price_B57.ToString("F4");
            txtB58.Text = ep.Price_B58.ToString("F2");

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

            // ── Header fields ────────────────────────────────────────────────
            project.QuotedBy = txtQuotedBy.Text.Trim();
            project.RevisionDate = dtpQuoteDate.Value.Date;
            project.ProjectName = txtProjectName.Text.Trim();
            project.CustomerName = txtCustomerName.Text.Trim();
            ep.PricingNotes = txtPricingNotes.Text.Trim();

            // ── Finish fields ────────────────────────────────────────────────
            project.FinishType = cmbFinType.SelectedItem?.ToString() ?? FinishTypeOptions[0];
            project.FinishGroup = cmbFinishGroup.SelectedItem?.ToString() ?? string.Empty;
            project.FinishName = cmbFinishName.Text.Trim();
            project.FinishApplicator = cmbFinishApplicator.SelectedItem?.ToString() ?? string.Empty;

            // ── Pricing fields ────────────────────────────────────────────────
            ep.CostType = CostTypeValues[Math.Max(0, cmbCostType.SelectedIndex)];
            // FinType: 1 = Powder Coat / Anodised, 0 = Mill Finish (not powder)
            ep.FinType = (project.FinishType == "Mill Finish") ? 0 : 1;
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

            project.CustomerSapDiscount = ParseDecimal(txtCustomerSapDiscount.Text);

            // ── Computed totals (Quantities section) ──────────────────────────
            var totalLineValue = lines.Sum(l => l.TotalPrice);
            ep.TotalKg = ep.MetalPrice > 0m
                ? totalLineValue / ep.MetalPrice
                : lines.Sum(l => l.Quantity * l.LengthM);
            ep.TotalFinishingM2 = lines.Sum(l => l.Quantity * l.LengthM);
            ep.TotalPieces = (int)Math.Round(lines.Sum(l => l.Quantity), MidpointRounding.AwayFromZero);

            // ── Margin calculations ───────────────────────────────────────────
            ep.IP1MarginPct = CalculateMarginPct(ep.MetalPrice + ep.IP1UpchargePerKg, ep.MetalPrice);
            ep.IP2MarginPct = CalculateMarginPct(ep.MetalPrice + ep.IP2UpchargePerKg, ep.MetalPrice);
            ep.IP3MarginPct = CalculateMarginPct(ep.IP3PanoramicFixedKgRate, ep.MetalPrice);
            ep.FinishingMarginPct = FormulaCalculationService.CalculateFinishingMarginPct(ep.FinishingPrice, ep.FinishingCostPricePerM2, ep.Price_B42, ep.CostType) * 100m;
            ep.ThermalBreakMarginPct = FormulaCalculationService.CalculateThresholdMarginPct(ep.Price_B58, ep.AnodisedThresholdCostPerM2) * 100m;

            // ── Update read-only labels ───────────────────────────────────────
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

            // ── Summary grid ──────────────────────────────────────────────────
            // TotalCost = what we pay (cost); TotalSellValue = what we charge the customer.
            // Matches Excel columns: Category | Total (Cost) | Total Value | Margin $ | Margin %.
            decimal millFinishCost = summary.MillFinishTotal - summary.MillFinishMarginDollar;
            decimal thermalBreakCost = summary.ThermalBreakMetalTotal - summary.ThermalBreakMarginDollar;
            decimal finishingCost = summary.FinishingTotal - summary.FinishingMarginDollar;
            decimal sundryTotalValue = summary.SlottingSundryTotal;

            gridSummary.DataSource = new List<SummaryRow>
            {
                new() { Category = "Mill Finish",         TotalCost = millFinishCost,    TotalSellValue = summary.MillFinishTotal,        MarginDollar = summary.MillFinishMarginDollar,    MarginPct = summary.MillFinishMarginPct },
                new() { Category = "Thermal Break Metal", TotalCost = thermalBreakCost,  TotalSellValue = summary.ThermalBreakMetalTotal, MarginDollar = summary.ThermalBreakMarginDollar,  MarginPct = summary.ThermalBreakMarginPct },
                new() { Category = "Slotting/Sundry",     TotalCost = sundryTotalValue,  TotalSellValue = sundryTotalValue,               MarginDollar = summary.SlottingSundryMarginDollar, MarginPct = summary.SlottingSundryMarginPct },
                new() { Category = "Finishing",           TotalCost = finishingCost,     TotalSellValue = summary.FinishingTotal,         MarginDollar = summary.FinishingMarginDollar,     MarginPct = summary.FinishingMarginPct },
                new() { Category = "Total",               TotalCost = summary.GrandTotal - summary.GrandMarginDollar, TotalSellValue = summary.GrandTotal, MarginDollar = summary.GrandMarginDollar, MarginPct = summary.GrandMarginPct }
            };

            // Highlight the Total row
            if (gridSummary.Rows.Count > 0)
            {
                var totalRow = gridSummary.Rows[gridSummary.Rows.Count - 1];
                totalRow.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 199, 206);
                totalRow.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            }

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

        private static System.Drawing.Font BoldFont(float size = 9F)
            => new System.Drawing.Font("Segoe UI", size, System.Drawing.FontStyle.Bold);

        private static System.Drawing.Font RegFont(float size = 9F)
            => new System.Drawing.Font("Segoe UI", size);

        private static decimal CalculateMarginPct(decimal sell, decimal cost)
            => sell > 0m ? (sell - cost) / sell * 100m : 0m;

        /// <summary>
        /// Sets a ComboBox selected item by matching the given value string.
        /// Falls back to <paramref name="fallback"/> if no match is found.
        /// </summary>
        private static void SetComboIndex(ComboBox combo, string? value, string fallback)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                int fb = combo.FindStringExact(fallback);
                combo.SelectedIndex = fb >= 0 ? fb : (combo.Items.Count > 0 ? 0 : -1);
                return;
            }
            int idx = combo.FindStringExact(value);
            combo.SelectedIndex = idx >= 0 ? idx : (combo.Items.Count > 0 ? 0 : -1);
        }

        /// <summary>
        /// Sets a ComboBox text (supports free-text combos like Finish Name).
        /// </summary>
        private static void SetComboText(ComboBox combo, string? value)
        {
            combo.Text = value ?? string.Empty;
        }

        /// <summary>
        /// Represents one row in the Summary grid.
        /// Columns match the sample.xlsm layout:
        ///   Category | Total (Cost) | Total Value | Margin $ | Margin %
        /// </summary>
        private class SummaryRow
        {
            public string Category { get; set; } = string.Empty;
            /// <summary>Total cost (what we pay to produce).</summary>
            public decimal TotalCost { get; set; }
            /// <summary>Total sell value (what we charge the customer).</summary>
            public decimal TotalSellValue { get; set; }
            public decimal MarginDollar { get; set; }
            public decimal MarginPct { get; set; }
        }
    }
}
