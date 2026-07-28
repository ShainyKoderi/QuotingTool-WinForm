using System.Windows.Forms;

namespace QuotingTool.Forms
{
    /// <summary>Represents the user's pricing choice after a quote import.</summary>
    public enum ImportPricingChoice
    {
        /// <summary>Keep the current calculated prices; do not change pricing.</summary>
        No,
        /// <summary>Override all pricing with the imported prices from the purchase order file.</summary>
        UseImportedPrices,
        /// <summary>Open Edit Pricing so the user can manually adjust pricing.</summary>
        Yes
    }

    /// <summary>
    /// "Imported Quote – Pricing" dialog.
    /// Asks the user how to handle pricing after importing a quote:
    ///   No / Use Imported Prices / Yes (edit pricing).
    /// </summary>
    public sealed class ImportPricingDialog : Form
    {
        public ImportPricingChoice Choice { get; private set; } = ImportPricingChoice.No;

        public ImportPricingDialog()
        {
            Text = "Imported Quote – Pricing";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width  = 400;
            Height = 155;

            Controls.Add(new Label
            {
                Left = 20, Top = 20, Width = 360, AutoSize = false, Height = 20,
                Text = "Do you want to edit pricing?"
            });

            var btnNo = new Button
            {
                Text = "No",
                Left = 20, Top = 60, Width = 90
            };
            var btnUseImported = new Button
            {
                Text = "Use Imported Prices",
                Left = 120, Top = 60, Width = 150
            };
            var btnYes = new Button
            {
                Text = "Yes",
                Left = 280, Top = 60, Width = 80
            };

            btnNo.Click += (_, _) =>
            {
                Choice = ImportPricingChoice.No;
                DialogResult = DialogResult.OK;
                Close();
            };

            btnUseImported.Click += (_, _) =>
            {
                Choice = ImportPricingChoice.UseImportedPrices;
                DialogResult = DialogResult.OK;
                Close();
            };

            btnYes.Click += (_, _) =>
            {
                Choice = ImportPricingChoice.Yes;
                DialogResult = DialogResult.OK;
                Close();
            };

            Controls.AddRange(new Control[] { btnNo, btnUseImported, btnYes });
        }
    }
}
