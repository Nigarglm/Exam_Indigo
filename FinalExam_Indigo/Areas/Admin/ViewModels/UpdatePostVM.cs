using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalExam_Indigo.Areas.Admin.ViewModels
{
    public class UpdatePostVM
    {
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(1500)]
        public string Description { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        public IFormFile? Photo { get; set; }
        public string ImageUrl { get; set; }
    }
}
