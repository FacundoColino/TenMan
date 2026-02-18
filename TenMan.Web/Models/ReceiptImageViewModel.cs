using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class ReceiptImageViewModel : ReceiptImage
    {
        [Display(Name = "Comrpobante")]
        public IFormFile ImageFile { get; set; }
    }
}
