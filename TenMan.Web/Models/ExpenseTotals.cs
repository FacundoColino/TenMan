using System.Collections.Generic;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class ExpenseTotals
    {
        public IList<TotalPerCategory> TotalsPerCategory { get; set; }
        = new List<TotalPerCategory>();
        public decimal Total { get; set; }
    }
}
