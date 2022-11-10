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
    public class PaymentsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public PaymentsController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payments
                .Include(p => p.Unit)
                .ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Amount,Status,PdfFile")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments
                .Include(p => p.Tenant)
                .ThenInclude(t => t.User)
                //.Include(p => p.Receipt)
                .Include(p => p.Unit)
                .ThenInclude(u => u.CheckingAccount)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }
            var model = new PaymentViewModel
            {
                Units = _combosHelper.GetComboUnits(payment.Tenant.Id),
                Amount = payment.Amount,
                Id = payment.Id,
                Date = payment.Date,
                PdfFile = payment.PdfFile,
                //Receipt = payment.Receipt,
                Status = payment.Status,
                Tenant = payment.Tenant,
                Unit = payment.Unit,
                UnitId = payment.Unit.Id,
                TenantId = payment.Tenant.Id,
                CheckingAccountId = payment.Unit.CheckingAccount.Id
            };
            return View(model);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Payment payment = new Payment
                    {
                        Id = model.Id,
                        PdfFile = model.PdfFile,
                        Amount = model.Amount,
                        Date = model.Date,
                        Unit = await _context.Units
                        .Include(u => u.CheckingAccount)
                        .FirstOrDefaultAsync(u => u.Id == model.UnitId),
                        Tenant = await _context.Tenants.FindAsync(model.TenantId),
                        Status = model.Status
                    };
                    if (payment.Status == "Aprobado")
                    {
                        //decimal balance = payment.Unit.CheckingAccount.Balance;
                        //decimal previousBalance = payment.Unit.CheckingAccount.PreviousBalance;
                        //decimal total = payment.Unit.CheckingAccount.Total;

                        //total = total - payment.Amount;
                        //previousBalance = previousBalance + total;
                        //balance = total;

                        //payment.Unit.CheckingAccount.Balance = balance;
                        //payment.Unit.CheckingAccount.PreviousBalance = previousBalance;

                        payment.Unit.CheckingAccount.YourPayment += payment.Amount;
                    }
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(model.Id))
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
            return View(model);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
