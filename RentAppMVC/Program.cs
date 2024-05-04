using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentAppMVC.Data;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer; // Import your ProductLogic namespace

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
// Register ProductLogic as a scoped service
builder.Services.AddScoped<ProductLogic>();
builder.Services.AddScoped<PrivateCustomerLogic>();
builder.Services.AddScoped<BusinessCustomerLogic>();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ShoppingCart>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Other configurations...

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "shoppingCart",
    pattern: "ShoppingCart",
    defaults: new { controller = "ShoppingCart", action = "Index" });

// Other configurations...

app.MapRazorPages();

app.Run();
