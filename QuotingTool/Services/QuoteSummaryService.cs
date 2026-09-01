using System;
using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class QuoteSummaryService
    {
        // Fallback cost ratio used only when no kg/cost data is available
        // (e.g. sections not yet seeded in ExtrusionMaster).
        private const decimal MillFinishCostRatioFallback = 0.87m;

        private readonly DatabaseContext _db;
        public QuoteSummaryService(DatabaseContext db) => _db = db;

        public QuotingTool.Data.QuoteSummary Calculate(int projectId)
        {
            var lines = _db.GetQuoteLines(projectId).ToList();
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing();
            var project = _db.GetProject(projectId) ?? new Project();

            // Metal cost per kg: Pricing Matrix B5 = LME + Billet + Spread.
            var constants = _db.GetLatestPricingConstants()
                ?? new PricingConstants { MetalCostPerKg = 8.73m, InternalSpread = 3.12m };
            decimal metalCostPerKg = constants.MetalCostPerKg;

            var extrusionLookup = _db.GetExtrusionMasterList()
                .ToDictionary(m => m.Section, m => m, StringComparer.OrdinalIgnoreCase);
            var sundryMaster = _db.GetSundryItems()
                .Where(i => !string.IsNullOrWhiteSpace(i.SectionCode))
                .ToDictionary(i => i.SectionCode.Trim(), i => i, StringComparer.OrdinalIgnoreCase);
            var subsillTiers = _db.GetSubsillTierPrices().ToDictionary(t => t.Tier);

            bool isMillFinishProject = string.Equals(project.FinishType, "Mill Finish",
                StringComparison.OrdinalIgnoreCase);
            // Round SF Sell/m² and SF Cost/m² to 2 decimal places, matching Excel's
            // AP sell rate (col AP) and cost rate (col AO) display precision.
            decimal finishingPricePerM2 = Math.Round(ep.FinishingPrice, 2);
            // Finishing cost rate mirrors AO column: FinishingCostPricePerM2 (B43).
            // Do NOT fall back to the sell price (FinishingPrice) — that collapses margin to zero.
            // Fall back to Price_B42 (POA/Other vendor cost) instead, which is the correct
            // cost basis when B43 has not been auto-populated from the Pricing Matrix.
            decimal finishingCostPerM2 = Math.Round(
                ep.FinishingCostPricePerM2 > 0m
                    ? ep.FinishingCostPricePerM2
                    : ep.Price_B42 > 0m ? ep.Price_B42 : 0m,
                2);

            // B58: Anodised threshold sell rate $/m² and its cost basis (B57).
            // Used instead of the standard finishing rate when a section is in the THRESHOLDS
            // named range and the project ThresholdChoice is not "No" (mirrors col AP formula).
            decimal thresholdSellPerM2 = Math.Round(ep.Price_B58, 2);
            decimal thresholdCostPerM2 = Math.Round(
                ep.AnodisedThresholdCostPerM2 > 0m ? ep.AnodisedThresholdCostPerM2 : ep.Price_B57, 2);
            bool thresholdActive = !string.Equals(project.ThresholdChoice, "No", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(project.ThresholdChoice);
            var thresholdSections = _db.GetThresholdSections();

            // IP upcharge rates from EditPricing (mirrors 3.Edit Pricing G33/H33/I33).
            //decimal ip1Rate = ep.IP1UpchargePerKg > 0m ? ep.IP1UpchargePerKg : 1.00m;
            //decimal ip2Rate = ep.IP2UpchargePerKg > 0m ? ep.IP2UpchargePerKg : 2.00m;
            //decimal ip3Rate = ep.IP3PanoramicFixedKgRate > 0m ? ep.IP3PanoramicFixedKgRate : 25.85m;
            decimal ip1Rate = ep.IP1UpchargePerKg ;
            decimal ip2Rate = ep.IP2UpchargePerKg ;
            decimal ip3Rate = ep.IP3PanoramicFixedKgRate ;
            // 3.Edit Pricing B33: metal sell price per kg
            decimal metalSellPrice = ep.MetalPrice;

            decimal nonTbSellTotal  = 0m;  // B62: Mill Finish total value
            decimal nonTbCostTotal  = 0m;  // C62 cost basis
            decimal tbSellTotal     = 0m;  // B63: Thermal Break total value
            decimal tbCostTotal     = 0m;  // C63 cost basis

            decimal finishingSellTotal = 0m;  // B65 (summed per line)
            decimal finishingCostTotal = 0m;  // C65 cost basis

            decimal slottingSellTotal  = 0m;  // slotting component of B64
            decimal slottingCostTotal  = 0m;
            decimal sundrySellTotal    = 0m;  // standalone sundry component of B64
            decimal sundryCostTotal    = 0m;

            foreach (var line in lines)
            {
                var code = line.ExtrusionCode?.Trim() ?? "";

                // Standalone sundry items (code exists in sundry master)
                if (!string.IsNullOrWhiteSpace(code) && sundryMaster.TryGetValue(code, out var sundry))
                {
                    sundrySellTotal += sundry.UnitSell * line.Quantity;
                    sundryCostTotal += sundry.UnitCost * line.Quantity;

                    // Sundry items that carry a finishing perimeter also contribute a finishing
                    // component to $ Each (mirroring QuoteForm.CalculateFinishingEach).
                    bool sundryIgnoreFinish = string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase);
                    decimal sundryPerimeterMm = sundry.Ap > 0 ? sundry.Ap : sundry.Pp;
                    if (!isMillFinishProject && !sundryIgnoreFinish && finishingPricePerM2 > 0m
                        && line.LengthM > 0m && sundryPerimeterMm > 0m)
                    {
                        decimal areaM2Each = sundryPerimeterMm / 1000m * line.LengthM;
                        finishingSellTotal += areaM2Each * finishingPricePerM2 * line.Quantity;
                        finishingCostTotal += areaM2Each * finishingCostPerM2  * line.Quantity;
                    }
                    continue;
                }

                // Blank-code / unrecognised lines (freeform description/sundry lines)
                // Accumulate sell value into sundry - no cost data available.
                if (string.IsNullOrWhiteSpace(code) || !extrusionLookup.TryGetValue(code, out var em))
                {
                    if (line.TotalPrice > 0m)
                        sundrySellTotal += line.TotalPrice;
                    continue;
                }

                // Finishing (SF) component — mirrors col AS formula:
                // AP col (sell rate) = B58 threshold rate when BF=1 and ThresholdChoice?No,
                //                     otherwise standard FinishingPrice (B44).
                // AO col (cost rate) = B57 threshold cost when threshold active, else B43.
                bool ignoreFinish = string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase);
                decimal perimeterMm = em.ApMm > 0m ? em.ApMm : em.PpMm;
                if (!isMillFinishProject && !ignoreFinish && line.LengthM > 0m && perimeterMm > 0m)
                {
                    bool isThresholdLine = thresholdActive && thresholdSections.Contains(code);
                    decimal sfSellRate = isThresholdLine && thresholdSellPerM2 > 0m
                        ? thresholdSellPerM2 : finishingPricePerM2;
                    decimal sfCostRate = isThresholdLine && thresholdCostPerM2 > 0m
                        ? thresholdCostPerM2 : finishingCostPerM2;
                    if (sfSellRate > 0m)
                    {
                        decimal areaM2Each = perimeterMm / 1000m * line.LengthM;
                        finishingSellTotal += areaM2Each * sfSellRate * line.Quantity;
                        finishingCostTotal += areaM2Each * sfCostRate * line.Quantity;
                    }
                }

                // Slotting/subsill surcharge
                if (line.SubsillSurcharge > 0m)
                {
                    slottingSellTotal += line.SubsillSurcharge * line.Quantity;

                    if (line.SubsillFlag && line.LengthM > 0m
                        && em.SubsillTier > 0
                        && subsillTiers.TryGetValue(em.SubsillTier, out var tier))
                    {
                        slottingCostTotal += tier.CostPerMetre * line.LengthM * line.Quantity;
                    }
                }

                // Metal (MF + IP) component
                // Mirrors Excel col AJ = (AH x G) + (AI x G):
                //   AH x G = kg x MetalSellPrice (B33)   -- mill finish sell
                //   AI x G = kg x IP upcharge rate        -- IP sell
                // B62 = SUMIF(ThermalBreak="No", AJ)
                // Computed directly from master rates, NOT back-calculated from TotalPrice.
                if (em.MassPerM <= 0m || line.LengthM <= 0m) continue;

                decimal kgLine = em.MassPerM * line.LengthM * line.Quantity;

                // AH x G: mill-finish sell (kg x B33)
                decimal mfSellLine = metalSellPrice > 0m ? kgLine * metalSellPrice : 0m;

                // AI x G: IP upcharge sell (kg x IP rate based on IpGrade 0-3)
                decimal ipSellLine = line.IpGrade switch
                {
                    1 => kgLine * ip1Rate,
                    2 => kgLine * ip2Rate,
                    3 => kgLine * ip3Rate,
                    _ => 0m
                };

                // AJ = AH x G + AI x G
                decimal metalSellLine = mfSellLine + ipSellLine;

                // AN: metal cost = kg x B5
                decimal metalCostLine = kgLine * metalCostPerKg;

                bool isTb = !string.Equals(em.ThermalBreakType, "No", StringComparison.OrdinalIgnoreCase);
                if (isTb)
                {
                    tbSellTotal  += metalSellLine;
                    tbCostTotal  += metalCostLine;
                }
                else
                {
                    nonTbSellTotal += metalSellLine;
                    nonTbCostTotal += metalCostLine;
                }
            }

            // B64: Slotting/Sundry
            decimal slottingSundryTotal = sundrySellTotal + slottingSellTotal;
            decimal slottingSundryMarginDollar = slottingSundryTotal - sundryCostTotal - slottingCostTotal;
            decimal slottingSundryMarginPct = slottingSundryTotal > 0m
                ? slottingSundryMarginDollar / slottingSundryTotal * 100m : 0m;

            // B65: Finishing — mirrors Excel B65 = SUM(AT12:AT327)
            // where AT (sell total per line) = AS (sell each) × G (qty)
            //       AS = (AP perimeter mm / 1000 × length m) × AP sell rate (FinishingPrice)
            //       AR (cost total per line) = area m² × AO cost rate (FinishingCostPricePerM2)
            // Accumulated above in finishingSellTotal / finishingCostTotal during the per-line loop.
            decimal finishingTotal        = finishingSellTotal;
            decimal finishingMarginDollar = finishingSellTotal - finishingCostTotal;
            decimal finishingMarginPct    = finishingTotal > 0m
                ? finishingMarginDollar / finishingTotal * 100m : 0m;

            // B62: Mill Finish Metal
            decimal millFinishTotal        = nonTbSellTotal;
            decimal millFinishCost         = nonTbCostTotal > 0m ? nonTbCostTotal
                : millFinishTotal * MillFinishCostRatioFallback;
            decimal millFinishMarginDollar = millFinishTotal - millFinishCost;
            decimal millFinishMarginPct    = millFinishTotal > 0m
                ? millFinishMarginDollar / millFinishTotal * 100m : 0m;

            // B63: Thermal Break Metal
            decimal thermalBreakTotal        = tbSellTotal;
            decimal thermalBreakMarginDollar = thermalBreakTotal - tbCostTotal;
            decimal thermalBreakMarginPct    = thermalBreakTotal > 0m
                ? thermalBreakMarginDollar / thermalBreakTotal * 100m : 0m;

            // B66: Grand Total = sum of the four computed category totals so the
            // Total row always reconciles with the rows displayed above it.
            decimal grandTotal = millFinishTotal + thermalBreakTotal + slottingSundryTotal + finishingTotal;

            // C66: Total margin
            decimal grandMarginDollar = millFinishMarginDollar + thermalBreakMarginDollar
                + slottingSundryMarginDollar + finishingMarginDollar;
            decimal grandMarginPct = grandTotal > 0m
                ? grandMarginDollar / grandTotal * 100m : 0m;

            var summary = new QuotingTool.Data.QuoteSummary
            {
                ProjectId = projectId,
                MillFinishTotal = millFinishTotal,
                MillFinishMarginDollar = millFinishMarginDollar,
                MillFinishMarginPct = millFinishMarginPct,
                ThermalBreakMetalTotal = thermalBreakTotal,
                ThermalBreakMarginDollar = thermalBreakMarginDollar,
                ThermalBreakMarginPct = thermalBreakMarginPct,
                SlottingSundryTotal = slottingSundryTotal,
                SlottingSundryMarginDollar = slottingSundryMarginDollar,
                SlottingSundryMarginPct = slottingSundryMarginPct,
                FinishingTotal = finishingTotal,
                FinishingMarginDollar = finishingMarginDollar,
                FinishingMarginPct = finishingMarginPct,
                GrandTotal = grandTotal,
                GrandMarginDollar = grandMarginDollar,
                GrandMarginPct = grandMarginPct,
                UpdatedAt = DateTime.Now
            };
            _db.UpsertQuoteSummary(summary);
            return summary;
        }
    }
}
