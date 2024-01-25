using System.ComponentModel.DataAnnotations;

namespace FinalExam_Indigo.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        public string Password { get; set; }

        public bool IsRemembered { get; set; }
    }
}
