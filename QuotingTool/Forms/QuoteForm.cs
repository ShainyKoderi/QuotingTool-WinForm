using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class QuoteForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly PageSetupService _pageSetup;
        private readonly LengthFixService _lengthFix;
        private readonly ResetPriceService _resetPrice;
        private readonly ExportQuoteService _exportQuote;
        private readonly SapExportService _sapExport;
        private int _projectId;
        private bool _bindingGrid;

        public QuoteForm(DatabaseContext db)
        {
            _db = db;
            _pageSetup = new PageSetupService(db);
            _lengthFix = new LengthFixService(db);
            _resetPrice = new ResetPriceService(db);
            _exportQuote = new ExportQuoteService(db);
            _sapExport = new SapExportService(db);
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
            _bindingGrid = true;
            var lines = new List<QuoteLine>(_db.GetQuoteLines(_projectId));
            foreach (var line in lines)
            {
                if (line.LineNumber <= 0)
                    line.LineNumber = line.RowInBlock;
                line.TotalPrice = line.Quantity * line.LengthM * line.Price;
            }
            gridQuote.DataSource = lines;
            _bindingGrid = false;
        }

        private void UpdatePrintAreaLabel()
        {
            if (_projectId <= 0) return;
            lblPrintArea.Text = _pageSetup.GetPrintAreaDescription(_projectId);
        }

        private void ConfigureGridColumns()
        {
            gridQuote.AutoGenerateColumns = true;
            gridQuote.DataBindingComplete += (s, e) =>
            {
                SetColHeader("PageBlock", "Page", 50, false);
                SetColHeader("LineNumber", "Ln", 50, false);
                SetColHeader("RowInBlock", "Row", 50, true);
                SetColHeader("ExtrusionCode", "Section", 110, false);
                SetColHeader("Description", "Description", 250, false);
                SetColHeader("Alloy", "Alloy", 70, false);
                SetColHeader("Temper", "Temper", 70, false);
                SetColHeader("Quantity", "Qty", 60, false);
                SetColHeader("LengthM", "Length (m)", 90, false);
                SetColHeader("Price", "$ Each", 90, false);
                SetColHeader("TotalPrice", "$ Total", 90, false);
                SetColHeader("SellPrice", "Sell Price", 90, true);
                SetColHeader("SubsillSurcharge", "Slotting (K)", 90, false);
                SetColHeader("IgnoreFinish", "Ign Fin", 80, false);
                SetColHeader("SapColourCode", "SAP Code", 90, false);
                SetColHeader("SubsillFlag", "SS?", 60, false);
                HideCol("Id");
                HideCol("ProjectId");
                if (gridQuote.Columns.Contains("TotalPrice"))
                    gridQuote.Columns["TotalPrice"].ReadOnly = true;
            };
        }

        private void SetColHeader(string propName, string header, int width, bool hide)
        {
            if (!gridQuote.Columns.Contains(propName)) return;
            var column = gridQuote.Columns[propName];
            column.HeaderText = header;
            column.Width = width;
            column.Visible = !hide;
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
            int fixedCount = _lengthFix.FixLengths(_projectId);
            MessageBox.Show($"Fixed {fixedCount} length(s) from mm → m.", "Fix Lengths",
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
                Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
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

        private void btnCopyForSap_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            int count = _sapExport.CopyToClipboard(_projectId);
            MessageBox.Show($"{count} lines copied to clipboard for SAP", "Copy for SAP",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSundryItems_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            using var form = new SundryItemsForm(_db);
            form.LoadProject(_projectId);
            form.ShowDialog(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gridQuote.IsCurrentCellDirty)
                gridQuote.CommitEdit(DataGridViewDataErrorContexts.Commit);
            gridQuote.EndEdit();

            var lines = (gridQuote.DataSource as IEnumerable<QuoteLine>)?.ToList()
                ?? _db.GetQuoteLines(_projectId).ToList();

            foreach (var line in lines)
            {
                line.ProjectId = _projectId;
                line.LineNumber = line.LineNumber <= 0 ? line.RowInBlock : line.LineNumber;
                line.TotalPrice = line.Quantity * line.LengthM * line.Price;
                _db.UpsertQuoteLine(line);
            }

            MessageBox.Show("Quote saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshGrid();
        }

        private void gridQuote_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridQuote.IsCurrentCellDirty)
                gridQuote.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridQuote_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingGrid || e.RowIndex < 0 || _projectId <= 0) return;
            if (gridQuote.Rows[e.RowIndex].DataBoundItem is not QuoteLine line) return;

            line.LineNumber = line.LineNumber <= 0 ? e.RowIndex + 1 : line.LineNumber;
            line.TotalPrice = line.Quantity * line.LengthM * line.Price;
            if (gridQuote.Columns.Contains("TotalPrice"))
                gridQuote.Rows[e.RowIndex].Cells["TotalPrice"].Value = line.TotalPrice;
            _db.UpsertQuoteLine(line);
        }
    }
}
