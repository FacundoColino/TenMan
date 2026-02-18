using System.Collections.Generic;

namespace TenMan.Web.Models
{
    public class ExpenseFieldViewModel
    {
        public int Number {  get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public List<ExpenseItemViewModel> Items { get; set; }
    }
}
