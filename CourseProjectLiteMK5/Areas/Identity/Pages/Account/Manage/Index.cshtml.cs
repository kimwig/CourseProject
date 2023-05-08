#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CourseProjectLiteMK5.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace CourseProjectLiteMK5.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Policy = "AccessCookiePolicy")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public IndexModel(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
    }
}
