using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class UnitViewModel : Unit
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Locatario")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un locatario.")]
        public int TenantId { get; set; }
        public IEnumerable<SelectListItem> Tenants { get; set; }
    }
}
