using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Data.Entities
{
    public class Tenant
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Unit> Units { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
