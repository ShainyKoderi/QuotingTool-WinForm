using System;
using System.ComponentModel;
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
        private int _projectId;

        public SundryItemsForm(DatabaseContext db)
        {
            _db = db;
            _service = new SundryItemsService(db);
            InitializeComponent();
        }

        public void LoadProject(int projectId)
        {
            _projectId = projectId;
            _items = new BindingList<SundryItem>(_service.GetItems(projectId)
                .OrderBy(i => i.LineNumber)
                .ThenBy(i => i.Id)
                .ToList());
            gridSundryItems.DataSource = _items;
            RenumberRows();
            RecalculateGrid();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            _items.Add(new SundryItem
            {
                ProjectId = _projectId,
                LineNumber = _items.Count + 1,
                Quantity = 1,
                Category = "Sundry"
            });
            RecalculateGrid();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (gridSundryItems.CurrentRow?.DataBoundItem is not SundryItem item) return;
            if (item.Id > 0)
                _service.DeleteItem(item.Id);
            _items.Remove(item);
            RenumberRows();
            RecalculateGrid();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
            MessageBox.Show("Sundry items saved.", "Sundry Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRecalculate_Click(object sender, EventArgs e) => RecalculateGrid();

        private void gridSundryItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridSundryItems.IsCurrentCellDirty)
                gridSundryItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridSundryItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _items.Count) return;
            RecalculateGrid();
        }

        private void SaveAll()
        {
            RecalculateGrid();
            foreach (var item in _items)
            {
                item.ProjectId = _projectId;
                item.TotalPrice = item.Quantity * item.UnitPrice;
                if (item.Id > 0)
                    _service.UpdateItem(item);
                else
                    _db.InsertSundryItem(item);
            }
            LoadProject(_projectId);
        }

        private void RenumberRows()
        {
            for (int i = 0; i < _items.Count; i++)
                _items[i].LineNumber = i + 1;
            gridSundryItems.Refresh();
        }

        private void RecalculateGrid()
        {
            RenumberRows();
            decimal total = 0m;
            foreach (var item in _items)
            {
                item.TotalPrice = item.Quantity * item.UnitPrice;
                total += item.TotalPrice;
            }
            lblSundryTotal.Text = $"Sundry Total: {total:C2}";
            gridSundryItems.Refresh();
        }
    }
}
