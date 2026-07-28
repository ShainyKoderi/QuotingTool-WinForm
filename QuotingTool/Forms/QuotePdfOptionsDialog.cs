using System.Windows.Forms;

namespace QuotingTool.Forms
{
    public sealed class QuotePdfOptionsDialog : Form
    {
        private readonly CheckBox _chkIncludeLetter;
        private readonly CheckBox _chkUnpriced;

        public bool IncludeQuoteLetter => _chkIncludeLetter.Checked;
        public bool Unpriced => _chkUnpriced.Checked;

        public QuotePdfOptionsDialog()
        {
            Text = "Quote PDF Options";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 320;
            Height = 180;

            _chkIncludeLetter = new CheckBox
            {
                Left = 20,
                Top = 20,
                Width = 240,
                Text = "Include Quote Letter",
                Checked = true
            };

            _chkUnpriced = new CheckBox
            {
                Left = 20,
                Top = 50,
                Width = 240,
                Text = "Unpriced version"
            };

            var btnOk = new Button
            {
                Text = "Export",
                DialogResult = DialogResult.OK,
                Left = 120,
                Width = 80,
                Top = 90
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Left = 205,
                Width = 80,
                Top = 90
            };

            Controls.AddRange(new Control[] { _chkIncludeLetter, _chkUnpriced, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}
