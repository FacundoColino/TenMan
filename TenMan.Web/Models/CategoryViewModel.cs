using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class CategoryViewModel
    {
        public int CommitteeId { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        public string Letra { get; set; }

        [Display(Name = "Letra")]
        public List<string> Letras { get; set; }
    }
}
