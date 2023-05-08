using CourseProjectLiteMK5.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.EntityFrameworkCore.Metadata;

namespace CourseProjectLiteMK5.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<AccessCode> AccessCodes { get; set; }
    public DbSet<AnonUser> AnonUsers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>(entity =>
        {
            entity.HasOne(p => p.User)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.UserId);
        });
    }
}
