using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;
using System.Reflection;

namespace QuotingTool.Forms
{
    public partial class QuoteForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly PageSetupService _pageSetup;
        private readonly LengthFixService _lengthFix;
        private readonly ResetPriceService _resetPrice;
        private readonly ExportQuoteService _exportQuote;
        private readonly SapExportService _sapExport;
        private readonly BomService _bom;
        private readonly QuotePdfService _quotePdf;
        private readonly Cre8ImportService _cre8Import;
        private readonly ImportPricesService _importPrices;
        private const int MaxQuotePages = 6;
        private const int MaxRowsPerPage = 40;
        private const int MaxQuoteLines = MaxQuotePages * MaxRowsPerPage;
        private const int QtyColumnWidth = 70;
        private const int LengthColumnWidth = 90;
        private const int PriceColumnWidth = 90;
        private const int TotalColumnWidth = 90;
        private const string MillFinishType = "Mill Finish";
        private BindingList<QuoteLine> _lines = new();
        private int _projectId;
        private bool _bindingGrid;
        private Dictionary<string, ExtrusionMaster> _extrusionMasterByCode = new(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, SundryItem> _sundryMasterByCode = new(StringComparer.OrdinalIgnoreCase);
        private decimal _metalPricePerKg;
        private decimal _finishingSellPricePerM2;
        // IP upcharge rates loaded from EditPricing (default to workbook constants when no project pricing exists)
        private decimal _ip1RatePerKg ;
        private decimal _ip2RatePerKg; 
        private decimal _ip3RatePerKg ;
        // PH code → IP category (0-3) lookup, sourced from 5.Edit_PH_IP sheet via IPMasterData table
        private Dictionary<string, int> _ipMasterByCodes = new(StringComparer.Ordinal);
        // Section codes in the THRESHOLDS named range; used for the BF column threshold flag
        private HashSet<string> _thresholdSections = new(StringComparer.OrdinalIgnoreCase);
        // SubsillTierPrices keyed by Tier, used to compute AZ (slot sell each) live
        private Dictionary<int, SubsillTierPrice> _subsillTiers = new();
        private string _projectFinishSapCode = "NON";
        private string _thresholdChoice = "No";
        private string _projectFinishType = "Mill Finish";
        private decimal _millFinishMarginPct;
        private decimal _finishingMarginPct;
        private decimal _finishingCostPricePerM2;
        // B58: Anodised threshold sell rate $/m² — used instead of FinishingPrice when a
        // section is flagged as a threshold (BF col = 1) and ThresholdChoice is not "No".
        private decimal _thresholdSellPricePerM2;
        // B36/B37 on 3.Edit Pricing: thermal-break calc type ("Margin" or "List less Discount")
        // and its margin/discount value, used by the PDB path of MF Each (col AH).
        private string _thermalBreakCalcType = "Margin";
        private decimal _thermalBreakMargin = 0.5m;
        // Pricing Matrix F4/F5 — PDB metal cost base ($/kg) and ALF cost per metre ($/m).
        private decimal _pdbMetalCostBase;
        private decimal _alfCostPerMetre;
        // Table_TB_Cost rows keyed by Section (case-insensitive), used for the PLS AH-column path.
        private Dictionary<string, ThermalBreakCost> _thermalBreakCostBySection = new(StringComparer.OrdinalIgnoreCase);
        // TB_MPG rows keyed by 2-char material price group code, used for the PDB "List less Discount" path.
        private Dictionary<string, ThermalBreakMaterialPriceGroup> _tbMaterialPriceGroupsByCode = new(StringComparer.OrdinalIgnoreCase);

        public QuoteForm(DatabaseContext db)
        {
            _db = db;
            _pageSetup = new PageSetupService(db);
            _lengthFix = new LengthFixService(db);
            _resetPrice = new ResetPriceService(db);
            _exportQuote = new ExportQuoteService(db);
            _sapExport = new SapExportService(db);
            _bom = new BomService(db);
            _quotePdf = new QuotePdfService(db);
            _cre8Import = new Cre8ImportService(db);
            _importPrices = new ImportPricesService(db);
            InitializeComponent();
            LoadExtrusionMaster();
            LoadSundryMaster();
            LoadIPMasterData();
            LoadSubsillTiers();
            LoadThermalBreakCostData();
            ConfigureGridColumns();
            gridQuote.CellToolTipTextNeeded += gridQuote_CellToolTipTextNeeded;
        }

        private void LoadExtrusionMaster()
        {
            // Section is the primary key in ExtrusionMaster; guard defensively against
            // any case-insensitive collisions when building the case-insensitive lookup.
            _extrusionMasterByCode = new Dictionary<string, ExtrusionMaster>(StringComparer.OrdinalIgnoreCase);
            foreach (var master in _db.GetExtrusionMasterList())
                _extrusionMasterByCode[master.Section] = master;
        }

        private void LoadIPMasterData()
        {
            try
            {
                _ipMasterByCodes = _db.GetIPMasterByCodes();
                _thresholdSections = _db.GetThresholdSections();
            }
            catch
            {
                // Tables may not yet exist on first launch before schema migration completes;
                // fall back to empty lookups so the form still opens.
                _ipMasterByCodes = new Dictionary<string, int>(StringComparer.Ordinal);
                _thresholdSections = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private void LoadSubsillTiers()
        {
            try { _subsillTiers = _db.GetSubsillTierPrices().ToDictionary(t => t.Tier); }
            catch { _subsillTiers = new Dictionary<int, SubsillTierPrice>(); }
        }

        private void LoadThermalBreakCostData()
        {
            try
            {
                _thermalBreakCostBySection = _db.GetThermalBreakCosts()
                    .ToDictionary(t => t.Section, StringComparer.OrdinalIgnoreCase);
                _tbMaterialPriceGroupsByCode = _db.GetThermalBreakMaterialPriceGroups()
                    .ToDictionary(t => t.GroupCode, StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                // Tables may not yet exist before schema migration completes, or no data has
                // been seeded yet; fall back to empty lookups so PLS/PDB sections simply fall
                // through to the standard mill-finish path rather than throwing.
                _thermalBreakCostBySection = new Dictionary<string, ThermalBreakCost>(StringComparer.OrdinalIgnoreCase);
                _tbMaterialPriceGroupsByCode = new Dictionary<string, ThermalBreakMaterialPriceGroup>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private void LoadSundryMaster()
        {
            _sundryMasterByCode = new Dictionary<string, SundryItem>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in _db.GetSundryItems())
            {
                if (!string.IsNullOrWhiteSpace(item.SectionCode))
                    _sundryMasterByCode[item.SectionCode.Trim()] = item;
            }
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            LoadProjectSettings();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            if (_projectId <= 0) return;
            LoadProjectSettings();
            LoadSundryMaster();
            _bindingGrid = true;
            _lines = new BindingList<QuoteLine>(_db.GetQuoteLines(_projectId).ToList());
            foreach (var line in _lines)
            {
                if (line.LineNumber <= 0)
                    line.LineNumber = line.RowInBlock;
                ApplySectionLookup(line);
                var storedPrice = line.Price;
                RecalculatePrice(line);
                if (_metalPricePerKg > 0 && line.Price != storedPrice && IsMeaningfulLine(line))
                    _db.UpsertQuoteLine(line);
            }
            EnsureTrailingBlankRow();
            gridQuote.DataSource = _lines;
            _bindingGrid = false;
            UpdatePageLayout();
            UpdateValidationStates();
            UpdatePriceBreakdown();
        }

        /// <summary>
        /// Auto-loads Description/Alloy/Temper/SubsillFlag/IpGrade for a quote line based on its
        /// selected Section (ExtrusionCode), mirroring the Table5 VLOOKUP formulas in the workbook.
        ///
        /// Col D formula (simplified): * prefix when TB type is PDB/PLS; base description from Table5[2];
        ///   " **MILL FINISH**" suffix when IgnoreFinish = "Yes".
        /// Col V formula: VLOOKUP(C, Table5[], 11) → subsill tier; SubsillFlag = tier > 0.
        /// Col AV formula: VLOOKUP(PH code, 5.Edit_PH_IP, col D) → IP category 0-3.
        /// </summary>
        private void ApplySectionLookup(QuoteLine line)
        {
            var sectionCode = NormalizeSectionCode(line.ExtrusionCode);
            if (string.IsNullOrWhiteSpace(sectionCode))
            {
                line.ExtrusionCode = string.Empty;
                line.Description = string.Empty;
                return;
            }
            line.ExtrusionCode = sectionCode;

            if (!_extrusionMasterByCode.TryGetValue(sectionCode, out var master))
            {
                if (_sundryMasterByCode.TryGetValue(sectionCode, out var sundry))
                {
                    // Sundry descriptions may be overwritten by the user (e.g. replace
                    // "Sundry description - 1" with "Airfreight"), so only default the
                    // description from the master when the line has none of its own.
                    if (string.IsNullOrWhiteSpace(line.Description))
                        line.Description = sundry.Description ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(line.Alloy))
                        line.Alloy = "6060";
                    if (string.IsNullOrWhiteSpace(line.Temper))
                        line.Temper = "T5";
                }
                return;
            }

            // Build Description: prepend * for thermal-break sections (PDB/PLS), then base description,
            // then append " **MILL FINISH**" when Ignore Finish is set to "Yes".
            var tbType = master.ThermalBreakType ?? "No";
            var descBase = master.Description ?? string.Empty;
            var prefix = (tbType == "PLS" || tbType == "PDB") ? "*" : string.Empty;
            var suffix = string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase)
                ? " **MILL FINISH**"
                : string.Empty;
            line.Description = prefix + descBase + suffix;

            if (string.IsNullOrWhiteSpace(line.Alloy))
                line.Alloy = master.DefaultAlloy;
            if (string.IsNullOrWhiteSpace(line.Temper))
                line.Temper = master.DefaultTemper;

            // Col V: SubsillFlag = tier > 0 (auto-filled from Table5 col K; user can still override).
            // AZ12: Slot Sell ea = VLOOKUP(V, SubSill_Tier_Price, 2) * H
            //   Tier 4 (Panoramic door): +150% market factor applied to sell price.
            // Extra/Sundry Charge (ea) (SubsillSurcharge) is a manually entered field, so only
            // apply the tier-based default the first time (while it is still 0) — never
            // overwrite a value the user has already typed in.
            if (master.SubsillTier > 0)
            {
                line.SubsillFlag = true;
                if (line.SubsillSurcharge == 0m && line.LengthM > 0m &&
                    _subsillTiers.TryGetValue(master.SubsillTier, out var tier))
                {
                    line.SubsillSurcharge = tier.SellPricePerMetre * line.LengthM;
                    if (master.SubsillTier == 4)
                        line.SubsillSurcharge += line.SubsillSurcharge * 1.5m;
                }
            }

            // Col AV: IP grade auto-computed from PH code via IPMasterData lookup.
            // IpGrade is stored so the user can override it if needed.
            if (!string.IsNullOrWhiteSpace(master.PhCode) &&
                _ipMasterByCodes.TryGetValue(master.PhCode, out var ipCat))
            {
                line.IpGrade = ipCat;
            }
        }

        /// <summary>
        /// $ Each is calculated from:
        ///   MF Each (AH) = kg/m × Length × MetalSellPrice (standard path)
        ///   IP Each (AI) = kg/m × Length × IP upcharge rate (based on IpGrade: 0=none,1=IP1,2=IP2,3=IP3)
        ///   SF Sell Each (AS) = finishing area × finishing sell rate (AP fallback to PP perimeter)
        ///   Slotting/Sundry ea (K) = user-entered SubsillSurcharge
        /// </summary>
        private void RecalculatePrice(QuoteLine line)
        {
            var millFinishEach = CalculateMillFinishEach(line);
            var ipEach = CalculateIpEach(line);
            var slottingSundryEach = line.SubsillSurcharge;
            var sfSellEach = CalculateFinishingEach(line);
            var calculatedPrice = FormulaCalculationService.CalculatePriceEach(
                millFinishEach,
                ipEach: ipEach,
                sfSellEach: sfSellEach,
                slottingSundryEach: slottingSundryEach);
            line.Price = line.ManualPriceOverride ?? calculatedPrice;
            line.TotalPrice = FormulaCalculationService.CalculateSellTotal(line.Price, line.Quantity);
            // Keep legacy SellPrice (col J) synchronized with TotalPrice so existing
            // exports/queries that still read SellPrice receive the recalculated value.
            line.SellPrice = line.TotalPrice;
            line.SapColourCode = FormulaCalculationService.CalculateSapColourCode(
                line.ExtrusionCode,
                _thresholdSections.Contains(line.ExtrusionCode),
                NormalizeThresholdChoice(_thresholdChoice),
                line.IgnoreFinish,
                _projectFinishSapCode);
        }

        /// <summary>
        /// Col AH – MF Each. Mirrors the Excel array formula:
        ///   IF(AV=3, IF(Y&lt;0.5, Z*2.5, 0),
        ///     IF(P="PLS", TB_Cost.MetalSellPrice / TB_Cost.ReferenceLengthM * Length,
        ///       IF(P="PDB",
        ///         IFS(tb_calc_type="Margin", ((Z*F4)+(Length*F5)) / (1-tb_calc_value),
        ///             tb_calc_type="List less Discount", ((Z*TB_MPG.Rate)+(Length*F5*3)) * (1-tb_calc_value)),
        ///         Z * MetalSellPrice)))
        /// Where Z = kg each, Y = kg/m, F4/F5 = PricingConstants.PdbMetalCostBase/AlfCostPerMetre.
        /// Falls back to the standard mill-finish path when the required PLS/PDB reference
        /// data (Table_TB_Cost / TB_MPG) has not been seeded, so pricing is still produced.
        /// </summary>
        private decimal CalculateMillFinishEach(QuoteLine line)
        {
            var massPerM = GetMassPerM(line.ExtrusionCode);
            if (massPerM <= 0 || line.LengthM <= 0) return 0m;

            var kgEach = massPerM * line.LengthM;

            // AV=3 (IP3 panoramic): rolling factor for light sections (Y < 0.5 kg/m).
            if (line.IpGrade == 3)
                return massPerM < 0.5m ? kgEach * 2.5m : 0m;

            var thermalBreakType = _extrusionMasterByCode.TryGetValue(line.ExtrusionCode, out var master)
                ? master.ThermalBreakType ?? "No"
                : "No";

            if (string.Equals(thermalBreakType, "PLS", StringComparison.OrdinalIgnoreCase))
            {
                if (_thermalBreakCostBySection.TryGetValue(line.ExtrusionCode, out var tbCost) &&
                    tbCost.ReferenceLengthM > 0)
                {
                    // MetalSellPrice / ReferenceLengthM = $/m rate; scale by the quoted length.
                    return (tbCost.MetalSellPrice / tbCost.ReferenceLengthM) * line.LengthM;
                }
                // No Table_TB_Cost data seeded for this section – fall back to the standard
                // path below rather than silently returning 0.
            }
            else if (string.Equals(thermalBreakType, "PDB", StringComparison.OrdinalIgnoreCase))
            {
                bool isMarginCalc = string.Equals(_thermalBreakCalcType, "Margin", StringComparison.OrdinalIgnoreCase);
                if (isMarginCalc)
                {
                    var denominator = 1m - _thermalBreakMargin;
                    if (denominator > 0m && _pdbMetalCostBase > 0m)
                        return ((kgEach * _pdbMetalCostBase) + (line.LengthM * _alfCostPerMetre)) / denominator;
                }
                else
                {
                    // "List less Discount": needs the material price group rate (TB_MPG),
                    // looked up via the first 2 chars of the section's MaterialPriceGroupCode.
                    if (_thermalBreakCostBySection.TryGetValue(line.ExtrusionCode, out var tbCost) &&
                        !string.IsNullOrWhiteSpace(tbCost.MaterialPriceGroupCode))
                    {
                        var groupKey = tbCost.MaterialPriceGroupCode.Length >= 2
                            ? tbCost.MaterialPriceGroupCode.Substring(0, 2)
                            : tbCost.MaterialPriceGroupCode;
                        if (_tbMaterialPriceGroupsByCode.TryGetValue(groupKey, out var mpg))
                        {
                            return ((kgEach * mpg.RatePerKg) + (line.LengthM * _alfCostPerMetre * 3m))
                                   * (1m - _thermalBreakMargin);
                        }
                    }
                }
                // Missing TB_MPG/reference data – fall back to the standard path below.
            }

            // Standard mill-finish path.
            if (_metalPricePerKg <= 0) return 0m;
            return kgEach * _metalPricePerKg;
        }

        /// <summary>
        /// Col AI – IP Each = Z × rate ($/kg), mirroring the Excel formula:
        ///   =IF(AV=1, Z*G33, IF(AV=2, Z*H33, IF(AV=3, Z*I33, 0)))
        /// Where Z (col Z) = kg each = kg/m × Length (m), and G33/H33/I33 are the
        /// IP1/IP2/IP3 upcharge rates from '3.Edit Pricing'.
        /// </summary>
        private decimal CalculateIpEach(QuoteLine line)
        {
            // Z32: kg each = kg/m × Length(m)
            var kgEach = FormulaCalculationService.CalculateKgEach(
                GetMassPerM(line.ExtrusionCode), line.LengthM);
            // AI32: IP Each = Z32 × G33/H33/I33 based on IP grade (AV col)
            return FormulaCalculationService.CalculateIpEach(
                line.IpGrade, kgEach, _ip1RatePerKg, _ip2RatePerKg, _ip3RatePerKg);
        }

        private decimal CalculateFinishingEach(QuoteLine line)
        {
            if (string.Equals(_projectFinishType, MillFinishType, StringComparison.OrdinalIgnoreCase))
                return 0m;
            // d. IgnoreFinish="Yes" means this line is priced as Mill Finish regardless of the
            // project's finish type (mirrors the Excel col L / **MILL FINISH** override logic).
            if (string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase))
                return 0m;
            if (line.LengthM <= 0 || string.IsNullOrWhiteSpace(line.ExtrusionCode))
                return 0m;

            // Determine whether this line should use Capral Mill (AP) finishing rules.
            // Use the per-line MillPCOverride value (stored as "Yes"/"No" or bool) when present.
            bool millPcOverride = GetLineBoolFlag(line, new[] { "MillPCOverride" });
            bool useCapralAp = string.Equals(_projectFinishType, "Capral Mill (AP)", StringComparison.OrdinalIgnoreCase)
                || millPcOverride;

            // When not using Capral AP, choose threshold vs standard finishing sell rate as before.
            bool isThreshold = _thresholdSections.Contains(line.ExtrusionCode)
                && !string.Equals(NormalizeThresholdChoice(_thresholdChoice), "No", StringComparison.OrdinalIgnoreCase);

            decimal sfSellRatePerM2 = _finishingSellPricePerM2;
            decimal sfCostPerM2 = _finishingCostPricePerM2;
            if (!useCapralAp)
            {
                sfSellRatePerM2 = isThreshold && _thresholdSellPricePerM2 > 0m
                    ? _thresholdSellPricePerM2
                    : _finishingSellPricePerM2;
            }

            decimal perimeterMm = 0m;
            if (_extrusionMasterByCode.TryGetValue(line.ExtrusionCode, out var master))
                perimeterMm = useCapralAp ? master.ApMm : (master.ApMm > 0 ? master.ApMm : master.PpMm);
            else if (_sundryMasterByCode.TryGetValue(line.ExtrusionCode, out var sundry))
                perimeterMm = useCapralAp ? sundry.Ap : (sundry.Ap > 0 ? sundry.Ap : sundry.Pp);

            if (perimeterMm <= 0)
                return 0m;

            // AC: AP each (per piece) = AB(perimeter mm) × H(length m) / 1000
            var areaM2Each = (perimeterMm * line.LengthM) / 1000m;
            if (areaM2Each <= 0)
                return 0m;

            if (useCapralAp)
            {
                // AD: AP total = AC(area each) × G(quantity). The Bulk/Non-bulk/Minfin band
                // (col U) is classified from the TOTAL area, not the per-piece area.
                var areaM2Total = areaM2Each * line.Quantity;
                string band = areaM2Total < 0m ? "Minfin" : (areaM2Total < 80m ? "Non-bulk" : "Bulk");

                var pricing = _db.GetEditPricing(_projectId);
                string[] sellCandidates;
                string[] costCandidates;
                if (band == "Bulk")
                {
                    // 3.Edit Pricing B50 = Mill BULK Sell Price $/m², B49 = Mill BULK Cost Price $/m² (auto)
                    sellCandidates = new[] { "Price_B50", "CapralApSellPrice_Bulk", "Price_AP_Bulk", "AP_Sell_Bulk", "APBulkSellPrice", "APBulkSell", "APSellPrice_Bulk" };
                    costCandidates = new[] { "Price_B49", "CapralApCostPrice_Bulk", "Cost_AP_Bulk", "AP_Cost_Bulk", "APBulkCostPrice", "APBulkCost" };
                }
                else if (band == "Non-bulk")
                {
                    // 3.Edit Pricing B54 = Mill NON-Bulk Sell Price $/m², B53 = Mill NON-Bulk Cost Price $/m² (auto)
                    sellCandidates = new[] { "Price_B54", "CapralApSellPrice_NonBulk", "Price_AP_NonBulk", "AP_Sell_NonBulk", "APNonBulkSellPrice", "APNonBulkSell", "APSellPrice_NonBulk" };
                    costCandidates = new[] { "Price_B53", "CapralApCostPrice_NonBulk", "Cost_AP_NonBulk", "AP_Cost_NonBulk", "APNonBulkCostPrice", "APNonBulkCost" };
                }
                else
                {
                    // No dedicated Minfin rate exists in EditPricing; fall back to the anodised
                    // threshold sell/cost price ($/m²) as the closest equivalent.
                    sellCandidates = new[] { "Price_B58", "CapralApSellPrice_Minfin", "Price_AP_Minfin", "AP_Sell_Minfin", "APMinfinSellPrice" };
                    costCandidates = new[] { "AnodisedThresholdCostPerM2", "CapralApCostPrice_Minfin", "Cost_AP_Minfin", "AP_Cost_Minfin", "APMinfinCostPrice" };
                }

                if (!TryGetDecimalFromObject(pricing, sellCandidates, out var foundSell) || foundSell <= 0m)
                    foundSell = _finishingSellPricePerM2;
                if (!TryGetDecimalFromObject(pricing, costCandidates, out var foundCost) || foundCost <= 0m)
                    foundCost = _finishingCostPricePerM2;

                sfSellRatePerM2 = foundSell;
                sfCostPerM2 = foundCost;
            }

            return areaM2Each * sfSellRatePerM2;
        }

        private decimal GetMassPerM(string? sectionCode)
        {
            if (string.IsNullOrWhiteSpace(sectionCode))
                return 0m;
            if (_extrusionMasterByCode.TryGetValue(sectionCode, out var master))
                return master.MassPerM;
            if (_sundryMasterByCode.TryGetValue(sectionCode, out var sundry))
                return sundry.MassKgPerM;
            return 0m;
        }

        // Helper: reads a boolean-like flag from the QuoteLine by several candidate property names.
        private bool GetLineBoolFlag(QuoteLine line, string[] candidateNames)
        {
            if (line == null) return false;
            var t = line.GetType();
            foreach (var name in candidateNames)
            {
                var prop = t.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null) continue;
                var val = prop.GetValue(line);
                if (val == null) continue;
                if (val is bool b) return b;
                var s = val.ToString();
                if (string.Equals(s, "Yes", StringComparison.OrdinalIgnoreCase) || string.Equals(s, "Y", StringComparison.OrdinalIgnoreCase) || s == "1")
                    return true;
            }
            return false;
        }

        // Helper: tries to extract a decimal-valued property from an object using a list of candidate property names.
        private bool TryGetDecimalFromObject(object? obj, string[] candidateNames, out decimal value)
        {
            value = 0m;
            if (obj == null) return false;
            var t = obj.GetType();
            foreach (var name in candidateNames)
            {
                var prop = t.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null) continue;
                var val = prop.GetValue(obj);
                if (val == null) continue;
                switch (val)
                {
                    case decimal d:
                        value = d; return true;
                    case double db:
                        value = (decimal)db; return true;
                    case float f:
                        value = (decimal)f; return true;
                    case int i:
                        value = i; return true;
                    case long l:
                        value = l; return true;
                    default:
                        if (decimal.TryParse(val.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed) ||
                            decimal.TryParse(val.ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, out parsed))
                        {
                            value = parsed; return true;
                        }
                        break;
                }
            }
            return false;
        }

        private void ConfigureGridColumns()
        {
            gridQuote.AutoGenerateColumns = true;
            gridQuote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            gridQuote.DataError += (s, e) =>
            {
                e.ThrowException = false;
                System.Diagnostics.Debug.WriteLine(
                    $"gridQuote DataError at row {e.RowIndex}, col {e.ColumnIndex}: {e.Exception?.Message}");
            };
            gridQuote.DataBindingComplete += (s, e) =>
            {
                SetColHeader("PageBlock", "Page", 50, false);
                SetColHeader("LineNumber", "Ln", 50, false);
                SetColHeader("RowInBlock", "Row", 50, true);
                SetColHeader("ExtrusionCode", "Section", 110, false);
                SetColHeader("Description", "Description", 250, false);
                SetColHeader("Alloy", "Alloy", 70, false);
                SetColHeader("Temper", "Temper", 70, false);
                SetColHeader("Quantity", "Qty", QtyColumnWidth, false);
                SetColHeader("LengthM", "Length (m)", LengthColumnWidth, false);
                SetColHeader("IpGrade", "IP", 50, false);
                SetColHeader("ManualPriceOverride", "Override $ Each", 110, false);
                SetColHeader("Price", "$ Each", PriceColumnWidth, false);
                SetColHeader("TotalPrice", "$ Total", TotalColumnWidth, false);
                SetColHeader("SellPrice", "Sell Price", 90, true);
                SetColHeader("SubsillSurcharge", "Extra/Sundry Charge (ea)", 150, false);
                SetColHeader("IgnoreFinish", "Ign Fin", 80, false);
                // New manual control to force Capral Mill AP finishing on a row
                SetColHeader("MillPCOverride", "Mill PC Over-ride", 110, false);
                // Ensure MillPCOverride is presented as a Yes/No dropdown bound to the boolean model property.
                if (gridQuote.Columns.Contains("MillPCOverride"))
                {
                    var existing = gridQuote.Columns["MillPCOverride"];
                    if (existing is not DataGridViewComboBoxColumn)
                    {
                        int displayIdx = existing.DisplayIndex;
                        int colIndex = existing.Index;
                        var combo = new DataGridViewComboBoxColumn
                        {
                            DataPropertyName = "MillPCOverride",
                            Name = "MillPCOverride",
                            HeaderText = existing.HeaderText,
                            Width = existing.Width,
                            ValueType = typeof(string),
                            FlatStyle = FlatStyle.Flat
                        };
                        // Use a small DataTable as the data source so we can show "Yes"/"No" while binding to string values.
                        var dt = new System.Data.DataTable();
                        dt.Columns.Add("Value", typeof(string));
                        dt.Columns.Add("Text", typeof(string));
                        dt.Rows.Add("Yes", "Yes");
                        dt.Rows.Add("No", "No");
                        combo.DataSource = dt;
                        combo.ValueMember = "Value";
                        combo.DisplayMember = "Text";

                        // Preserve display index when replacing column
                        gridQuote.Columns.RemoveAt(colIndex);
                        gridQuote.Columns.Add(combo);
                        combo.DisplayIndex = displayIdx;
                    }
                }
                SetColHeader("SapColourCode", "SAP Code", 90, false);
                SetColHeader("SubsillFlag", "SS?", 60, false);
                HideCol("Id");
                HideCol("ProjectId");
                // Description/Alloy/Temper auto-load from the Section master; $ Each and
                // $ Total are calculated from Length, so none of these are manually edited.
                SetReadOnly("Alloy", true);
                SetReadOnly("Temper", true);
                SetReadOnly("Price", true);
                SetReadOnly("TotalPrice", true);
                SetReadOnly("ManualPriceOverride", !UserSession.HasRole(UserRole.SalesManager));

                SetColFormat("Quantity", "0.##", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("LengthM", "0.###", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("ManualPriceOverride", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("Price", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("TotalPrice", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("SubsillSurcharge", "0.0000", DataGridViewContentAlignment.MiddleRight);

                if (gridQuote.Columns.Contains("Price"))
                    gridQuote.Columns["Price"].ToolTipText = "$ Each = MF Each + IP Each + SF Sell Each + Extra/Sundry Charge (ea)";
                if (gridQuote.Columns.Contains("TotalPrice"))
                    gridQuote.Columns["TotalPrice"].ToolTipText = "$ Total = $ Each × Qty";

                // Visually differentiate information-only columns from editable ones.
                foreach (DataGridViewColumn column in gridQuote.Columns)
                {
                    column.DefaultCellStyle.BackColor = column.ReadOnly
                        ? System.Drawing.Color.FromArgb(240, 240, 240)
                        : System.Drawing.Color.White;
                }

                // Reorder columns to match the numbered layout (left-to-right positions):
                // 1=LineNumber, 2=ExtrusionCode, 3=Description, 4=Alloy, 5=Temper,
                // 6=Price, 7=Quantity, 8=LengthM, 9=TotalPrice, 10=SubsillSurcharge,
                // 11=SapColourCode, 12=SubsillFlag, 13=IgnoreFinish, 14=MillPCOverride, 15=ManualPriceOverride, 16=PageBlock
                var displayOrder = new Dictionary<string, int>
                {
                    {"LineNumber", 0},      // 1
                    {"ExtrusionCode", 1},   // 2
                    {"Description", 2},     // 3
                    {"Alloy", 3},           // 4
                    {"Temper", 4},          // 5
                    {"Price", 5},           // 6
                    {"Quantity", 6},        // 7
                    {"LengthM", 7},         // 8
                    {"TotalPrice", 8},      // 9
                    {"SubsillSurcharge", 9},//10
                    {"SapColourCode", 10},  //11
                    {"SubsillFlag", 11},    //12
                    {"IgnoreFinish", 12},   //13
                    {"MillPCOverride",13},  //14
                    {"ManualPriceOverride",14},//15
                    {"PageBlock",15}        //16
                };

                foreach (var kv in displayOrder)
                {
                    if (gridQuote.Columns.Contains(kv.Key))
                        gridQuote.Columns[kv.Key].DisplayIndex = kv.Value;
                }
            };
        }

        private void SetReadOnly(string propName, bool readOnly)
        {
            if (gridQuote.Columns.Contains(propName))
                gridQuote.Columns[propName].ReadOnly = readOnly;
        }

        private void SetColHeader(string propName, string header, int width, bool hide)
        {
            if (!gridQuote.Columns.Contains(propName)) return;
            var column = gridQuote.Columns[propName];
            column.HeaderText = header;
            column.Width = width;
            column.Visible = !hide;
        }

        private void HideCol(string propName)
        {
            if (gridQuote.Columns.Contains(propName))
                gridQuote.Columns[propName].Visible = false;
        }

        private void SetColFormat(string propName, string format, DataGridViewContentAlignment alignment)
        {
            if (!gridQuote.Columns.Contains(propName)) return;
            var style = gridQuote.Columns[propName].DefaultCellStyle;
            style.Format = format;
            style.Alignment = alignment;
        }

        private void btnFixLengths_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int fixedCount = _lengthFix.FixLengths(_projectId);
            MessageBox.Show($"Fixed {fixedCount} length(s) from mm → m.", "Fix Lengths",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshGrid();
        }

        private void btnResetPrices_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int reset = _resetPrice.ResetPriceFormulas(_projectId);
            MessageBox.Show($"Reset {reset} price formula(s).", "Reset Prices",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshGrid();
        }

        private void btnExportQuote_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            var result = _exportQuote.ExportQuoteData(_projectId);
            if (result.Status != ExportStatus.OK)
                MessageBox.Show(result.Message, "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                FileName = $"Quote_{DateTime.Today:yyyyMMdd}",
                DefaultExt = "xlsx"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                var exporter = new Helpers.ExcelExporter(_db);
                if (sfd.FilterIndex == 1) exporter.ExportToExcel(_projectId, sfd.FileName);
                else exporter.ExportToCsv(_projectId, sfd.FileName);
                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyForSap_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int count = _sapExport.CopyToClipboard(_projectId);
            MessageBox.Show($"{count} lines copied to clipboard for SAP", "Copy for SAP",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSundryItems_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            using var form = new SundryItemsForm(_db);
            form.LoadProject(_projectId);
            form.ShowDialog(this);
            LoadSundryMaster();
            RefreshGrid();
        }

        private QuoteLine? CreateBlankLine()
        {
            int maxSlotIndex = _lines
                .Where(l => l.PageBlock > 0 && l.RowInBlock > 0)
                .Select(l => ((l.PageBlock - 1) * MaxRowsPerPage) + (l.RowInBlock - 1))
                .DefaultIfEmpty(-1)
                .Max();

            int nextIndex = maxSlotIndex + 1;
            if (nextIndex >= MaxQuoteLines)
                return null;

            return new QuoteLine
            {
                ProjectId = _projectId,
                PageBlock = (nextIndex / MaxRowsPerPage) + 1,
                RowInBlock = (nextIndex % MaxRowsPerPage) + 1,
                LineNumber = _lines.Select(l => l.LineNumber).DefaultIfEmpty(0).Max() + 1
            };
        }

        /// <summary>
        /// Keeps one empty entry row at the bottom of the grid at all times so new
        /// lines can be typed straight in without clicking "Add Row" for each one.
        /// </summary>
        private void EnsureTrailingBlankRow()
        {
            if (_projectId <= 0)
                return;
            if (_lines.Count > 0 && !IsMeaningfulLine(_lines[^1]))
                return;

            var blank = CreateBlankLine();
            if (blank != null)
                _lines.Add(blank);
        }

        //private void btnAddRow_Click(object sender, EventArgs e)
        //{
        //    if (_projectId <= 0) return;

        //    var line = CreateBlankLine();
        //    if (line == null)
        //    {
        //        MessageBox.Show("Maximum of 240 quote lines (6 pages) reached.", "Add Row",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    _lines.Add(line);

        //    BeginInvoke(new Action(() =>
        //    {
        //        if (gridQuote.Rows.Count == 0 || gridQuote.Columns.Count == 0) return;
        //        if (gridQuote.Rows.Count != _lines.Count) return;
        //        int rowIndex = gridQuote.Rows.Count - 1;
        //        try
        //        {
        //            DataGridViewColumn? firstVisibleColumn = null;
        //            foreach (DataGridViewColumn column in gridQuote.Columns)
        //            {
        //                if (column.Visible)
        //                {
        //                    firstVisibleColumn = column;
        //                    break;
        //                }
        //            }

        //            if (firstVisibleColumn != null)
        //            {
        //                gridQuote.CurrentCell = gridQuote.Rows[rowIndex].Cells[firstVisibleColumn.Index];
        //                gridQuote.FirstDisplayedScrollingRowIndex = rowIndex;
        //            }
        //        }
        //        catch (InvalidOperationException)
        //        {
        //            // Best-effort UX convenience only; ignore if the grid cannot
        //            // accept a current-cell/scroll change (e.g. mid-layout, no visible columns, etc.)
        //        }
        //    }));
        //    UpdatePageLayout();
        //}

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            if (gridQuote.CurrentRow?.DataBoundItem is not QuoteLine line) return;

            if (line.Id > 0)
                _db.DeleteQuoteLine(line.Id);

            _lines.Remove(line);
            UpdatePageLayout();
            UpdatePriceBreakdown();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            if (!_lines.Any(IsMeaningfulLine)) return;

            var answer = MessageBox.Show(
                "Delete ALL quote lines for this project?\n\nThis cannot be undone.",
                "Delete All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes)
                return;

            _db.DeleteQuoteLines(_projectId);
            RefreshGrid();
        }



        public void SaveQuote(bool showConfirmation)
        {
            if (_projectId <= 0)
            {
                if (showConfirmation)
                    MessageBox.Show("No project open.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gridQuote.IsCurrentCellDirty)
                gridQuote.CommitEdit(DataGridViewDataErrorContexts.Commit);
            gridQuote.EndEdit();

            // Only persist meaningful lines – the always-present blank entry row at the
            // bottom of the grid must not create empty database rows.
            var lines = _lines.Where(IsMeaningfulLine).ToList();
            var validationErrors = CollectValidationErrors(lines);
            if (validationErrors.Count > 0)
            {
                if (showConfirmation)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, validationErrors), "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silent auto-save (tab switch): persist only the valid lines.
                lines = lines.Where(l => string.IsNullOrWhiteSpace(ValidateLine(l))).ToList();
            }

            foreach (var line in lines)
            {
                line.ProjectId = _projectId;
                line.LineNumber = line.LineNumber <= 0 ? line.RowInBlock : line.LineNumber;
                ApplySectionLookup(line);
                RecalculatePrice(line);
                _db.UpsertQuoteLine(line);
            }

            UpdatePageLayout();
            if (showConfirmation)
            {
                MessageBox.Show("Quote saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
        }

        private void gridQuote_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridQuote.IsCurrentCellDirty)
                gridQuote.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridQuote_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingGrid || e.RowIndex < 0 || _projectId <= 0) return;
            if (gridQuote.Rows[e.RowIndex].DataBoundItem is not QuoteLine line) return;

            // Refresh metal price and IP rates in case the Edit Pricing tab was saved since this tab loaded.
            LoadProjectSettings();

            line.LineNumber = line.LineNumber <= 0 ? e.RowIndex + 1 : line.LineNumber;

            // Extra/Sundry Charge (ea) (SubsillSurcharge) is a manually entered field.
            // ApplySectionLookup re-derives it from the section's subsill tier data (or resets
            // it to 0 when there's no tier), which would immediately overwrite whatever the
            // user just typed. Skip the section lookup when this is the column being edited —
            // the Section itself hasn't changed, so Description/Alloy/Temper/IpGrade/SubsillFlag
            // don't need to be re-derived anyway.
            var editedColumnName = e.ColumnIndex >= 0 && e.ColumnIndex < gridQuote.Columns.Count
                ? gridQuote.Columns[e.ColumnIndex].DataPropertyName
                : null;
            if (!string.Equals(editedColumnName, nameof(QuoteLine.SubsillSurcharge), StringComparison.Ordinal))
            {
                // Always re-run the section lookup so Description/Alloy/Temper/IpGrade/SubsillFlag stay
                // in sync regardless of which column was just edited (Section, Qty, Length, IgnoreFinish, etc.).
                ApplySectionLookup(line);
            }
            RecalculatePrice(line);

            // Push computed values back into the grid cells, suppressing re-entrant events.
            _bindingGrid = true;
            var row = gridQuote.Rows[e.RowIndex];
            SetCellValue(row, "Description", line.Description);
            SetCellValue(row, "Alloy", line.Alloy);
            SetCellValue(row, "Temper", line.Temper);
            SetCellValue(row, "IpGrade", line.IpGrade);
            SetCellValue(row, "SapColourCode", line.SapColourCode);
            SetCellValue(row, "Price", line.Price);
            SetCellValue(row, "TotalPrice", line.TotalPrice);
            _bindingGrid = false;

            // Repaint so read-only cells (Alloy, Temper, $ Each, $ Total) show the
            // new values immediately without waiting for the next paint cycle.
            gridQuote.InvalidateRow(e.RowIndex);

            row.ErrorText = ValidateLine(line);
            UpdatePageLayout();
            UpdatePriceBreakdown();
            if (IsMeaningfulLine(line))
                _db.UpsertQuoteLine(line);
            EnsureTrailingBlankRow();
        }

        private void SetCellValue(DataGridViewRow row, string columnName, object? value)
        {
            if (gridQuote.Columns.Contains(columnName))
                row.Cells[columnName].Value = value;
        }

        private void LoadProjectSettings()
        {
            var pricing = _db.GetEditPricing(_projectId);
            _metalPricePerKg = pricing?.MetalPrice ?? 0m;
            _finishingSellPricePerM2 = pricing?.FinishingPrice ?? 0m;
            if (pricing != null)
            {
                // Use IP margin % fields from DB; fall back to workbook constants when zero/unset.
                _ip1RatePerKg = pricing.IP1UpchargePerKg; //> 0 ? pricing.IP1MarginPct : 1.00m;
                _ip2RatePerKg = pricing.IP2UpchargePerKg; //> 0 ? pricing.IP2MarginPct : 2.00m;
                _ip3RatePerKg = pricing.IP3PanoramicFixedKgRate; //> 0 ? pricing.IP3MarginPct : 25.85m;

                _millFinishMarginPct = pricing.MillFinishMarginPct;
                _finishingMarginPct = pricing.FinishingMarginPct;
                _finishingCostPricePerM2 = pricing.FinishingCostPricePerM2 > 0
                    ? pricing.FinishingCostPricePerM2
                    : pricing.FinishingPrice;
                _thresholdSellPricePerM2 = pricing.Price_B58;
                _thermalBreakCalcType = string.IsNullOrWhiteSpace(pricing.ThermalBreakCalcType)
                    ? "Margin" : pricing.ThermalBreakCalcType;
                _thermalBreakMargin = pricing.ThermalBreakMargin;
            }

            var project = _db.GetProject(_projectId);
            _projectFinishSapCode = string.IsNullOrWhiteSpace(project?.FinishSapCode) ? "NON" : project.FinishSapCode;
            _thresholdChoice = string.IsNullOrWhiteSpace(project?.ThresholdChoice) ? "No" : project.ThresholdChoice;
            _projectFinishType = string.IsNullOrWhiteSpace(project?.FinishType) ? MillFinishType : project.FinishType;

            // Pricing Matrix F4/F5 — PDB metal cost base and ALF cost per metre, pinned to the
            // project's PricingConstants snapshot (never silently updated by later reseeds).
            var constants = new FormulaCalculationService(_db).GetPricingConstantsForProject(project);
            _pdbMetalCostBase = constants.PdbMetalCostBase;
            _alfCostPerMetre = constants.AlfCostPerMetre;
        }

        private void UpdatePageLayout()
        {
            if (_projectId <= 0)
                return;

            int activeLineCount = _lines.Count(IsMeaningfulLine);
            int pageCount = Math.Min(MaxQuotePages, Math.Max(1, (int)Math.Ceiling(activeLineCount / (decimal)MaxRowsPerPage)));
            _pageSetup.SetPageLayout(_projectId, pageCount);
        }

        private void UpdateValidationStates()
        {
            for (int i = 0; i < gridQuote.Rows.Count && i < _lines.Count; i++)
                gridQuote.Rows[i].ErrorText = ValidateLine(_lines[i]);
        }

        private List<string> CollectValidationErrors(IEnumerable<QuoteLine> lines)
            => lines
                .Select((line, index) => (Message: ValidateLine(line), Index: index))
                .Where(x => !string.IsNullOrWhiteSpace(x.Message))
                .Select(x => $"Row {x.Index + 1}: {x.Message}")
                .ToList();

        private string ValidateLine(QuoteLine line)
        {
            if (!IsMeaningfulLine(line))
                return string.Empty;

            if (line.Quantity <= 0)
                return "Quantity must be greater than 0.";
            if (line.LengthM <= 0 && !HasManualPriceWithoutLength(line))
                return "Length must be greater than 0.";
            if (line.ManualPriceOverride.HasValue && line.ManualPriceOverride.Value <= 0)
                return "Price override must be greater than 0.";
            return string.Empty;
        }

        private static bool IsMeaningfulLine(QuoteLine line)
            => !string.IsNullOrWhiteSpace(line.ExtrusionCode)
               || !string.IsNullOrWhiteSpace(line.Description)
               || line.Quantity > 0
               || line.LengthM > 0
               || line.ManualPriceOverride.HasValue;

        private static bool HasManualPriceWithoutLength(QuoteLine line)
            => line.ManualPriceOverride.HasValue
               && line.ManualPriceOverride.Value > 0
               && !string.IsNullOrWhiteSpace(line.ExtrusionCode)
               && line.ExtrusionCode.StartsWith("SUNDRY", StringComparison.OrdinalIgnoreCase);

        private static string NormalizeSectionCode(string? sectionCode)
            => string.IsNullOrWhiteSpace(sectionCode) ? string.Empty : sectionCode.Trim();

        private void gridQuote_SelectionChanged(object sender, EventArgs e) => UpdatePriceBreakdown();

        private void UpdatePriceBreakdown()
        {
            if (gridQuote.CurrentRow?.DataBoundItem is not QuoteLine line)
            {
                lblPriceBreakdown.Text = "Select a quote line to review the calculation.";
                return;
            }

            decimal kgEach = 0m;
            kgEach = FormulaCalculationService.CalculateKgEach(GetMassPerM(line.ExtrusionCode), line.LengthM);

            var millFinishEach = CalculateMillFinishEach(line);
            var ipEach = CalculateIpEach(line);
            var sfEach = CalculateFinishingEach(line);
            var overrideText = line.ManualPriceOverride.HasValue
                ? $"{line.ManualPriceOverride.Value.ToString("0.0000", CultureInfo.InvariantCulture)} (manual override)"
                : "—";

            lblPriceBreakdown.Text =
                $"Section: {line.ExtrusionCode}{Environment.NewLine}" +
                $"Kg each: {kgEach:0.####}{Environment.NewLine}" +
                $"Mill finish each: {millFinishEach:0.0000}{Environment.NewLine}" +
                $"IP each: {ipEach:0.0000}{Environment.NewLine}" +
                $"SF each: {sfEach:0.0000}{Environment.NewLine}" +
                $"Extra/Sundry Charge (ea): {line.SubsillSurcharge:0.0000}{Environment.NewLine}" +
                $"Override: {overrideText}{Environment.NewLine}" +
                $"Final $ each: {line.Price:0.0000}{Environment.NewLine}" +
                $"Line total: {line.TotalPrice:0.0000}{Environment.NewLine}" +
                $"SAP code: {line.SapColourCode}{Environment.NewLine}" +
                $"{Environment.NewLine}Margins{Environment.NewLine}" +
                $"Mill finish margin: {_millFinishMarginPct:0.##}%{Environment.NewLine}" +
                $"Finishing margin: {_finishingMarginPct:0.##}%{Environment.NewLine}" +
                $"Finishing cost: {_finishingCostPricePerM2:0.00}/m² vs sell {_finishingSellPricePerM2:0.00}/m²";
        }

        private static string NormalizeThresholdChoice(string thresholdChoice)
            => string.Equals(thresholdChoice, "Project Colour", StringComparison.OrdinalIgnoreCase) ? "No" : thresholdChoice;

        private void gridQuote_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _lines.Count || e.ColumnIndex < 0)
                return;
            var column = gridQuote.Columns[e.ColumnIndex];
            if (column == null)
                return;
            if (gridQuote.Rows[e.RowIndex].DataBoundItem is not QuoteLine line)
                return;

            if (column.DataPropertyName == nameof(QuoteLine.Price))
            {
                var mfEach = CalculateMillFinishEach(line);
                var ipEach = CalculateIpEach(line);
                var sfEach = CalculateFinishingEach(line);
                var sundryEach = line.SubsillSurcharge;
                e.ToolTipText =
                    $"$ Each = MF + IP + SF + Extra/Sundry Charge (ea){Environment.NewLine}" +
                    $"= {mfEach:0.0000} + {ipEach:0.0000} + {sfEach:0.0000} + {sundryEach:0.0000}{Environment.NewLine}" +
                    $"= {line.Price:0.0000}";
            }
            else if (column.DataPropertyName == nameof(QuoteLine.TotalPrice))
            {
                e.ToolTipText =
                    $"$ Total = $ Each × Qty{Environment.NewLine}" +
                    $"= {line.Price:0.0000} × {line.Quantity:0.####}{Environment.NewLine}" +
                    $"= {line.TotalPrice:0.0000}";
            }
        }

        private void btnImportQuoteLines_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.");
                return;
            }

            // Ask whether this is a CRE8 Extrusion PO or a regular BOM / quote lines file
            //var isCre8 = MessageBox.Show(
            //    "Is this a CRE8 Extrusion PO file?\n\nClick 'Yes' for CRE8 PO, or 'No' for regular quote lines / BOM.",
            //    "Import Type",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Question) == DialogResult.Yes;

            //string fileFilter = isCre8
            //    ? "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            //    : "BOM files (*.csv;*.xlsx;*.xlsm)|*.csv;*.xlsx;*.xlsm|CSV Files (*.csv)|*.csv|Excel Files (*.xlsx;*.xlsm)|*.xlsx;*.xlsm";

            string fileFilter = "BOM files (*.csv;*.xlsx;*.xlsm)|*.csv;*.xlsx;*.xlsm|CSV Files (*.csv)|*.csv|Excel Files (*.xlsx;*.xlsm)|*.xlsx;*.xlsm";


            using var ofd = new OpenFileDialog
            {
                Filter = fileFilter,
                //Title = isCre8 ? "Import CRE8 Extrusion PO" : "Import quote lines"
                Title = "Import quote lines"
            };

            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                int imported;
                //if (isCre8)
                //{
                //    // Import project-level details (customer, finish, etc.) from the CRE8 PO
                //    _cre8Import.ImportProjectDetails(_projectId, ofd.FileName);
                //    // Also treat the same file as a BOM to import the line items
                //    imported = _bom.ImportBomFromCsv(_projectId, ofd.FileName);
                //}
                //else
                //{
                imported = ofd.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)
                      ? _bom.ImportBomFromCsv(_projectId, ofd.FileName)
                      : _bom.ImportBomFromWorkbook(_projectId, ofd.FileName);
                //}

                int distributed = _bom.DistributeBomToQuote(_projectId);

                // RefreshGrid calls LoadProjectSettings internally, picking up any CRE8-imported finish/SAP values
                RefreshGrid();

                // ── Post-import dialog 1: Finish Summary ─────────────────────────────
                var uniqueFinishes = _bom.GetUniqueFinishes(_projectId);
                var mostCommonFinish = _bom.GetMostCommonFinish(_projectId);

                using (var summaryDlg = new ImportFinishSummaryDialog(mostCommonFinish, uniqueFinishes))
                {
                    summaryDlg.ShowDialog(this);

                    // ── Post-import dialog 2 (conditional): Set Finish ────────────────
                    if (summaryDlg.EditFinishColours)
                    {
                        var project = _db.GetProject(_projectId);
                        if (project != null)
                        {
                            var colourGroups = _db.GetColourGroups().ToList();
                            using var setFinishDlg = new ImportSetFinishDialog(
                                colourGroups,
                                project.FinishType,
                                project.FinishApplicator,
                                project.FinishGroup,
                                project.FinishName,
                                project.FinishSapCode);

                            if (setFinishDlg.ShowDialog(this) == DialogResult.OK)
                            {
                                project.FinishType = setFinishDlg.FinishType;
                                project.FinishApplicator = setFinishDlg.FinishApplicator;
                                project.FinishGroup = setFinishDlg.FinishGroup;
                                project.FinishName = setFinishDlg.FinishName;
                                project.FinishSapCode = setFinishDlg.FinishSapCode;
                                _db.UpdateProject(project);
                                // RefreshGrid calls LoadProjectSettings internally so the updated finish SAP code is active
                                RefreshGrid();
                            }
                        }
                    }
                }

                // ── Post-import dialog 3: Pricing ────────────────────────────────────
                using (var pricingDlg = new ImportPricingDialog())
                {
                    if (pricingDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        if (pricingDlg.Choice == ImportPricingChoice.UseImportedPrices)
                        {
                            int pricesUpdated = _importPrices.ImportPricesToQuote(_projectId);
                            RefreshGrid();
                            MessageBox.Show(
                                $"Applied {pricesUpdated} imported price(s) to the quote.\n" +
                                "Use 'Reset Price Formulas' to return to calculated pricing.",
                                "Import Prices", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (pricingDlg.Choice == ImportPricingChoice.Yes)
                        {
                            MessageBox.Show(
                                "Switch to the Edit Pricing tab to review and adjust pricing.",
                                "Edit Pricing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                MessageBox.Show(
                    $"Imported {imported} row(s) and distributed {distributed} to the quote.",
                    "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Import failed:\n{ex.Message}", "Import Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.");
                return;
            }

            var project = _db.GetProject(_projectId);
            var logoLabel = project == null ? string.Empty : QuotePdfService.GetLocalLogoLabel(project);
            using var options = new QuotePdfOptionsDialog(hasLocalLogo: !string.IsNullOrWhiteSpace(logoLabel));
            if (options.ShowDialog(this) != DialogResult.OK)
                return;

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"Quote_{DateTime.Today:yyyyMMdd}.pdf"
            };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            var millFinishLines = options.AddMillFinishVersion ? BuildMillFinishLines() : null;
            _quotePdf.ExportQuotePdf(_projectId, sfd.FileName, options.IncludeQuoteLetter, options.Unpriced,
                options.IncludeLocalLogo, millFinishLines);
            MessageBox.Show($"PDF saved to:\n{sfd.FileName}", "Export PDF",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Builds a Mill Finish alternative of the current quote lines: same sections,
        /// quantities and lengths but with the finishing (SF) component removed from
        /// the price and the SAP colour code set to NON.
        /// </summary>
        private List<QuoteLine> BuildMillFinishLines()
        {
            var result = new List<QuoteLine>();
            foreach (var line in _lines.Where(IsMeaningfulLine))
            {
                var millFinishEach = CalculateMillFinishEach(line);
                var ipEach = CalculateIpEach(line);
                var price = line.ManualPriceOverride
                            ?? FormulaCalculationService.CalculatePriceEach(
                                millFinishEach,
                                ipEach: ipEach,
                                sfSellEach: 0m,
                                slottingSundryEach: line.SubsillSurcharge);
                result.Add(new QuoteLine
                {
                    ProjectId = line.ProjectId,
                    PageBlock = line.PageBlock,
                    RowInBlock = line.RowInBlock,
                    LineNumber = line.LineNumber,
                    ExtrusionCode = line.ExtrusionCode,
                    Description = line.Description,
                    Alloy = line.Alloy,
                    Temper = line.Temper,
                    Quantity = line.Quantity,
                    LengthM = line.LengthM,
                    Price = price,
                    TotalPrice = FormulaCalculationService.CalculateSellTotal(price, line.Quantity),
                    SapColourCode = "NON",
                    IgnoreFinish = "Yes"
                });
            }
            return result;
        }

        private void gridQuote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardIntoGrid();
                e.Handled = true;
            }
        }

        private void PasteClipboardIntoGrid()
        {
            if (_projectId <= 0 || gridQuote.CurrentCell == null)
                return;

            var text = Clipboard.GetText();
            if (string.IsNullOrWhiteSpace(text))
                return;

            var editableColumns = gridQuote.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible && !c.ReadOnly)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            if (editableColumns.Count == 0)
                return;

            int startColumnIndex = editableColumns.FindIndex(c => c.Index == gridQuote.CurrentCell.ColumnIndex);
            if (startColumnIndex < 0)
                startColumnIndex = 0;

            var rows = text.Replace("\r\n", "\n").TrimEnd('\n').Split('\n');
            int rowIndex = gridQuote.CurrentCell.RowIndex;

            foreach (var rowText in rows)
            {
                //if (rowIndex >= _lines.Count)
                //    btnAddRow_Click(this, EventArgs.Empty);

                if (rowIndex >= _lines.Count)
                    break;

                var cells = rowText.Split('\t');
                for (int i = 0; i < cells.Length && startColumnIndex + i < editableColumns.Count; i++)
                {
                    var column = editableColumns[startColumnIndex + i];
                    gridQuote.Rows[rowIndex].Cells[column.Index].Value = ConvertClipboardValue(column.DataPropertyName, cells[i]);
                }

                rowIndex++;
            }

            gridQuote.EndEdit();
        }

        /// <summary>
        /// Converts pasted clipboard values into the grid's expected typed values.
        /// Numeric fields accept both invariant-culture and current-culture decimal input so
        /// pasted Excel data works for users with different decimal separators.
        /// </summary>
        private static object ConvertClipboardValue(string propertyName, string value)
        {
            value = value.Trim();
            if (propertyName is nameof(QuoteLine.Quantity) or nameof(QuoteLine.LengthM) or nameof(QuoteLine.ManualPriceOverride) or nameof(QuoteLine.SubsillSurcharge))
            {
                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var numeric) ||
                    decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out numeric))
                {
                    return numeric;
                }
                return 0m;
            }

            if (propertyName == nameof(QuoteLine.SubsillFlag))
                return value.Equals("Y", StringComparison.OrdinalIgnoreCase) || value.Equals("Yes", StringComparison.OrdinalIgnoreCase) || value == "1";

            return value;
        }

       
    }
}
