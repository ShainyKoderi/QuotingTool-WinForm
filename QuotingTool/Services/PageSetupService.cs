using System.Collections.Generic;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 01_PageSetup_SetPageLayout.ts
    /// Controls how many pages are used in the Quote sheet and updates
    /// footer visibility and print area accordingly.
    /// </summary>
    public class PageSetupService
    {
        // LastRow per page count (1-6)
        public static readonly int[] LastRowByPage = { 56, 111, 166, 221, 276, 331 };

        private readonly DatabaseContext _db;

        public PageSetupService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Sets the active page count (1-6).
        /// - Active footer page: FontColor = #000000
        /// - All other footer pages: FontColor = #FFFFFF
        /// - Updates PrintSettings LastRow and PrintAreaCols.
        /// </summary>
        public void SetPageLayout(int projectId, int pageCount)
        {
            if (pageCount < 1 || pageCount > 6)
                throw new System.ArgumentException("pageCount must be between 1 and 6.");

            int lastRow = LastRowByPage[pageCount - 1];

            // Update footer colours — active page black, others white
            for (int page = 1; page <= 6; page++)
            {
                _db.UpsertQuoteFooter(new QuoteFooter
                {
                    ProjectId = projectId,
                    PageNumber = page,
                    FontColor = (page == pageCount) ? "#000000" : "#FFFFFF"
                });
            }

            // Update print settings for "2.Quote"
            _db.UpsertPrintSetting(new PrintSetting
            {
                ProjectId = projectId,
                SheetName = "2.Quote",
                PageCount = pageCount,
                LastRow = lastRow,
                PrintAreaCols = "A:J"
            });

            // Update print settings for "AltQuote"
            _db.UpsertPrintSetting(new PrintSetting
            {
                ProjectId = projectId,
                SheetName = "AltQuote",
                PageCount = pageCount,
                LastRow = lastRow,
                PrintAreaCols = "A:H"
            });
        }

        /// <summary>
        /// Returns the print area description string for display in the UI.
        /// E.g. "Print area: A1:J111 (2 pages)"
        /// </summary>
        public string GetPrintAreaDescription(int projectId)
        {
            var ps = _db.GetPrintSetting(projectId, "2.Quote");
            if (ps == null) return "Print area: not configured";
            return $"Print area: A1:{ps.PrintAreaCols.Split(':')[1]}{ps.LastRow} ({ps.PageCount} page{(ps.PageCount > 1 ? "s" : "")})";
        }
    }
}
