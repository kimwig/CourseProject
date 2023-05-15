using CourseProjectLiteMK5.Areas.Identity.Data;
using CourseProjectLiteMK5.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourseProjectLiteMK5.Pages
{
    [Authorize(Policy = "AccessCookiePolicy")]
    public class SubmitModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public SubmitModel (UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [MaxLength(64)]
            [RegularExpression("^[a-öA-Ö0-9\\s!\\?+\\-@\\\"\"#\\$%&'()*+´\\-.\\/:;<=>?@\\[\\\\\\]^_`~]*$", ErrorMessage = "Special charecters are not allowed.")]
            public string Title { get; set; }

            [Required]
            [MaxLength(8192)]
            [RegularExpression("^[a-öA-Ö0-9\\s!\\?+\\-@\\\"\"#\\$%&'()*+´\\-.\\/:;<=>?@\\[\\\\\\]^_`~£€{}¤µ|½§]*$", ErrorMessage = "Special charecters are not allowed.")]
            public string Content { get; set; }
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Stupid solution don't do this
                return Redirect($"/Identity/Account/Login?returnUrl=%2FSubmit");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (userId != null)
                {
                    var post = new Post
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = Input.Title,
                        Content = Input.Content,
                        CreationDate = DateTime.Now,
                        UserId = userId,
                        NewTitle = "",
                        NewContent = ""
                    };

                    try
                    {
                        await _context.Posts.AddAsync(post);
                        await _context.SaveChangesAsync();
                        ModelState.Clear();
                        StatusMessage = "Post created!";
                    }
                    catch (DbUpdateException exception)
                    {
                        Console.WriteLine($"A database error occurred: {exception.Message}");
                        StatusMessage = "Error: Internal server error.";
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"An error occurred: {exception.Message}");
                        StatusMessage = "Error: Internal server error.";
                    }

                    return RedirectToPage();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Log in to create a post.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
