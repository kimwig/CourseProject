using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CourseProjectLiteMK5.Data;
using CourseProjectLiteMK5.Areas.Identity.Data;
using CourseProjectLiteMK5;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DevDbConnectionString") ?? 
    throw new InvalidOperationException("Connection string 'DevDbConnectionString' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/NotAuthorized";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Pages/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AccessCookiePolicy", policy =>
    {
        policy.Requirements.Add(new CookieAuthorizationRequirement("AccessCookie"));
    });
});
builder.Services.AddScoped<IAuthorizationHandler, CookieAuthorizationHandler>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 10;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ0123456789-_";
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddRoles<IdentityRole>()
.AddSignInManager();

builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<StatsCounter>();

builder.Services.AddMemoryCache();

builder.Services.AddDataProtection()
.SetApplicationName("CourseProjectLiteMK5")
.PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "keys")));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ApplicationInitializer>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var appInit = services.GetRequiredService<ApplicationInitializer>();

    await appInit.InitializeAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStatusCodePages();

app.MapRazorPages();
app.MapBlazorHub();

app.MapAreaControllerRoute(
    name: "IdentityArea",
    areaName: "Identity",
    pattern: "Identity/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();