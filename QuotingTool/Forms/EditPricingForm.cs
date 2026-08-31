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
        private static readonly System.Drawing.Font FontBold13 = new("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
        private static readonly System.Drawing.Font FontBold9 = new("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        private static readonly System.Drawing.Font FontBold85 = new("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        private static readonly System.Drawing.Font FontItalic75 = new("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);

        private readonly DatabaseContext _db;
        private readonly ShowHideBulkService _showHide;
        private readonly EditPricingRowVisibilityService _rowVis;
        private readonly QuoteSummaryService _quoteSummaryService;
        private readonly PivotRefreshService _pivotRefresh;
        private readonly FormulaCalculationService _formulaSvc;
        private Dictionary<string, ExtrusionMaster> _extrusionByCode = [];
        private Dictionary<string, SundryItem> _sundryByCode = [];
        private readonly ToolTip _formulaToolTip = new();
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
            _formulaSvc = new FormulaCalculationService(db);
            InitializeComponent();
            // LoadLogos();
            BuildLayout();

            // Finish fields are read-only displays: values come from Project Details.
            txtFinType.ReadOnly = true;
            txtFinishGroup.ReadOnly = true;
            txtFinishName.ReadOnly = true;
            txtApplicatorGroup.ReadOnly = true;
            txtFinishApplicator.ReadOnly = true;

            // B43 is auto-calculated via Pricing Matrix lookup — not user-editable.
            txtFinishingCostPricePerM2.ReadOnly = true;

            // Powder priced as
            cmbPowderPricedAs.Items.AddRange(PowderPricedAsOptions);

            // Cost type (for Finishing section)
            cmbCostType.Items.AddRange(CostTypeOptions);

            // Thermal Break method
            cmbTBCalcType.Items.AddRange(new object[] { "Margin", "List less Discount" });

            // Ensure valid initial selections
            if (cmbPowderPricedAs.SelectedIndex < 0) cmbPowderPricedAs.SelectedIndex = 0;
            if (cmbCostType.SelectedIndex < 0) cmbCostType.SelectedIndex = 0;
            if (cmbTBCalcType.SelectedIndex < 0) cmbTBCalcType.SelectedIndex = 0;
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            // Refresh the extrusion master cache so TotalKg and guidance labels
            // always reflect the latest section data in the database.
            _extrusionByCode = _db.GetExtrusionMasterList()
                .ToDictionary(m => m.Section, m => m, StringComparer.OrdinalIgnoreCase);
            _sundryByCode = _db.GetSundryItems()
                .Where(s => !string.IsNullOrWhiteSpace(s.SectionCode))
                .ToDictionary(s => s.SectionCode.Trim(), s => s, StringComparer.OrdinalIgnoreCase);
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };
            var project = _db.GetProject(projectId) ?? new Project();

            // ── Header section ──────────────────────────────────────────────
            txtQuotedBy.Text = project.QuotedBy;
            dtpQuoteDate.Value = project.RevisionDate ?? DateTime.Today;
            txtProjectName.Text = project.ProjectName;
            txtCustomerName.Text = project.CustomerName;
            txtPricingNotes.Text = ep.PricingNotes;

            // ── Finish section ───────────────────────────────────────────────
            // These fields are read-only displays: the values are owned by Project Details
            // and only reflected here for reference. They cannot be edited in Edit Pricing.
            txtFinType.Text = project.FinishType;
            txtFinishGroup.Text = project.FinishGroup;
            txtFinishName.Text = project.FinishName;
            txtApplicatorGroup.Text = project.FinishGroup;
            txtFinishApplicator.Text = project.FinishApplicator;

            // ── Mill Finish / IP Upcharges ────────────────────────────────────
            txtCustomerSapDiscount.Text = project.CustomerSapDiscount.ToString("F2");
            txtDeliveryCharge.Text = project.DeliveryCharge.ToString("F2");
            txtMetalPrice.Text = ep.MetalPrice.ToString("F4");
            txtIP1Upcharge.Text = ep.IP1UpchargePerKg.ToString("F2");
            txtIP2Upcharge.Text = ep.IP2UpchargePerKg.ToString("F2");
            txtIP3Rate.Text = ep.IP3PanoramicFixedKgRate.ToString("F2");

            // ── Thermal Break ──────────────────────────────────────────────────
            int tbIdx = cmbTBCalcType.FindStringExact(ep.ThermalBreakCalcType);
            cmbTBCalcType.SelectedIndex = tbIdx >= 0 ? tbIdx : 0;
            // Stored as a fraction (0.5) but displayed as a percentage (50.0) like Excel B37.
            txtTBMargin.Text = (ep.ThermalBreakMargin * 100m).ToString("F1");

            // ── Powder / Finishing pricing ─────────────────────────────────────
            int ctIndex = Array.IndexOf(CostTypeValues, ep.CostType);
            cmbCostType.SelectedIndex = ctIndex >= 0 ? ctIndex : 0;
            //chkCostTypeOverride.Checked = ep.CostTypeOverride;
            // PowderPricedAs: no stored preference yet; always defaults to first option (AP).
            cmbPowderPricedAs.SelectedIndex = 0;
            // txtFinishingCostPricePerM2 (B43) is a calculated field — populated by UpdateDerivedFields.
            txtB42.Text = ep.Price_B42.ToString("F2");
            txtFinishingPrice.Text = ep.FinishingPrice.ToString("F4");

            txtB48.Text = ep.Price_B48.ToString("F2");
            txtB49.Text = ep.Price_B49.ToString("F4");
            txtB50.Text = ep.Price_B50.ToString("F2");
            txtB52.Text = ep.Price_B52.ToString("F2");
            txtB53.Text = ep.Price_B53.ToString("F4");
            txtB54.Text = ep.Price_B54.ToString("F2");

            // ── Anodised Thresholds ────────────────────────────────────────────
            txtAnodisedThresholdCost.Text = ep.AnodisedThresholdCostPerM2.ToString("F2");

            txtB58.Text = ep.Price_B58.ToString("F2");

            // ApplyRoleAccess();
            UpdateDerivedFields(saveChanges: false);
        }

        public void SavePricing()
        {
            if (_projectId <= 0) return;
            UpdateDerivedFields(saveChanges: true);
            MessageBox.Show("Pricing saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e) => SavePricing();

        //private void btnToggleOverride_Click(object sender, EventArgs e)
        //{
        //    if (_projectId <= 0) return;
        //    if (!UserSession.HasRole(UserRole.SalesManager))
        //    {
        //        MessageBox.Show("Sales Manager or Administrator access is required for pricing overrides.",
        //            "Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }
        //    chkCostTypeOverride.Checked = _showHide.ToggleCostTypeOverride(_projectId);

        //}


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

        //private void ApplyRoleAccess()
        //{
        //    var canAuthorize = UserSession.HasRole(UserRole.SalesManager);
        //    chkCostTypeOverride.Enabled = canAuthorize;
        //    btnToggleOverride.Enabled = canAuthorize;
        //}

        private void btnBulkNonBulkRates_Click(object sender, EventArgs e)
        {
            grpBulkCategories.Visible = true;

          //  MessageBox.Show("Bulk rates apply when total finishing area > 100m². Non-bulk rates apply below this threshold.",
            //    "Bulk/Non-bulk Rates", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            project.FinishType = txtFinType.Text;
            project.FinishGroup = txtFinishGroup.Text;
            project.FinishName = txtFinishName.Text;
            project.FinishApplicator = txtFinishApplicator.Text;

            // ── Pricing fields ────────────────────────────────────────────────
            ep.CostType = CostTypeValues[Math.Max(0, cmbCostType.SelectedIndex)];
            // FinType: 1 = Powder Coat / Anodised, 0 = Mill Finish (not powder)
            ep.FinType = (txtFinType.Text == "Mill Finish") ? 0 : 1;
            //    ep.CostTypeOverride = chkCostTypeOverride.Checked;
            ep.MetalPrice = ParseDecimal(txtMetalPrice.Text);
            ep.FinishingPrice = ParseDecimal(txtFinishingPrice.Text);
            ep.Price_B42 = ParseDecimal(txtB42.Text);

            // ── B43: Finishing Cost Price $/m² (auto-calculated) ──────────────────
            // Mirrors 3.Edit Pricing!B43 = IFERROR('Pricing Matrix'!E44, 0)
            // Pricing Matrix!E44 = VLOOKUP(vendor, $B$27:$K$40, MATCH(colourGroup, ...), FALSE)
            // When the colour group is POA (sentinel 99), the Pricing Matrix cell links
            // back to B42, so we fall through to the manually entered POA cost.
            var rawFinishCost = _formulaSvc.LookupPowderCostByVendorAndGroup(
                project.FinishApplicator, project.FinishGroup);
            ep.FinishingCostPricePerM2 = (rawFinishCost == FormulaCalculationService.PricePOASentinel || rawFinishCost == 0m)
                ? ep.Price_B42
                : rawFinishCost;
            txtFinishingCostPricePerM2.Text = ep.FinishingCostPricePerM2.ToString("F4");

            ep.Price_B48 = ParseDecimal(txtB48.Text);
            ep.Price_B49 = ParseDecimal(txtB49.Text);
            ep.Price_B50 = ParseDecimal(txtB50.Text);
            ep.Price_B52 = ParseDecimal(txtB52.Text);
            ep.Price_B53 = ParseDecimal(txtB53.Text);
            ep.Price_B54 = ParseDecimal(txtB54.Text);

            ep.Price_B58 = ParseDecimal(txtB58.Text);
            ep.ThermalBreakCalcType = cmbTBCalcType.SelectedItem?.ToString() ?? "Margin";
            // TB margin is entered as a percentage (e.g. 50.0); values ≤ 1 are treated as
            // an already-fractional entry for backwards compatibility.
            var tbEntered = ParseDecimal(txtTBMargin.Text);
            ep.ThermalBreakMargin = tbEntered > 1m ? tbEntered / 100m : tbEntered;
            ep.IP1UpchargePerKg = ParseDecimal(txtIP1Upcharge.Text, DefaultIP1Upcharge);
            ep.IP2UpchargePerKg = ParseDecimal(txtIP2Upcharge.Text, DefaultIP2Upcharge);
            ep.IP3PanoramicFixedKgRate = ParseDecimal(txtIP3Rate.Text, DefaultIP3Rate);
            ep.AnodisedThresholdCostPerM2 = ParseDecimal(txtAnodisedThresholdCost.Text, DefaultAnodisedThresholdCost);
            if (ep.FinishingPrice == 0m && ep.Price_B44 > 0m)
                ep.FinishingPrice = ep.Price_B44;
            if (ep.FinishingCostPricePerM2 == 0m && ep.Price_B57 > 0m)
                ep.FinishingCostPricePerM2 = ep.Price_B57;
            if (ep.Price_B49 == 0m)
                ep.Price_B49 = ep.FinishingCostPricePerM2;
            if (ep.Price_B53 == 0m)
                ep.Price_B53 = ep.FinishingCostPricePerM2;
            txtB49.Text = ep.Price_B49.ToString("F4");
            txtB53.Text = ep.Price_B53.ToString("F4");

            project.CustomerSapDiscount = ParseDecimal(txtCustomerSapDiscount.Text);
            project.DeliveryCharge = ParseDecimal(txtDeliveryCharge.Text, 97m);

            // ── Computed totals (Quantities section) ──────────────────────────
            // Compute total kg accurately using the kg/m value from ExtrusionMaster
            // (Y12 * H12 formula equivalent).  This avoids the circular dependency
            // where TotalKg = TotalPrice / MetalPrice is 0 whenever prices haven't
            // been persisted yet (e.g. first load after a BOM import).
            decimal totalKg = 0m;
            foreach (var l in lines)
            {
                var massPerM = GetMassPerM(l.ExtrusionCode);
                if (massPerM > 0)
                    totalKg += l.Quantity * l.LengthM * massPerM;
            }

            var totalLineValue = lines.Sum(l => l.TotalPrice);
            // Use kg/m-based total as primary; fall back to price-derived or metre-based
            // estimates only when no section master data is available.
            ep.TotalKg = totalKg > 0m ? totalKg
                : ep.MetalPrice > 0m ? totalLineValue / ep.MetalPrice
                : lines.Sum(l => l.Quantity * l.LengthM);

            // Total Finishing m² (AP basis) — mirrors Excel col AD = AB * H / 1000 * G
            // where AB = ApMm (AP per metre in mm units from DATA col E).
            // Excludes lines with IgnoreFinish = "Yes" (mill finish override).
            decimal totalFinishingM2Ap = 0m;
            foreach (var l in lines)
            {
                if (string.Equals(l.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase)) continue;
                var apMm = GetApMm(l.ExtrusionCode);
                if (apMm > 0m)
                    totalFinishingM2Ap += l.Quantity * l.LengthM * apMm / 1000m;
            }
            // Fall back to total linear metres when no AP data is available.
            ep.TotalFinishingM2 = totalFinishingM2Ap > 0m
                ? totalFinishingM2Ap
                : lines.Where(l => !string.Equals(l.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase))
                       .Sum(l => l.Quantity * l.LengthM);

            ep.TotalPieces = (int)Math.Round(lines.Sum(l => l.Quantity), MidpointRounding.AwayFromZero);

            // ── Margin calculations ───────────────────────────────────────────
            var pricingConstants = _formulaSvc.GetPricingConstants();

            // Metal cost per kg (Pricing Matrix B5 = LME + Billet + Spread).
            // Excel uses the single global monthly rate for all Edit Pricing margins.
            var metalCostPerKg = pricingConstants.MetalCostPerKg;

            // IP1/IP2 margins (Excel G34/H34): margin on the combined (sell + upcharge) $/kg.
            ep.IP1MarginPct = FormulaCalculationService.CalculateIpUpchargeMarginPct(
                ep.IP1UpchargePerKg, ep.MetalPrice, metalCostPerKg) * 100m;
            ep.IP2MarginPct = FormulaCalculationService.CalculateIpUpchargeMarginPct(
                ep.IP2UpchargePerKg, ep.MetalPrice, metalCostPerKg) * 100m;
            // IP3 margin (Excel I34): standalone fixed $/kg rate vs metal cost.
            ep.IP3MarginPct = FormulaCalculationService.CalculateIp3MarginPct(
                ep.IP3PanoramicFixedKgRate, metalCostPerKg) * 100m;

            ep.MillFinishMarginPct = FormulaCalculationService.CalculateMillFinishMarginPct(ep.MetalPrice, metalCostPerKg) * 100m;
            // h. Excel B45: =(B44-B43)/B44 (CostType=0) or =(B44-B42)/B44 (POA).
            // B44 = sell price (Price_B44), B43 = cost price (FinishingCostPricePerM2), B42 = POA cost.
            //
            // Excel C25 = SUM(C22:C23:C24) is auto-derived from the finish configuration:
            //   C22 = VLOOKUP(FinishApplicatorGroup, ...) → "Group POA" → C22=1 → C25≠0 → use B42 path.
            // The stored ep.CostType reflects the user combobox and may show "0 – Standard" even
            // when the finish group is POA. Derive an effective cost type to mirror Excel's C25:
            //   FinishGroup contains "POA" → force the POA (B42) path regardless of combobox.
            bool finishGroupIsPoa = project.FinishGroup.Contains("POA", StringComparison.OrdinalIgnoreCase);
            int effectiveCostType = (finishGroupIsPoa && ep.CostType == 0) ? 1 : ep.CostType;
            ep.FinishingMarginPct = FormulaCalculationService.CalculateFinishingMarginPct(ep.FinishingPrice, ep.FinishingCostPricePerM2, ep.Price_B42, ep.CostType) * 100m;
            // h. Excel B59: =(B58-B57)/B58 — B57 is the auto-calculated threshold cost (Price_B57).
            ep.ThermalBreakMarginPct = FormulaCalculationService.CalculateThresholdMarginPct(ep.Price_B58, ep.AnodisedThresholdCostPerM2) * 100m;

            // B51: Mill BULK Finish Margin % = (B50 − B49) / B50
            var bulkMarginPct = FormulaCalculationService.CalculateBulkFinishingMarginPct(
                ep.Price_B50, ep.Price_B49, ep.Price_B48, ep.CostType, ep.CostTypeOverride);
            // B55: Mill NON-Bulk Finish Margin % = (B54 − B53) / B54
            var nonBulkMarginPct = FormulaCalculationService.CalculateBulkFinishingMarginPct(
                ep.Price_B54, ep.Price_B53, ep.Price_B52, ep.CostType, ep.CostTypeOverride);

            // ── Update read-only labels ───────────────────────────────────────
            lblTotalKgValue.Text = ep.TotalKg.ToString("N2");
            lblTotalFinishingM2Value.Text = ep.TotalFinishingM2.ToString("N2");
            lblTotalPiecesValue.Text = ep.TotalPieces.ToString();
            lblIP1MarginValue.Text = $"{ep.IP1MarginPct:N2}%";
            lblIP2MarginValue.Text = $"{ep.IP2MarginPct:N2}%";
            lblIP3MarginValue.Text = $"{ep.IP3MarginPct:N2}%";
            lblMillFinishMarginValue.Text = $"{ep.MillFinishMarginPct:N2}%";
            lblFinishingMarginValue.Text = $"{ep.FinishingMarginPct:N2}%";
            lblThresholdMarginValue.Text = $"{ep.ThermalBreakMarginPct:N2}%";  // always show % suffix

            // ── Metal price guidance (Pricing Matrix rows 8-13) ────────────────
            // Excel C/D/E: Typical = MetalCost + Spread, Min = Typical − MinFactor,
            // Max = Typical + MaxFactor — computed live from the current metal cost.
            var (suggestedTypical, suggestedMin, suggestedMax) =
                _formulaSvc.LookupMetalGuidance(ep.TotalKg, metalCostPerKg);
            lblMetalPriceTyp.Text = suggestedTypical > 0m ? $"${suggestedTypical:F2}" : "—";
            lblMetalPriceMin.Text = suggestedMin > 0m ? $"${suggestedMin:F2}" : "—";
            lblMetalPriceMax.Text = suggestedMax > 0m ? $"${suggestedMax:F2}" : "—";

            // Typical/Min/Max margin % at the breakpoint typical/min/max sell prices.
            var mfMarginTyp = suggestedTypical > 0m
                ? FormulaCalculationService.CalculateMillFinishMarginPct(suggestedTypical, metalCostPerKg) * 100m : 0m;
            var mfMarginMin = suggestedMin > 0m
                ? FormulaCalculationService.CalculateMillFinishMarginPct(suggestedMin, metalCostPerKg) * 100m : 0m;
            var mfMarginMax = suggestedMax > 0m
                ? FormulaCalculationService.CalculateMillFinishMarginPct(suggestedMax, metalCostPerKg) * 100m : 0m;
            label1.Text = suggestedTypical > 0m ? $"{mfMarginTyp:F1}%" : "—";
            label2.Text = suggestedMin > 0m ? $"{mfMarginMin:F1}%" : "—";
            label3.Text = suggestedMax > 0m ? $"{mfMarginMax:F1}%" : "—";

            // ── Thermal Break guidance (from Pricing Matrix Table_TB_BP) ──────
            //bool hasThermalBreakContent = _db.GetQuoteLines(_projectId)
            //    .Any(line => !string.IsNullOrWhiteSpace(line.ExtrusionCode)
            //        && _extrusionByCode.TryGetValue(line.ExtrusionCode.Trim(), out var extrusion)
            //        && !string.Equals(extrusion.ThermalBreakType, "No", StringComparison.OrdinalIgnoreCase)
            //        && GetMassPerM(line.ExtrusionCode) * line.LengthM * line.Quantity > 0m);
            //if (hasThermalBreakContent)
            //{
            var (tbTypical, tbMin, tbMax) = _formulaSvc.LookupThermalBreakGuidance(
            ep.TotalKg, ep.ThermalBreakCalcType);
            label6.Text = tbTypical > 0m ? $"{tbTypical:P1}" : "—";
            label4.Text = tbMin > 0m ? $"{tbMin:P1}" : "—";
            label5.Text = tbMax > 0m ? $"{tbMax:P1}" : "—";
            //}
            //else
            //{
            //    label6.Text = "—";
            //    label4.Text = "—";
            //    label5.Text = "—";
            //}

            // ── Standard Finishing guidance (Pricing Matrix G44:I45) ──────────
            // Range category is driven by CostType:
            //   0 = Standard/Capral → 'd'
            //   1 = POA Colour      → 'b'
            //   2 = Custom Supplier → 'c'
            //   4/5 = Bremer Mill   → 'a'
            bool isAP = cmbPowderPricedAs.SelectedIndex <= 0;
            char finRange = ep.CostType switch
            {
                1 => 'b',
                2 => 'c',
                4 or 5 => 'a',
                _ => 'd'   // CostType 0 = Standard
            };

            var (finTypical, finMin, finMax) = _formulaSvc.LookupFinishBreakpointMargin(
                ep.TotalFinishingM2, isAP, finRange);

            // Use the best available cost for guidance:
            // B43 (FinishingCostPricePerM2) is preferred; fall back to B42 (POA/Other Vendor Cost)
            // when B43 is zero or holds the POA sentinel (99).
            static decimal BestCost(decimal primary, decimal fallback)
                => (primary > 0m && primary != FormulaCalculationService.PricePOASentinel)
                    ? primary : fallback;

            // Like CalculateFinishSellGuidance but without the POA-sentinel guard.
            // Used when the cost is known to be a real value from B49/B53 (e.g. $99.00
            // from Pricing Matrix E46/E47) that happens to equal the sentinel by coincidence.
            static decimal DirectSellGuidance(decimal cost, decimal margin)
                => cost > 0m && margin < 1m ? cost / (1m - margin) : 0m;

            var stdCost = BestCost(ep.FinishingCostPricePerM2, ep.Price_B42);
            var stdSellTyp = FormulaCalculationService.CalculateFinishSellGuidance(stdCost, finTypical);
            var stdSellMin = FormulaCalculationService.CalculateFinishSellGuidance(stdCost, finMin);
            var stdSellMax = FormulaCalculationService.CalculateFinishSellGuidance(stdCost, finMax);
            label13.Text = stdSellTyp > 0m ? $"${stdSellTyp:F2}" : "—";
            label14.Text = stdSellMin > 0m ? $"${stdSellMin:F2}" : "—";
            label15.Text = stdSellMax > 0m ? $"${stdSellMax:F2}" : "—";

            bool stdCostValid = stdCost > 0m && stdCost != FormulaCalculationService.PricePOASentinel;
            label19.Text = stdCostValid && finTypical > 0m ? $"{finTypical:P1}" : "—";
            label20.Text = stdCostValid && finMin > 0m ? $"{finMin:P1}" : "—";
            label21.Text = stdCostValid && finMax > 0m ? $"{finMax:P1}" : "—";

            // ── Bulk / Non-Bulk margin % (B51 and B55) ────────────────────────
            label24.Text = ep.Price_B50 > 0m ? $"{bulkMarginPct * 100m:F2}%" : "—";
            label25.Text = ep.Price_B54 > 0m ? $"{nonBulkMarginPct * 100m:F2}%" : "—";

            // Bulk sell $ guidance (Pricing Matrix G46:I46) and margin % (G48:I48).
            // Use B49 directly when populated — it is a legitimate cost (e.g. $99) and must not
            // be skipped by BestCost's sentinel check (PricePOASentinel == 99).
            // Fallback chain: B49 → B48 (POA) → stdCost (mirrors Excel B49 auto = B43).
            var bulkCost = ep.Price_B49 > 0m ? ep.Price_B49 : BestCost(ep.Price_B48, stdCost);
            var bulkSellTyp = DirectSellGuidance(bulkCost, finTypical);
            var bulkSellMin = DirectSellGuidance(bulkCost, finMin);
            var bulkSellMax = DirectSellGuidance(bulkCost, finMax);
            bool bulkCostValid = bulkCost > 0m;
            label26.Text = bulkSellTyp > 0m ? $"${bulkSellTyp:F2}" : "—";
            label27.Text = bulkSellMin > 0m ? $"${bulkSellMin:F2}" : "—";
            label28.Text = bulkSellMax > 0m ? $"${bulkSellMax:F2}" : "—";
            label7.Text = bulkCostValid && finTypical > 0m ? $"{finTypical:P1}" : "—";
            label8.Text = bulkCostValid && finMin > 0m ? $"{finMin:P1}" : "—";
            label9.Text = bulkCostValid && finMax > 0m ? $"{finMax:P1}" : "—";

            // Non-bulk sell $ guidance (Pricing Matrix G47:I47) and margin % (G48:I48).
            // Use B53 directly when populated — same sentinel-collision guard as bulkCost above.
            // Fallback chain: B53 → B52 (POA) → stdCost (mirrors Excel B53 auto = B43).
            var nbCost = ep.Price_B53 > 0m ? ep.Price_B53 : BestCost(ep.Price_B52, stdCost);
            var nbSellTyp = DirectSellGuidance(nbCost, finTypical);
            var nbSellMin = DirectSellGuidance(nbCost, finMin);
            var nbSellMax = DirectSellGuidance(nbCost, finMax);
            bool nbCostValid = nbCost > 0m;
            label16.Text = nbSellTyp > 0m ? $"${nbSellTyp:F2}" : "—";
            label17.Text = nbSellMin > 0m ? $"${nbSellMin:F2}" : "—";
            label18.Text = nbSellMax > 0m ? $"${nbSellMax:F2}" : "—";
            label10.Text = nbCostValid && finTypical > 0m ? $"{finTypical:P1}" : "—";
            label11.Text = nbCostValid && finMin > 0m ? $"{finMin:P1}" : "—";
            label12.Text = nbCostValid && finMax > 0m ? $"{finMax:P1}" : "—";

            if (saveChanges)
            {
                _db.UpdateProject(project);
                _db.UpsertEditPricing(ep);
            }

            var summary = _quoteSummaryService.Calculate(_projectId);

            if (saveChanges)
                _db.UpsertEditPricing(ep);

            // ── Summary grid ──────────────────────────────────────────────────
            // Matches Excel columns: Category | Total Value | Margin $ | Margin %.
            decimal sundryTotalValue = summary.SlottingSundryTotal;

            gridSummary.DataSource = new List<SummaryRow>
            {
                new() { Category = "Mill Finish",         TotalValue = summary.MillFinishTotal,        MarginDollar = summary.MillFinishMarginDollar,    MarginPct = summary.MillFinishMarginPct },
                new() { Category = "Thermal Break Metal", TotalValue = summary.ThermalBreakMetalTotal, MarginDollar = summary.ThermalBreakMarginDollar,  MarginPct = summary.ThermalBreakMarginPct },
                new() { Category = "Slotting/Sundry",     TotalValue = sundryTotalValue,               MarginDollar = summary.SlottingSundryMarginDollar, MarginPct = summary.SlottingSundryMarginPct },
                new() { Category = "Finishing",           TotalValue = summary.FinishingTotal,         MarginDollar = summary.FinishingMarginDollar,     MarginPct = summary.FinishingMarginPct },
                new() { Category = "Total",               TotalValue = summary.GrandTotal,             MarginDollar = summary.GrandMarginDollar,         MarginPct = summary.GrandMarginPct }
            };

            // Set column headers to match Excel layout
            if (gridSummary.Columns[nameof(SummaryRow.Category)] is { } catCol)
                catCol.HeaderText = "Category";
            if (gridSummary.Columns[nameof(SummaryRow.TotalValue)] is { } tvCol)
                tvCol.HeaderText = "Total Value";
            if (gridSummary.Columns[nameof(SummaryRow.MarginDollar)] is { } mdCol)
                mdCol.HeaderText = "Margin $";
            if (gridSummary.Columns[nameof(SummaryRow.MarginPct)] is { } mpCol)
                mpCol.HeaderText = "Margin %";

            // Bold the Total row font (no colour — keeps the plain form style)
            if (gridSummary.Rows.Count > 0)
            {
                var totalRow = gridSummary.Rows[gridSummary.Rows.Count - 1];
                totalRow.DefaultCellStyle.Font = FontBold9;
            }

            // Number formats matching the Excel Totals table (currency + percent).
            FormatSummaryColumns();

            // Apply bold font to the (now auto-generated) Category column.
            StyleCategoryColumn();
            UpdateCalculationTooltips(ep, metalCostPerKg, suggestedTypical, suggestedMin, suggestedMax,
                finTypical, finMin, finMax);


        }

        private decimal GetMassPerM(string? sectionCode)
        {
            if (string.IsNullOrWhiteSpace(sectionCode))
                return 0m;

            var code = sectionCode.Trim();
            if (_extrusionByCode.TryGetValue(code, out var extrusion))
                return extrusion.MassPerM;
            if (_sundryByCode.TryGetValue(code, out var sundry))
                return sundry.MassKgPerM;
            return 0m;
        }

        /// <summary>
        /// Returns the AP (Anodised Perimeter) value in mm/m for the given section code.
        /// Used to compute Total Finishing m² (AP basis) matching Excel col AD = AB * H / 1000 * G.
        /// </summary>
        private decimal GetApMm(string? sectionCode)
        {
            if (string.IsNullOrWhiteSpace(sectionCode)) return 0m;
            return _extrusionByCode.TryGetValue(sectionCode.Trim(), out var extrusion)
                ? extrusion.ApMm
                : 0m;
        }

        private static decimal ParseDecimal(string text, decimal fallback = 0m)
            => decimal.TryParse(text, out decimal value) ? value : fallback;

        /// <summary>
        /// Loads the Capral, LocAl Super Green, and LocAl Green logos from the
        /// Resources folder located alongside the application executable.
        /// Images are loaded gracefully — a missing file is silently skipped.
        /// </summary>
       /* private void LoadLogos()
        {
            var resourcesDir = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Resources");

            TryLoadImage(picCapralLogo,       System.IO.Path.Combine(resourcesDir, "capral_logo.png"));
            TryLoadImage(picLocalSuperGreen,  System.IO.Path.Combine(resourcesDir, "local_super_green.png"));
            TryLoadImage(picLocalGreen,       System.IO.Path.Combine(resourcesDir, "local_green.png"));
        }
       
        private static void TryLoadImage(System.Windows.Forms.PictureBox box, string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                    box.Image = System.Drawing.Image.FromFile(path);
            }
            catch { }
        }
*/

        private static System.Drawing.Font BoldFont(float size = 9F)
            => size switch
            {
                13F => FontBold13,
                8.5F => FontBold85,
                _ => FontBold9
            };

        private static decimal CalculateMarginPct(decimal sell, decimal cost)
            => sell > 0m ? (sell - cost) / sell * 100m : 0m;

        private void UpdateCalculationTooltips(EditPricing ep, decimal metalCostPerKg,
            decimal suggestedTypical, decimal suggestedMin, decimal suggestedMax,
            decimal finTypical, decimal finMin, decimal finMax)
        {
            var metalTooltip =
                $"Mill Finish Sell Price $/kg (B33){Environment.NewLine}" +
                $"Guidance from Table_Metal_BP: Typical = MetalCost + Spread{Environment.NewLine}" +
                $"(typical {suggestedTypical:F2}, min {suggestedMin:F2}, max {suggestedMax:F2}).{Environment.NewLine}" +
                $"Current input: {ep.MetalPrice:F4}";
            _formulaToolTip.SetToolTip(lblMetalPrice, metalTooltip);
            _formulaToolTip.SetToolTip(txtMetalPrice, metalTooltip);

            var millMarginTooltip =
                $"Margin % (B34) = (Sell − Metal Cost) / Sell{Environment.NewLine}" +
                $"= ({ep.MetalPrice:F4} − {metalCostPerKg:F4}) / {ep.MetalPrice:F4} = {ep.MillFinishMarginPct:F2}%";
            _formulaToolTip.SetToolTip(lblMillFinishMargin, millMarginTooltip);
            _formulaToolTip.SetToolTip(lblMillFinishMarginValue, millMarginTooltip);

            var ip1Tooltip =
                $"IP1 Margin % (G34) = (IP1 + Sell − Metal Cost) / (IP1 + Sell){Environment.NewLine}" +
                $"= ({ep.IP1UpchargePerKg:F2} + {ep.MetalPrice:F4} − {metalCostPerKg:F4}) / " +
                $"({ep.IP1UpchargePerKg:F2} + {ep.MetalPrice:F4}) = {ep.IP1MarginPct:F2}%";
            _formulaToolTip.SetToolTip(txtIP1Upcharge, ip1Tooltip);
            _formulaToolTip.SetToolTip(lblIP1MarginValue, ip1Tooltip);

            var ip2Tooltip =
                $"IP2 Margin % (H34) = (IP2 + Sell − Metal Cost) / (IP2 + Sell){Environment.NewLine}" +
                $"= ({ep.IP2UpchargePerKg:F2} + {ep.MetalPrice:F4} − {metalCostPerKg:F4}) / " +
                $"({ep.IP2UpchargePerKg:F2} + {ep.MetalPrice:F4}) = {ep.IP2MarginPct:F2}%";
            _formulaToolTip.SetToolTip(txtIP2Upcharge, ip2Tooltip);
            _formulaToolTip.SetToolTip(lblIP2MarginValue, ip2Tooltip);

            var ip3Tooltip =
                $"IP3 Margin % (I34) = (IP3 Rate − Metal Cost) / IP3 Rate{Environment.NewLine}" +
                $"= ({ep.IP3PanoramicFixedKgRate:F2} − {metalCostPerKg:F4}) / {ep.IP3PanoramicFixedKgRate:F2}" +
                $" = {ep.IP3MarginPct:F2}%";
            _formulaToolTip.SetToolTip(txtIP3Rate, ip3Tooltip);
            _formulaToolTip.SetToolTip(lblIP3MarginValue, ip3Tooltip);

            var tbTooltip =
                $"Thermal Break Enter Margin % (B37){Environment.NewLine}" +
                $"Entered as a percentage (e.g. 50 = 50%).{Environment.NewLine}" +
                $"Guidance Typical/Min/Max from Table_TB_BP by Total kg.";
            _formulaToolTip.SetToolTip(lblTBMargin, tbTooltip);
            _formulaToolTip.SetToolTip(txtTBMargin, tbTooltip);

            var finishingMarginTooltip =
                $"Margin % (B45) = IF(CostType=0, (B44−B43)/B44, (B44−B42)/B44){Environment.NewLine}" +
                $"Current: CostType={ep.CostType}, Sell(B44)={ep.Price_B44:F4}, Cost(B43)={ep.FinishingCostPricePerM2:F4}, POA(B42)={ep.Price_B42:F4}{Environment.NewLine}" +
                $"Result: {ep.FinishingMarginPct:F2}%";
            _formulaToolTip.SetToolTip(lblFinishingMargin, finishingMarginTooltip);
            _formulaToolTip.SetToolTip(lblFinishingMarginValue, finishingMarginTooltip);

            var finishSellTooltip =
                $"Finishing Sell Price $/m² (B44){Environment.NewLine}" +
                $"Guidance (G44:I44) = Cost / (1 − BreakpointMargin){Environment.NewLine}" +
                $"Breakpoint margins by Total m²: typical {finTypical:P1}, min {finMin:P1}, max {finMax:P1}.{Environment.NewLine}" +
                $"Blank when cost is POA (99) or unavailable.";

            _formulaToolTip.SetToolTip(txtFinishingPrice, finishSellTooltip);

            var thresholdMarginTooltip =
                $"Margin % (B59) = (B58 − B57) / B58{Environment.NewLine}" +
                $"= ({ep.Price_B58:F4} − {ep.Price_B57:F4}) / {ep.Price_B58:F4} = {ep.ThermalBreakMarginPct:F2}%";
            _formulaToolTip.SetToolTip(lblThresholdMargin, thresholdMarginTooltip);
            _formulaToolTip.SetToolTip(lblThresholdMarginValue, thresholdMarginTooltip);

            var bulkCostTooltip = "Finishing BULK Cost Price $/m² (B49), sourced from Pricing Matrix E46.";
            _formulaToolTip.SetToolTip(lblB49, bulkCostTooltip);
            _formulaToolTip.SetToolTip(txtB49, bulkCostTooltip);

            var bulkSellTooltip =
                $"Mill BULK Sell Price $/m² (B50){Environment.NewLine}" +
                $"Margin % (B51) = (B50 − B49) / B50. Guidance = Cost / (1 − BreakpointMargin).";
            _formulaToolTip.SetToolTip(lblB50, bulkSellTooltip);
            _formulaToolTip.SetToolTip(txtB50, bulkSellTooltip);

            var nonBulkCostTooltip = "Finishing NON-Bulk Cost Price $/m² (B53), sourced from Pricing Matrix E47.";
            _formulaToolTip.SetToolTip(lblB53, nonBulkCostTooltip);
            _formulaToolTip.SetToolTip(txtB53, nonBulkCostTooltip);

            var nonBulkSellTooltip =
                $"Mill NON-Bulk Sell Price $/m² (B54){Environment.NewLine}" +
                $"Margin % (B55) = (B54 − B53) / B54. Guidance = Cost / (1 − BreakpointMargin).";
            _formulaToolTip.SetToolTip(lblB54, nonBulkSellTooltip);
            _formulaToolTip.SetToolTip(txtB54, nonBulkSellTooltip);
        }

        /// <summary>
        /// Applies Excel-like number formats to the summary grid:
        /// currency (N2) for $ columns and percent (0.00%) for margin %.
        /// </summary>
        private void FormatSummaryColumns()
        {
            foreach (var colName in new[]
                     { nameof(SummaryRow.TotalValue), nameof(SummaryRow.MarginDollar) })
            {
                var col = gridSummary.Columns[colName];
                if (col != null) col.DefaultCellStyle.Format = "N2";
            }
            var pctCol = gridSummary.Columns[nameof(SummaryRow.MarginPct)];
            if (pctCol != null) pctCol.DefaultCellStyle.Format = "0.00\\%";
        }

        /// <summary>
        /// Applies runtime font styling.
        /// All control layout and parenting is already done in InitializeComponent().
        /// </summary>
        private void BuildLayout()
        {
            // ── Title ─────────────────────────────────────────────────────────
            lblTitle.Font = BoldFont(13F);

            // ── Fonts ─────────────────────────────────────────────────────────
            lblMillFinishMarginValue.Font = BoldFont();
            lblFinishingMarginValue.Font = BoldFont();
            lblThresholdMarginValue.Font = BoldFont();
            lblTotalKgValue.Font = BoldFont();
            lblTotalFinishingM2Value.Font = BoldFont();
            lblTotalPiecesValue.Font = BoldFont();
            lblApNote.Font = FontItalic75;
            lblSummaryNote1.Font = FontItalic75;
            lblSummaryNote2.Font = FontItalic75;

            // ── DataGridView header font ──────────────────────────────────────
            // Note: gridSummary is data-bound (columns are auto-generated from the
            // SummaryRow type when DataSource is set), so the "Category" column does
            // not exist yet here. Its font is applied after binding in
            // UpdateDerivedFields via StyleCategoryColumn().
            gridSummary.ColumnHeadersDefaultCellStyle.Font = BoldFont(8.5F);
        }

        /// <summary>
        /// Applies the bold font to the auto-generated "Category" column.
        /// Safe to call after the grid's DataSource has been set.
        /// </summary>
        private void StyleCategoryColumn()
        {
            var categoryColumn = gridSummary.Columns[nameof(SummaryRow.Category)];
            if (categoryColumn != null)
                categoryColumn.DefaultCellStyle.Font = BoldFont();
        }

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
            /// <summary>Total sell value (what we charge the customer). Matches Excel "Total Value" column.</summary>
            public decimal TotalValue { get; set; }
            /// <summary>Margin in dollars. Matches Excel "Margin $" column.</summary>
            public decimal MarginDollar { get; set; }
            /// <summary>Margin percentage. Matches Excel "Margin %" column.</summary>
            public decimal MarginPct { get; set; }
        }

       
    }
}
