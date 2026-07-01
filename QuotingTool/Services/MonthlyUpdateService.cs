using System;
using System.Collections.Generic;
using QuotingTool.Data;
using QuotingTool.Helpers;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 06_MonthlyUpdate_Open.ts and 07_MonthlyUpdate_Close.ts
    /// Manages the monthly pricing maintenance cycle.
    /// </summary>
    public class MonthlyUpdateService
    {
        private static readonly string[] MaintenanceSheets =
        {
            "Revision Control", "PricingMatrix", "3.Edit Pricing"
        };

        private readonly DatabaseContext _db;
        private readonly ProtectionHelper _protection;

        public MonthlyUpdateService(DatabaseContext db, ProtectionHelper protection)
        {
            _db = db;
            _protection = protection;
        }

        /// <summary>
        /// Maps to 06_MonthlyUpdate_Open.ts
        /// Marks maintenance sheets as unlocked so they can be edited.
        /// </summary>
        /// <returns>List of sheet names that were unlocked.</returns>
        public List<string> OpenMonthlyUpdate(int projectId)
        {
            var unlocked = new List<string>();
            foreach (var sheet in MaintenanceSheets)
            {
                _protection.Unlock(projectId, sheet);
                unlocked.Add(sheet);
            }
            return unlocked;
        }

        /// <summary>
        /// Maps to 07_MonthlyUpdate_Close.ts
        /// Saves new revision rates, resets B42/B44/B48/B50 to 99, locks sheets.
        /// </summary>
        public void CloseMonthlyUpdate(int projectId, DateTime revisionDate, decimal lmeRate, decimal billetPremium)
        {
            // Save revision record
            _db.InsertRevision(new RevisionControl
            {
                RevisionDate = revisionDate,
                LMERate = lmeRate,
                BilletPremium = billetPremium
            });

            // Update project rates
            var project = _db.GetProject(projectId);
            if (project != null)
            {
                project.RevisionDate = revisionDate;
                project.LMERate = lmeRate;
                project.BilletPremium = billetPremium;
                _db.UpdateProject(project);
            }

            // Reset B42/B44/B48/B50 to 99
            var ep = _db.GetEditPricing(projectId) ?? new EditPricing { ProjectId = projectId };
            ep.Price_B42 = 99;
            ep.Price_B44 = 99;
            ep.Price_B48 = 99;
            ep.Price_B50 = 99;
            _db.UpsertEditPricing(ep);

            // Lock all maintenance sheets
            foreach (var sheet in MaintenanceSheets)
                _protection.Lock(projectId, sheet);
        }
    }
}
