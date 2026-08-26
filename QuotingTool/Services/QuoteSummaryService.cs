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
            decimal finishingPricePerM2 = ep.FinishingPrice;
            decimal finishingCostPerM2  = ep.FinishingCostPricePerM2 > 0m
                ? ep.FinishingCostPricePerM2 : ep.FinishingPrice;

            // ── Compute all components directly from per-line data ────────────
            // This mirrors the 2.Quote column formulas exactly:
            //   AH = MF each (kg/m × L × MetalSell)
            //   AI = IP each (kg/m × L × IP rate)
            //   AS = SF Sell each (perimeter/1000 × L × FinishSell)
            //   AR = SF Cost each (perimeter/1000 × L × FinishCost)
            //   K  = SubsillSurcharge each (stored on line)
            //   AJ = AH + AI (non-TB metal each)
            //   AL = AH + AI (TB metal each)
            //   AN = AH cost each = kg/m × L × metalCostPerKg

            decimal nonTbSellTotal  = 0m;  // B62
            decimal nonTbCostTotal  = 0m;  // C62 cost basis
            decimal tbSellTotal     = 0m;  // B63
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

                // ── Standalone sundry items ────────────────────────────────────
                if (!string.IsNullOrWhiteSpace(code) && sundryMaster.TryGetValue(code, out var sundry))
                {
                    // Use the actual line TotalPrice so the summary matches the quote total exactly.
                    decimal lineTotal = line.TotalPrice;
                    sundrySellTotal += lineTotal;
                    sundryCostTotal += sundry.UnitCost * line.Quantity;
                    continue;
                }

                // ── Blank-code / unrecognised lines (freeform sundry/description) ─────
                if (string.IsNullOrWhiteSpace(code) || !extrusionLookup.TryGetValue(code, out var em))
                {
                    if (line.TotalPrice > 0m)
                        sundrySellTotal += line.TotalPrice;
                    continue;
                }

                // ── Finishing (SF) component ───────────────────────────────────
                bool ignoreFinish = string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase);
                // Use the same AP→PP fallback for both finishingSellTotal and sfSellLine
                // so the finishing portion is never double-counted in Mill Finish.
                decimal perimeterMm = em.ApMm > 0m ? em.ApMm : em.PpMm;
                if (!isMillFinishProject && !ignoreFinish && finishingPricePerM2 > 0m && line.LengthM > 0m)
                {
                    // perimeterMm already assigned above
                    if (perimeterMm > 0m)
                    {
                        decimal areaM2Each = perimeterMm / 1000m * line.LengthM;
                        finishingSellTotal += areaM2Each * finishingPricePerM2 * line.Quantity;
                        finishingCostTotal += areaM2Each * finishingCostPerM2  * line.Quantity;
                    }
                }

                // ── Slotting/subsill surcharge ─────────────────────────────────
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

                // ── Metal (MF + IP) component ──────────────────────────────────
                if (em.MassPerM <= 0m || line.LengthM <= 0m) continue;

                decimal kgLine = em.MassPerM * line.LengthM * line.Quantity;

                // Sell: use actual stored TotalPrice minus the separable finishing and slotting portions.
                // Uses the same perimeterMm (AP→PP fallback) as finishingSellTotal to prevent
                // finishing value leaking into the metal total when ApMm=0 but PpMm>0.
                decimal sfSellLine = (!isMillFinishProject && !ignoreFinish && perimeterMm > 0m)
                    ? (perimeterMm / 1000m * line.LengthM * finishingPricePerM2 * line.Quantity) : 0m;
                decimal slottingLine = line.SubsillSurcharge * line.Quantity;
                decimal metalSellLine = line.TotalPrice - sfSellLine - slottingLine;

                decimal metalCostLine = kgLine * metalCostPerKg;

                bool isTb = !string.Equals(em.ThermalBreakType, "No", StringComparison.OrdinalIgnoreCase);
                if (isTb)
                {
                    tbSellTotal  += metalSellLine;
                    tbCostTotal  += metalCostLine;
                }
                else
                {
                    nonTbSellTotal  += metalSellLine;
                    nonTbCostTotal  += metalCostLine;
                }
            }

            // ── B64: Slotting/Sundry ──────────────────────────────────────────
            decimal slottingSundryTotal = sundrySellTotal + slottingSellTotal;
            decimal slottingSundryMarginDollar = slottingSundryTotal - sundryCostTotal - slottingCostTotal;
            decimal slottingSundryMarginPct = slottingSundryTotal > 0m
                ? slottingSundryMarginDollar / slottingSundryTotal * 100m : 0m;

            // ── B65: Finishing ────────────────────────────────────────────────
            decimal finishingTotal        = finishingSellTotal;
            decimal finishingMarginDollar = finishingTotal - finishingCostTotal;
            decimal finishingMarginPct    = finishingTotal > 0m
                ? finishingMarginDollar / finishingTotal * 100m : 0m;

            // ── B62: Mill Finish Metal ────────────────────────────────────────
            decimal millFinishTotal        = nonTbSellTotal;
            decimal millFinishCost         = nonTbCostTotal > 0m ? nonTbCostTotal
                : millFinishTotal * MillFinishCostRatioFallback;
            decimal millFinishMarginDollar = millFinishTotal - millFinishCost;
            decimal millFinishMarginPct    = millFinishTotal > 0m
                ? millFinishMarginDollar / millFinishTotal * 100m : 0m;

            // ── B63: Thermal Break Metal ──────────────────────────────────────
            decimal thermalBreakTotal        = tbSellTotal;
            decimal thermalBreakMarginDollar = thermalBreakTotal - tbCostTotal;
            decimal thermalBreakMarginPct    = thermalBreakTotal > 0m
                ? thermalBreakMarginDollar / thermalBreakTotal * 100m : 0m;

            // ── B66: Grand Total ──────────────────────────────────────────────
            decimal grandTotal = lines.Sum(l => l.TotalPrice);

            // ── C66: Total margin ─────────────────────────────────────────────
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
