using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Services.ExpensesServices
{
    public class ExpenseSummaryBuilder : IExpenseSummaryBuilder
    {
        private readonly DataContext _context;
        private readonly IExpenseCalculator _calculator;

        public ExpenseSummaryBuilder(
            DataContext context,
            IExpenseCalculator calculator)
        {
            _context = context;
            _calculator = calculator;
        }

        public ExpenseSummaryViewModel Build(int committeeId)
        {
            var committee = _context.Committees
            .Include(c => c.Units)
            .ThenInclude(u => u.CheckingAccount)
            .Include(c => c.Units)
            .ThenInclude(u => u.CategoriesPercents)
            .Include(c => c.Fields)
            .Include(c => c.FixedCosts)
            .ThenInclude(fc => fc.Category)
            .Include(c => c.Expenses)
            .ThenInclude(c => c.ExpensesCosts)
            .Include(c => c.Administrator)
            .ThenInclude(a => a.User)
            .FirstOrDefault(c => c.Id == committeeId);

            var expense = _context.Expenses
           .Include(e => e.ExpensesCosts)
           .ThenInclude(ec => ec.Category)
           .Include(e => e.Committee)
           .Where(e => e.CommitteeId == committeeId && e.Current)
           .FirstOrDefault();

            // Trae la expensa pero en modo lista para poder usar GroupBy
            var expenses = _context.Expenses
            .Include(e => e.ExpensesCosts)
                .ThenInclude(ec => ec.Category)
            .Include(e => e.Committee)
                .Where(e => e.CommitteeId == committeeId && e.Current)
            .ToList();

            var settings = _context.ExpenseSummarySettings
                .FirstOrDefault(s => s.CommitteeId == committeeId);

            if (expense == null)
                return null;

            ExpenseTotals totals = _calculator.Calculate(expense);

            List<ExpenseFieldViewModel> fieldItems = new List<ExpenseFieldViewModel>();

            foreach (Field field in committee.Fields)
            {
                decimal total = 0;
                string name = field.Description;
                int number = field.Number;

                List<ExpenseItemViewModel> items = new List<ExpenseItemViewModel>();

                foreach (var cost in expense.ExpensesCosts)
                {
                    if (cost.Field.Description == field.Description)
                    {
                        total += cost.Amount;
                        items.Add(new ExpenseItemViewModel
                        {
                            Amount = cost.Amount,
                            Description = cost.Description
                        });
                    }
                }
                fieldItems.Add(new ExpenseFieldViewModel
                {
                    Number = number,
                    Name = name,
                    Total = total,
                    Items = items
                });
            }

           List<UnitDescriptionLine> unitDescriptionLines = new List<UnitDescriptionLine>();

            foreach (Unit unit in committee.Units)
            {
                decimal expA = (decimal)unit.Percentage / 100 * totals.Total;
                decimal prev = unit.CheckingAccount.Total;
                decimal payment = unit.CheckingAccount.YourPayment;
                decimal pending = unit.CheckingAccount.Total - payment;
                decimal balance = expA + pending;

                List<AmountByPercent> amountsByPercent = new List<AmountByPercent>();

                foreach (TotalPerCategory tc in totals.TotalsPerCategory)
                {
                    var catPer = unit.CategoriesPercents.FirstOrDefault(cp => cp.CategoryId == tc.Category.Id);

                    if (catPer == null) continue;

                    decimal amount = (decimal)(catPer.Percent / 100) * tc.Total;

                    amountsByPercent.Add(
                        new AmountByPercent
                        {
                            Amount = amount,
                            Percent = catPer.Percent,
                            CategoryId = catPer.Category.Id,
                            Category = tc.Category
                        }
                        );
                }

                UnitDescriptionLine unitLine = new UnitDescriptionLine
                {
                    Unit = unit,
                    CategoriesPercents = unit.CategoriesPercents,
                    AmountsByPercent = amountsByPercent,
                    YourPayment = payment,
                    PreviousBalance = prev,
                    PendingBalance = pending,
                    NewUnitTotal = balance,
                    ExpA = expA,
                    Interest = 0
                };
                unitDescriptionLines.Add(unitLine);
                //unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
                //unit.CheckingAccount.Balance = balance;
            }

            return new ExpenseSummaryViewModel
            {
                Committee = expense.Committee,
                Expense = expense,
                UnitDescriptionLines = unitDescriptionLines,
                TotalExpenses = totals.Total
            };
        }
    }
}
