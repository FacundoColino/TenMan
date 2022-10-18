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
    //[Authorize(Roles = "Administrator")]
    public class TenantsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IIFileHelper _fileHelper;

        public TenantsController(
            DataContext context,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IIFileHelper fileHelper
            )
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _fileHelper = fileHelper;

        }
        public IActionResult Index()
        {
            return View(_context.Tenants
                .Include(t => t.User)
                .Include(t => t.Units)
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
                .Include(o => o.Payments)
                //.ThenInclude(p => p.Receipt)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }
        public async Task<IActionResult> DetailsPayment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(t => t.Tenant)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
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

            var tenant = await _context.Tenants
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id.Value);

            if (tenant == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = tenant.User.Address,
                Document = tenant.User.Document,
                FirstName = tenant.User.FirstName,
                Id = tenant.Id,
                LastName = tenant.User.LastName,
                PhoneNumber = tenant.User.PhoneNumber
                //Username = tenant.User.Email
            };

            return View(model);

        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tenant = await _context.Tenants
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Id == model.Id);

                tenant.User.Document = model.Document;
                tenant.User.FirstName = model.FirstName;
                tenant.User.LastName = model.LastName;
                tenant.User.Address = model.Address;
                tenant.User.PhoneNumber = model.PhoneNumber;
                //tenant.User.UserName = model.Username;

                await _userHelper.UpdateUserAsync(tenant.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);

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
       
     

        //[Authorize(Roles ="Tenant")]
        // GET: Payments/Create
        public async Task<IActionResult> AddPayment()
        {
            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.User.Email == User.Identity.Name);

            var model = new PaymentViewModel
            {
                Units = _combosHelper.GetComboUnits(tenant.Id),
                TenantId = tenant.Id,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var tenant = _context.Tenants.FirstOrDefault(t => t.User.Email == User.Identity.Name);
                //payment.Tenant = tenant;

                var payment = new Payment
                {
                    Status = "Pendiente",
                    Tenant = await _context.Tenants.FindAsync(model.TenantId),
                    Unit = await _context.Units.FindAsync(model.UnitId),
                    Amount = model.Amount,
                    Date = model.Date,
                };

                if (model.PdfFormFile != null)
                {
                    var path = await _fileHelper.UploadFileAsync(model.PdfFormFile);
                    payment.PdfFile = path;
                }

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                //return RedirectToAction($"{nameof(IndexPayments)}/{payment.Tenant.Id}");
                return RedirectToAction("IndexPayments");
            }
            return View(model);
        }
        public IActionResult IndexPayments()
        {
            string email = User.Identity.Name;

            var tenant = _context.Tenants
                .Include(t => t.User)
                .Include(t => t.Payments)
                .ThenInclude(p => p.Unit)
                .FirstOrDefault(t => t.User.Email == User.Identity.Name);

            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant.Payments);
        }
    }
}
