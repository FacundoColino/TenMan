using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha de Pago")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Importe")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        [Display(Name = "Estado")]
        public string Status { get; set; }

        [Display(Name = "Comprobante")]
        public string PdfFile { get; set; }

        // TODO: Change the path when publish
        public string PdfFullPath => string.IsNullOrEmpty(PdfFile)
            ? null
            : $"https://TBD.azurewebsites.net{PdfFile.Substring(1)}";

        public Receipt Receipt { get; set; }

        public Tenant Tenant { get; set; }
    }
}
