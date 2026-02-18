using System.Linq;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Services.ExpensesServices
{
    public class ExpenseCalculator : IExpenseCalculator
    {
        public ExpenseTotals Calculate(Expenses expense)
        {
            decimal total = 0;
            var totals = new ExpenseTotals();

            // Ver de dividir los expensesCost y los FixedCosts y que cada método retorne un ExpenseTotal y se asigné en éste
            foreach (ExpensesCost eCost in expense.ExpensesCosts)
            {
                totals.Total += eCost.Amount;

                var categoryTotal = totals.TotalsPerCategory
       .FirstOrDefault(t => t.Category.Id == eCost.Category.Id);

                if (categoryTotal == null)
                {
                    totals.TotalsPerCategory.Add(new TotalPerCategory
                    {
                        Category = eCost.Category,
                        Total = eCost.Amount
                    });
                }
                else
                {
                    categoryTotal.Total += eCost.Amount;
                }
            }
            foreach (FixedCost fCost in expense.Committee.FixedCosts)
            {
                totals.Total += fCost.Amount;

                var categoryTotal = totals.TotalsPerCategory
      .FirstOrDefault(t => t.Category.Id == fCost.Category.Id);

                if (categoryTotal == null)
                {
                    totals.TotalsPerCategory.Add(new TotalPerCategory
                    {
                        Category = fCost.Category,
                        Total = fCost.Amount
                    });
                }
                else
                {
                    categoryTotal.Total += fCost.Amount;
                }
            }
            return totals;
        }
    }
}
