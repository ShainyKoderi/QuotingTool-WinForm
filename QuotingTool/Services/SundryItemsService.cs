using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class SundryItemsService
    {
        private readonly DatabaseContext _db;
        public SundryItemsService(DatabaseContext db) => _db = db;

        public List<SundryItem> GetItems(int projectId) => _db.GetSundryItems(projectId).ToList();

        public void AddItem(int projectId, string description, decimal qty, decimal unitPrice, string category = "Sundry")
        {
            var item = new SundryItem
            {
                ProjectId = projectId,
                Description = description,
                Quantity = qty,
                UnitPrice = unitPrice,
                Category = category,
                TotalPrice = qty * unitPrice
            };
            _db.InsertSundryItem(item);
        }

        public void UpdateItem(SundryItem item)
        {
            item.TotalPrice = item.Quantity * item.UnitPrice;
            _db.UpdateSundryItem(item);
        }

        public void DeleteItem(int id) => _db.DeleteSundryItem(id);
        public decimal GetTotal(int projectId) => _db.GetSundryTotal(projectId);
    }
}
