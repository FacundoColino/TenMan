using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Data.Entities
{
    public class CategoryPercent
    {
        public int? Id { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public double? Percent { get; set; }

        public Unit Unit { get; set; }
    }
}
