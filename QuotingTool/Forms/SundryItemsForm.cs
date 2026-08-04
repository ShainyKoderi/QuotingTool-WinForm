using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuotingTool.Data;
using QuotingTool.Services;

namespace QuotingTool.Forms
{
    public partial class SundryItemsForm : Form
    {
        private readonly DatabaseContext _db;
        private readonly SundryItemsService _service;
        private BindingList<SundryItem> _items = new();

        public SundryItemsForm(DatabaseContext db)
        {
            _db = db;
            _service = new SundryItemsService(db);
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _items = new BindingList<SundryItem>(BuildDisplayList(_service.GetItems()));
            gridSundryItems.DataSource = _items;
            ConfigureGrid();
        }

        private static List<SundryItem> BuildDisplayList(IEnumerable<SundryItem> dbItems)
        {
            var byCode = dbItems
                .Where(i => !string.IsNullOrWhiteSpace(i.SectionCode))
                .GroupBy(i => i.SectionCode, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
            var list = new List<SundryItem>();

            for (int i = 1; i <= 26; i++)
            {
                var code = $"SUNDRY-{i}";
                if (byCode.TryGetValue(code, out var existing))
                    list.Add(existing);
                else
                    list.Add(new SundryItem { SectionCode = code, Description = $"Sundry description - {i}" });
            }

            list.AddRange(byCode.Values
                .Where(i => TryGetSundryNumber(i.SectionCode, out var n) && n > 26)
                .OrderBy(i => ParseSundryNumber(i.SectionCode)));

            return list;
        }

        private void ConfigureGrid()
        {
            if (gridSundryItems.Columns.Contains(nameof(SundryItem.SectionCode)))
                gridSundryItems.Columns[nameof(SundryItem.SectionCode)].ReadOnly = true;
            if (gridSundryItems.Columns.Contains(nameof(SundryItem.Ap)))
                gridSundryItems.Columns[nameof(SundryItem.Ap)].DefaultCellStyle.Format = "0";
            if (gridSundryItems.Columns.Contains(nameof(SundryItem.Pp)))
                gridSundryItems.Columns[nameof(SundryItem.Pp)].DefaultCellStyle.Format = "0";
            if (gridSundryItems.Columns.Contains(nameof(SundryItem.MassKgPerM)))
                gridSundryItems.Columns[nameof(SundryItem.MassKgPerM)].DefaultCellStyle.Format = "0.0000";
            if (gridSundryItems.Columns.Contains(nameof(SundryItem.UnitCost)))
                gridSundryItems.Columns[nameof(SundryItem.UnitCost)].DefaultCellStyle.Format = "0.0000";
            if (gridSundryItems.Columns.Contains(nameof(SundryItem.UnitSell)))
                gridSundryItems.Columns[nameof(SundryItem.UnitSell)].DefaultCellStyle.Format = "0.0000";
        }

        private void btnEdit26Plus_Click(object sender, EventArgs e)
        {
            int next = _items
                .Select(i => ParseSundryNumber(i.SectionCode))
                .DefaultIfEmpty(0)
                .Max() + 1;
            var code = $"SUNDRY-{next}";
            _items.Add(new SundryItem
            {
                SectionCode = code,
                Description = $"Sundry description - {next}",
                MassKgPerM = 0,
                Ap = 0,
                Pp = 0,
                UnitCost = 0,
                UnitSell = 0
            });

            int rowIndex = _items.Count - 1;
            if (rowIndex >= 0)
            {
                gridSundryItems.ClearSelection();
                gridSundryItems.Rows[rowIndex].Selected = true;
                gridSundryItems.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void gridSundryItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridSundryItems.IsCurrentCellDirty)
                gridSundryItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridSundryItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _items.Count) return;
            if (_items[e.RowIndex].Ap < 0) _items[e.RowIndex].Ap = 0;
            if (_items[e.RowIndex].Pp < 0) _items[e.RowIndex].Pp = 0;
            if (_items[e.RowIndex].MassKgPerM < 0) _items[e.RowIndex].MassKgPerM = 0;
            if (_items[e.RowIndex].UnitCost < 0) _items[e.RowIndex].UnitCost = 0;
            if (_items[e.RowIndex].UnitSell < 0) _items[e.RowIndex].UnitSell = 0;
        }

        private void btnCloseSave_Click(object sender, EventArgs e)
        {
            SaveAll();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveAll()
        {
            foreach (var item in _items)
            {
                item.Description ??= string.Empty;
                item.SectionCode = item.SectionCode?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(item.SectionCode))
                    continue;
                if (item.Id > 0)
                    _service.UpdateItem(item);
                else
                    _db.InsertSundryItem(item);
            }
            MessageBox.Show("Sundry items saved.", "Sundry Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static int ParseSundryNumber(string? sectionCode)
            => TryGetSundryNumber(sectionCode, out var n) ? n : 0;

        private static bool TryGetSundryNumber(string? sectionCode, out int number)
        {
            number = 0;
            if (string.IsNullOrWhiteSpace(sectionCode))
                return false;

            var parts = sectionCode.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return parts.Length == 2 && parts[0].Equals("SUNDRY", StringComparison.OrdinalIgnoreCase)
                && int.TryParse(parts[1], out number);
        }
    }
}
