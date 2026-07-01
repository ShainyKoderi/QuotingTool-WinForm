using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 13_IP_Reset.ts
    /// Resets the IP (Imported Price) calculation columns by copying master
    /// formula values back to QuoteLines. This is the IP-specific variant
    /// of ResetPriceService — it targets only rows that have an ImportedPrice
    /// set in BomLines.
    /// </summary>
    public class IPResetService
    {
        private readonly DatabaseContext _db;

        public IPResetService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Resets the IP calculation columns for the project.
        /// Copies BomLines.ImportedPrice back to QuoteLines.Price for all
        /// rows that have a matching BOM entry.
        /// </summary>
        /// <returns>Number of rows reset.</returns>
        public int ResetIPFormulas(int projectId)
        {
            var bomLines   = _db.GetBomLines(projectId).OrderBy(b => b.RowNumber).ToList();
            var quoteLines = _db.GetQuoteLines(projectId)
                               .OrderBy(q => q.PageBlock).ThenBy(q => q.RowInBlock).ToList();

            int resetCount = 0;
            for (int i = 0; i < bomLines.Count && i < quoteLines.Count; i++)
            {
                var bom   = bomLines[i];
                var quote = quoteLines[i];
                if (bom.ImportedPrice != quote.Price)
                {
                    _db.UpdateQuoteLinePrice(projectId, quote.PageBlock, quote.RowInBlock, bom.ImportedPrice);
                    resetCount++;
                }
            }
            return resetCount;
        }
    }
}
