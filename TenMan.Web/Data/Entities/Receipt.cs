using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Receipt
    {
        public int Id { get; set; }

        [Display(Name = "Comprobante")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string FileUrl { get; set; }

        // TODO: Change the path when publish
        public string FileFullPath => $"https://TBD.azurewebsites.net{FileUrl.Substring(1)}";
    }
}
