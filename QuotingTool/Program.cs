using System;
using System.Windows.Forms;
using QuestPDF.Infrastructure;
using QuotingTool.Data;
using QuotingTool.Forms;
using QuotingTool.Services;

namespace QuotingTool
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            ApplicationConfiguration.Initialize();

            var db = new DatabaseContext();
            db.EnsureSchemaExists();
            var authService = new AuthService(db);
            var seededAdminPassword = authService.EnsureDefaultAdministrator();
            if (!string.IsNullOrWhiteSpace(seededAdminPassword))
            {
                MessageBox.Show(
                    $"Default administrator created.\n\nUsername: admin\nPassword: {seededAdminPassword}\n\nPlease change this password after login.",
                    "First Run Security",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            using var login = new LoginForm(authService);
            if (login.ShowDialog() != DialogResult.OK)
                return;

            Application.Run(new MainForm());
        }
    }
}
