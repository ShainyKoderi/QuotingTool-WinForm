using System.Windows.Forms;

namespace QuotingTool.Forms
{
    public sealed class QuotePdfOptionsDialog : Form
    {
        private readonly CheckBox _chkIncludeLetter;
        private readonly CheckBox _chkUnpriced;
        private readonly CheckBox _chkIncludeLocalLogo;
        private readonly CheckBox _chkAddMillFinishVersion;

        public bool IncludeQuoteLetter => _chkIncludeLetter.Checked;
        public bool Unpriced => _chkUnpriced.Checked;
        public bool IncludeLocalLogo => _chkIncludeLocalLogo.Checked;
        public bool AddMillFinishVersion => _chkAddMillFinishVersion.Checked;

        public QuotePdfOptionsDialog(bool hasLocalLogo = false)
        {
            Text = "Quote PDF Options";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 320;
            Height = 240;

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

            _chkIncludeLocalLogo = new CheckBox
            {
                Left = 20,
                Top = 80,
                Width = 260,
                Text = "Add LocAl logo to Quote Letter",
                Checked = hasLocalLogo,
                Enabled = hasLocalLogo
            };

            _chkAddMillFinishVersion = new CheckBox
            {
                Left = 20,
                Top = 110,
                Width = 260,
                Text = "Add Mill Finish version"
            };

            var btnOk = new Button
            {
                Text = "Export",
                DialogResult = DialogResult.OK,
                Left = 120,
                Width = 80,
                Top = 150
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Left = 205,
                Width = 80,
                Top = 150
            };

            Controls.AddRange(new Control[] { _chkIncludeLetter, _chkUnpriced, _chkIncludeLocalLogo, _chkAddMillFinishVersion, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}
