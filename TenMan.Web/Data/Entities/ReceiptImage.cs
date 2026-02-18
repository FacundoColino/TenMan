using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Data.Entities
{
    public class ReceiptImage
    {
        public int Id { get; set; }
        public int? ExpenseCostId { get; set; }

        public int? FixedCostId { get; set; }

        [Display(Name = "Comprobante")]
        public string ImageUrl { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TBD.azurewebsites.net{ImageUrl.Substring(1)}";

        public ExpensesCost ExpensesCost { get; set; }

        public FixedCost FixedCost { get; set; }
    }
}
