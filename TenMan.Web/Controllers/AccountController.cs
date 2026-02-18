using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
using TenMan.Web.Models;

namespace TenMan.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AccountController(
            DataContext context,
            IUserHelper userHelper,
            IConfiguration configuration)
        {
            _context = context;
            _userHelper = userHelper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (user == null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                             new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created(string.Empty, results);

                    }
                }
            }

            return BadRequest();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.ContainsKey("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user != null)
                {
                    if (model.SelectedRole == "Administrator")
                    {
                        var admin = new Administrator
                        {
                            Committees = new List<Committee>(),
                            User = user,
                            CUIT = ""
                        };
                        _context.Administrators.Add(admin);
                    }
                    else if (model.SelectedRole == "Tenant")
                    {
                        var tenant = new Tenant
                        {
                            Units = new List<Unit>(),
                            Payments = new List<Payment>(),
                            User = user
                        };
                        _context.Tenants.Add(tenant);
                    }
                    else if (model.SelectedRole == "Worker")
                    {
                        var worker = new Worker
                        {
                            Requests = new List<Request>(),
                            User = user
                        };
                        _context.Workers.Add(worker);
                    }
                    // Si falla
                    else
                    {
                        var tenant = new Tenant
                        {
                            Units = new List<Unit>(),
                            Payments = new List<Payment>(),
                            User = user
                        };
                        _context.Tenants.Add(tenant);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ésta dirección de email");
            }
            return View(model);
        }
        private async Task<User> CreateUserAsync(RegisterUserViewModel model)
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

                if (model.SelectedRole == "Administrator")
                    await _userHelper.AddUserToRoleAsync(user, "Administrator");

                else if (model.SelectedRole == "Tenant")
                    await _userHelper.AddUserToRoleAsync(user, "Tenant");

                else if (model.SelectedRole == "Worker")
                    await _userHelper.AddUserToRoleAsync(user, "Worker");

                else
                {
                    await _userHelper.AddUserToRoleAsync(user, "Tenant");
                }
                return user;
            }
            return null;
        }
    }
}
