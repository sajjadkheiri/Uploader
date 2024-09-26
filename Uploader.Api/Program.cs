using System.Reflection;
using OfficeOpenXml;
using Uploader.ApplicationService;
using Uploader.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Uploader Api", Version = "v1" });
       });

ConfigureAdditionalServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.Run();

void ConfigureAdditionalServices()
{
    var services = builder.Services;

    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
}