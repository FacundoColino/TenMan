using Microsoft.EntityFrameworkCore;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestImage> RequestImages { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Worker> Workers { get; set; }

    }
}
