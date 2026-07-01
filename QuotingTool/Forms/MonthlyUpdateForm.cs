using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        private int _projectId;

        public MonthlyUpdateForm(DatabaseContext db)
        {
            _db = db;
            _protection    = new ProtectionHelper(db);
            _monthlyUpdate = new MonthlyUpdateService(db, _protection);
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
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
            if (_projectId <= 0) { MessageBox.Show("No project open."); return; }
            var sheets = _monthlyUpdate.OpenMonthlyUpdate(_projectId);
            lblStatus.Text = $"Unlocked: {string.Join(", ", sheets)}";
            MessageBox.Show($"Maintenance sheets unlocked:\n  • {string.Join("\n  • ", sheets)}",
                "Monthly Update Opened", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
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
    }
}
