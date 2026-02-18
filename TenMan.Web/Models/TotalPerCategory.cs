using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class TotalPerCategory
    {
        public Category Category { get; set; }
        public decimal Total { get; set; }
    }
}
