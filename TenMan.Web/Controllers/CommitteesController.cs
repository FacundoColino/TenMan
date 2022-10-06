using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    public class CommitteesController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public CommitteesController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> AddUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var committee = await _context.Committees.FindAsync(id);

            if (committee == null)
                return NotFound();

            var model = new UnitViewModel
            {
                CommitteeId = committee.Id,
                Tenants = _combosHelper.GetComboTenants()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUnit(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                CheckingAccount account = new CheckingAccount
                {
                    Number = model.CommitteeId + model.Id + "000" + model.Number,
                    PreviousBalance = 0,
                    Balance = 0
                };
                _context.CheckingAccounts.Add(account);
                var unit = new Unit
                {
                    Number = model.Number,
                    Floor = model.Floor,
                    Apartment = model.Apartment,
                    SquareMeters = model.SquareMeters,
                    CheckingAccount = account,
                    Committee = _context.Committees.FindAsync(model.CommitteeId).Result,
                    Tenant = _context.Tenants.FindAsync(model.TenantId).Result
                };
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.CommitteeId}");
            }
            return View(model);
        }
     
        // GET: Committees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Committees.ToListAsync());
        }

        // GET: Committees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .Include(c => c.Units)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // GET: Committees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Committees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Neighborhood,Address,Price")] Committee committee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(committee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(committee);
        }

        // GET: Committees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees.FindAsync(id);
            if (committee == null)
            {
                return NotFound();
            }
            return View(committee);
        }

        // POST: Committees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Neighborhood,Address,Price")] Committee committee)
        {
            if (id != committee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(committee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommitteeExists(committee.Id))
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
            return View(committee);
        }

        // GET: Committees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
        }

        // POST: Committees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var committee = await _context.Committees.FindAsync(id);
            _context.Committees.Remove(committee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommitteeExists(int id)
        {
            return _context.Committees.Any(e => e.Id == id);
        }
        public void CalculateCosts(int? id)
        {
            if (id == null)
            {
                return;
            }
            var committee = _context.Committees
                .Include(c => c.Units)
                .ThenInclude(u => u.CheckingAccount)
                .FirstOrDefault();

            if (committee != null)
            {
                foreach (Unit unit in committee.Units)
                {
                    decimal balance = unit.SquareMeters * committee.Price;
                    decimal prev = unit.CheckingAccount.PreviousBalance;

                    unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
                    unit.CheckingAccount.Balance = balance;
                }
                _context.Committees.Update(committee);
                _context.SaveChanges();
                RedirectToAction($"Details/{id}");
            }
        }
        //private async Task<IActionResult> CalculateCosts(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var committee = await _context.Committees.Find(id);

        //    if (committee != null)
        //    {
        //        foreach (Unit unit in committee.Units)
        //        {
        //            decimal balance = unit.SquareMeters * committee.Price;
        //            decimal prev = unit.CheckingAccount.PreviousBalance;

        //            unit.CheckingAccount.PreviousBalance = prev + unit.CheckingAccount.Balance;
        //            unit.CheckingAccount.Balance = balance;
        //        }
        //        _context.Committees.Update(committee);
        //        _context.SaveChanges();
        //        //RedirectToAction($"Details/{id}");
        //    }
        //}
    }
}
