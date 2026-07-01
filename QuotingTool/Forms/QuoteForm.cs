using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    /// <summary>
    /// Tab 2 – Quote
    /// Shows all quote lines in a DataGridView with page selector and action buttons.
    /// Column headers match the 2.Quote sheet (row 11) from the .xlsm:
    ///   C=Section, D=Description, E=Alloy, F=Temper, G=Qty, H=Length(m),
    ///   I=$ Each, J=$ Total, K=Slotting/Sundry Charge(ea),
    ///   L=Ignore Finish, M=SAP Colour Code, V=SS?
    /// </summary>
    public partial class QuoteForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly PageSetupService _pageSetup;
        private readonly LengthFixService _lengthFix;
        private readonly ResetPriceService _resetPrice;
        private readonly ExportQuoteService _exportQuote;
        private int _projectId;

        public QuoteForm(DatabaseContext db)
        {
            _db          = db;
            _pageSetup   = new PageSetupService(db);
            _lengthFix   = new LengthFixService(db);
            _resetPrice  = new ResetPriceService(db);
            _exportQuote = new ExportQuoteService(db);
            InitializeComponent();
            ConfigureGridColumns();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            RefreshGrid();
            UpdatePrintAreaLabel();
        }

        private void RefreshGrid()
        {
            if (_projectId <= 0) return;
            var lines = new System.Collections.Generic.List<QuoteLine>(_db.GetQuoteLines(_projectId));
            gridQuote.DataSource = lines;
        }

        private void UpdatePrintAreaLabel()
        {
            if (_projectId <= 0) return;
            lblPrintArea.Text = _pageSetup.GetPrintAreaDescription(_projectId);
        }

        /// <summary>
        /// Configures DataGridView column headers to match the 2.Quote sheet column names.
        /// Auto-binding from QuoteLine model properties shows all columns;
        /// this method sets friendly headers and hides internal-only columns.
        /// </summary>
        private void ConfigureGridColumns()
        {
            gridQuote.AutoGenerateColumns = true;
            gridQuote.DataBindingComplete += (s, e) =>
            {
                SetColHeader("PageBlock",         "Page",          50);
                SetColHeader("RowInBlock",        "Ln",            40);
                SetColHeader("ExtrusionCode",     "Section (C)",  120);
                SetColHeader("Description",       "Description (D)", 250);
                SetColHeader("Alloy",             "Alloy (E)",     70);
                SetColHeader("Temper",            "Temper (F)",    70);
                SetColHeader("Quantity",          "Qty (G)",       60);
                SetColHeader("LengthM",           "Length m (H)",  90);
                SetColHeader("Price",             "$ Each (I)",    90);
                SetColHeader("SellPrice",         "$ Total (J)",   90);
                SetColHeader("SubsillSurcharge",  "Slotting (K)",  90);
                SetColHeader("IgnoreFinish",      "Ign Fin (L)",   80);
                SetColHeader("SapColourCode",     "SAP Code (M)",  90);
                SetColHeader("SubsillFlag",       "SS? (V)",       60);
                // Hide internal/Id columns
                HideCol("Id");
                HideCol("ProjectId");
            };
        }

        private void SetColHeader(string propName, string header, int width)
        {
            if (gridQuote.Columns.Contains(propName))
            {
                gridQuote.Columns[propName].HeaderText = header;
                gridQuote.Columns[propName].Width = width;
            }
        }

        private void HideCol(string propName)
        {
            if (gridQuote.Columns.Contains(propName))
                gridQuote.Columns[propName].Visible = false;
        }

        private void cmbPageSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int pageCount = cmbPageSelector.SelectedIndex + 1;
            _pageSetup.SetPageLayout(_projectId, pageCount);
            UpdatePrintAreaLabel();
        }

        private void btnFixLengths_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int fixed_ = _lengthFix.FixLengths(_projectId);
            MessageBox.Show($"Fixed {fixed_} length(s) from mm → m.", "Fix Lengths",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshGrid();
        }

        private void btnResetPrices_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int reset = _resetPrice.ResetPriceFormulas(_projectId);
            MessageBox.Show($"Reset {reset} price formula(s).", "Reset Prices",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshGrid();
        }

        private void btnExportQuote_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            var result = _exportQuote.ExportQuoteData(_projectId);
            if (result.Status != ExportStatus.OK)
                MessageBox.Show(result.Message, "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            using var sfd = new SaveFileDialog
            {
                Filter   = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                FileName = $"Quote_{DateTime.Today:yyyyMMdd}",
                DefaultExt = "xlsx"
            };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                var exporter = new Helpers.ExcelExporter(_db);
                if (sfd.FilterIndex == 1) exporter.ExportToExcel(_projectId, sfd.FileName);
                else exporter.ExportToCsv(_projectId, sfd.FileName);
                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
