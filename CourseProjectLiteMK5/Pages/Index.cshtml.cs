#nullable disable

using CourseProjectLiteMK5.Areas.Identity.Data;
using CourseProjectLiteMK5.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseProjectLiteMK5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataProtector _dataProtector;

        public IndexModel(ApplicationDbContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtector = dataProtectionProvider.CreateProtector("AccessService");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [MaxLength(16)]
            [RegularExpression("^[a-öA-Ö0-9]*$", ErrorMessage = "Only letters and numbers are allowed.")]
            public string Code { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.TryGetValue("AccessCookie", out string cookieValue))
            {
                var anonUser = await _context.AnonUsers.FirstOrDefaultAsync(anonUser => anonUser.Identifier == cookieValue);
                if (anonUser != null)
                {
                    return Redirect("/Home");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var code = await _context.AccessCodes.FirstOrDefaultAsync(ac => ac.Code == Input.Code);
                if (code != null && code.IsActive)
                {
                    var encryptedIdentifier = Encrypt(Guid.NewGuid().ToString());
                    var anonUser = new AnonUser
                    {
                        Identifier = encryptedIdentifier,
                        ExpiryDate = DateTime.Now.AddDays(14)
                    };

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(14),
                        IsEssential = true,
                        Secure = true,
                        HttpOnly = true,
                        MaxAge = TimeSpan.FromDays(14),
                        SameSite = SameSiteMode.Strict
                    };

                    Response.Cookies.Append("AccessCookie", encryptedIdentifier, cookieOptions);

                    await _context.AnonUsers.AddAsync(anonUser);
                    await _context.SaveChangesAsync();

                    return Redirect("/Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect code.");
                    return Page();
                }
            }
            return Page();
        }

        public string Encrypt(string input)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(input);
            byte[] protectedBytes = _dataProtector.Protect(plainTextBytes);
            return Convert.ToBase64String(protectedBytes);
        }

        public string Decrypt(string input)
        {
            byte[] protectedBytes = Encoding.UTF8.GetBytes(input);
            byte[] plainTextBytes = _dataProtector.Unprotect(protectedBytes);
            return Encoding.UTF8.GetString(plainTextBytes);
        }
    }
}
