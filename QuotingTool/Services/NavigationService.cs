using System;
using System.Windows.Forms;

namespace QuotingTool.Services
{
    /// <summary>
    /// Maps to 14_NavigateToSheet.ts
    /// Switches the active tab on the MainForm to the tab matching sheetName.
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Activates the tab on <paramref name="tabControl"/> whose Text matches
        /// <paramref name="sheetName"/> (case-insensitive).
        /// </summary>
        public void NavigateTo(string sheetName, TabControl tabControl)
        {
            if (tabControl == null) throw new ArgumentNullException(nameof(tabControl));
            if (string.IsNullOrWhiteSpace(sheetName)) return;

            foreach (TabPage tab in tabControl.TabPages)
            {
                if (string.Equals(tab.Text, sheetName, StringComparison.OrdinalIgnoreCase)
                    || tab.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
                {
                    tabControl.SelectedTab = tab;
                    return;
                }
            }
        }

        /// <summary>
        /// Overload that accepts friendly sheet name aliases used in the original scripts.
        /// </summary>
        public void NavigateTo(string sheetName, TabControl tabControl, Action<string>? onNotFound)
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                if (string.Equals(tab.Text, sheetName, StringComparison.OrdinalIgnoreCase)
                    || tab.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
                {
                    tabControl.SelectedTab = tab;
                    return;
                }
            }
            onNotFound?.Invoke(sheetName);
        }
    }
}
