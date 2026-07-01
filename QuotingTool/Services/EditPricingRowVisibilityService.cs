using System.Collections.Generic;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Result returned by EditPricingRowVisibilityService.GetVisibleRows.
    /// Mirrors the row show/hide logic from 05_EditPricing_RowVisibility.ts.
    /// </summary>
    public class RowVisibilityResult
    {
        public List<int> VisibleRows { get; set; } = new();
        public List<int> HiddenRows { get; set; } = new();
    }

    /// <summary>
    /// Maps to 05_EditPricing_RowVisibility.ts
    /// Determines which rows in the "3.Edit Pricing" sheet are visible or hidden
    /// based on FinType, CostTypeOverride, and CostType.
    /// </summary>
    public class EditPricingRowVisibilityService
    {
        private readonly DatabaseContext _db;

        public EditPricingRowVisibilityService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Returns which rows (40-55) in the Edit Pricing sheet should be
        /// visible or hidden based on the current pricing configuration.
        /// </summary>
        public RowVisibilityResult GetVisibleRows(int projectId)
        {
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };
            return Calculate(ep.FinType, ep.CostTypeOverride, ep.CostType);
        }

        /// <summary>
        /// Pure calculation — can be called directly with known values.
        /// </summary>
        public static RowVisibilityResult Calculate(int finType, bool costTypeOverride, int costType)
        {
            var result = new RowVisibilityResult();

            // Rule 1: FinType == 0 → hide rows 47-51
            if (finType == 0)
            {
                result.HiddenRows.AddRange(new[] { 47, 48, 49, 50, 51 });
            }

            // Rule 2: CostTypeOverride → show ALL rows 40-55
            if (costTypeOverride)
            {
                result.VisibleRows.AddRange(Range(40, 55));
                result.HiddenRows.Clear();
                return result;
            }

            // Rule 3: CostType-based visibility
            switch (costType)
            {
                case 0: // Standard
                    AddVisible(result, new[] { 40, 41, 43, 44, 45 });
                    AddHidden(result, new[] { 42 });
                    AddHidden(result, Range(47, 55));
                    break;

                case 1: // POA Colour
                case 2: // Custom Supplier
                    AddVisible(result, new[] { 40, 41, 42, 44, 45 });
                    AddHidden(result, new[] { 43 });
                    AddHidden(result, Range(47, 55));
                    break;

                case 4: // Bremer Mill Standard
                    AddHidden(result, Range(40, 45));
                    AddHidden(result, new[] { 48, 52 });
                    AddVisible(result, new[] { 47, 49, 50, 51, 53, 54, 55 });
                    break;

                case 5: // Bremer Mill POA
                    AddHidden(result, Range(40, 45));
                    AddHidden(result, new[] { 49, 53 });
                    AddVisible(result, new[] { 47, 48, 50, 51, 52, 54, 55 });
                    break;

                default:
                    // Fallback: show all rows 40-55
                    result.VisibleRows.AddRange(Range(40, 55));
                    break;
            }

            // Apply FinType==0 override on top: hide 47-51 regardless
            if (finType == 0)
            {
                for (int r = 47; r <= 51; r++)
                {
                    result.VisibleRows.Remove(r);
                    if (!result.HiddenRows.Contains(r))
                        result.HiddenRows.Add(r);
                }
            }

            return result;
        }

        private static List<int> Range(int from, int to)
        {
            var list = new List<int>();
            for (int i = from; i <= to; i++) list.Add(i);
            return list;
        }

        private static void AddVisible(RowVisibilityResult r, IEnumerable<int> rows)
        {
            foreach (var row in rows)
                if (!r.VisibleRows.Contains(row))
                    r.VisibleRows.Add(row);
        }

        private static void AddHidden(RowVisibilityResult r, IEnumerable<int> rows)
        {
            foreach (var row in rows)
                if (!r.HiddenRows.Contains(row))
                    r.HiddenRows.Add(row);
        }
    }
}
