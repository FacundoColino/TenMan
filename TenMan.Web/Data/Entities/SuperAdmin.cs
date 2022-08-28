using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class SuperAdmin
    {
        public int Id { get; set; }

        public User User { get; set; }
    }
}
