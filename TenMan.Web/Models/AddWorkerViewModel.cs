using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Models
{
    public class AddWorkerViewModel : AddUserViewModel
    {

        [Display(Name = "Matricula")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string RegisterNumber { get; set; }

        public int SpecialtyId { get; set; }
        public IEnumerable<SelectListItem> Specialties { get; set; }
    }
}
