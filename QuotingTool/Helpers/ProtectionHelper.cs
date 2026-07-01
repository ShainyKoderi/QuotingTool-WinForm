using QuotingTool.Data;

namespace QuotingTool.Helpers
{
    /// <summary>
    /// Manages logical "sheet" protection state using the SheetLocks table.
    /// In the original Excel, sheets were protected with the password "party".
    /// Here, role-based SQL security is used instead; this helper tracks the
    /// locked/unlocked state in the database.
    /// </summary>
    public class ProtectionHelper
    {
        private readonly DatabaseContext _db;

        public ProtectionHelper(DatabaseContext db) => _db = db;

        /// <summary>
        /// Returns true if the named sheet is currently locked for the project.
        /// Defaults to locked if no record exists.
        /// </summary>
        public bool IsLocked(int projectId, string sheetName)
            => _db.IsSheetLocked(projectId, sheetName);

        /// <summary>
        /// Marks the named sheet as locked.
        /// </summary>
        public void Lock(int projectId, string sheetName)
            => _db.SetSheetLock(projectId, sheetName, true);

        /// <summary>
        /// Marks the named sheet as unlocked.
        /// </summary>
        public void Unlock(int projectId, string sheetName)
            => _db.SetSheetLock(projectId, sheetName, false);
    }
}
