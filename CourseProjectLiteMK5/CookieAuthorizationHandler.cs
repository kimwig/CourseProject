#nullable disable

using CourseProjectLiteMK5.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CourseProjectLiteMK5
{
    public class CookieAuthorizationHandler : AuthorizationHandler<CookieAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _dbContext;

        public CookieAuthorizationHandler(IHttpContextAccessor contextAccessor, ApplicationDbContext dbContext)
        {
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CookieAuthorizationRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext.Request.Cookies.TryGetValue(requirement.CookieName, out var cookieValue))
            {
                var anonUser = await _dbContext.AnonUsers.FirstOrDefaultAsync(anonUser => anonUser.Identifier == cookieValue);
                if (anonUser != null)
                {
                    context.Succeed(requirement);
                    await Task.CompletedTask;
                    return;
                }
            }

            context.Fail();
            await Task.CompletedTask;
        }
    }
}
