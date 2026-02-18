using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class AmountByPercent
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public double? Percent { get; set; }

        public decimal Amount { get; set; }
    }
}
