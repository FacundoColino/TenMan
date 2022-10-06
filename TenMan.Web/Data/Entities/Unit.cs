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

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Unidad")]
        public int Number { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Piso")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Departamento")]
        public string Apartment { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Metros Cuadrados")]
        public int SquareMeters { get; set; }

        [Display(Name = "Unidad Funcional")]
        public string UnitFullName => $"{Number} - {Floor}{Apartment}";

        public Committee Committee { get; set; }

        public CheckingAccount CheckingAccount { get; set; }

        public  Tenant Tenant { get; set; }
    }
}
