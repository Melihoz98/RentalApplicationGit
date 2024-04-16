using RentalService.Business;
using RentalService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddSingleton<ICategoryData, CategorydataLogic>();
builder.Services.AddSingleton<ICategoryAccess, CategoryAccess>();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
