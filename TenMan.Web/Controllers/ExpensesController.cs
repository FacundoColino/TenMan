using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ExpensesController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Expenses.Include(e => e.Committee);
            return View(await dataContext.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .Include(e => e.Committee)
                .Include(e => e.ExpensesCosts)
                .ThenInclude(ec => ec.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Address");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Month,Year,Date,CommitteeId")] Expenses expenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Address", expenses.CommitteeId);
            return View(expenses);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Address", expenses.CommitteeId);
            return View(expenses);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Month,Year,Date,CommitteeId")] Expenses expenses)
        {
            if (id != expenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensesExists(expenses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Address", expenses.CommitteeId);
            return View(expenses);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .Include(e => e.Committee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        public IActionResult CurrentExpense(int id)
        {
            var expenses = _context.Expenses
                .Include(e => e.ExpensesCosts)
                .FirstOrDefault(e => e.Id == id);

            if (expenses == null)
                return NotFound();

            return View(expenses);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenses = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddCost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var expense = _context.Expenses
                .Include(e => e.Committee)
                .ThenInclude(c => c.Categories)
                .FirstOrDefault(e => e.Id == id);

            if (expense == null)
                return NotFound();

            var model = new AddCostViewModel
            {
                CommitteeId = expense.CommitteeId,
                ExpenseId = expense.Id,
                Fields = _combosHelper.GetComboFields(),
                Categories = _combosHelper.GetComboCategories(expense.CommitteeId)
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCost(AddCostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cost = new ExpensesCost
                {
                    Description = model.Description,
                    Amount = model.Amount,
                    Field = _context.Fields.FindAsync(model.FieldId).Result,
                    CategoryId = model.CategoryId,
                    Category = _context.Categories.FindAsync(model.CategoryId).Result,
                    Expenses = _context.Expenses.FindAsync(model.ExpenseId).Result
                };
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.ExpenseId}");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteCost(int id, int expensesId)
        {
            var cost = _context.ExpensesCosts.Find(id);
            _context.ExpensesCosts.Remove(cost);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = expensesId });
        }
        private bool ExpensesExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
