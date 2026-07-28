using System.Collections.Generic;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 03_ResetPriceFormulas.ts
    /// Restores QuoteLines.Price values from the MasterFormulas backup
    /// (which maps to the col BA "master formulas" in the original Excel).
    /// </summary>
    public class ResetPriceService
    {
        private readonly DatabaseContext _db;

        public ResetPriceService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Copies MasterFormulas.FormulaExpression (interpreted as a price value)
        /// back to QuoteLines.Price for all 6 page blocks, and clears any
        /// ManualPriceOverride values (including those set by "Use Imported Prices").
        /// </summary>
        /// <returns>Number of price cells reset.</returns>
        public int ResetPriceFormulas(int projectId)
        {
            var masterFormulas = _db.GetMasterFormulas(projectId);
            int resetCount = 0;

            foreach (var mf in masterFormulas)
            {
                if (decimal.TryParse(mf.FormulaExpression, out decimal price))
                {
                    _db.UpdateQuoteLinePrice(projectId, mf.PageBlock, mf.RowInBlock, price);
                    resetCount++;
                }
            }

            // Clear any manual price overrides (including those applied by "Use Imported Prices")
            // so that price calculations return to the formula-driven values.
            _db.ClearAllManualPriceOverrides(projectId);

            return resetCount;
        }
    }
}
