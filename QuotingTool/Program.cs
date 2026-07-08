using System;
using System.Windows.Forms;
using QuestPDF.Infrastructure;
using QuotingTool.Forms;

namespace QuotingTool
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
