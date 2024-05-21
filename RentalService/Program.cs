using Microsoft.AspNetCore.Builder;
using RentalService.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentalService.Business;
using RentalService.DataAccess;

var builder = WebApplication.CreateBuilder(args);


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
builder.Services.AddSingleton<IAspNetUserData, AspNetUserDataLogic>();
builder.Services.AddSingleton<IAspNetUserAccess, AspNetUserAccess>();


builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
    
    .AddJwtBearer("JwtBearer", jwtOptions => {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            
            ValidateIssuerSigningKey = true,
            
            IssuerSigningKey = new SecurityHelper(builder.Configuration).GetSecurityKey(),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://localhost:7023",
            ValidAudience = "https://localhost:7023",
            ValidateLifetime = true
        };
    });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
   
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    
    
});

app.Run();
