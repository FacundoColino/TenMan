using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class RequestImage
    {
        public int Id { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string ImageUrl { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => $"https://TBD.azurewebsites.net{ImageUrl.Substring(1)}";

        public Request Request { get; set; }
    }
}
