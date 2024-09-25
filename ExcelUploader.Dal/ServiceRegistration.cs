using Uploader.Persistance.EF;
using Uploader.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Uploader.Persistance.Repository;
using Uploader.Domain.Entities.Employee;
using Uploader.Domain.Entities.ExcelFile;
using Microsoft.Extensions.DependencyInjection;
using Uploader.Persistance.Repository.Employee;
using Uploader.Persistance.Repository.ExcelFile;

namespace Uploader.Persistance
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<UploaderDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial catalog=ConfigSampleDb;User=sa;Password=1qaz@WSX;TrustServerCertificate=True;");
            });

            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<IExcelFileRepository, ExcelFileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
