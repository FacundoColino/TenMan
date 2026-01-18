using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class AddCostViewModel
    {
        /* 
         Esta clase se utiliza para los FixedCost del Consorcio y los ExpenseCosts de las expensas
         */
        public int FieldId { get; set; }
        public int CommitteeId { get; set; }
        public int ExpenseId { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Importe")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }
        public IEnumerable<SelectListItem> Fields { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
