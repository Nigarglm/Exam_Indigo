using System.ComponentModel.DataAnnotations.Schema;

namespace FinalExam_Indigo.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
