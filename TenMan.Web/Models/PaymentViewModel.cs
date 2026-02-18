using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class PaymentViewModel : Payment
    {
        public int TenantId { get; set; }

        [Display(Name = "Comprobante")]
        public IFormFile PdfFormFile { get; set; }

        [Display(Name = "Unidad Funcional")]
        public int UnitId { get; set; }

        public int CheckingAccountId { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; }
    }
}
