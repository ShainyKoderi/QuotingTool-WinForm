using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 04_ShowHideBulk.ts
    /// Toggles the CostTypeOverride flag on the EditPricing record,
    /// which in the original Excel hid/showed bulk pricing rows.
    /// </summary>
    public class ShowHideBulkService
    {
        private readonly DatabaseContext _db;

        public ShowHideBulkService(DatabaseContext db) => _db = db;

        /// <summary>
        /// Flips EditPricing.CostTypeOverride between false and true.
        /// </summary>
        /// <returns>The new value of CostTypeOverride.</returns>
        public bool ToggleCostTypeOverride(int projectId)
        {
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };
            ep.CostTypeOverride = !ep.CostTypeOverride;
            _db.UpsertEditPricing(ep);
            return ep.CostTypeOverride;
        }
    }
}
