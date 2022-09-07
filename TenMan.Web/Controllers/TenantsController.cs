﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TenantsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public TenantsController(
            DataContext context, 
            IUserHelper userHelper, 
            ICombosHelper combosHelper, 
            IConverterHelper converterHelper,
            IImageHelper imageHelper
            )
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }
        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id.Value);
            if (request == null)
            {
                return NotFound();
            }

            var model = new RequestImageViewModel
            {
                Id = request.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(RequestImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }

                var requestImage = new RequestImage
                {
                    ImageUrl = path,
                    Request = await _context.Requests.FindAsync(model.Id)
                };

                _context.RequestImages.Add(requestImage);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsRequest)}/{model.Id}");
            }

            return View(model);
        }

        // GET: Tenants
        public IActionResult Index()
        {
            return View(_context.Tenants
                .Include(t => t.User)
                .Include(t => t.Units)
                .Include(t => t.Requests)
                .Include(t => t.Payments));
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .Include(o => o.User)
                .Include(o => o.Units)
                .Include(o => o.Requests)
                .Include(o => o.Payments)
                //.ThenInclude(p => p.Receipt)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user != null)
                {
                    var tenant = new Tenant
                    {
                        Units = new List<Unit>(),
                        Requests = new List<Request>(),
                        Payments = new List<Payment>(),
                        User = user
                    };
                    _context.Tenants.Add(tenant);
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
                await _userHelper.AddUserToRoleAsync(user, "Tenant");
                return user;
            }
            return null;
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.Id))
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
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }
        //TODO: agregar lo mismo para AddUnit (usar el Create?)
        public async Task<IActionResult> AddRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
                return NotFound();

            var model = new RequestViewModel
            {
                TenantId = tenant.Id,
                Specialties = _combosHelper.GetComboSpecialties()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddRequest(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await _converterHelper.ToRequestAsync(model, true);
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.TenantId}");
            }
            return View(model);
        }
        public async Task<IActionResult> EditRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _context.Requests
                .Include(r => r.Tenant)
                .Include(r => r.Worker)
                .Include(r => r.Speciality)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            var model = _converterHelper.ToRequestViewModel(request);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Editequest(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await _converterHelper.ToRequestAsync(model, false);
                _context.Requests.Update(request);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.TenantId}");
            }
            return View(model);
        }
        public async Task<IActionResult> DetailsRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _context.Requests
            .FirstOrDefaultAsync(m => m.Id == id);
            /*
            var request = await _context.Requests
                //.Include(t => t.Tenant)
                //.ThenInclude(t => t.User)
                //.Include(r => r.Worker)
                //.ThenInclude(w => w.User)
                .Include(r => r.StartDate)
                //.Include(r => r.EndDate)
                //.Include(r => r.Speciality)
                .Include(r => r.Status)
                //.Include(i => i.Images)
                .FirstOrDefaultAsync(m => m.Id == id);*/

            if (request == null)
                return NotFound();

            return View(request);
        }
    }
}
