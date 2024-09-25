using System.Reflection;
using OfficeOpenXml;
using Uploader.ApplicationService;
using Uploader.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddDbContext<UploaderDbContext>(options =>
//{
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

ConfigureAdditionalServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureAdditionalServices()
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var services = builder.Services;

    //services.AddDbContext<UploaderDbContext>(options =>
    //    options.UseSqlServer(connectionString));


    // Set the license context for openXml
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
}