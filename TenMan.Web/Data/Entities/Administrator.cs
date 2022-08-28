using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenMan.Web.Data.Entities
{
    public class Administrator
    {
        public int Id { get; set; }

        public User User { get; set; }    

        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Matricula")]
        public string RegisterNumber { get; set; }

        public ICollection<Committee> Committees { get; set; }

    }
}
