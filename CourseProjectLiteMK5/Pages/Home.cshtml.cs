using CourseProjectLiteMK5.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProjectLiteMK5.Pages
{
    [Authorize(Policy = "AccessCookiePolicy")]
    public class HomeModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public HomeModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IdentityUser? CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }
    }
}
