using Microsoft.AspNetCore.Builder;
using RentalService.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentalService.Business;
using RentalService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<ICategoryData, CategoryDataLogic>();
builder.Services.AddSingleton<ICategoryAccess, CategoryAccess>();
builder.Services.AddSingleton<IProductData, ProductDataLogic>();
builder.Services.AddSingleton<IProductAccess, ProductAccess>();
builder.Services.AddSingleton<IBusinessCustomerData, BusinessCustomerDataLogic>();
builder.Services.AddSingleton<IBusinessCustomerAccess, BusinessCustomerAccess>();
builder.Services.AddSingleton<IPrivateCustomerData, PrivateCustomerDataLogic>();
builder.Services.AddSingleton<IPrivateCustomerAccess, PrivateCustomerAccess>();
builder.Services.AddSingleton<IOrderData, OrderDataLogic>();
builder.Services.AddSingleton<IOrderAccess, OrderAccess>();
builder.Services.AddSingleton<IOrderLineData, OrderLineDataLogic>();
builder.Services.AddSingleton<IOrderLineAccess, OrderLineAccess>();
builder.Services.AddSingleton<IProductCopyData, ProductCopyDataLogic>();
builder.Services.AddSingleton<IProductCopyAccess, ProductCopyAccess>();

// Configure the JWT Authentication Service
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
    //amo kefak
    .AddJwtBearer("JwtBearer", jwtOptions => {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            // The SigningKey is defined in the TokenController class
            ValidateIssuerSigningKey = true,
            // IssuerSigningKey = new SecurityHelper(configuration).GetSecurityKey(),
            IssuerSigningKey = new SecurityHelper(builder.Configuration).GetSecurityKey(),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://localhost:7023",
            ValidAudience = "https://localhost:7023",
            ValidateLifetime = true
        };
    });

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

app.UseAuthentication();
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
