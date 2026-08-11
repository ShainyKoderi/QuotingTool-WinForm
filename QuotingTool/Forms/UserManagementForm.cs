using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public sealed class UserManagementForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly BindingList<User> _users = new();
        private readonly DataGridView _grid;
        private readonly TextBox _txtNewPassword;

        public UserManagementForm(DatabaseContext db)
        {
            _db = db;
            Text = "User Management";
            Width = 900;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false
            };
            _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(User.Username), HeaderText = "Username", Width = 160 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(User.DisplayName), HeaderText = "Display Name", Width = 200 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(User.Email), HeaderText = "Email", Width = 200 });
            _grid.Columns.Add(new DataGridViewComboBoxColumn
            {
                DataPropertyName = nameof(User.Region),
                HeaderText = "Region",
                Width = 90,
                DataSource = new string[] { "", "NQ", "NSW", "NT", "SA", "SEQ", "TAS", "VIC", "WA" }
            });
            _grid.Columns.Add(new DataGridViewComboBoxColumn
            {
                DataPropertyName = nameof(User.Role),
                HeaderText = "Role",
                Width = 140,
                DataSource = Enum.GetValues(typeof(UserRole))
            });
            _grid.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = nameof(User.IsActive), HeaderText = "Active", Width = 70 });

            var pnl = new Panel { Dock = DockStyle.Top, Height = 52 };
            var btnAdd = new Button { Left = 12, Top = 12, Width = 90, Text = "Add User" };
            var btnSave = new Button { Left = 108, Top = 12, Width = 110, Text = "Save Changes" };
            _txtNewPassword = new TextBox { Left = 236, Top = 14, Width = 180, UseSystemPasswordChar = true, PlaceholderText = "New password" };
            var btnSetPassword = new Button { Left = 422, Top = 12, Width = 120, Text = "Set Password" };
            var btnClose = new Button { Left = 548, Top = 12, Width = 90, Text = "Close" };

            btnAdd.Click += (_, _) => _users.Add(new User { IsActive = true, Role = UserRole.GeneralUser });
            btnSave.Click += (_, _) => SaveChanges();
            btnSetPassword.Click += (_, _) => SetPassword();
            btnClose.Click += (_, _) => Close();

            pnl.Controls.AddRange(new Control[] { btnAdd, btnSave, _txtNewPassword, btnSetPassword, btnClose });

            Controls.Add(_grid);
            Controls.Add(pnl);
            LoadUsers();
        }

        private void LoadUsers()
        {
            _users.Clear();
            foreach (var user in _db.GetUsers())
                _users.Add(user);
            _grid.DataSource = _users;
        }

        private void SaveChanges()
        {
            var newPassword = _txtNewPassword.Text.Trim();
            bool hasNewUsers = _users.Any(u => u.Id == 0 && !string.IsNullOrWhiteSpace(u.Username));
            if (hasNewUsers && !AuthService.IsStrongPassword(newPassword))
            {
                MessageBox.Show(this,
                    "Enter a strong password in 'New password' before saving new users (8+ chars, upper, lower, number, symbol).",
                    "User Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            foreach (var user in _users)
            {
                if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.DisplayName))
                    continue;

                if (user.Id == 0)
                {
                    if (_db.GetUserByUsername(user.Username.Trim()) != null)
                        continue;

                    user.Username = user.Username.Trim();
                    user.DisplayName = user.DisplayName.Trim();
                    AuthService.CreatePasswordHash(newPassword, out var hash, out var salt);
                    user.PasswordHash = hash;
                    user.PasswordSalt = salt;
                    _db.InsertUser(user);
                }
                else
                {
                    user.DisplayName = user.DisplayName.Trim();
                    user.Email = user.Email?.Trim() ?? string.Empty;
                    _db.UpdateUser(user);
                }
            }

            _txtNewPassword.Text = string.Empty;
            MessageBox.Show(this, "Users saved.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadUsers();
        }

        private void SetPassword()
        {
            if (_grid.CurrentRow?.DataBoundItem is not User selected || selected.Id <= 0)
                return;

            var newPassword = _txtNewPassword.Text.Trim();
            if (!AuthService.IsStrongPassword(newPassword))
            {
                MessageBox.Show(this, "Password must be at least 8 characters and include upper, lower, number, and symbol.",
                    "User Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AuthService.CreatePasswordHash(newPassword, out var hash, out var salt);
            _db.UpdateUserPassword(selected.Id, hash, salt);
            _txtNewPassword.Text = string.Empty;
            MessageBox.Show(this, $"Password updated for {selected.Username}.", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
