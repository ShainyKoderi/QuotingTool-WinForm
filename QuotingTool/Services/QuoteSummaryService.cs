using System;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class QuoteSummaryService
    {
        private readonly DatabaseContext _db;
        public QuoteSummaryService(DatabaseContext db) => _db = db;

        public QuotingTool.Data.QuoteSummary Calculate(int projectId)
        {
            var lines = _db.GetQuoteLines(projectId).ToList();
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing();
            var sundryTotal = _db.GetSundryTotal(projectId);

            decimal millFinishTotal = lines.Sum(l => l.TotalPrice);
            decimal millFinishCost = millFinishTotal * 0.87m;
            decimal millFinishMarginDollar = millFinishTotal - millFinishCost;
            decimal millFinishMarginPct = millFinishTotal > 0 ? millFinishMarginDollar / millFinishTotal * 100m : 0m;

            decimal finishingTotal = ep.FinishingPrice * ep.TotalFinishingM2;
            decimal finishingCost = ep.FinishingCostPricePerM2 * ep.TotalFinishingM2;
            decimal finishingMarginDollar = finishingTotal - finishingCost;
            decimal finishingMarginPct = finishingTotal > 0 ? finishingMarginDollar / finishingTotal * 100m : 0m;

            decimal grandTotal = millFinishTotal + finishingTotal + sundryTotal;
            decimal grandCost = millFinishCost + finishingCost;
            decimal grandMarginDollar = grandTotal - grandCost;
            decimal grandMarginPct = grandTotal > 0 ? grandMarginDollar / grandTotal * 100m : 0m;

            var summary = new QuotingTool.Data.QuoteSummary
            {
                ProjectId = projectId,
                MillFinishTotal = millFinishTotal,
                MillFinishMarginDollar = millFinishMarginDollar,
                MillFinishMarginPct = millFinishMarginPct,
                SlottingSundryTotal = sundryTotal,
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
