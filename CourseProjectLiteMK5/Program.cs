using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CourseProjectLiteMK5.Data;
using CourseProjectLiteMK5.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DevDbConnectionString") ?? 
    throw new InvalidOperationException("Connection string 'DevDbConnectionString' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapRazorPages();

app.MapAreaControllerRoute(
    name: "IdentityArea",
    areaName: "Identity",
    pattern: "Identity/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "MainArea",
    areaName: "Main",
    pattern: "Main/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();