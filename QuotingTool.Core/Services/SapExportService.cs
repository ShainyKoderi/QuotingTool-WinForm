using System.Linq;
using System.Text;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class SapExportService
    {
        private readonly DatabaseContext _db;
        public SapExportService(DatabaseContext db) => _db = db;

        public string GetSapFormattedText(int projectId)
        {
            var lines = _db.GetQuoteLines(projectId).ToList();
            var sb = new StringBuilder();
            sb.AppendLine("Section\tDescription\tAlloy\tTemper\tQty\tLength(m)\t$Each\t$Total");
            foreach (var l in lines)
                if (!string.IsNullOrWhiteSpace(l.ExtrusionCode))
                    sb.AppendLine($"{l.ExtrusionCode}\t{l.Description}\t{l.Alloy}\t{l.Temper}\t{l.Quantity}\t{l.LengthM}\t{l.Price}\t{l.TotalPrice}");
            return sb.ToString();
        }
    }
}
