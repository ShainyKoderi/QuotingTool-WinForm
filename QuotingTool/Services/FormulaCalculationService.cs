using System;
using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Translates key Excel formulas from "Project Pricing_Quotation Tool.xlsm" into C# calculation methods.
    ///
    /// Sheet protection password (for reference): "party"
    /// VeryHidden sheets in original VBA: Colour_Groups, DATA_EDGE
    ///
    /// Formula sources:
    ///   2.Quote row 12 formulas (extracted via openpyxl)
    ///   3.Edit Pricing B33-B65 formulas
    ///   Pricing Matrix lookup tables
    /// </summary>
    public class FormulaCalculationService
    {
        private readonly DatabaseContext _db;

        public FormulaCalculationService(DatabaseContext db)
        {
            _db = db;
        }

        // ── 2.Quote column formulas ──────────────────────────────────────────

        /// <summary>
        /// Col J: $ Total = I * G
        /// Original Excel: =IFERROR(I12*G12,"")
        /// </summary>
        public static decimal CalculateSellTotal(decimal priceEach, decimal quantity)
            => priceEach * quantity;

        /// <summary>
        /// Col I: $ Each = MF_Each + IP_Each + SF_Sell_Each + SlottingSundry_Each
        /// Original Excel: =IFERROR(AH12+AI12+AS12+K12,"")
        /// Where AH=MF Each, AI=IP Each, AS=SF Sell Each, K=Slotting/Sundry Charge(ea)
        /// </summary>
        public static decimal CalculatePriceEach(
            decimal mfEach, decimal ipEach, decimal sfSellEach, decimal slottingSundryEach)
            => mfEach + ipEach + sfSellEach + slottingSundryEach;

        /// <summary>
        /// Col K: Slotting/Sundry Charge each = SlotSellEa + SundrySellEa
        /// Original Excel: =IFERROR(AZ12+BC12,"")
        /// Where AZ=Slot Sell ea, BC=Sundry Sell (ea)
        /// </summary>
        public static decimal CalculateSlottingSundryEach(decimal slotSellEach, decimal sundrySellEach)
            => slotSellEach + sundrySellEach;

        /// <summary>
        /// Col M: SAP Colour Code
        /// Original Excel: =IF(C12="","", IF(AND(BF12>0, NOT(ThresholdChoice="No")),
        ///   VLOOKUP(ThresholdChoice, Threshold_Code, 2, FALSE),
        ///   IF(L12="No", SAP_Colour_Code, "NON")))
        /// Where BF12 = 1 if section is in THRESHOLDS list, ThresholdChoice from 1.Project Details E33
        /// </summary>
        public static string CalculateSapColourCode(
            string extrusionCode,
            bool isThreshold,          // BF col: section found in THRESHOLDS named range
            string thresholdChoice,    // 1.Project Details E33 (e.g. "20um Clear", "No")
            string ignoreFinish,       // col L: "Yes" or "No"
            string projectSapColourCode) // named range SAP_Colour_Code = 1.Project Details!E32
        {
            if (string.IsNullOrWhiteSpace(extrusionCode)) return string.Empty;

            if (isThreshold && !string.Equals(thresholdChoice, "No", StringComparison.OrdinalIgnoreCase))
            {
                // Return the threshold SAP code (e.g. "3A" for 20um Clear, "4A" for 25um Clear)
                return LookupThresholdCode(thresholdChoice);
            }
            return string.Equals(ignoreFinish, "No", StringComparison.OrdinalIgnoreCase)
                ? projectSapColourCode
                : "NON";
        }

        /// <summary>
        /// Threshold_Code named range lookup: Validation!$E$9:$F$11
        /// Maps threshold option to SAP code.
        /// Data from .xlsm Validation sheet row 10-11:
        ///   "Anodised Thresholds" -> (SAP code from row, depends on choice)
        ///   "20um Clear" -> "3A"   (Validation!E10, F10)
        ///   "25um Clear" -> "4A"   (Validation!E11, F11)
        /// </summary>
        private static readonly Dictionary<string, string> ThresholdCodeLookup = new(StringComparer.OrdinalIgnoreCase)
        {
            { "20um Clear", "3A" },
            { "25um Clear", "4A" },
        };

        public static string LookupThresholdCode(string thresholdChoice)
            => ThresholdCodeLookup.TryGetValue(thresholdChoice, out var code) ? code : string.Empty;

        /// <summary>
        /// Col Z: kg each = kg/m * Length(m)
        /// Original Excel: =IFERROR(Y12*H12,"")
        /// </summary>
        public static decimal CalculateKgEach(decimal kgPerMetre, decimal lengthM)
            => kgPerMetre > 0 && lengthM > 0 ? kgPerMetre * lengthM : 0m;

        /// <summary>
        /// Col AA: total kg = kg each * Qty
        /// Original Excel: =IFERROR(Z12*G12,"")
        /// </summary>
        public static decimal CalculateTotalKg(decimal kgEach, decimal quantity)
            => kgEach * quantity;

        /// <summary>
        /// Col AJ: MF Total = (MF Each * Qty) + (IP Each * Qty)
        /// Original Excel: =IFERROR((AH12*G12)+(AI12*G12),"")
        /// </summary>
        public static decimal CalculateMfTotal(decimal mfEach, decimal ipEach, decimal quantity)
            => (mfEach + ipEach) * quantity;

        /// <summary>
        /// Col AK: Extra/Sundry Total = Slotting/Sundry Charge each * Qty
        /// Original Excel: =IFERROR(K12*G12,"")
        /// </summary>
        public static decimal CalculateExtraSundryTotal(decimal slottingSundryEach, decimal quantity)
            => slottingSundryEach * quantity;

        /// <summary>
        /// Col AY: Slot Cost Total (est) = IF(V>0, VLOOKUP(V, SubSill_Tier_Price, 3) * H * G, 0)
        /// Original Excel: =IFERROR(IF(V12>0, VLOOKUP(V12,SubSill_Tier_Price,3,FALSE)*H12*G12,0),"")
        /// SubSill_Tier_Price col 3 = CostPerMetre (Pricing Matrix C65:C68)
        /// </summary>
        public decimal CalculateSlotCostTotal(
            decimal subsillTier, decimal lengthM, decimal quantity,
            IEnumerable<SubsillTierPrice> tierPrices)
        {
            if (subsillTier <= 0) return 0m;
            var tier = tierPrices.FirstOrDefault(t => t.Tier == (int)subsillTier);
            return tier != null ? tier.CostPerMetre * lengthM * quantity : 0m;
        }

        /// <summary>
        /// Col AZ: Slot Sell ea = IF(V>0, VLOOKUP(V, SubSill_Tier_Price, 2) * H, 0)
        /// Original Excel: =IFERROR(IF(V12>0, VLOOKUP(V12,SubSill_Tier_Price,2,FALSE)*H12,0),"")
        /// SubSill_Tier_Price col 2 = SellPricePerMetre (Pricing Matrix B65:B68)
        /// Tier 4 (Panoramic door): price + price*1.5 (150% Market Factor per notes in Pricing Matrix C68)
        /// </summary>
        public decimal CalculateSlotSellEach(
            decimal subsillTier, decimal lengthM,
            IEnumerable<SubsillTierPrice> tierPrices)
        {
            if (subsillTier <= 0) return 0m;
            var tier = tierPrices.FirstOrDefault(t => t.Tier == (int)subsillTier);
            if (tier == null) return 0m;
            var basePrice = tier.SellPricePerMetre * lengthM;
            // Tier 4 = Panoramic door: receives 150% Market Factor (price + price*1.5)
            return (int)subsillTier == 4 ? basePrice + basePrice * 1.5m : basePrice;
        }

        // ── 3.Edit Pricing formulas ──────────────────────────────────────────

        /// <summary>
        /// B34: Mill Finish Margin % = (B33 - MetalCost) / B33
        /// Original Excel: =IFERROR((B33 - 'Pricing Matrix'!$B$5) / B33, 0)
        /// Where Pricing Matrix B5 = MetalCost (LME + billet premium + spread)
        /// </summary>
        public static decimal CalculateMillFinishMarginPct(decimal sellPricePerKg, decimal metalCostPerKg)
            => sellPricePerKg > 0 ? (sellPricePerKg - metalCostPerKg) / sellPricePerKg : 0m;

        /// <summary>
        /// B45: Finishing Margin % = (SellPrice - CostPrice) / SellPrice
        /// Original Excel: =IFERROR(IF(C25=0,(B44-B43)/B44,(B44-B42)/B44),0)
        /// When CostType=0 (standard), use FinishingCostPrice; otherwise use POA cost
        /// </summary>
        public static decimal CalculateFinishingMarginPct(
            decimal sellPrice, decimal costPrice, decimal poaCost, int costType)
        {
            if (sellPrice <= 0) return 0m;
            return costType == 0
                ? (sellPrice - costPrice) / sellPrice
                : (sellPrice - poaCost) / sellPrice;
        }

        /// <summary>
        /// B51: Mill BULK Finishing Margin %
        /// Original Excel: =IFERROR(IF($C$25=4,(B50-B49)/B50, IF($D$25=1,(B50-B49)/B50,(B50-B48)/B50)),0)
        /// When CostType=4 (Bremer Mill Std) or Override=true, use B49 cost; else use B48 POA cost
        /// </summary>
        public static decimal CalculateBulkFinishingMarginPct(
            decimal bulkSellPrice, decimal bulkCostPrice, decimal bulkPoaCost,
            int costType, bool costTypeOverride)
        {
            if (bulkSellPrice <= 0) return 0m;
            return (costType == 4 || costTypeOverride)
                ? (bulkSellPrice - bulkCostPrice) / bulkSellPrice
                : (bulkSellPrice - bulkPoaCost) / bulkSellPrice;
        }

        /// <summary>
        /// B59: Anodised Threshold Margin %
        /// Original Excel: =(B58-B57)/B58
        /// </summary>
        public static decimal CalculateThresholdMarginPct(decimal thresholdSellPrice, decimal thresholdCostPrice)
            => thresholdSellPrice > 0 ? (thresholdSellPrice - thresholdCostPrice) / thresholdSellPrice : 0m;

        // ── Pricing Matrix calculations ──────────────────────────────────────

        /// <summary>
        /// Metal cost per kg = LME rate + Billet Premium + Internal Spread
        /// From Pricing Matrix:
        ///   B3 = LME + billet premium (e.g. 5.61)
        ///   B4 = Internal Spread (e.g. 3.12)
        ///   B5 = Cost total (B3 + B4, e.g. 8.73)
        /// Original: Pricing Matrix!$B$5
        /// </summary>
        public static decimal CalculateMetalCostPerKg(decimal lmeRate, decimal billetPremium, decimal internalSpread)
            => lmeRate + billetPremium + internalSpread;

        /// <summary>
        /// PDB metal cost base = Metal Cost (B5) + Extra Spread (F3).
        /// Original Excel: Pricing Matrix!$F$4
        /// Used by the PDB path of the MF Each (col AH) calculation.
        /// </summary>
        public static decimal CalculatePdbMetalCostBase(decimal metalCostPerKg, decimal extraSpread)
            => metalCostPerKg + extraSpread;

        /// <summary>
        /// Looks up the current global Pricing Matrix constants (B3-B5, F3-F5, H3, K5)
        /// from the <see cref="PricingConstants"/> table, falling back to the workbook's
        /// June 2026 rates if no row has been seeded yet.
        /// </summary>
        public PricingConstants GetPricingConstants()
            => _db.GetLatestPricingConstants() ?? new PricingConstants
            {
                LmeBilletPremium = 5.61m,
                InternalSpread = 3.12m,
                MetalCostPerKg = 8.73m,
                ExtraSpread = 0.35m,
                PdbMetalCostBase = 9.08m,
                AlfCostPerMetre = 6.46m,
                StandardDeliveryCharge = 97m,
                ApToPpFactor = 1.9m
            };

        /// <summary>
        /// Metal sell price lookup by total kg via Table_Metal_BP (Pricing Matrix rows 7-13).
        /// Returns the typical sell price for the given breakpoint.
        /// Original Excel in 3.Edit Pricing: =IFERROR(VLOOKUP(B27, Table_Metal_BP[], 3), "")
        /// Where col 3 = TypicalPrice in MetalBreakpoints table.
        /// </summary>
        public decimal LookupMetalTypicalPrice(decimal totalKg)
        {
            var breakpoints = _db.GetMetalBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
            return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.TypicalPrice);
        }

        public decimal LookupMetalMinPrice(decimal totalKg)
        {
            var breakpoints = _db.GetMetalBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
            return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.MinPrice);
        }

        public decimal LookupMetalMaxPrice(decimal totalKg)
        {
            var breakpoints = _db.GetMetalBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
            return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.MaxPrice);
        }

        /// <summary>
        /// Metal Typical/Min/Max sell $/kg computed live from the current metal cost,
        /// exactly as the Pricing Matrix does (rows 8-13):
        ///   C8 (Typical) = $B$5 + Spread
        ///   D8 (Min)     = C8 − Min Factor
        ///   E8 (Max)     = C8 + Max Factor
        /// This keeps the guidance consistent with the current LME/spread rates rather
        /// than the static prices stored when the breakpoint table was seeded.
        /// Returns (0,0,0) when no breakpoint data exists.
        /// </summary>
        public (decimal Typical, decimal Min, decimal Max) LookupMetalGuidance(
            decimal totalKg, decimal metalCostPerKg)
        {
            var breakpoints = _db.GetMetalBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
            MetalBreakpoint? matched = null;
            foreach (var bp in breakpoints)
            {
                if (bp.BreakpointKg <= totalKg) matched = bp;
                else break;
            }
            if (matched == null) return (0m, 0m, 0m);

            var typical = metalCostPerKg + matched.Spread;
            return (typical, typical - matched.MinFactor, typical + matched.MaxFactor);
        }

        /// <summary>
        /// IP1/IP2 upcharge margin % (3.Edit Pricing G34/H34):
        ///   =IFERROR((Upcharge + SellPrice − 'Pricing Matrix'!$B$5) / (Upcharge + SellPrice), 0)
        /// The upcharge is added on top of the mill finish sell price, so the margin
        /// is computed on the combined $/kg.
        /// </summary>
        public static decimal CalculateIpUpchargeMarginPct(
            decimal upchargePerKg, decimal sellPricePerKg, decimal metalCostPerKg)
        {
            var combined = upchargePerKg + sellPricePerKg;
            return combined > 0m ? (combined - metalCostPerKg) / combined : 0m;
        }

        /// <summary>
        /// IP3 (Panoramic fixed rate) margin % (3.Edit Pricing I34):
        ///   =IFERROR((I33 − 'Pricing Matrix'!$B$5) / I33, 0)
        /// The IP3 rate is a standalone $/kg sell rate (not an upcharge).
        /// </summary>
        public static decimal CalculateIp3MarginPct(decimal ip3RatePerKg, decimal metalCostPerKg)
            => ip3RatePerKg > 0m ? (ip3RatePerKg - metalCostPerKg) / ip3RatePerKg : 0m;

        /// <summary>
        /// Finishing sell price guidance $/m² (Pricing Matrix G44/H44/I44):
        ///   = Cost / (1 − BreakpointMargin)
        /// Returns 0 when the cost is unavailable (0) or POA (99) — Excel shows blank.
        /// </summary>
        public static decimal CalculateFinishSellGuidance(decimal costPerM2, decimal breakpointMargin)
        {
            if (costPerM2 <= 0m || costPerM2 == PricePOASentinel || breakpointMargin >= 1m)
                return 0m;
            return costPerM2 / (1m - breakpointMargin);
        }

        ///// <summary>
        ///// Thermal Break margin lookup by total kg via Table_TB_BP (Pricing Matrix rows 15-21).
        ///// Original Excel: =IFERROR(VLOOKUP($B$27, Table_TB_BP[], IF(tb_calc_type="Margin",2,5)), "")
        ///// Where col 2 = MarginTyp, col 5 = ListTyp
        ///// </summary>
        //public decimal LookupThermalBreakTypicalMargin(decimal totalKg)
        //{
        //    var breakpoints = _db.GetThermalBreakBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
        //    return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.MarginTyp);
        //}

        //public decimal LookupThermalBreakMinMargin(decimal totalKg)
        //{
        //    var breakpoints = _db.GetThermalBreakBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
        //    return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.MarginMin);
        //}

        //public decimal LookupThermalBreakMaxMargin(decimal totalKg)
        //{
        //    var breakpoints = _db.GetThermalBreakBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
        //    return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.MarginMax);
        //}

        //public decimal LookupThermalBreakTypicalListPrice(decimal totalKg)
        //{
        //    var breakpoints = _db.GetThermalBreakBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
        //    return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.ListTyp);
        //}

        //public decimal LookupThermalBreakMinListPrice(decimal totalKg)
        //{
        //    var breakpoints = _db.GetThermalBreakBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
        //    return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.ListMin);
        //}

        //public decimal LookupThermalBreakMaxListPrice(decimal totalKg)
        //{
        //    var breakpoints = _db.GetThermalBreakBreakpoints().OrderBy(b => b.BreakpointKg).ToList();
        //    return VlookupLessOrEqual(totalKg, breakpoints, b => b.BreakpointKg, b => b.ListMax);
        //}

        ///// <summary>
        ///// Looks up Thermal Break Typical/Min/Max guidance for the given calc type.
        ///// When tb_calc_type="Margin", returns MarginTyp/Min/Max.
        ///// When tb_calc_type="List less Discount", returns ListTyp/Min/Max.
        ///// </summary>
        //public (decimal Typical, decimal Min, decimal Max) LookupThermalBreakGuidance(
        //    decimal totalKg, string tbCalcType)
        //{
        //    bool isMargin = string.Equals(tbCalcType, "Margin", StringComparison.OrdinalIgnoreCase);
        //    return isMargin
        //        ? (LookupThermalBreakTypicalMargin(totalKg),
        //           LookupThermalBreakMinMargin(totalKg),
        //           LookupThermalBreakMaxMargin(totalKg))
        //        : (LookupThermalBreakTypicalListPrice(totalKg),
        //           LookupThermalBreakMinListPrice(totalKg),
        //           LookupThermalBreakMaxListPrice(totalKg));
        //}

        /// <summary>
        /// Looks up Thermal Break Typical/Min/Max guidance for the given calc type.
        /// Mirrors the Excel approximate-match VLOOKUP on Table_TB_BP (Pricing Matrix rows 15-21):
        ///   C37 =IFERROR(VLOOKUP($B$27,Table_TB_BP,IF(tb_calc_type="Margin",2,5)),"")
        ///   D37 =IFERROR(VLOOKUP($B$27,Table_TB_BP,IF(tb_calc_type="Margin",3,6)),"")
        ///   E37 =IFERROR(VLOOKUP($B$27,Table_TB_BP,IF(tb_calc_type="Margin",4,7)),"")
        /// Approximate match: largest MinKg ≤ totalKg (same as Excel omitting FALSE).
        /// When tb_calc_type="Margin" → columns 2/3/4 (MarginTyp/Min/Max).
        /// Otherwise              → columns 5/6/7 (ListTyp/Min/Max).
        /// </summary>
        public (decimal Typical, decimal Min, decimal Max) LookupThermalBreakGuidance(
            decimal totalKg, string tbCalcType)
        {
            var breakpoints = _db.GetThermalBreakBreakpoints()
                .OrderBy(b => b.BreakpointKg)
                .ToList();

            if (breakpoints.Count == 0)
                return (0m, 0m, 0m);

            // Approximate match: find the largest MinKg that is ≤ totalKg.
            var row = breakpoints.LastOrDefault(b => b.BreakpointKg <= totalKg)
                      ?? breakpoints.First();

            bool isMargin = string.Equals(tbCalcType, "Margin", StringComparison.OrdinalIgnoreCase);

            return isMargin
                ? (row.MarginTyp, row.MarginMin, row.MarginMax)
                : (row.ListTyp, row.ListMin, row.ListMax);
        }



        /// <summary>
        /// Sentinel value used throughout the Pricing Matrix to represent
        /// "Price On Application" (POA).  Any rate stored as 99 in the DB
        /// means the price must be obtained manually from the supplier.
        /// (Mirrors the Excel convention in PowderCostTable.GroupPOARate = 99.)
        /// </summary>
        public const decimal PricePOASentinel = 99m;

        /// <summary>
        /// Powder cost lookup from PowderCostTable by vendor and colour group.
        /// Maps colour group name (e.g. "GROUP 1") to the appropriate rate column.
        /// Returns <see cref="PricePOASentinel"/> (99) when the vendor is not found
        /// or the colour group requires a manual POA price.
        /// </summary>
        public decimal LookupPowderCostByVendorAndGroup(string vendor, string colourGroup)
        {
            var powderTable = _db.GetPowderCostTable().ToList();
            var row = powderTable.FirstOrDefault(r =>
                string.Equals(r.Vendor, vendor, StringComparison.OrdinalIgnoreCase));
            if (row == null) return PricePOASentinel;

            return colourGroup?.ToUpperInvariant() switch
            {
                "GROUP 1"     => row.Group1Rate,
                "GROUP 2"     => row.Group2Rate,
                "GROUP 3"     => row.Group3Rate,
                "GROUP 4"     => row.Group4Rate,
                "GROUP 5"     => row.Group5Rate,
                "20UM CLEAR"  => row.Anodised20umRate,
                "25UM CLEAR"  => row.Anodised25umRate,
                "GROUP POA"   => row.GroupPOARate,
                _             => PricePOASentinel   // unknown group → POA
            };
        }

        /// <summary>
        /// Finish breakpoint range lookup.
        /// Determines which breakpoint range category ('a','b','c','d') applies
        /// given total m2 and AP/PP type, then returns the typical margin from FinishBreakpoints.
        /// This maps to the Pricing Matrix rows 52-60 (FINISH BREAKPOINTS section).
        /// Range selection:
        ///   'a' = Bremer Mill (CostType 4/5)
        ///   'b' = POA Colour  (CostType 1)
        ///   'c' = Custom Supplier (CostType 2)
        ///   'd' = Standard / Capral (CostType 0)  ← default for new quotes
        /// </summary>
        public (decimal Typical, decimal Min, decimal Max) LookupFinishBreakpointMargin(
            decimal totalM2, bool isAP, char rangeCategory = 'd')
        {
            var breakpoints = _db.GetFinishBreakpoints()
                .OrderBy(b => isAP ? b.BreakpointAP : b.BreakpointPP)
                .ToList();

            if (!breakpoints.Any()) return (0m, 0m, 0m);

            FinishBreakpoint? selected = null;
            foreach (var bp in breakpoints)
            {
                var threshold = isAP ? bp.BreakpointAP : bp.BreakpointPP;
                if (totalM2 >= threshold) selected = bp;
                else break;
            }
            selected ??= breakpoints.First();

            return (rangeCategory | 0x20) switch          // normalise to lower-case
            {
                'b' => (selected.RangeB_Typical, selected.RangeB_Min, selected.RangeB_Max),
                'c' => (selected.RangeC_Typical, selected.RangeC_Min, selected.RangeC_Max),
                'd' => (selected.RangeD_Typical, selected.RangeD_Min, selected.RangeD_Max),
                _   => (selected.RangeA_Typical, selected.RangeA_Min, selected.RangeA_Max),
            };
        }

        // ── Utility: VLOOKUP equivalent (match type = TRUE / less-or-equal) ──

        /// <summary>
        /// Excel VLOOKUP with approximate match (match_type=TRUE):
        /// Finds the largest key ≤ lookup value and returns the corresponding result.
        /// </summary>
        private static TResult VlookupLessOrEqual<TRow, TResult>(
            decimal lookupValue,
            IList<TRow> table,
            Func<TRow, decimal> keySelector,
            Func<TRow, TResult> resultSelector)
            where TResult : struct
        {
            TRow? matched = default;
            foreach (var row in table)
            {
                if (keySelector(row) <= lookupValue)
                    matched = row;
                else
                    break;
            }
            return matched != null ? resultSelector(matched) : default;
        }

        // ── Page layout helpers ──────────────────────────────────────────────

        /// <summary>
        /// Returns the last data row for a given page count (1-6).
        /// Each page block = 55 rows (40 data + header/footer).
        /// Page 1 ends at row 56, Page 2 at 111, ..., Page 6 at 331.
        /// Named ranges: page_one (A1:J56), page_two (A1:J111).
        /// From PageSetupService / 01_PageSetup_SetPageLayout.ts
        /// </summary>
        public static int GetLastRowForPageCount(int pageCount)
        {
            if (pageCount < 1 || pageCount > 6)
                throw new ArgumentOutOfRangeException(nameof(pageCount), "Must be 1-6.");
            int[] lastRows = { 56, 111, 166, 221, 276, 331 };
            return lastRows[pageCount - 1];
        }

        // ── C16 / C25 / D25 row visibility logic ───────────────────────────

        /// <summary>
        /// C16: FinType = IF(FinishType="Powder", 1, 0)
        /// Original Excel (3.Edit Pricing C16): =IF(B16="Powder",1,0)
        /// </summary>
        public static int CalculateFinType(string finishType)
            => string.Equals(finishType, "Powder", StringComparison.OrdinalIgnoreCase) ? 1 : 0;

        /// <summary>
        /// C25: CostType = SUM(C22, C23, C24)
        /// C22 = VLOOKUP(FinishApplicatorGroup, Validation!$X$2:$AA$7, 4) → capral group code
        /// C24 = VLOOKUP(Applicator, Validation!$R$2:$S$11, 2) → applicator code
        /// Effective combined cost type value used to determine which pricing rows are visible.
        /// </summary>
        public static int CalculateCostType(int c22Value, int c23Value, int c24Value)
            => c22Value + c23Value + c24Value;

        // ── LME / metal cost formulas ────────────────────────────────────────

        /// <summary>
        /// LME-based metal cost: LMERate + BilletPremium
        /// The total cost (Pricing Matrix B5) is: LME + BilletPremium + InternalSpread
        /// From 3.Edit Pricing B34: =IFERROR((B33 - 'Pricing Matrix'!$B$5) / B33, 0)
        /// </summary>
        public static decimal CalculateLmeCost(decimal lmeRate, decimal billetPremium)
            => lmeRate + billetPremium;

        /// <summary>
        /// IP (Inventory Point) upcharge per kg.
        /// Col AI in 2.Quote:
        ///   =IF(AV=1, Z*G33_IP1, IF(AV=2, Z*G33_IP2, IF(AV=3, Z*I33_IP3, 0)))
        /// Where G33=IP1 rate, H33=IP2 rate, I33=IP3 (Panoramic) fixed rate.
        /// IP rates from 3.Edit Pricing: G33=1 $/kg, H33=2 $/kg, I33=VLOOKUP("U3",Table_Costs,2)
        /// </summary>
        public static decimal CalculateIpEach(
            int ipLevel,        // AV col: 0=none, 1=IP1, 2=IP2, 3=IP3(Panoramic)
            decimal kgEach,     // Z col: kg each
            decimal ip1Rate,    // 3.Edit Pricing G33 (default 1 $/kg)
            decimal ip2Rate,    // 3.Edit Pricing H33 (default 2 $/kg)
            decimal ip3Rate)    // 3.Edit Pricing I33 (panoramic fixed rate from Table_Costs "U3")
        {
            return ipLevel switch
            {
                1 => kgEach * ip1Rate,
                2 => kgEach * ip2Rate,
                3 => kgEach * ip3Rate,
                _ => 0m
            };
        }

        // ── Finishing cost formulas ──────────────────────────────────────────

        /// <summary>
        /// SF Sell Each (col AS):
        ///   =IFERROR(IF(AQ="Anodised Perimeter (AP)", AC*AP + IF(U="Minfin",(40-AD)*AP/G,0), AF*AP), "")
        /// Where:
        ///   AQ = Costed/Priced As (e.g. "Anodised Perimeter (AP)", "Paint Perimeter (PP)")
        ///   AC = ap each (ap/m * LengthM / 1000)
        ///   AP = SF Sell $/m2
        ///   AD = ap total
        ///   AF = pp each
        ///   U = "Bulk", "Non-bulk", "Minfin", or ""
        ///   G = quantity
        /// </summary>
        public static decimal CalculateSfSellEach(
            string costedPricedAs,
            decimal apEach,           // AC
            decimal sfSellPerM2,      // AP col
            string millPcCategory,    // U col: "Bulk","Non-bulk","Minfin",""
            decimal apTotal,          // AD col
            decimal quantity,         // G col
            decimal ppEach)           // AF col
        {
            if (string.Equals(costedPricedAs, "Anodised Perimeter (AP)", StringComparison.OrdinalIgnoreCase))
            {
                var base_ = apEach * sfSellPerM2;
                var minfin = string.Equals(millPcCategory, "Minfin", StringComparison.OrdinalIgnoreCase)
                    ? (40m - apTotal) * sfSellPerM2 / (quantity > 0 ? quantity : 1m)
                    : 0m;
                return base_ + minfin;
            }
            return ppEach * sfSellPerM2;
        }

        /// <summary>
        /// SF Cost Total (col AR):
        ///   =IFERROR(IF(AQ="Anodised Perimeter (AP)", IF(U="Minfin",40,AD)*AO, AG*AO), "")
        /// Where AO = SF Cost $/m2, AG = pp total
        /// </summary>
        public static decimal CalculateSfCostTotal(
            string costedPricedAs,
            string millPcCategory,
            decimal apTotal,
            decimal sfCostPerM2,
            decimal ppTotal)
        {
            if (string.Equals(costedPricedAs, "Anodised Perimeter (AP)", StringComparison.OrdinalIgnoreCase))
            {
                var effectiveAp = string.Equals(millPcCategory, "Minfin", StringComparison.OrdinalIgnoreCase)
                    ? 40m : apTotal;
                return effectiveAp * sfCostPerM2;
            }
            return ppTotal * sfCostPerM2;
        }
    }
}
