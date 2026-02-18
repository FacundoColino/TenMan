using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class CommitteeViewModel
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


        [Display(Name = "Clave SUTERH")]
        [MaxLength(8, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string SuterhKey { get; set; }

        [Display(Name = "CUIT")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres. Recuerde incluir los guiones")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string CUIT { get; set; }

        public ICollection<Field> Fields { get; set; }
        public IList<Category> Categories { get; set; }
        public IEnumerable<SelectListItem> Administrators { get; set; }
        public int? AdministratorId { get; set; }
    }
}
