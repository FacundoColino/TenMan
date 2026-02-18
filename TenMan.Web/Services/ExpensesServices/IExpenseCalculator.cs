
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Services.ExpensesServices
{
    public interface IExpenseCalculator
    {
        ExpenseTotals Calculate(Expenses expense);
    }
}
