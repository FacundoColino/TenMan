using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class ExpensesViewModel
    {
        public Committee Committee { get; set; }
        public IEnumerable<Field> Fields { get; set; }
    }
}
