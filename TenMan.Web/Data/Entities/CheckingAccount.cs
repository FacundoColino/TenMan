using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class CheckingAccount
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Número")]
        public string Number { get; set; }

        [Display(Name = "Saldo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Balance { get; set; }

        [Display(Name = "Saldo Anterior")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal PreviousBalance { get; set; }

        [Display(Name = "Saldo Pendiente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal PendingBalance { get; set; }

        [Display(Name = "EXP A")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal ExpABalance { get; set; }

        [Display(Name = "Su Pago")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal YourPayment { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Total => Balance + PreviousBalance;
    }
}
