using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentAppMVC.Data;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer; 

var builder = WebApplication.CreateBuilder(args);


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
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ShoppingCart>();
builder.Services.AddScoped<ShoppingCartLogic>();
builder.Services.AddScoped<ProductCopyLogic>();
builder.Services.AddScoped<OrderLogic>();
builder.Services.AddScoped<OrderLineLogic>();
builder.Services.AddScoped<IOrderAccess, OrderAccess>();
builder.Services.AddScoped<IProductCopyAccess, ProductCopyAccess>();


builder.Services.AddScoped<IPrivateCustomerAccess, PrivateCustomerAccess>();
builder.Services.AddScoped<IBusinessCustomerAccess, BusinessCustomerAccess>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
     https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "shoppingCart",
    pattern: "ShoppingCart",
    defaults: new { controller = "ShoppingCart", action = "Index" });



app.MapRazorPages();

app.Run();
