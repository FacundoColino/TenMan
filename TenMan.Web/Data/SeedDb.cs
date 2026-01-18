using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;

namespace TenMan.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();

            var admin = await CheckUserAsync("27010101", "Faqundo", "Colino", "faqundo.colino@gmail.com", "1130767787", "Aguilar 2497", "Administrator");
            var admin2 = await CheckUserAsync("36780653", "Magnus", "Carlsen", "magnus.carlsen@jmailx.com", "113222222", "Cabildo 1950", "Administrator");
            var sa = await CheckUserAsync("27010102", "Facundo", "Colino", "facundo.colino@usal.edu.ar", "1130767787", "Aguilar 2497", "SuperAdmin");
            var tenant = await CheckUserAsync("27845890", "Carolina", "Roman", "karitovr80@gmail.com", "1136557821", "Balbin 300", "Tenant");
            var worker = await CheckUserAsync("30987172", "Francisco", "Perez", "facundo.colino@xerox.com", "1120984530", "Bolivar 3300", "Worker");

            await CheckAdministratorsAsync(admin);

            await CheckCommitteesAsync();
            await CheckSpecialtiesAsync();
            await CheckStatusTypesAsync();
            await CheckSuperAdminsAsync(sa);

            await CheckTenantsAsync(tenant);

            await CheckWorkersAsync(worker);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }
        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("SuperAdmin");
            await _userHelper.CheckRoleAsync("Administrator");
            await _userHelper.CheckRoleAsync("Tenant");
            await _userHelper.CheckRoleAsync("Worker");
        }

        private async Task CheckAdministratorsAsync(User user)
        {
            if (!_context.Administrators.Any())
            {
                _context.Administrators.Add(new Administrator
                {
                    User = user,
                    CUIT = "2327212129"
                });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckCommitteesAsync()
        {
            var admin = _context.Administrators.FirstOrDefault();

            if (!_context.Committees.Any())
            {
                Field rubro1 = new Field { Number = 1, Description = "DETALLE DE SUELDO Y CARGAS SOCIALES" };
                Field rubro2 = new Field { Number = 2, Description = "SERVICIOS PÚBLICOS" };
                Field rubro3 = new Field { Number = 3, Description = "ABONOS DE SERVICIOS" };
                Field rubro4 = new Field { Number = 4, Description = "MANTENIMIENTO DE PARTES COMÚNES" };
                Field rubro5 = new Field { Number = 5, Description = "REPARACIONES EN UNIDADES FUNCIONALES" };
                Field rubro6 = new Field { Number = 6, Description = "GASTOS BANCARIOS" };
                Field rubro7 = new Field { Number = 7, Description = "GASTOS DE LIMPIEZA" };
                Field rubro8 = new Field { Number = 8, Description = "GASTOS DE ADMINISTRACIÓN" };
                Field rubro9 = new Field { Number = 9, Description = "OTROS" };

                List<Field> rubros = new List<Field>();
                rubros.Add(rubro1);
                rubros.Add(rubro2);
                rubros.Add(rubro3);
                rubros.Add(rubro4); 
                rubros.Add(rubro5); 
                rubros.Add(rubro6);
                rubros.Add(rubro7);
                rubros.Add(rubro8);
                rubros.Add(rubro9);

                List<Category> categories = new List<Category>();
                Category category = new Category
                {
                    Letra = "A",
                    Description = "Gastos generales comúnes a todas las unidades"
                };
                categories.Add(category);

                _context.Committees.Add(new Entities.Committee { Description = "Consorcio Inicial", Address = "Aguilar 2497", Neighborhood = "Colegiales",CUIT = "11111111", SuterhKey="2837283", Fields = rubros, Categories = categories, Administrator = admin });

                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckSpecialtiesAsync()
        {
            if (!_context.Specialties.Any())
            {
                _context.Specialties.Add(new Entities.Specialty { Name = "Plomería" });
                _context.Specialties.Add(new Entities.Specialty { Name = "Gas" });
                _context.Specialties.Add(new Entities.Specialty { Name = "Electricidad" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckStatusTypesAsync()
        {
            if (!_context.StatusTypes.Any())
            {
                _context.StatusTypes.Add(new Entities.StatusType { Description = "Generada" });
                _context.StatusTypes.Add(new Entities.StatusType { Description = "Asignada" });
                _context.StatusTypes.Add(new Entities.StatusType { Description = "En Proceso" });
                _context.StatusTypes.Add(new Entities.StatusType { Description = "Finalizada" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckSuperAdminsAsync(User user)
        {
            if (!_context.SuperAdmins.Any())
            {
                _context.SuperAdmins.Add(new SuperAdmin { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTenantsAsync(User user)
        {
            if (!_context.Tenants.Any())
            {
                _context.Tenants.Add(new Tenant { User = user });

                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckWorkersAsync(User user)
        {
            if (!_context.Workers.Any())
            {
                _context.Workers.Add(new Worker { User = user });

                await _context.SaveChangesAsync();
            }
        }
    }
}
