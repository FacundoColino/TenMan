using System.Collections.Generic;
using System.Linq;

namespace TenMan.Web.Models
{
    public class BalanceIndexViewModel
    {
            public string CommitteeName { get; set; }
            public List<BalanceViewModel> UnitsBalances { get; set; }

            public decimal TotalDebt =>
                UnitsBalances.Sum(x => x.Balance);
    }
}
