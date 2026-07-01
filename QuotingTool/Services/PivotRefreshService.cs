using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Summary totals for a single page block.
    /// </summary>
    public class PageSummary
    {
        public int PageBlock { get; set; }
        public int RowCount { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalSellPrice { get; set; }
    }

    /// <summary>
    /// Overall summary across all pages.
    /// </summary>
    public class QuoteSummary
    {
        public List<PageSummary> Pages { get; set; } = new();
        public decimal GrandTotalPrice { get; set; }
        public decimal GrandTotalSellPrice { get; set; }
        public int TotalRows { get; set; }
    }

    /// <summary>
    /// Maps to 12_RefreshPivots.ts
    /// Recalculates summary/aggregation data (totals per page block, grand total).
    /// In the original Excel this refreshed PivotTables; here it returns a
    /// computed summary model.
    /// </summary>
    public class PivotRefreshService
    {
        private readonly DatabaseContext _db;

        public PivotRefreshService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Recalculates totals per page block and grand totals.
        /// </summary>
        public QuoteSummary RefreshSummary(int projectId)
        {
            var lines = _db.GetQuoteLines(projectId).ToList();

            var pageSummaries = lines
                .GroupBy(l => l.PageBlock)
                .OrderBy(g => g.Key)
                .Select(g => new PageSummary
                {
                    PageBlock       = g.Key,
                    RowCount        = g.Count(),
                    TotalQuantity   = g.Sum(l => l.Quantity),
                    TotalPrice      = g.Sum(l => l.Price),
                    TotalSellPrice  = g.Sum(l => l.SellPrice)
                })
                .ToList();

            return new QuoteSummary
            {
                Pages              = pageSummaries,
                GrandTotalPrice    = pageSummaries.Sum(p => p.TotalPrice),
                GrandTotalSellPrice = pageSummaries.Sum(p => p.TotalSellPrice),
                TotalRows          = lines.Count
            };
        }
    }
}
