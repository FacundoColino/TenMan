using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdministratorsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IIFileHelper _fileHelper;

        public AdministratorsController(
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
            return View(_context.Administrators
                .Include(a => a.User)
                .Include(a => a.Committees)
                .Include(a => a.RegisterNumber));
        }

        // GET: Administrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Administrators
                .Include(a => a.User)
                .Include(a => a.Committees)
                .Include(a => a.RegisterNumber)
                //.ThenInclude(p => p.Receipt)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }
        public async Task<IActionResult> DetailsCommittees(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var committee = await _context.Committees
                .Include(a => a.Administrator)
                .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (committee == null)
            {
                return NotFound();
            }

            return View(committee);
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
                    var admin = new Administrator
                    {
                        Committees = new List<Committee>(),
                        User = user
                    };
                    _context.Administrators.Add(admin);
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
                await _userHelper.AddUserToRoleAsync(user, "Administrator");
                return user;
            }
            return null;
        }

        // GET: Administrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Administrators
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id.Value);

            if (admin == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = admin.User.Address,
                Document = admin.User.Document,
                FirstName = admin.User.FirstName,
                Id = admin.Id,
                LastName = admin.User.LastName,
                PhoneNumber = admin.User.PhoneNumber
                //Username = tenant.User.Email
            };

            return View(model);

        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Administrators
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => a.Id == model.Id);

                admin.User.Document = model.Document;
                admin.User.FirstName = model.FirstName;
                admin.User.LastName = model.LastName;
                admin.User.Address = model.Address;
                admin.User.PhoneNumber = model.PhoneNumber;
                //tenant.User.UserName = model.Username;

                await _userHelper.UpdateUserAsync(admin.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        // GET: Administrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Administrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Administrators.FindAsync(id);
            _context.Administrators.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /*
        private bool AdministratortExists(int id)
        {
            return _context.Administrators.Any(e => e.Id == id);
        }*/
    }
}
