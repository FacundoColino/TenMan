using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Letra")]
        public string Letra { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Description { get; set; }

        public Committee Committee { get; set; }
    }
}
