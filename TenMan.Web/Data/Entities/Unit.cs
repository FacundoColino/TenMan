using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Unit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Unidad")]
        public int Number { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Piso")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Departamento")]
        public string Apartment { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Metros Cuadrados")]
        public int SquareMeters { get; set; }

        public Committee Committee { get; set; }

        public CheckingAccount CheckingAccount { get; set; }

        public  Tenant Tenant { get; set; }
    }
}
