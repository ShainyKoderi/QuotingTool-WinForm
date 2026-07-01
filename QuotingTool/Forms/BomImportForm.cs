using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Tab 4 – BOM Import
    /// Browse CSV, preview, import, distribute to quote, analyse finishes.
    /// </summary>
    public partial class BomImportForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly BomService _bom;
        private readonly ImportPricesService _importPrices;
        private int _projectId;

        public BomImportForm(DatabaseContext db)
        {
            _db           = db;
            _bom          = new BomService(db);
            _importPrices = new ImportPricesService(db);
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            RefreshGrid();
            UpdateLabels();
        }

        private void RefreshGrid()
        {
            if (_projectId <= 0) return;
            var lines = new System.Collections.Generic.List<BomLine>(_db.GetBomLines(_projectId));
            gridBom.DataSource = lines;
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            if (_projectId <= 0) return;
            var lines = new System.Collections.Generic.List<BomLine>(_db.GetBomLines(_projectId));
            int rowCount = lines.Count;
            int pages = BomService.DeterminePageCount(rowCount);
            string mostCommon = _bom.GetMostCommonFinish(_projectId);
            lblRowCount.Text      = $"Rows: {rowCount}";
            lblPagesNeeded.Text   = $"Pages needed: {pages}";
            lblMostCommonFinish.Text = $"Most common finish: {(string.IsNullOrEmpty(mostCommon) ? "—" : mostCommon)}";
        }

        private void btnBrowseCsv_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                Title  = "Select BOM CSV File"
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                txtCsvPath.Text = ofd.FileName;
            }
        }

        private void btnImportDistribute_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) { MessageBox.Show("No project open."); return; }
            if (string.IsNullOrWhiteSpace(txtCsvPath.Text))
            {
                MessageBox.Show("Please select a CSV file first.", "No File",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int imported = _bom.ImportBomFromCsv(_projectId, txtCsvPath.Text);
                int distributed = _bom.DistributeBomToQuote(_projectId);
                RefreshGrid();
                MessageBox.Show(
                    $"Imported {imported} BOM row(s).\nDistributed {distributed} row(s) to Quote.",
                    "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Import failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetFinishes_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            var finishes = _bom.GetUniqueFinishes(_projectId);
            string most  = _bom.GetMostCommonFinish(_projectId);
            string list  = finishes.Count > 0 ? string.Join("\n  • ", finishes) : "(none)";
            MessageBox.Show(
                $"Unique finishes ({finishes.Count}):\n  • {list}\n\nMost common: {most}",
                "Finish Analysis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateLabels();
        }

        private void btnImportPrices_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int updated = _importPrices.ImportPricesToQuote(_projectId);
            MessageBox.Show($"Copied {updated} price(s) from BOM to Quote.",
                "Import Prices", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
