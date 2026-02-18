using TenMan.Web.Models;

namespace TenMan.Web.Services.ExpensesServices
{
    public interface IExpenseSummaryBuilder
    {
        ExpenseSummaryViewModel Build(int expenseId);
    }
}
