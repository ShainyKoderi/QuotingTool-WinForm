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
    /// <summary>
    /// A read-only view of the quote with all finish types overridden to Mill Finish,
    /// so the finishing (SF) cost component is always zero.  Displayed alongside the
    /// standard Quote tab when GenerateMillFinishAlternative = Yes.
    /// </summary>
    public partial class MillFinishQuoteForm : Form
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
        private decimal _ip1RatePerKg = 1.00m;
        private decimal _ip2RatePerKg = 2.00m;
        private decimal _ip3RatePerKg = 25.85m;
        private Dictionary<string, int> _ipMasterByCodes = new(StringComparer.Ordinal);
        private HashSet<string> _thresholdSections = new(StringComparer.OrdinalIgnoreCase);
        private string _projectFinishSapCode = "NON";
        private string _thresholdChoice = "No";
        // Always Mill Finish – finishing cost is always zero in this form.
        private readonly string _projectFinishType = MillFinishType;
        private decimal _millFinishMarginPct;
        private decimal _finishingMarginPct;
        private decimal _finishingCostPricePerM2;

        public MillFinishQuoteForm(DatabaseContext db)
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
                RecalculatePrice(line);
            }
            EnsureTrailingBlankRow();
            gridQuote.DataSource = _lines;
            _bindingGrid = false;
            UpdatePageLayout();
            UpdateValidationStates();
            UpdatePriceBreakdown();
        }

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
                    if (string.IsNullOrWhiteSpace(line.Description))
                        line.Description = sundry.Description ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(line.Alloy))
                        line.Alloy = "6060";
                    if (string.IsNullOrWhiteSpace(line.Temper))
                        line.Temper = "T5";
                }
                return;
            }

            var tbType = master.ThermalBreakType ?? "No";
            var descBase = master.Description ?? string.Empty;
            var prefix = (tbType == "PLS" || tbType == "PDB") ? "*" : string.Empty;
            // In Mill Finish mode every line is effectively Mill Finish.
            line.Description = prefix + descBase + " **MILL FINISH**";

            if (string.IsNullOrWhiteSpace(line.Alloy))
                line.Alloy = master.DefaultAlloy;
            if (string.IsNullOrWhiteSpace(line.Temper))
                line.Temper = master.DefaultTemper;

            if (master.SubsillTier > 0)
                line.SubsillFlag = true;

            if (!string.IsNullOrWhiteSpace(master.PhCode) &&
                _ipMasterByCodes.TryGetValue(master.PhCode, out var ipCat))
            {
                line.IpGrade = ipCat;
            }
        }

        private void RecalculatePrice(QuoteLine line)
        {
            var millFinishEach = CalculateMillFinishEach(line);
            var ipEach = CalculateIpEach(line);
            var slottingSundryEach = line.SubsillSurcharge;
            // SF component is always zero – this is the Mill Finish alternative view.
            var sfSellEach = 0m;
            var calculatedPrice = FormulaCalculationService.CalculatePriceEach(
                millFinishEach,
                ipEach: ipEach,
                sfSellEach: sfSellEach,
                slottingSundryEach: slottingSundryEach);
            line.Price = line.ManualPriceOverride ?? calculatedPrice;
            line.TotalPrice = FormulaCalculationService.CalculateSellTotal(line.Price, line.Quantity);
            line.SellPrice = line.TotalPrice;
            line.SapColourCode = "NON";
        }

        private decimal CalculateMillFinishEach(QuoteLine line)
        {
            var massPerM = GetMassPerM(line.ExtrusionCode);
            if (massPerM <= 0 || line.LengthM <= 0 || _metalPricePerKg <= 0) return 0m;
            return massPerM * line.LengthM * _metalPricePerKg;
        }

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
                SetColHeader("ManualPriceOverride", "Override $ Each", 110, true);
                SetColHeader("Price", "$ Each (MF)", PriceColumnWidth, false);
                SetColHeader("TotalPrice", "$ Total (MF)", TotalColumnWidth, false);
                SetColHeader("SellPrice", "Sell Price", 90, true);
                SetColHeader("SubsillSurcharge", "Slotting (K)", 90, false);
                SetColHeader("IgnoreFinish", "Ign Fin", 80, true);
                SetColHeader("SapColourCode", "SAP Code", 90, false);
                SetColHeader("SubsillFlag", "SS?", 60, false);
                HideCol("Id");
                HideCol("ProjectId");
                // All columns read-only – this is a view-only Mill Finish alternative.
                foreach (DataGridViewColumn col in gridQuote.Columns)
                    col.ReadOnly = true;

                SetColFormat("Quantity", "0.##", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("LengthM", "0.###", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("ManualPriceOverride", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("Price", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("TotalPrice", "0.0000", DataGridViewContentAlignment.MiddleRight);
                SetColFormat("SubsillSurcharge", "0.0000", DataGridViewContentAlignment.MiddleRight);

                if (gridQuote.Columns.Contains("Price"))
                    gridQuote.Columns["Price"].ToolTipText = "Mill Finish $ Each = MF Each + IP Each + Slotting/Sundry (no finishing cost)";
                if (gridQuote.Columns.Contains("TotalPrice"))
                    gridQuote.Columns["TotalPrice"].ToolTipText = "Mill Finish $ Total = $ Each × Qty";

                foreach (DataGridViewColumn column in gridQuote.Columns)
                    column.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
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

        private void SetColFormat(string propName, string format, DataGridViewContentAlignment alignment)
        {
            if (!gridQuote.Columns.Contains(propName)) return;
            var style = gridQuote.Columns[propName].DefaultCellStyle;
            style.Format = format;
            style.Alignment = alignment;
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

        private void LoadProjectSettings()
        {
            var pricing = _db.GetEditPricing(_projectId);
            _metalPricePerKg = pricing?.MetalPrice ?? 0m;
            // Finishing sell price is not used (always 0 for Mill Finish), but kept for breakdown label.
            _finishingSellPricePerM2 = pricing?.Price_B44 ?? 0m;
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
            // SAP code is always NON for Mill Finish alternative.
            _projectFinishSapCode = "NON";
            _thresholdChoice = string.IsNullOrWhiteSpace(project?.ThresholdChoice) ? "No" : project.ThresholdChoice;
            // _projectFinishType is readonly = MillFinishType; finishing cost is always 0.
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
                gridQuote.Rows[i].ErrorText = string.Empty;
        }

        private void gridQuote_SelectionChanged(object sender, EventArgs e) => UpdatePriceBreakdown();

        private void UpdatePriceBreakdown()
        {
            if (gridQuote.CurrentRow?.DataBoundItem is not QuoteLine line)
            {
                lblPriceBreakdown.Text = "Select a quote line to review the Mill Finish calculation.";
                return;
            }

            decimal kgEach = FormulaCalculationService.CalculateKgEach(GetMassPerM(line.ExtrusionCode), line.LengthM);
            var millFinishEach = CalculateMillFinishEach(line);
            var ipEach = CalculateIpEach(line);

            lblPriceBreakdown.Text =
                $"Section: {line.ExtrusionCode}{Environment.NewLine}" +
                $"Kg each: {kgEach:0.####}{Environment.NewLine}" +
                $"Mill finish each: {millFinishEach:0.0000}{Environment.NewLine}" +
                $"IP each: {ipEach:0.0000}{Environment.NewLine}" +
                $"SF each: 0.0000 (Mill Finish){Environment.NewLine}" +
                $"Slotting / sundry: {line.SubsillSurcharge:0.0000}{Environment.NewLine}" +
                $"Final $ each (MF): {line.Price:0.0000}{Environment.NewLine}" +
                $"Line total (MF): {line.TotalPrice:0.0000}{Environment.NewLine}" +
                $"SAP code: NON{Environment.NewLine}" +
                $"{Environment.NewLine}Margins{Environment.NewLine}" +
                $"Mill finish margin: {_millFinishMarginPct:0.##}%{Environment.NewLine}" +
                $"(Finishing suppressed – Mill Finish alternative)";
        }

        private void gridQuote_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _lines.Count || e.ColumnIndex < 0)
                return;
            var column = gridQuote.Columns[e.ColumnIndex];
            if (column == null) return;
            if (gridQuote.Rows[e.RowIndex].DataBoundItem is not QuoteLine line) return;

            if (column.DataPropertyName == nameof(QuoteLine.Price))
            {
                var mfEach = CalculateMillFinishEach(line);
                var ipEach = CalculateIpEach(line);
                e.ToolTipText =
                    $"Mill Finish $ Each = MF + IP + Slotting{Environment.NewLine}" +
                    $"= {mfEach:0.0000} + {ipEach:0.0000} + {line.SubsillSurcharge:0.0000}{Environment.NewLine}" +
                    $"= {line.Price:0.0000} (no finishing cost)";
            }
            else if (column.DataPropertyName == nameof(QuoteLine.TotalPrice))
            {
                e.ToolTipText =
                    $"$ Total = $ Each × Qty{Environment.NewLine}" +
                    $"= {line.Price:0.0000} × {line.Quantity:0.####}{Environment.NewLine}" +
                    $"= {line.TotalPrice:0.0000}";
            }
        }

        private static bool IsMeaningfulLine(QuoteLine line)
            => !string.IsNullOrWhiteSpace(line.ExtrusionCode)
               || !string.IsNullOrWhiteSpace(line.Description)
               || line.Quantity > 0
               || line.LengthM > 0
               || line.ManualPriceOverride.HasValue;

        private static string NormalizeSectionCode(string? sectionCode)
            => string.IsNullOrWhiteSpace(sectionCode) ? string.Empty : sectionCode.Trim();

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (_projectId <= 0)
            {
                MessageBox.Show("No project open.");
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"Quote_MillFinish_{DateTime.Today:yyyyMMdd}.pdf"
            };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            var millFinishLines = BuildMillFinishLines();
            _quotePdf.ExportQuotePdf(_projectId, sfd.FileName, includeQuoteLetter: false, unpriced: false,
                includeLocalLogo: true, millFinishLines);
            MessageBox.Show($"Mill Finish PDF saved to:\n{sfd.FileName}", "Export PDF",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<QuoteLine> BuildMillFinishLines()
        {
            var result = new List<QuoteLine>();
            foreach (var line in _lines.Where(IsMeaningfulLine))
            {
                var mfEach = CalculateMillFinishEach(line);
                var ipEach = CalculateIpEach(line);
                var price = line.ManualPriceOverride
                            ?? FormulaCalculationService.CalculatePriceEach(
                                mfEach, ipEach: ipEach, sfSellEach: 0m,
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
    }
}
