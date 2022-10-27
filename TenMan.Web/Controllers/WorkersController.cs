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
    public class WorkersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public WorkersController(
            DataContext context,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper
            )
        {
            _userHelper = userHelper;
            _context = context;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        // GET: Workers
        public IActionResult Index()
        {
            return View(_context.Workers
                .Include(w => w.User)
                .Include(w => w.Requests));
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .Include(o => o.User)
                .Include(o => o.Requests)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            var model = new AddWorkerViewModel
            {
                Specialties = _combosHelper.GetComboSpecialties()
            };
            return View(model);
        }

        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddWorkerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user != null)
                {
                    var worker = new Worker { 
                        Requests = new List<Request>(),
                        User = user
                    };
                    _context.Workers.Add(worker);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ésta dirección de email");
            }
            return View(model);
        }
        private async Task<User> CreateUserAsync(AddUserViewModel model)
        {
            var user = new User
            {
                Document = model.Document,
                Address = model.Address,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            };
            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userHelper.GetUserByEmailAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, "Worker");
                return user;
            }
            return null;
        }
        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegisterNumber")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _context.Requests
            .Include(r => r.Unit)
            .Include(r => r.Images)
            .Include(r => r.Worker)
            .ThenInclude(w => w.User)
            .Include(r => r.Statuses)
            .ThenInclude(s => s.StatusType)
            .Include(r => r.Speciality)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            var model = _converterHelper.ToRequestViewModel(request);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRequest(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var actualStatus = _context.StatusTypes.FirstOrDefault(st => st.Id == model.StatusTypeId);
                model.ActualStatus = actualStatus.Description;

                var request = await _converterHelper.ToRequestAsync(model, false);

                if (model.ActualStatus == "Finalizada")
                {
                    model.EndDate = DateTime.Now;
                }
                Status status = new Status
                {
                    Date = DateTime.Now,
                    Request = request,
                    StatusType = actualStatus
                };

                request.Statuses.Add(status);

                _context.Requests.Update(request);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexRequests");
            }
            return View(model);
        }
        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
        public IActionResult IndexRequests()
        {
            string email = User.Identity.Name;

            var worker = _context.Workers
                .Include(w => w.User)
                .Include(w => w.Requests)
                .ThenInclude(r => r.Speciality)
                .FirstOrDefault(w => w.User.Email == User.Identity.Name);

            if (worker == null)
            {
                return NotFound();
            }
            return View(worker.Requests);
        }
    }
}
