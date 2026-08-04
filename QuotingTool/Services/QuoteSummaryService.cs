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
            var sundryTotal = _db.GetSundryTotal(projectId);

            // Metal cost per kg: use PricingConstants (Pricing Matrix B5 = LME+Billet+Spread).
            // Excel uses the single global monthly rate ('Pricing Matrix'!$B$5) for all
            // margin calculations, so mirror that exactly here.
            var constants = _db.GetLatestPricingConstants()
                ?? new PricingConstants { MetalCostPerKg = 8.73m, InternalSpread = 3.12m };
            decimal metalCostPerKg = constants.MetalCostPerKg;

            // Compute total kg from per-line kg/m values (mirrors 3.Edit Pricing B26 = SUM AA).
            // This avoids stale TotalKg on first load before ep has been persisted.
            var extrusionLookup = _db.GetExtrusionMasterList()
                .ToDictionary(m => m.Section, m => m, StringComparer.OrdinalIgnoreCase);
            decimal totalKg = 0m;
            foreach (var l in lines)
            {
                if (string.IsNullOrWhiteSpace(l.ExtrusionCode)) continue;
                if (extrusionLookup.TryGetValue(l.ExtrusionCode.Trim(), out var em) && em.MassPerM > 0m)
                    totalKg += l.Quantity * l.LengthM * em.MassPerM;
            }
            // Fall back to persisted TotalKg if section master data is unavailable.
            if (totalKg <= 0m) totalKg = ep.TotalKg;

            // Mill Finish sell total = sum of all quote line sell prices.
            decimal millFinishTotal = lines.Sum(l => l.TotalPrice);

            // Mill Finish cost = total kg × metal cost per kg (Excel: SUM(AA) × Pricing Matrix B5).
            decimal millFinishCost;
            if (totalKg > 0m && metalCostPerKg > 0m)
            {
                millFinishCost = totalKg * metalCostPerKg;
            }
            else
            {
                millFinishCost = millFinishTotal * MillFinishCostRatioFallback;
            }

            decimal millFinishMarginDollar = millFinishTotal - millFinishCost;
            decimal millFinishMarginPct = millFinishTotal > 0m
                ? millFinishMarginDollar / millFinishTotal * 100m : 0m;

            // Finishing applies only when the project uses a powder/anodised finish
            // AND there is measurable finishing area.  Mill Finish projects have zero
            // finishing cost/sell (matching Excel rows 61-66 semantics).
            bool hasFinishing = !string.Equals(project.FinishType, "Mill Finish",
                                    StringComparison.OrdinalIgnoreCase)
                                && ep.TotalFinishingM2 > 0m
                                && ep.FinishingPrice > 0m;

            decimal finishingTotal = hasFinishing ? ep.FinishingPrice * ep.TotalFinishingM2 : 0m;
            decimal finishingCost  = hasFinishing ? ep.FinishingCostPricePerM2 * ep.TotalFinishingM2 : 0m;
            decimal finishingMarginDollar = finishingTotal - finishingCost;
            decimal finishingMarginPct = finishingTotal > 0m
                ? finishingMarginDollar / finishingTotal * 100m : 0m;

            var sundryMaster = _db.GetSundryItems()
                .Where(i => !string.IsNullOrWhiteSpace(i.SectionCode))
                .ToDictionary(i => i.SectionCode.Trim(), i => i, StringComparer.OrdinalIgnoreCase);
            var subsillTiers = _db.GetSubsillTierPrices().ToDictionary(t => t.Tier);

            decimal sundrySellTotal = 0m;
            decimal sundryCostTotal = 0m;
            decimal slottingSellTotal = 0m;
            decimal slottingCostTotal = 0m;

            foreach (var line in lines)
            {
                var sectionCode = line.ExtrusionCode?.Trim();
                if (!string.IsNullOrWhiteSpace(sectionCode) &&
                    sundryMaster.TryGetValue(sectionCode, out var sundry))
                {
                    sundrySellTotal += sundry.UnitSell * line.Quantity;
                    sundryCostTotal += sundry.UnitCost * line.Quantity;
                    continue;
                }

                if (line.SubsillFlag && line.LengthM > 0m)
                {
                    slottingSellTotal += line.SubsillSurcharge * line.Quantity;

                    if (!string.IsNullOrWhiteSpace(sectionCode) &&
                        extrusionLookup.TryGetValue(sectionCode, out var master) &&
                        master.SubsillTier > 0 &&
                        subsillTiers.TryGetValue(master.SubsillTier, out var tier))
                    {
                        slottingCostTotal += tier.CostPerMetre * line.LengthM * line.Quantity;
                    }
                }
            }

            decimal slottingSundrySellTotal = sundrySellTotal + slottingSellTotal;
            decimal slottingSundryCostTotal = sundryCostTotal + slottingCostTotal;
            decimal slottingSundryMarginDollar = slottingSundrySellTotal - slottingSundryCostTotal;
            decimal slottingSundryMarginPct = slottingSundrySellTotal > 0m
                ? slottingSundryMarginDollar / slottingSundrySellTotal * 100m : 0m;

            decimal grandTotal = millFinishTotal + finishingTotal + sundryTotal;
            decimal grandCost = millFinishCost + finishingCost;
            decimal grandMarginDollar = grandTotal - grandCost;
            decimal grandMarginPct = grandTotal > 0m ? grandMarginDollar / grandTotal * 100m : 0m;

            var summary = new QuotingTool.Data.QuoteSummary
            {
                ProjectId = projectId,
                MillFinishTotal = millFinishTotal,
                MillFinishMarginDollar = millFinishMarginDollar,
                MillFinishMarginPct = millFinishMarginPct,
                SlottingSundryTotal = sundryTotal,
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
