using CourseProjectLiteMK5.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CourseProjectLiteMK5
{
    public class ApplicationInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            await CreateAdminRoleAsync();
            await CreateAdminUserAsync();
            await AssignAdminRoleAsync();
        }

        private async Task CreateAdminRoleAsync()
        {
            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        private async Task CreateAdminUserAsync()
        {
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new User { 
                    Id = Guid.NewGuid().ToString(),
                    CreationDate = DateTime.Now,
                    UserName = "admin",
                    Email = "admin@administrator.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };
                await _userManager.CreateAsync(adminUser, "AdminPassword1234567890!");
            }
        }

        private async Task AssignAdminRoleAsync()
        {
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(adminUser, "Admin");
                if (!isAdmin)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

    }
}
