using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Worker
    {
        public int Id { get; set; }

        public User User { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Matricula")]
        public string RegisterNumber { get; set; }

        public Specialty Specialty { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
