using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Models
{
    public class EditAdminViewModel : EditUserViewModel
    {
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Inscripción RPA")]
        public string RegisterNumber { get; set; }

        [Display(Name = "CUIT")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres. Recuerde incluir los guiones")]
        public string CUIT { get; set; }
    }
}
