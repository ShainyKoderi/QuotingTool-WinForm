using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly BomService _bom;
        private readonly QuotePdfService _quotePdf;
        private readonly Cre8ImportService _cre8Import;
        private readonly ImportPricesService _importPrices;
        private const int MaxQuotePages = 6;
        private const int MaxRowsPerPage = 40;
        private const int MaxQuoteLines = MaxQuotePages * MaxRowsPerPage;
        private const int QtyColumnWidth = 70;
        private const int LengthColumnWidth = 90;
        private const int PriceColumnWidth = 90;
        private const int TotalColumnWidth = 90;
        private const string MillFinishType = "Mill Finish";
        private BindingList<QuoteLine> _lines = new();
        private int _projectId;
        private bool _bindingGrid;
        private Dictionary<string, ExtrusionMaster> _extrusionMasterByCode = new(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, SundryItem> _sundryMasterByCode = new(StringComparer.OrdinalIgnoreCase);
        private decimal _metalPricePerKg;
        private decimal _finishingSellPricePerM2;
        // IP upcharge rates loaded from EditPricing (default to workbook constants when no project pricing exists)
        private decimal _ip1RatePerKg = 1.00m;
        private decimal _ip2RatePerKg = 2.00m;
        private decimal _ip3RatePerKg = 25.85m;
        // PH code → IP category (0-3) lookup, sourced from 5.Edit_PH_IP sheet via IPMasterData table
        private Dictionary<string, int> _ipMasterByCodes = new(StringComparer.Ordinal);
        // Section codes in the THRESHOLDS named range; used for the BF column threshold flag
        private HashSet<string> _thresholdSections = new(StringComparer.OrdinalIgnoreCase);
        private string _projectFinishSapCode = "NON";
        private string _thresholdChoice = "No";
        private string _projectFinishType = "Mill Finish";
        private decimal _millFinishMarginPct;
        private decimal _finishingMarginPct;
        private decimal _finishingCostPricePerM2;

        public QuoteForm(DatabaseContext db)
        {
            _db = db;
            _pageSetup = new PageSetupService(db);
            _lengthFix = new LengthFixService(db);
            _resetPrice = new ResetPriceService(db);
            _exportQuote = new ExportQuoteService(db);
            _sapExport = new SapExportService(db);
            _bom = new BomService(db);
            _quotePdf = new QuotePdfService(db);
            _cre8Import = new Cre8ImportService(db);
            _importPrices = new ImportPricesService(db);
            InitializeComponent();
            LoadExtrusionMaster();
            LoadSundryMaster();
            LoadIPMasterData();
            ConfigureGridColumns();
            gridQuote.CellToolTipTextNeeded += gridQuote_CellToolTipTextNeeded;
        }

        private void LoadExtrusionMaster()
        {
            // Section is the primary key in ExtrusionMaster; guard defensively against
            // any case-insensitive collisions when building the case-insensitive lookup.
            _extrusionMasterByCode = new Dictionary<string, ExtrusionMaster>(StringComparer.OrdinalIgnoreCase);
            foreach (var master in _db.GetExtrusionMasterList())
                _extrusionMasterByCode[master.Section] = master;
        }

        private void LoadIPMasterData()
        {
            try
            {
                _ipMasterByCodes = _db.GetIPMasterByCodes();
                _thresholdSections = _db.GetThresholdSections();
            }
            catch
            {
                // Tables may not yet exist on first launch before schema migration completes;
                // fall back to empty lookups so the form still opens.
                _ipMasterByCodes = new Dictionary<string, int>(StringComparer.Ordinal);
                _thresholdSections = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private void LoadSundryMaster()
        {
            _sundryMasterByCode = new Dictionary<string, SundryItem>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in _db.GetSundryItems())
            {
                if (!string.IsNullOrWhiteSpace(item.SectionCode))
                    _sundryMasterByCode[item.SectionCode.Trim()] = item;
            }
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            LoadProjectSettings();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            if (_projectId <= 0) return;
            LoadProjectSettings();
            LoadSundryMaster();
            _bindingGrid = true;
            _lines = new BindingList<QuoteLine>(_db.GetQuoteLines(_projectId).ToList());
            foreach (var line in _lines)
            {
                if (line.LineNumber <= 0)
                    line.LineNumber = line.RowInBlock;
                ApplySectionLookup(line);
                var storedPrice = line.Price;
                RecalculatePrice(line);
                if (_metalPricePerKg > 0 && line.Price != storedPrice && IsMeaningfulLine(line))
                    _db.UpsertQuoteLine(line);
            }
            EnsureTrailingBlankRow();
            gridQuote.DataSource = _lines;
            _bindingGrid = false;
            UpdatePageLayout();
            UpdateValidationStates();
            UpdatePriceBreakdown();
        }

        /// <summary>
        /// Auto-loads Description/Alloy/Temper/SubsillFlag/IpGrade for a quote line based on its
        /// selected Section (ExtrusionCode), mirroring the Table5 VLOOKUP formulas in the workbook.
        ///
        /// Col D formula (simplified): * prefix when TB type is PDB/PLS; base description from Table5[2];
        ///   " **MILL FINISH**" suffix when IgnoreFinish = "Yes".
        /// Col V formula: VLOOKUP(C, Table5[], 11) → subsill tier; SubsillFlag = tier > 0.
        /// Col AV formula: VLOOKUP(PH code, 5.Edit_PH_IP, col D) → IP category 0-3.
        /// </summary>
        private void ApplySectionLookup(QuoteLine line)
        {
            var sectionCode = NormalizeSectionCode(line.ExtrusionCode);
            if (string.IsNullOrWhiteSpace(sectionCode))
            {
                line.ExtrusionCode = string.Empty;
                line.Description = string.Empty;
                return;
            }
            line.ExtrusionCode = sectionCode;

            if (!_extrusionMasterByCode.TryGetValue(sectionCode, out var master))
            {
                if (_sundryMasterByCode.TryGetValue(sectionCode, out var sundry))
                {
                    // Sundry descriptions may be overwritten by the user (e.g. replace
                    // "Sundry description - 1" with "Airfreight"), so only default the
                    // description from the master when the line has none of its own.
                    if (string.IsNullOrWhiteSpace(line.Description))
                        line.Description = sundry.Description ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(line.Alloy))
                        line.Alloy = "6060";
                    if (string.IsNullOrWhiteSpace(line.Temper))
                        line.Temper = "T5";
                }
                return;
            }

            // Build Description: prepend * for thermal-break sections (PDB/PLS), then base description,
            // then append " **MILL FINISH**" when Ignore Finish is set to "Yes".
            var tbType = master.ThermalBreakType ?? "No";
            var descBase = master.Description ?? string.Empty;
            var prefix = (tbType == "PLS" || tbType == "PDB") ? "*" : string.Empty;
            var suffix = string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase)
                ? " **MILL FINISH**"
                : string.Empty;
            line.Description = prefix + descBase + suffix;

            if (string.IsNullOrWhiteSpace(line.Alloy))
                line.Alloy = master.DefaultAlloy;
            if (string.IsNullOrWhiteSpace(line.Temper))
                line.Temper = master.DefaultTemper;

            // Col V: SubsillFlag = tier > 0 (auto-filled from Table5 col K; user can still override).
            if (master.SubsillTier > 0)
                line.SubsillFlag = true;

            // Col AV: IP grade auto-computed from PH code via IPMasterData lookup.
            // IpGrade is stored so the user can override it if needed.
            if (!string.IsNullOrWhiteSpace(master.PhCode) &&
                _ipMasterByCodes.TryGetValue(master.PhCode, out var ipCat))
            {
                line.IpGrade = ipCat;
            }
        }

        /// <summary>
        /// $ Each is calculated from:
        ///   MF Each (AH) = kg/m × Length × MetalSellPrice (standard path)
        ///   IP Each (AI) = kg/m × Length × IP upcharge rate (based on IpGrade: 0=none,1=IP1,2=IP2,3=IP3)
        ///   SF Sell Each (AS) = finishing area × finishing sell rate (AP fallback to PP perimeter)
        ///   Slotting/Sundry ea (K) = user-entered SubsillSurcharge
        /// </summary>
        private void RecalculatePrice(QuoteLine line)
        {
            var millFinishEach = CalculateMillFinishEach(line);
            var ipEach = CalculateIpEach(line);
            var slottingSundryEach = line.SubsillSurcharge;
            var sfSellEach = CalculateFinishingEach(line);
            var calculatedPrice = FormulaCalculationService.CalculatePriceEach(
                millFinishEach,
                ipEach: ipEach,
                sfSellEach: sfSellEach,
                slottingSundryEach: slottingSundryEach);
            line.Price = line.ManualPriceOverride ?? calculatedPrice;
            line.TotalPrice = FormulaCalculationService.CalculateSellTotal(line.Price, line.Quantity);
            // Keep legacy SellPrice (col J) synchronized with TotalPrice so existing
            // exports/queries that still read SellPrice receive the recalculated value.
            line.SellPrice = line.TotalPrice;
            line.SapColourCode = FormulaCalculationService.CalculateSapColourCode(
                line.ExtrusionCode,
                _thresholdSections.Contains(line.ExtrusionCode),
                NormalizeThresholdChoice(_thresholdChoice),
                line.IgnoreFinish,
                _projectFinishSapCode);
        }

        private decimal CalculateMillFinishEach(QuoteLine line)
        {
            var massPerM = GetMassPerM(line.ExtrusionCode);
            if (massPerM <= 0 || line.LengthM <= 0 || _metalPricePerKg <= 0) return 0m;
            return massPerM * line.LengthM * _metalPricePerKg;
        }

        /// <summary>
        /// Col AI – IP Each = kg/m × Length (m) × IP upcharge rate ($/kg).
        /// Rate lookup: 0 → $0, 1 → IP1UpchargePerKg, 2 → IP2UpchargePerKg, 3 → IP3PanoramicFixedKgRate.
        /// </summary>
        private decimal CalculateIpEach(QuoteLine line)
        {
            if (line.IpGrade <= 0) return 0m;
            var massPerM = GetMassPerM(line.ExtrusionCode);
            if (massPerM <= 0 || line.LengthM <= 0) return 0m;

            var kgEach = massPerM * line.LengthM;
            return line.IpGrade switch
            {
                1 => kgEach * _ip1RatePerKg,
                2 => kgEach * _ip2RatePerKg,
                3 => kgEach * _ip3RatePerKg,
                _ => 0m
            };
        }

        private decimal CalculateFinishingEach(QuoteLine line)
        {
            if (string.Equals(_projectFinishType, MillFinishType, StringComparison.OrdinalIgnoreCase))
                return 0m;
            // d. IgnoreFinish="Yes" means this line is priced as Mill Finish regardless of the
            // project's finish type (mirrors the Excel col L / **MILL FINISH** override logic).
            if (string.Equals(line.IgnoreFinish, "Yes", StringComparison.OrdinalIgnoreCase))
                return 0m;
            if (_finishingSellPricePerM2 <= 0 || line.LengthM <= 0 || string.IsNullOrWhiteSpace(line.ExtrusionCode))
                return 0m;

            decimal perimeterMm = 0m;
            if (_extrusionMasterByCode.TryGetValue(line.ExtrusionCode, out var master))
                perimeterMm = master.ApMm > 0 ? master.ApMm : master.PpMm;
            else if (_sundryMasterByCode.TryGetValue(line.ExtrusionCode, out var sundry))
                perimeterMm = sundry.Ap > 0 ? sundry.Ap : sundry.Pp;

            if (perimeterMm <= 0)
                return 0m;

            var areaM2Each = (perimeterMm / 1000m) * line.LengthM;
            return areaM2Each * _finishingSellPricePerM2;
        }

        private decimal GetMassPerM(string? sectionCode)
        {
            if (string.IsNullOrWhiteSpace(sectionCode))
                return 0m;
            if (_extrusionMasterByCode.TryGetValue(sectionCode, out var master))
                return master.MassPerM;
            if (_sundryMasterByCode.TryGetValue(sectionCode, out var sundry))
                return sundry.MassKgPerM;
            return 0m;
        }

        private void ConfigureGridColumns()
        {
            gridQuote.AutoGenerateColumns = true;
            gridQuote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            gridQuote.DataError += (s, e) =>
            {
                e.ThrowException = false;
                System.Diagnostics.Debug.WriteLine(
                    $"gridQuote DataError at row {e.RowIndex}, col {e.ColumnIndex}: {e.Exception?.Message}");
            };
            gridQuote.DataBindingComplete += (s, e) =>
            {
                SetColHeader("PageBlock", "Page", 50, false);
                SetColHeader("LineNumber", "Ln", 50, false);
                SetColHeader("RowInBlock", "Row", 50, true);
                SetColHeader("ExtrusionCode", "Section", 110, false);
                SetColHeader("Description", "Description", 250, false);
                SetColHeader("Alloy", "Alloy", 70, false);
                SetColHeader("Temper", "Temper", 70, false);
                SetColHeader("Quantity", "Qty", QtyColumnWidth, false);
                SetColHeader("LengthM", "Length (m)", LengthColumnWidth, false);
                SetColHeader("IpGrade", "IP", 50, false);
                SetColHeader("ManualPriceOverride", "Override $ Each", 110, false);
                SetColHeader("Price", "$ Each", PriceColumnWidth, false);
                SetColHeader("TotalPrice", "$ Total", TotalColumnWidth, false);
                SetColHeader("SellPrice", "Sell Price", 90, true);
                SetColHeader("SubsillSurcharge", "Slotting (K)", 90, false);
                SetColHeader("IgnoreFinish", "Ign Fin", 80, false);
                SetColHeader("SapColourCode", "SAP Code", 90, false);
                SetColHeader("SubsillFlag", "SS?", 60, false);
                HideCol("Id");
                HideCol("ProjectId");
                // Description/Alloy/Temper auto-load from the Section master; $ Each and
                // $ Total are calculated from Length, so none of these are manually edited.
                SetReadOnly("Alloy", true);
                SetReadOnly("Temper", true);
                SetReadOnly("Price", true);
                SetReadOnly("TotalPrice", true);
                SetReadOnly("ManualPriceOverride", !UserSession.HasRole(UserRole.SalesManager));

                SetColFormat("Quantity", "0.##", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("LengthM", "0.###", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("ManualPriceOverride", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("Price", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("TotalPrice", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("SubsillSurcharge", "0.0000", DataGridViewContentAlignment.MiddleRight);

                if (gridQuote.Columns.Contains("Price"))
                    gridQuote.Columns["Price"].ToolTipText = "$ Each = MF Each + IP Each + SF Sell Each + Slotting/Sundry";
                if (gridQuote.Columns.Contains("TotalPrice"))
                    gridQuote.Columns["TotalPrice"].ToolTipText = "$ Total = $ Each × Qty";

                // Visually differentiate information-only columns from editable ones.
                foreach (DataGridViewColumn column in gridQuote.Columns)
                {
                    column.DefaultCellStyle.BackColor = column.ReadOnly
                        ? System.Drawing.Color.FromArgb(240, 240, 240)
                        : System.Drawing.Color.White;
                }
            };
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
            LoadSundryMaster();
            RefreshGrid();
        }

        private QuoteLine? CreateBlankLine()
        {
            int maxSlotIndex = _lines
                .Where(l => l.PageBlock > 0 && l.RowInBlock > 0)
                .Select(l => ((l.PageBlock - 1) * MaxRowsPerPage) + (l.RowInBlock - 1))
                .DefaultIfEmpty(-1)
                .Max();

            int nextIndex = maxSlotIndex + 1;
            if (nextIndex >= MaxQuoteLines)
                return null;

            return new QuoteLine
            {
                ProjectId = _projectId,
                PageBlock = (nextIndex / MaxRowsPerPage) + 1,
                RowInBlock = (nextIndex % MaxRowsPerPage) + 1,
                LineNumber = _lines.Select(l => l.LineNumber).DefaultIfEmpty(0).Max() + 1
            };
        }

        /// <summary>
        /// Keeps one empty entry row at the bottom of the grid at all times so new
        /// lines can be typed straight in without clicking "Add Row" for each one.
        /// </summary>
        private void EnsureTrailingBlankRow()
        {
            if (_projectId <= 0)
                return;
            if (_lines.Count > 0 && !IsMeaningfulLine(_lines[^1]))
                return;

            var blank = CreateBlankLine();
            if (blank != null)
                _lines.Add(blank);
        }

        //private void btnAddRow_Click(object sender, EventArgs e)
        //{
        //    if (_projectId <= 0) return;

        //    var line = CreateBlankLine();
        //    if (line == null)
        //    {
        //        MessageBox.Show("Maximum of 240 quote lines (6 pages) reached.", "Add Row",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    _lines.Add(line);

        //    BeginInvoke(new Action(() =>
        //    {
        //        if (gridQuote.Rows.Count == 0 || gridQuote.Columns.Count == 0) return;
        //        if (gridQuote.Rows.Count != _lines.Count) return;
        //        int rowIndex = gridQuote.Rows.Count - 1;
        //        try
        //        {
        //            DataGridViewColumn? firstVisibleColumn = null;
        //            foreach (DataGridViewColumn column in gridQuote.Columns)
        //            {
        //                if (column.Visible)
        //                {
        //                    firstVisibleColumn = column;
        //                    break;
        //                }
        //            }

        //            if (firstVisibleColumn != null)
        //            {
        //                gridQuote.CurrentCell = gridQuote.Rows[rowIndex].Cells[firstVisibleColumn.Index];
        //                gridQuote.FirstDisplayedScrollingRowIndex = rowIndex;
        //            }
        //        }
        //        catch (InvalidOperationException)
        //        {
        //            // Best-effort UX convenience only; ignore if the grid cannot
        //            // accept a current-cell/scroll change (e.g. mid-layout, no visible columns, etc.)
        //        }
        //    }));
        //    UpdatePageLayout();
        //}

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            if (gridQuote.CurrentRow?.DataBoundItem is not QuoteLine line) return;

            if (line.Id > 0)
                _db.DeleteQuoteLine(line.Id);

            _lines.Remove(line);
            UpdatePageLayout();
            UpdatePriceBreakdown();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0) return;
            if (!_lines.Any(IsMeaningfulLine)) return;

            var answer = MessageBox.Show(
                "Delete ALL quote lines for this project?\n\nThis cannot be undone.",
                "Delete All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes)
                return;

            _db.DeleteQuoteLines(_projectId);
            RefreshGrid();
        }



        public void SaveQuote(bool showConfirmation)
        {
            if (_projectId <= 0)
            {
                if (showConfirmation)
                    MessageBox.Show("No project open.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gridQuote.IsCurrentCellDirty)
                gridQuote.CommitEdit(DataGridViewDataErrorContexts.Commit);
            gridQuote.EndEdit();

            // Only persist meaningful lines – the always-present blank entry row at the
            // bottom of the grid must not create empty database rows.
            var lines = _lines.Where(IsMeaningfulLine).ToList();
            var validationErrors = CollectValidationErrors(lines);
            if (validationErrors.Count > 0)
            {
                if (showConfirmation)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, validationErrors), "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Silent auto-save (tab switch): persist only the valid lines.
                lines = lines.Where(l => string.IsNullOrWhiteSpace(ValidateLine(l))).ToList();
            }

            foreach (var line in lines)
            {
                line.ProjectId = _projectId;
                line.LineNumber = line.LineNumber <= 0 ? line.RowInBlock : line.LineNumber;
                ApplySectionLookup(line);
                RecalculatePrice(line);
                _db.UpsertQuoteLine(line);
            }

            UpdatePageLayout();
            if (showConfirmation)
            {
                MessageBox.Show("Quote saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
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

            // Refresh metal price and IP rates in case the Edit Pricing tab was saved since this tab loaded.
            LoadProjectSettings();

            line.LineNumber = line.LineNumber <= 0 ? e.RowIndex + 1 : line.LineNumber;

            // Always re-run the section lookup so Description/Alloy/Temper/IpGrade/SubsillFlag stay
            // in sync regardless of which column was just edited (Section, Qty, Length, IgnoreFinish, etc.).
            ApplySectionLookup(line);
            RecalculatePrice(line);

            // Push computed values back into the grid cells, suppressing re-entrant events.
            _bindingGrid = true;
            var row = gridQuote.Rows[e.RowIndex];
            SetCellValue(row, "Description", line.Description);
            SetCellValue(row, "Alloy", line.Alloy);
            SetCellValue(row, "Temper", line.Temper);
            SetCellValue(row, "IpGrade", line.IpGrade);
            SetCellValue(row, "SapColourCode", line.SapColourCode);
            SetCellValue(row, "Price", line.Price);
            SetCellValue(row, "TotalPrice", line.TotalPrice);
            _bindingGrid = false;

            // Repaint so read-only cells (Alloy, Temper, $ Each, $ Total) show the
            // new values immediately without waiting for the next paint cycle.
            gridQuote.InvalidateRow(e.RowIndex);

            row.ErrorText = ValidateLine(line);
            UpdatePageLayout();
            UpdatePriceBreakdown();
            if (IsMeaningfulLine(line))
                _db.UpsertQuoteLine(line);
            EnsureTrailingBlankRow();
        }

        private void SetCellValue(DataGridViewRow row, string columnName, object? value)
        {
            if (gridQuote.Columns.Contains(columnName))
                row.Cells[columnName].Value = value;
        }

        private void LoadProjectSettings()
        {
            var pricing = _db.GetEditPricing(_projectId);
            _metalPricePerKg = pricing?.MetalPrice ?? 0m;
            //Changing the field to fix the SF each price - Shainy
            //_finishingSellPricePerM2 = pricing?.Price_B44 ?? 0m;
            _finishingSellPricePerM2 = pricing?.FinishingPrice ?? 0m;
            if (pricing != null)
            {
                _ip1RatePerKg = pricing.IP1UpchargePerKg > 0 ? pricing.IP1UpchargePerKg : 1.00m;
                _ip2RatePerKg = pricing.IP2UpchargePerKg > 0 ? pricing.IP2UpchargePerKg : 2.00m;
                _ip3RatePerKg = pricing.IP3PanoramicFixedKgRate > 0 ? pricing.IP3PanoramicFixedKgRate : 25.85m;
                _millFinishMarginPct = pricing.MillFinishMarginPct;
                _finishingMarginPct = pricing.FinishingMarginPct;
                _finishingCostPricePerM2 = pricing.FinishingCostPricePerM2 > 0
                    ? pricing.FinishingCostPricePerM2
                    : pricing.FinishingPrice;
            }

            var project = _db.GetProject(_projectId);
            _projectFinishSapCode = string.IsNullOrWhiteSpace(project?.FinishSapCode) ? "NON" : project.FinishSapCode;
            _thresholdChoice = string.IsNullOrWhiteSpace(project?.ThresholdChoice) ? "No" : project.ThresholdChoice;
            _projectFinishType = string.IsNullOrWhiteSpace(project?.FinishType) ? MillFinishType : project.FinishType;
        }

        private void UpdatePageLayout()
        {
            if (_projectId <= 0)
                return;

            int activeLineCount = _lines.Count(IsMeaningfulLine);
            int pageCount = Math.Min(MaxQuotePages, Math.Max(1, (int)Math.Ceiling(activeLineCount / (decimal)MaxRowsPerPage)));
            _pageSetup.SetPageLayout(_projectId, pageCount);
        }

        private void UpdateValidationStates()
        {
            for (int i = 0; i < gridQuote.Rows.Count && i < _lines.Count; i++)
                gridQuote.Rows[i].ErrorText = ValidateLine(_lines[i]);
        }

        private List<string> CollectValidationErrors(IEnumerable<QuoteLine> lines)
            => lines
                .Select((line, index) => (Message: ValidateLine(line), Index: index))
                .Where(x => !string.IsNullOrWhiteSpace(x.Message))
                .Select(x => $"Row {x.Index + 1}: {x.Message}")
                .ToList();

        private string ValidateLine(QuoteLine line)
        {
            if (!IsMeaningfulLine(line))
                return string.Empty;

            if (line.Quantity <= 0)
                return "Quantity must be greater than 0.";
            if (line.LengthM <= 0 && !HasManualPriceWithoutLength(line))
                return "Length must be greater than 0.";
            if (line.ManualPriceOverride.HasValue && line.ManualPriceOverride.Value <= 0)
                return "Price override must be greater than 0.";
            return string.Empty;
        }

        private static bool IsMeaningfulLine(QuoteLine line)
            => !string.IsNullOrWhiteSpace(line.ExtrusionCode)
               || !string.IsNullOrWhiteSpace(line.Description)
               || line.Quantity > 0
               || line.LengthM > 0
               || line.ManualPriceOverride.HasValue;

        private static bool HasManualPriceWithoutLength(QuoteLine line)
            => line.ManualPriceOverride.HasValue
               && line.ManualPriceOverride.Value > 0
               && !string.IsNullOrWhiteSpace(line.ExtrusionCode)
               && line.ExtrusionCode.StartsWith("SUNDRY", StringComparison.OrdinalIgnoreCase);

        private static string NormalizeSectionCode(string? sectionCode)
            => string.IsNullOrWhiteSpace(sectionCode) ? string.Empty : sectionCode.Trim();

        private void gridQuote_SelectionChanged(object sender, EventArgs e) => UpdatePriceBreakdown();

        private void UpdatePriceBreakdown()
        {
            if (gridQuote.CurrentRow?.DataBoundItem is not QuoteLine line)
            {
                lblPriceBreakdown.Text = "Select a quote line to review the calculation.";
                return;
            }

            decimal kgEach = 0m;
            kgEach = FormulaCalculationService.CalculateKgEach(GetMassPerM(line.ExtrusionCode), line.LengthM);

            var millFinishEach = CalculateMillFinishEach(line);
            var ipEach = CalculateIpEach(line);
            var sfEach = CalculateFinishingEach(line);
            var overrideText = line.ManualPriceOverride.HasValue
                ? $"{line.ManualPriceOverride.Value.ToString("0.0000", CultureInfo.InvariantCulture)} (manual override)"
                : "—";

            lblPriceBreakdown.Text =
                $"Section: {line.ExtrusionCode}{Environment.NewLine}" +
                $"Kg each: {kgEach:0.####}{Environment.NewLine}" +
                $"Mill finish each: {millFinishEach:0.0000}{Environment.NewLine}" +
                $"IP each: {ipEach:0.0000}{Environment.NewLine}" +
                $"SF each: {sfEach:0.0000}{Environment.NewLine}" +
                $"Slotting / sundry: {line.SubsillSurcharge:0.0000}{Environment.NewLine}" +
                $"Override: {overrideText}{Environment.NewLine}" +
                $"Final $ each: {line.Price:0.0000}{Environment.NewLine}" +
                $"Line total: {line.TotalPrice:0.0000}{Environment.NewLine}" +
                $"SAP code: {line.SapColourCode}{Environment.NewLine}" +
                $"{Environment.NewLine}Margins{Environment.NewLine}" +
                $"Mill finish margin: {_millFinishMarginPct:0.##}%{Environment.NewLine}" +
                $"Finishing margin: {_finishingMarginPct:0.##}%{Environment.NewLine}" +
                $"Finishing cost: {_finishingCostPricePerM2:0.00}/m² vs sell {_finishingSellPricePerM2:0.00}/m²";
        }

        private static string NormalizeThresholdChoice(string thresholdChoice)
            => string.Equals(thresholdChoice, "Project Colour", StringComparison.OrdinalIgnoreCase) ? "No" : thresholdChoice;

        private void gridQuote_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _lines.Count || e.ColumnIndex < 0)
                return;
            var column = gridQuote.Columns[e.ColumnIndex];
            if (column == null)
                return;
            if (gridQuote.Rows[e.RowIndex].DataBoundItem is not QuoteLine line)
                return;

            if (column.DataPropertyName == nameof(QuoteLine.Price))
            {
                var mfEach = CalculateMillFinishEach(line);
                var ipEach = CalculateIpEach(line);
                var sfEach = CalculateFinishingEach(line);
                var sundryEach = line.SubsillSurcharge;
                e.ToolTipText =
                    $"$ Each = MF + IP + SF + Slotting/Sundry{Environment.NewLine}" +
                    $"= {mfEach:0.0000} + {ipEach:0.0000} + {sfEach:0.0000} + {sundryEach:0.0000}{Environment.NewLine}" +
                    $"= {line.Price:0.0000}";
            }
            else if (column.DataPropertyName == nameof(QuoteLine.TotalPrice))
            {
                e.ToolTipText =
                    $"$ Total = $ Each × Qty{Environment.NewLine}" +
                    $"= {line.Price:0.0000} × {line.Quantity:0.####}{Environment.NewLine}" +
                    $"= {line.TotalPrice:0.0000}";
            }
        }

        private void btnImportQuoteLines_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.");
                return;
            }

            // Ask whether this is a CRE8 Extrusion PO or a regular BOM / quote lines file
            //var isCre8 = MessageBox.Show(
            //    "Is this a CRE8 Extrusion PO file?\n\nClick 'Yes' for CRE8 PO, or 'No' for regular quote lines / BOM.",
            //    "Import Type",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Question) == DialogResult.Yes;

            //string fileFilter = isCre8
            //    ? "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            //    : "BOM files (*.csv;*.xlsx;*.xlsm)|*.csv;*.xlsx;*.xlsm|CSV Files (*.csv)|*.csv|Excel Files (*.xlsx;*.xlsm)|*.xlsx;*.xlsm";

            string fileFilter = "BOM files (*.csv;*.xlsx;*.xlsm)|*.csv;*.xlsx;*.xlsm|CSV Files (*.csv)|*.csv|Excel Files (*.xlsx;*.xlsm)|*.xlsx;*.xlsm";


            using var ofd = new OpenFileDialog
            {
                Filter = fileFilter,
                //Title = isCre8 ? "Import CRE8 Extrusion PO" : "Import quote lines"
                Title = "Import quote lines"
            };

            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                int imported;
                //if (isCre8)
                //{
                //    // Import project-level details (customer, finish, etc.) from the CRE8 PO
                //    _cre8Import.ImportProjectDetails(_projectId, ofd.FileName);
                //    // Also treat the same file as a BOM to import the line items
                //    imported = _bom.ImportBomFromCsv(_projectId, ofd.FileName);
                //}
                //else
                //{
                imported = ofd.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)
                      ? _bom.ImportBomFromCsv(_projectId, ofd.FileName)
                      : _bom.ImportBomFromWorkbook(_projectId, ofd.FileName);
                //}

                int distributed = _bom.DistributeBomToQuote(_projectId);

                // RefreshGrid calls LoadProjectSettings internally, picking up any CRE8-imported finish/SAP values
                RefreshGrid();

                // ── Post-import dialog 1: Finish Summary ─────────────────────────────
                var uniqueFinishes = _bom.GetUniqueFinishes(_projectId);
                var mostCommonFinish = _bom.GetMostCommonFinish(_projectId);

                using (var summaryDlg = new ImportFinishSummaryDialog(mostCommonFinish, uniqueFinishes))
                {
                    summaryDlg.ShowDialog(this);

                    // ── Post-import dialog 2 (conditional): Set Finish ────────────────
                    if (summaryDlg.EditFinishColours)
                    {
                        var project = _db.GetProject(_projectId);
                        if (project != null)
                        {
                            var colourGroups = _db.GetColourGroups().ToList();
                            using var setFinishDlg = new ImportSetFinishDialog(
                                colourGroups,
                                project.FinishType,
                                project.FinishApplicator,
                                project.FinishGroup,
                                project.FinishName,
                                project.FinishSapCode);

                            if (setFinishDlg.ShowDialog(this) == DialogResult.OK)
                            {
                                project.FinishType = setFinishDlg.FinishType;
                                project.FinishApplicator = setFinishDlg.FinishApplicator;
                                project.FinishGroup = setFinishDlg.FinishGroup;
                                project.FinishName = setFinishDlg.FinishName;
                                project.FinishSapCode = setFinishDlg.FinishSapCode;
                                _db.UpdateProject(project);
                                // RefreshGrid calls LoadProjectSettings internally so the updated finish SAP code is active
                                RefreshGrid();
                            }
                        }
                    }
                }

                // ── Post-import dialog 3: Pricing ────────────────────────────────────
                using (var pricingDlg = new ImportPricingDialog())
                {
                    if (pricingDlg.ShowDialog(this) == DialogResult.OK)
                    {
                        if (pricingDlg.Choice == ImportPricingChoice.UseImportedPrices)
                        {
                            int pricesUpdated = _importPrices.ImportPricesToQuote(_projectId);
                            RefreshGrid();
                            MessageBox.Show(
                                $"Applied {pricesUpdated} imported price(s) to the quote.\n" +
                                "Use 'Reset Price Formulas' to return to calculated pricing.",
                                "Import Prices", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (pricingDlg.Choice == ImportPricingChoice.Yes)
                        {
                            MessageBox.Show(
                                "Switch to the Edit Pricing tab to review and adjust pricing.",
                                "Edit Pricing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                MessageBox.Show(
                    $"Imported {imported} row(s) and distributed {distributed} to the quote.",
                    "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Import failed:\n{ex.Message}", "Import Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.");
                return;
            }

            var project = _db.GetProject(_projectId);
            var logoLabel = project == null ? string.Empty : QuotePdfService.GetLocalLogoLabel(project);
            using var options = new QuotePdfOptionsDialog(hasLocalLogo: !string.IsNullOrWhiteSpace(logoLabel));
            if (options.ShowDialog(this) != DialogResult.OK)
                return;

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"Quote_{DateTime.Today:yyyyMMdd}.pdf"
            };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            var millFinishLines = options.AddMillFinishVersion ? BuildMillFinishLines() : null;
            _quotePdf.ExportQuotePdf(_projectId, sfd.FileName, options.IncludeQuoteLetter, options.Unpriced,
                options.IncludeLocalLogo, millFinishLines);
            MessageBox.Show($"PDF saved to:\n{sfd.FileName}", "Export PDF",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Builds a Mill Finish alternative of the current quote lines: same sections,
        /// quantities and lengths but with the finishing (SF) component removed from
        /// the price and the SAP colour code set to NON.
        /// </summary>
        private List<QuoteLine> BuildMillFinishLines()
        {
            var result = new List<QuoteLine>();
            foreach (var line in _lines.Where(IsMeaningfulLine))
            {
                var millFinishEach = CalculateMillFinishEach(line);
                var ipEach = CalculateIpEach(line);
                var price = line.ManualPriceOverride
                            ?? FormulaCalculationService.CalculatePriceEach(
                                millFinishEach,
                                ipEach: ipEach,
                                sfSellEach: 0m,
                                slottingSundryEach: line.SubsillSurcharge);
                result.Add(new QuoteLine
                {
                    ProjectId = line.ProjectId,
                    PageBlock = line.PageBlock,
                    RowInBlock = line.RowInBlock,
                    LineNumber = line.LineNumber,
                    ExtrusionCode = line.ExtrusionCode,
                    Description = line.Description,
                    Alloy = line.Alloy,
                    Temper = line.Temper,
                    Quantity = line.Quantity,
                    LengthM = line.LengthM,
                    Price = price,
                    TotalPrice = FormulaCalculationService.CalculateSellTotal(price, line.Quantity),
                    SapColourCode = "NON",
                    IgnoreFinish = "Yes"
                });
            }
            return result;
        }

        private void gridQuote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardIntoGrid();
                e.Handled = true;
            }
        }

        private void PasteClipboardIntoGrid()
        {
            if (_projectId <= 0 || gridQuote.CurrentCell == null)
                return;

            var text = Clipboard.GetText();
            if (string.IsNullOrWhiteSpace(text))
                return;

            var editableColumns = gridQuote.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible && !c.ReadOnly)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            if (editableColumns.Count == 0)
                return;

            int startColumnIndex = editableColumns.FindIndex(c => c.Index == gridQuote.CurrentCell.ColumnIndex);
            if (startColumnIndex < 0)
                startColumnIndex = 0;

            var rows = text.Replace("\r\n", "\n").TrimEnd('\n').Split('\n');
            int rowIndex = gridQuote.CurrentCell.RowIndex;

            foreach (var rowText in rows)
            {
                //if (rowIndex >= _lines.Count)
                //    btnAddRow_Click(this, EventArgs.Empty);

                if (rowIndex >= _lines.Count)
                    break;

                var cells = rowText.Split('\t');
                for (int i = 0; i < cells.Length && startColumnIndex + i < editableColumns.Count; i++)
                {
                    var column = editableColumns[startColumnIndex + i];
                    gridQuote.Rows[rowIndex].Cells[column.Index].Value = ConvertClipboardValue(column.DataPropertyName, cells[i]);
                }

                rowIndex++;
            }

            gridQuote.EndEdit();
        }

        /// <summary>
        /// Converts pasted clipboard values into the grid's expected typed values.
        /// Numeric fields accept both invariant-culture and current-culture decimal input so
        /// pasted Excel data works for users with different decimal separators.
        /// </summary>
        private static object ConvertClipboardValue(string propertyName, string value)
        {
            value = value.Trim();
            if (propertyName is nameof(QuoteLine.Quantity) or nameof(QuoteLine.LengthM) or nameof(QuoteLine.ManualPriceOverride) or nameof(QuoteLine.SubsillSurcharge))
            {
                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var numeric) ||
                    decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out numeric))
                {
                    return numeric;
                }
                return 0m;
            }

            if (propertyName == nameof(QuoteLine.SubsillFlag))
                return value.Equals("Y", StringComparison.OrdinalIgnoreCase) || value.Equals("Yes", StringComparison.OrdinalIgnoreCase) || value == "1";

            return value;
        }

       
    }
}
