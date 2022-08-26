using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class StatusType
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede contener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Description { get; set; }
    }
}
