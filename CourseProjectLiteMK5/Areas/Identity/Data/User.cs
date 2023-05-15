using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CourseProjectLiteMK5.Areas.Identity.Data;

public class User : IdentityUser
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }

    public ICollection<Post> Posts { get; set; }
}

