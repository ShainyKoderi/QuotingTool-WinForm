using System;
using System.Windows.Forms;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public sealed class LoginForm : Form
    {
        private readonly AuthService _authService;
        private readonly TextBox _txtUsername;
        private readonly TextBox _txtPassword;

        public LoginForm(AuthService authService)
        {
            _authService = authService;
            Text = "QuotingTool Login";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 380;
            Height = 220;

            Controls.Add(new Label { Left = 20, Top = 25, Width = 100, Text = "Username" });
            Controls.Add(new Label { Left = 20, Top = 65, Width = 100, Text = "Password" });

            _txtUsername = new TextBox { Left = 130, Top = 22, Width = 210 };
            _txtPassword = new TextBox { Left = 130, Top = 62, Width = 210, UseSystemPasswordChar = true };

            var btnLogin = new Button { Text = "Login", Left = 184, Top = 110, Width = 75 };
            var btnCancel = new Button { Text = "Cancel", Left = 265, Top = 110, Width = 75 };

            btnLogin.Click += (_, _) => TryLogin();
            btnCancel.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.AddRange(new Control[] { _txtUsername, _txtPassword, btnLogin, btnCancel });
            AcceptButton = btnLogin;
            CancelButton = btnCancel;
        }

        private void TryLogin()
        {
            var user = _authService.Authenticate(_txtUsername.Text, _txtPassword.Text);
            if (user == null)
            {
                MessageBox.Show(this, "Invalid username or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserSession.SetCurrentUser(user);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
