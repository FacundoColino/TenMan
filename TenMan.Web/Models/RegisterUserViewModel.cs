using System.ComponentModel.DataAnnotations;

namespace TenMan.Web.Models
{
    public class RegisterUserViewModel : AddUserViewModel
    {
        [Required]
        public string SelectedRole { get; set; }
    }
}
