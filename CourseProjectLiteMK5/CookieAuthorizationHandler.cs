using CourseProjectLiteMK5.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CourseProjectLiteMK5
{
    public class CookieAuthorizationHandler : AuthorizationHandler<CookieAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMemoryCache _cache;

        public CookieAuthorizationHandler(IHttpContextAccessor contextAccessor, ApplicationDbContext dbContext, IMemoryCache cache)
        {
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
            _cache = cache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CookieAuthorizationRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext.Request.Cookies.TryGetValue(requirement.CookieName, out var cookieValue))
            {
                if (_cache.TryGetValue(cookieValue, out bool isAuthorized))
                {
                    if (isAuthorized)
                    {
                        context.Succeed(requirement);
                        await Task.CompletedTask;
                        return;
                    }
                }
                else
                {
                    var anonUser = await _dbContext.AnonUsers.FirstOrDefaultAsync(anonUser => anonUser.Identifier == cookieValue);
                    if (anonUser != null)
                    {
                        _cache.Set(cookieValue, true, TimeSpan.FromMinutes(30));
                        context.Succeed(requirement);
                        await Task.CompletedTask;
                        return;
                    }
                }
            }

            context.Fail();
            await Task.CompletedTask;



            /* Legacy method querying database on every http get request, not good idea for performance etc
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
            */
        }
    }
}
