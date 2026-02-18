using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Models
{
    public class BalanceViewModel
    {
        public int UnitId { get; set; }
        public int UnitNumber { get; set; }
        public string OwnerName { get; set; }
        public string AccountNumber { get; set; }

        public decimal PreviousBalance { get; set; }
        public decimal YourPayment { get; set; }
        public decimal PendingBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
