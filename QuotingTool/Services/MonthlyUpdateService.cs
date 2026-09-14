using System;
using System.Collections.Generic;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 06_MonthlyUpdate_Open.ts and 07_MonthlyUpdate_Close.ts
    /// Manages the monthly pricing maintenance cycle.
    /// </summary>
    public class MonthlyUpdateService
    {
        private readonly DatabaseContext _db;

        public MonthlyUpdateService(DatabaseContext db)
        {
            _db = db;
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
        }
    }
}
