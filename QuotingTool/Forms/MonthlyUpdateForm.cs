using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using QuotingTool.Data;
using QuotingTool.Helpers;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Tab 5 – Monthly Update
    /// Manages the monthly LME Rate / Billet Premium revision cycle.
    /// </summary>
    public partial class MonthlyUpdateForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly MonthlyUpdateService _monthlyUpdate;
        private readonly ProtectionHelper _protection;
        private readonly string _monthlyUpdatePassword;
        private int _projectId;
        private bool _isUnlocked;

        public MonthlyUpdateForm(DatabaseContext db)
        {
            _db = db;
            _protection    = new ProtectionHelper(db);
            _monthlyUpdate = new MonthlyUpdateService(db, _protection);
            _monthlyUpdatePassword = LoadMonthlyUpdatePassword();
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            if (UserSession.IsAdministrator)
            {
                SetMaintenanceVisibility(true);
                btnUnlock.Visible = false;
            }
            else
            {
                SetMaintenanceVisibility(false);
                btnUnlock.Visible = true;
            }
            RefreshPricingMatrix();

            // Pre-populate from latest revision
            var rev = _db.GetLatestRevision();
            if (rev != null)
            {
                dtpRevisionDate.Value = rev.RevisionDate ?? DateTime.Today;
                txtLMERate.Text       = rev.LMERate.ToString("F4");
                txtBilletPremium.Text = rev.BilletPremium.ToString("F4");
            }
        }

        private void RefreshPricingMatrix()
        {
            var matrix = new System.Collections.Generic.List<PricingMatrix>(_db.GetPricingMatrix());
            gridPricingMatrix.DataSource = matrix;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!EnsureUnlocked()) return;
            if (_projectId <= 0) { MessageBox.Show("No project open."); return; }
            var sheets = _monthlyUpdate.OpenMonthlyUpdate(_projectId);
            lblStatus.Text = $"Unlocked: {string.Join(", ", sheets)}";
            MessageBox.Show($"Maintenance sheets unlocked:\n  • {string.Join("\n  • ", sheets)}",
                "Monthly Update Opened", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!EnsureUnlocked()) return;
            if (_projectId <= 0) { MessageBox.Show("No project open."); return; }

            if (!decimal.TryParse(txtLMERate.Text, out decimal lme) || lme <= 0)
            {
                MessageBox.Show("Please enter a valid LME Rate.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtBilletPremium.Text, out decimal billet) || billet < 0)
            {
                MessageBox.Show("Please enter a valid Billet Premium.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _monthlyUpdate.CloseMonthlyUpdate(_projectId, dtpRevisionDate.Value.Date, lme, billet);
            lblStatus.Text = $"Locked. Rates saved: LME={lme:F4}, Billet={billet:F4}, Date={dtpRevisionDate.Value:d}";
            MessageBox.Show("Monthly update complete. Sheets locked and rates saved.",
                "Monthly Update Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (!PromptForPassword())
                MessageBox.Show("Incorrect password.", "Monthly Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool EnsureUnlocked() => _isUnlocked || PromptForPassword();

        private bool PromptForPassword()
        {
            if (UserSession.IsAdministrator)
                return true;

            if (string.IsNullOrWhiteSpace(_monthlyUpdatePassword))
            {
                MessageBox.Show(
                    "Monthly update password is not configured. Set QUOTINGTOOL_MONTHLY_UPDATE_PASSWORD (takes precedence) or Security:MonthlyUpdatePassword in appsettings.json.",
                    "Monthly Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using var prompt = new Form
            {
                Width = 320,
                Height = 150,
                Text = "Monthly Update Password",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var label = new Label { Left = 12, Top = 15, Width = 280, Text = "Enter the monthly update password:" };
            var input = new TextBox { Left = 12, Top = 40, Width = 280, UseSystemPasswordChar = true };
            var ok = new Button { Left = 136, Top = 75, Width = 75, Text = "OK", DialogResult = DialogResult.OK };
            var cancel = new Button { Left = 217, Top = 75, Width = 75, Text = "Cancel", DialogResult = DialogResult.Cancel };
            prompt.Controls.AddRange(new Control[] { label, input, ok, cancel });
            prompt.AcceptButton = ok;
            prompt.CancelButton = cancel;

            if (prompt.ShowDialog(this) != DialogResult.OK || input.Text != _monthlyUpdatePassword)
                return false;

            SetMaintenanceVisibility(true);
            lblStatus.Text = "Status: admin settings unlocked";
            return true;
        }

        private void SetMaintenanceVisibility(bool isUnlocked)
        {
            _isUnlocked = isUnlocked;
            txtLMERate.Visible = isUnlocked;
            txtBilletPremium.Visible = isUnlocked;
            lblLMERate.Visible = isUnlocked;
            lblBillet.Visible = isUnlocked;
            grpMatrix.Visible = isUnlocked;
            btnOpen.Enabled = isUnlocked;
            btnClose.Enabled = isUnlocked;
            if (!isUnlocked)
                lblStatus.Text = "Status: admin settings hidden until unlocked";
        }

        private static string LoadMonthlyUpdatePassword()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            return Environment.GetEnvironmentVariable("QUOTINGTOOL_MONTHLY_UPDATE_PASSWORD")
                ?? config["Security:MonthlyUpdatePassword"]
                ?? string.Empty;
        }
    }
}
