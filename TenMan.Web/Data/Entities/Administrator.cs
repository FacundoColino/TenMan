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

        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Telefono Fijo")]
        public string FixedPhone { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Celular")]
        public string CellPhone { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de  {1} caracteres.")]
        [Display(Name = "Matricula")]
        public string RegisterNumber { get; set; }

        [Display(Name = "Nombre y Apellido")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Nombre y Apellido")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public ICollection<Committee> Committees { get; set; }

    }
}
