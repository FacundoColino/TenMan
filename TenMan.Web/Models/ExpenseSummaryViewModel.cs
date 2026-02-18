using System.Collections.Generic;
using TenMan.Web.Data.Entities;
using TenMan.Web.Data.Entities.Settings;

namespace TenMan.Web.Models
{
    public class ExpenseSummaryViewModel
    {
        public Committee Committee { get; set; }
        public Expenses Expense { get; set; }

        public ExpenseSummarySettings Settings { get; set; }

        public decimal TotalExpenses { get; set; }

        //public List<ExpenseItemViewModel> Items { get; set; }
        public List<ExpenseFieldViewModel> Fields { get; set; }

        public List<UnitDescriptionLine> UnitDescriptionLines { get; set; }
    }
}
