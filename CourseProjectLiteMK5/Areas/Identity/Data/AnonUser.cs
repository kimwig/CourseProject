using System.ComponentModel.DataAnnotations;

namespace CourseProjectLiteMK5.Areas.Identity.Data
{
    public class AnonUser
    {
        [Key]
        public string? Identifier { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
