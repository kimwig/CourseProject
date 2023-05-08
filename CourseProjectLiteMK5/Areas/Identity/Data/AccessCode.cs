using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProjectLiteMK5.Areas.Identity.Data
{
    public class AccessCode
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
