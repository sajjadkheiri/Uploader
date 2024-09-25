using Microsoft.Extensions.DependencyInjection;
using Uploader.ApplicationService.Services.Employee;
using Uploader.ApplicationService.Services.ExcelUploader;

namespace Uploader.ApplicationService
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IExcelUploaderService, ExcelUploaderService>();

            return services;
        }
    }
}
