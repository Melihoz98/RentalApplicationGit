using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentAppMVC.Data;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IProductAccess, ProductAccess>();
builder.Services.AddScoped<ProductLogic>();
builder.Services.AddScoped<PrivateCustomerLogic>();
builder.Services.AddScoped<BusinessCustomerLogic>();

// Add session
builder.Services.AddSession();

// Register ShoppingCart as a singleton service
builder.Services.AddSingleton<ShoppingCart>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "shoppingCart",
    pattern: "ShoppingCart",
    defaults: new { controller = "ShoppingCart", action = "Index" });

app.MapRazorPages();

app.Run();
