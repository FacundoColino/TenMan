using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Data
{
    // public class DataContext : IdentityDbContext
    public class DataContext : IdentityDbContext<User> // Modificado en Enero 2026
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Los DbSet son para poder llamar a _context.<Database>
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryPercent> CategoriesPercent { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<FixedCost> FixedCosts { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<ExpensesCost> ExpensesCosts { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestImage> RequestImages { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitDescriptionLine> UnitDescriptionLines { get; set; }
        public DbSet<Worker> Workers { get; set; }
        //public DbSet<TenMan.Web.Data.Entities.Field> Field { get; set; }
    }
}