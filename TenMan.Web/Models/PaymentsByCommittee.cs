using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class PaymentsByCommittee
    { 
        public int DebtsCant { get; set; }
        public int PaymentsCant { get; set; }

        public Committee committee { get; set; }
    }
}
