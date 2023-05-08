using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProjectLiteMK5.Pages
{
    [Authorize(Policy = "AccessCookiePolicy")]
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
