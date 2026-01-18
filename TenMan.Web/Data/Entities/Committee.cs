using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Committee
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Barrio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Neighborhood { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Address { get; set; }
        
        //Utilizado para calcular expensas
        [Display(Name = "Precio por Metro Cuadrado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        [Display(Name = "Clave SUTERH")]
        [MaxLength(8, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string SuterhKey { get; set; }

        [Display(Name = "CUIT")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres. Recuerde incluir los guiones")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string CUIT { get; set; }

        public ICollection<Unit> Units { get; set; }

        public ICollection<Expenses> Expenses { get; set; }

        public ICollection<FixedCost> FixedCosts { get; set; }

        public ICollection<Field> Fields { get; set; }

        public IList<Category> Categories { get; set; }

        public Administrator Administrator { get; set; }

    }
}
