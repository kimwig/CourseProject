using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProjectLiteMK5.Areas.Identity.Data
{
    public class Post
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(8192)]
        public string? Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Required]
        public string? UserId { get; set;}



        public User? User { get; set; }
    }
}
