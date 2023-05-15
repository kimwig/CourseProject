using CourseProjectLiteMK5.Areas.Identity.Data;
using CourseProjectLiteMK5.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseProjectLiteMK5
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetLatestPosts(int count)
        {
            return await _context.Posts
                .OrderByDescending(p => p.CreationDate)
                .Take(count)
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task UpdatePost(Post updatedPost)
        {
            var oldPost = await _context.Posts.FirstOrDefaultAsync(op => op.Id == updatedPost.Id);

            if (oldPost != null)
            {
                oldPost.Title = updatedPost.Title;
                oldPost.Content = updatedPost.Content;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Post not found.");
            }
        }

        public async Task DeletePost(Post deletePost)
        {
            var oldPost = await _context.Posts.FirstOrDefaultAsync(op => op.Id == deletePost.Id);

            if (oldPost != null)
            {
                _context.Posts.Remove(oldPost);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Post not found.");
            }
        }
    }
}
