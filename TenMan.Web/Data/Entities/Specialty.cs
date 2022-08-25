using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Specialty
    {
        public int Id { get; set; }

        [Display(Name = "Especialidad")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public ICollection<Request> Requests { get; set; }

        public ICollection<Worker> Workers { get; set; }
    }
}
