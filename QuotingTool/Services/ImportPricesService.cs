using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 11_ImportPrices_Distribute.ts
    /// Copies BomLines.ImportedPrice into QuoteLines.Price for the correct
    /// number of page blocks (determined by row count thresholds).
    /// </summary>
    public class ImportPricesService
    {
        private readonly DatabaseContext _db;

        public ImportPricesService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Copies ImportedPrice from BomLines to QuoteLines.ManualPriceOverride.
        /// Matches by RowNumber / (PageBlock + RowInBlock) ordering.
        /// Setting ManualPriceOverride ensures prices are treated as overrides and
        /// persist until the user explicitly resets price formulas.
        /// </summary>
        /// <returns>Number of prices updated.</returns>
        public int ImportPricesToQuote(int projectId)
        {
            var bomLines   = _db.GetBomLines(projectId).OrderBy(b => b.RowNumber).ToList();
            var quoteLines = _db.GetQuoteLines(projectId).OrderBy(q => q.PageBlock).ThenBy(q => q.RowInBlock).ToList();

            if (!bomLines.Any() || !quoteLines.Any()) return 0;

            int pageCount = BomService.DeterminePageCount(bomLines.Count);
            int updated   = 0;

            // Match BOM rows to quote lines by position
            int maxQuoteRows = pageCount * 40;
            for (int i = 0; i < bomLines.Count && i < quoteLines.Count && i < maxQuoteRows; i++)
            {
                var bom   = bomLines[i];
                var quote = quoteLines[i];
                if (bom.ImportedPrice > 0)
                {
                    _db.UpdateQuoteLineManualPriceOverride(projectId, quote.PageBlock, quote.RowInBlock, bom.ImportedPrice);
                    updated++;
                }
            }

            return updated;
        }
    }
}
