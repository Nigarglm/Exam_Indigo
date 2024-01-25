using System.ComponentModel.DataAnnotations;

namespace FinalExam_Indigo.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(25, ErrorMessage ="En chox 25 element istifade edin")]
        [MinLength(4, ErrorMessage ="En az 4 element istifade edin")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(25, ErrorMessage = "En chox 25 element istifade edin")]
        [MinLength(4, ErrorMessage = "En az 4 element istifade edin")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(30, ErrorMessage = "En chox 30 element istifade edin")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
