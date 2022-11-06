using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Administrator
    {
        public int Id { get; set; }

        public User User { get; set; }    

        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Inscripción RPA")]
        public string RegisterNumber { get; set; }

        [Display(Name = "CUIT")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres. Recuerde incluir los guiones")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string CUIT { get; set; }

        public ICollection<Committee> Committees { get; set; }

    }
}
