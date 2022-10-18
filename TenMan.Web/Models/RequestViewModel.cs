using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class RequestViewModel : Request
    {
        public int UnitId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    	[Display(Name = "Especialidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad.")]
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado.")]
        public int StatusTypeId { get; set; }

        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Especialista")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un especialista.")]
        public int WorkerId { get; set; }

        public IEnumerable<SelectListItem> Specialties { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        public IEnumerable<SelectListItem> StatusTypes { get; set; }

    }
}
