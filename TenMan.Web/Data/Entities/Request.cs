using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Request
    {
        public int Id { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        //Agregar fecha estimada

        [Display(Name = "Fecha Fin")]        
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [Display(Name = "Fecha Fin")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateLocal => EndDate.ToLocalTime();

        [Display(Name = "Estado Actual")]
        public string ActualStatus { get; set; }

        [Display(Name = "Observaciones")]
        public string Remarks { get; set; }

        [Display(Name = "Especialidad")]
        public Specialty Speciality { get; set; }

        public ICollection<RequestImage> Images { get; set; }

        public Unit Unit { get; set; }

        public Worker Worker { get; set; }

        public ICollection<Status> Statuses { get; set; }

    }
}
