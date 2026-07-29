using System.Collections.Generic;
using System.Linq;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class SundryItemsService
    {
        private readonly DatabaseContext _db;
        public SundryItemsService(DatabaseContext db) => _db = db;

        public List<SundryItem> GetItems() => _db.GetSundryItems().ToList();

        public void UpdateItem(SundryItem item)
        {
            _db.UpdateSundryItem(item);
        }

        public void DeleteItem(int id) => _db.DeleteSundryItem(id);
    }
}
