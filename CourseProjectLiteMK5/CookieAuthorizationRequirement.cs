using Microsoft.AspNetCore.Authorization;

namespace CourseProjectLiteMK5
{
    public class CookieAuthorizationRequirement : IAuthorizationRequirement
    {
        public string CookieName { get; }

        public CookieAuthorizationRequirement(string cookieName)
        {
            CookieName = cookieName;
        }
    }
}
