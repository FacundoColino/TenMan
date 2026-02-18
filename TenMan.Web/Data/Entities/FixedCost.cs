using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class FixedCost
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Importe")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Field Field { get; set; }
        public ICollection<ReceiptImage> ReceiptImages { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //[RegularExpression(@"^[a-zA-Z\s]+$",
        //ErrorMessage = "Solo se permiten letras")]
        public Category Category { get; set; }

        public Committee Committee { get; set; }
    }
}
