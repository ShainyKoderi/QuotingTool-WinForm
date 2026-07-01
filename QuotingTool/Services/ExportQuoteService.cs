using System.Collections.Generic;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Status codes returned by ExportQuoteService.
    /// </summary>
    public enum ExportStatus
    {
        OK,
        MISSING_METAL,
        MISSING_FINISHING,
        MISSING_BOTH
    }

    /// <summary>
    /// Holds the result of an export operation.
    /// </summary>
    public class ExportResult
    {
        public ExportStatus Status { get; set; }
        public List<QuoteLine> ExportedLines { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Maps to 08_ExportQuoteData_InWorkbook.ts
    /// Validates pricing is set and collects all 6 blocks of QuoteLines
    /// into a flat export list.
    /// </summary>
    public class ExportQuoteService
    {
        private readonly DatabaseContext _db;

        public ExportQuoteService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Exports quote data for the given project.
        /// Returns OK if both metal and finishing prices are set, otherwise
        /// returns MISSING_METAL, MISSING_FINISHING, or MISSING_BOTH.
        /// </summary>
        public ExportResult ExportQuoteData(int projectId)
        {
            var ep = _db.GetEditPricing(projectId);
            bool metalMissing    = ep == null || ep.MetalPrice <= 0;
            bool finishMissing   = ep == null || ep.FinishingPrice <= 0;

            ExportStatus status;
            if (metalMissing && finishMissing)
                status = ExportStatus.MISSING_BOTH;
            else if (metalMissing)
                status = ExportStatus.MISSING_METAL;
            else if (finishMissing)
                status = ExportStatus.MISSING_FINISHING;
            else
                status = ExportStatus.OK;

            var lines = new List<QuoteLine>(_db.GetQuoteLines(projectId));

            return new ExportResult
            {
                Status = status,
                ExportedLines = lines,
                Message = status == ExportStatus.OK
                    ? $"Export complete — {lines.Count} line(s) across {CountPages(lines)} page(s)."
                    : $"Warning: {status}. Prices may be incomplete. {lines.Count} line(s) exported."
            };
        }

        private static int CountPages(List<QuoteLine> lines)
        {
            int max = 0;
            foreach (var l in lines)
                if (l.PageBlock > max) max = l.PageBlock;
            return max;
        }
    }
}
