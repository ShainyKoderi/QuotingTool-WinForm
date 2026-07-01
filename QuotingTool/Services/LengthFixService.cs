using System.Collections.Generic;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 02_LengthFix_mm_to_m.ts
    /// Detects QuoteLines where LengthM > 1000 (indicating the value was
    /// entered in millimetres) and converts them to metres by dividing by 1000.
    /// </summary>
    public class LengthFixService
    {
        private readonly DatabaseContext _db;

        public LengthFixService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Fixes all quote lines where LengthM > 1000 by dividing by 1000.
        /// Works across all 6 page blocks.
        /// </summary>
        /// <returns>Number of rows fixed.</returns>
        public int FixLengths(int projectId)
        {
            var lines = _db.GetQuoteLines(projectId);
            int fixedCount = 0;

            foreach (var line in lines)
            {
                if (line.LengthM > 1000)
                {
                    decimal corrected = line.LengthM / 1000m;
                    _db.UpdateQuoteLineLengthM(projectId, line.PageBlock, line.RowInBlock, corrected);
                    fixedCount++;
                }
            }

            return fixedCount;
        }
    }
}
