using QuotingTool.Data;

namespace QuotingTool.Services
{
    public static class UserSession
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null && CurrentUser.IsActive;
        public static bool IsAdministrator => CurrentUser?.Role == UserRole.Administrator;
        public static bool IsSalesManager => CurrentUser?.Role == UserRole.SalesManager;

        public static bool HasRole(UserRole requiredRole)
            => CurrentUser != null && CurrentUser.IsActive && CurrentUser.Role >= requiredRole;

        public static void SetCurrentUser(User user) => CurrentUser = user;
        public static void Clear() => CurrentUser = null;
    }
}
