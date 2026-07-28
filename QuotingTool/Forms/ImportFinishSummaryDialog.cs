using System.Collections.Generic;
using System.Windows.Forms;

namespace QuotingTool.Forms
{
    /// <summary>
    /// "Imported Quote – Finish Summary" dialog.
    /// Displays the most common finish and all finish colours found in the imported quote,
    /// then asks whether the user wants to edit the finish colours.
    /// </summary>
    public sealed class ImportFinishSummaryDialog : Form
    {
        public bool EditFinishColours { get; private set; }

        public ImportFinishSummaryDialog(string mostCommonFinish, IReadOnlyList<string> allFinishes)
        {
            Text = "Imported Quote – Finish Summary";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 440;
            Height = 310;

            int y = 20;

            Controls.Add(new Label
            {
                Left = 20, Top = y, Width = 390, AutoSize = false, Height = 20,
                Text = $"Most common finish:  {(string.IsNullOrWhiteSpace(mostCommonFinish) ? "(none)" : mostCommonFinish)}"
            });
            y += 28;

            Controls.Add(new Label { Left = 20, Top = y, Width = 390, AutoSize = false, Height = 20, Text = "Finish colours in this quote:" });
            y += 22;

            var listBox = new ListBox { Left = 20, Top = y, Width = 390, Height = 110 };
            if (allFinishes != null)
            {
                foreach (var finish in allFinishes)
                    listBox.Items.Add(finish);
            }
            Controls.Add(listBox);
            y += 118;

            Controls.Add(new Label
            {
                Left = 20, Top = y, Width = 390, AutoSize = false, Height = 20,
                Text = "Do you want to edit the finish colours?"
            });
            y += 30;

            var btnYes = new Button { Text = "Yes", Left = 230, Top = y, Width = 80 };
            var btnNo  = new Button { Text = "No",  Left = 320, Top = y, Width = 80, DialogResult = DialogResult.OK };

            btnYes.Click += (_, _) =>
            {
                EditFinishColours = true;
                DialogResult = DialogResult.OK;
                Close();
            };

            Controls.AddRange(new Control[] { btnYes, btnNo });
            CancelButton = btnNo;
        }
    }
}
