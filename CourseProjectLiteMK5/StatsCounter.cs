using CourseProjectLiteMK5.Data;
using Microsoft.Extensions.Caching.Memory;

namespace CourseProjectLiteMK5
{
    public class StatsCounter
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public StatsCounter(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public int GetUserCount()
        {
            if (!_cache.TryGetValue("UserCount", out int userCount))
            {
                userCount = _context.Users.Count();

                _cache.Set("UserCount", userCount, TimeSpan.FromMinutes(30));
            }

            return userCount;
        }

        public int GetPostCount()
        {
            if (!_cache.TryGetValue("PostCount", out int postCount))
            {
                postCount = _context.Posts.Count();

                _cache.Set("PostCount", postCount, TimeSpan.FromMinutes(30));
            }

            return postCount;
        }
    }
}
