using System;
using System.ComponentModel;
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
        private const int MaxQuotePages = 6;
        private const int MaxRowsPerPage = 40;
        private const int MaxQuoteLines = MaxQuotePages * MaxRowsPerPage;
        private BindingList<QuoteLine> _lines = new();
        private int _projectId;
        private bool _bindingGrid;
        private Dictionary<string, ExtrusionMaster> _extrusionMasterByCode = new(StringComparer.OrdinalIgnoreCase);
        private decimal _metalPricePerKg;

        public QuoteForm(DatabaseContext db)
        {
            _db = db;
            _pageSetup = new PageSetupService(db);
            _lengthFix = new LengthFixService(db);
            _resetPrice = new ResetPriceService(db);
            _exportQuote = new ExportQuoteService(db);
            _sapExport = new SapExportService(db);
            InitializeComponent();
            LoadExtrusionMaster();
            ConfigureGridColumns();
        }

        private void LoadExtrusionMaster()
        {
            // Section is the primary key in ExtrusionMaster; guard defensively against
            // any case-insensitive collisions when building the case-insensitive lookup.
            _extrusionMasterByCode = new Dictionary<string, ExtrusionMaster>(StringComparer.OrdinalIgnoreCase);
            foreach (var master in _db.GetExtrusionMasterList())
                _extrusionMasterByCode[master.Section] = master;
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            _metalPricePerKg = _db.GetEditPricing(projectId)?.MetalPrice ?? 0m;
            RefreshGrid();
            UpdatePrintAreaLabel();
        }

        private void RefreshGrid()
        {
            if (_projectId <= 0) return;
            _bindingGrid = true;
            _lines = new BindingList<QuoteLine>(_db.GetQuoteLines(_projectId).ToList());
            foreach (var line in _lines)
            {
                if (line.LineNumber <= 0)
                    line.LineNumber = line.RowInBlock;
                ApplySectionLookup(line);
                RecalculatePrice(line);
            }
            gridQuote.DataSource = _lines;
            _bindingGrid = false;
        }

        /// <summary>
        /// Auto-loads Description/Alloy/Temper for a quote line based on its selected
        /// Section (ExtrusionCode), mirroring the Table5 VLOOKUP formulas in the workbook.
        /// </summary>
        private void ApplySectionLookup(QuoteLine line)
        {
            if (string.IsNullOrWhiteSpace(line.ExtrusionCode))
            {
                line.Description = string.Empty;
                return;
            }

            if (!_extrusionMasterByCode.TryGetValue(line.ExtrusionCode, out var master))
                return;

            line.Description = master.Description;
            if (string.IsNullOrWhiteSpace(line.Alloy))
                line.Alloy = master.DefaultAlloy;
            if (string.IsNullOrWhiteSpace(line.Temper))
                line.Temper = master.DefaultTemper;
        }

        /// <summary>
        /// $ Each is calculated from Length: MassPerM (kg/m, from the Section master) ×
        /// Length (m) × the project's metal sell price ($/kg from Edit Pricing).
        /// </summary>
        private void RecalculatePrice(QuoteLine line)
        {
            var millFinishEach = CalculateMillFinishEach(line);
            var slottingSundryEach = line.SubsillSurcharge;

            line.Price = FormulaCalculationService.CalculatePriceEach(
                millFinishEach,
                ipEach: 0m,
                sfSellEach: 0m,
                slottingSundryEach: slottingSundryEach);

            line.TotalPrice = FormulaCalculationService.CalculateSellTotal(line.Price, line.Quantity);
            // Keep legacy SellPrice (col J) synchronized with TotalPrice so existing
            // exports/queries that still read SellPrice receive the recalculated value.
            line.SellPrice = line.TotalPrice;
        }

        private decimal CalculateMillFinishEach(QuoteLine line)
        {
            if (string.IsNullOrWhiteSpace(line.ExtrusionCode)) return 0m;
            if (!_extrusionMasterByCode.TryGetValue(line.ExtrusionCode, out var master)) return 0m;
            if (master.MassPerM <= 0 || line.LengthM <= 0 || _metalPricePerKg <= 0) return 0m;
            return master.MassPerM * line.LengthM * _metalPricePerKg;
        }

        private void UpdatePrintAreaLabel()
        {
            if (_projectId <= 0) return;
            lblPrintArea.Text = _pageSetup.GetPrintAreaDescription(_projectId);
        }

        private void ConfigureGridColumns()
        {
            gridQuote.AutoGenerateColumns = true;
            gridQuote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            gridQuote.DataError += (s, e) =>
            {
                // The Section combo box only accepts values from ExtrusionMaster; suppress
                // the formatting exception for legacy/custom codes not in that list, but keep
                // the row's typed value instead of silently discarding it.
                e.ThrowException = false;
                System.Diagnostics.Debug.WriteLine(
                    $"gridQuote DataError at row {e.RowIndex}, col {e.ColumnIndex}: {e.Exception?.Message}");
            };
            gridQuote.DataBindingComplete += (s, e) =>
            {
                EnsureSectionComboColumn();
                SetColHeader("PageBlock", "Page", 50, false);
                SetColHeader("LineNumber", "Ln", 50, false);
                SetColHeader("RowInBlock", "Row", 50, true);
                SetColHeader("ExtrusionCode", "Section", 110, false);
                SetColHeader("Description", "Description", 250, false);
                SetColHeader("Alloy", "Alloy", 70, false);
                SetColHeader("Temper", "Temper", 70, false);
                SetColHeader("Quantity", "Qty", 70, false);
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
                // Description/Alloy/Temper auto-load from the Section master; $ Each and
                // $ Total are calculated from Length, so none of these are manually edited.
                SetReadOnly("Description", true);
                SetReadOnly("Alloy", true);
                SetReadOnly("Temper", true);
                SetReadOnly("Price", true);
                SetReadOnly("TotalPrice", true);

                SetColFormat("Quantity", "0.##", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("LengthM", "0.###", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("Price", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("TotalPrice", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("SubsillSurcharge", "0.0000", DataGridViewContentAlignment.MiddleRight);
            };
        }

        /// <summary>
        /// Replaces the auto-generated Section (ExtrusionCode) text column with a dropdown
        /// (ComboBox) column populated from the ExtrusionMaster list, so users pick a valid
        /// Section instead of typing it freehand.
        /// </summary>
        private void EnsureSectionComboColumn()
        {
            if (!gridQuote.Columns.Contains("ExtrusionCode")) return;
            if (gridQuote.Columns["ExtrusionCode"] is DataGridViewComboBoxColumn) return;

            int index = gridQuote.Columns["ExtrusionCode"].Index;
            gridQuote.Columns.Remove("ExtrusionCode");

            var combo = new DataGridViewComboBoxColumn
            {
                Name = "ExtrusionCode",
                DataPropertyName = "ExtrusionCode",
                HeaderText = "Section",
                Width = 110,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Standard,
                Sorted = true
            };
            combo.Items.AddRange(_extrusionMasterByCode.Keys.Cast<object>().ToArray());
            gridQuote.Columns.Insert(index, combo);
        }

        private void SetReadOnly(string propName, bool readOnly)
        {
            if (gridQuote.Columns.Contains(propName))
                gridQuote.Columns[propName].ReadOnly = readOnly;
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

        private void SetColFormat(string propName, string format, DataGridViewContentAlignment alignment)
        {
            if (!gridQuote.Columns.Contains(propName)) return;
            var style = gridQuote.Columns[propName].DefaultCellStyle;
            style.Format = format;
            style.Alignment = alignment;
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

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;

            int maxSlotIndex = _lines
                .Where(l => l.PageBlock > 0 && l.RowInBlock > 0)
                .Select(l => ((l.PageBlock - 1) * MaxRowsPerPage) + (l.RowInBlock - 1))
                .DefaultIfEmpty(-1)
                .Max();

            int nextIndex = maxSlotIndex + 1;
            if (nextIndex >= MaxQuoteLines)
            {
                MessageBox.Show("Maximum of 240 quote lines (6 pages) reached.", "Add Row",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var line = new QuoteLine
            {
                ProjectId = _projectId,
                PageBlock = (nextIndex / MaxRowsPerPage) + 1,
                RowInBlock = (nextIndex % MaxRowsPerPage) + 1,
                LineNumber = _lines.Select(l => l.LineNumber).DefaultIfEmpty(0).Max() + 1
            };

            _lines.Add(line);

            BeginInvoke(new Action(() =>
            {
                if (gridQuote.Rows.Count == 0 || gridQuote.Columns.Count == 0) return;
                if (gridQuote.Rows.Count != _lines.Count) return;
                int rowIndex = gridQuote.Rows.Count - 1;
                try
                {
                    DataGridViewColumn? firstVisibleColumn = null;
                    foreach (DataGridViewColumn column in gridQuote.Columns)
                    {
                        if (column.Visible)
                        {
                            firstVisibleColumn = column;
                            break;
                        }
                    }

                    if (firstVisibleColumn != null)
                    {
                        gridQuote.CurrentCell = gridQuote.Rows[rowIndex].Cells[firstVisibleColumn.Index];
                        gridQuote.FirstDisplayedScrollingRowIndex = rowIndex;
                    }
                }
                catch (InvalidOperationException)
                {
                    // Best-effort UX convenience only; ignore if the grid cannot
                    // accept a current-cell/scroll change (e.g. mid-layout, no visible columns, etc.)
                }
            }));
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            if (gridQuote.CurrentRow?.DataBoundItem is not QuoteLine line) return;

            if (line.Id > 0)
                _db.DeleteQuoteLine(line.Id);

            _lines.Remove(line);
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

            var lines = _lines.ToList();

            foreach (var line in lines)
            {
                line.ProjectId = _projectId;
                line.LineNumber = line.LineNumber <= 0 ? line.RowInBlock : line.LineNumber;
                ApplySectionLookup(line);
                RecalculatePrice(line);
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

            // Refresh metal price in case the Edit Pricing tab was saved since this tab loaded.
            _metalPricePerKg = _db.GetEditPricing(_projectId)?.MetalPrice ?? _metalPricePerKg;

            var columnName = gridQuote.Columns[e.ColumnIndex].Name;
            line.LineNumber = line.LineNumber <= 0 ? e.RowIndex + 1 : line.LineNumber;

            if (columnName == "ExtrusionCode")
                ApplySectionLookup(line);

            RecalculatePrice(line);

            var row = gridQuote.Rows[e.RowIndex];
            SetCellValue(row, "Description", line.Description);
            SetCellValue(row, "Alloy", line.Alloy);
            SetCellValue(row, "Temper", line.Temper);
            SetCellValue(row, "Price", line.Price);
            SetCellValue(row, "TotalPrice", line.TotalPrice);

            _db.UpsertQuoteLine(line);
        }

        private void SetCellValue(DataGridViewRow row, string columnName, object? value)
        {
            if (gridQuote.Columns.Contains(columnName))
                row.Cells[columnName].Value = value;
        }
    }
}
