using System.ComponentModel.DataAnnotations;

namespace CourseProjectLiteMK5.Areas.Identity.Data
{
    public class Post
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(64)]
        [RegularExpression("^[a-öA-Ö0-9\\s!\\?+\\-@\\\"\"#\\$%&'()*+´\\-.\\/:;<=>?@\\[\\\\\\]^_`~]*$", ErrorMessage = "Special charecters are not allowed.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(8192)]
        [RegularExpression("^[a-öA-Ö0-9\\s!\\?+\\-@\\\"\"#\\$%&'()*+´\\-.\\/:;<=>?@\\[\\\\\\]^_`~£€{}¤µ|½§]*$", ErrorMessage = "Special charecters are not allowed.")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Required]
        public string UserId { get; set;}



        public User User { get; set; }

        // ** ONLY USE FOR UPDATING POSTS **
        [Required(ErrorMessage ="The Title field is required.")]
        [StringLength(64, ErrorMessage ="The Title field has a maximum length of 64.")]
        [RegularExpression("^[a-öA-Ö0-9\\s!\\?+\\-@\\\"\"#\\$%&'()*+´\\-.\\/:;<=>?@\\[\\\\\\]^_`~]*$", ErrorMessage = "Special charecters are not allowed.")]
        public string NewTitle { get; set; }

        [Required(ErrorMessage = "The Content field is required.")]
        [StringLength(8192, ErrorMessage = "The Content field has a maximum length of 8192.")]
        [RegularExpression("^[a-öA-Ö0-9\\s!\\?+\\-@\\\"\"#\\$%&'()*+´\\-.\\/:;<=>?@\\[\\\\\\]^_`~£€{}¤µ|½§]*$", ErrorMessage = "Special charecters are not allowed.")]
        public string NewContent { get; set; }
        // ** ONLY USE FOR UPDATING POSTS **
    }
}
