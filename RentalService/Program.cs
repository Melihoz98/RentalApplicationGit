using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RentalService.Business;
using RentalService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<ICategoryData, CategorydataLogic>();
builder.Services.AddSingleton<ICategoryAccess, CategoryAccess>();
builder.Services.AddSingleton<IProductData, ProductDataLogic>();
builder.Services.AddSingleton<IProductAccess, ProductAccess>();



// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
    // Optionally, configure XML documentation
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    // Optionally, configure the Swagger UI route
    // c.RoutePrefix = "api/docs";
});

app.Run();
