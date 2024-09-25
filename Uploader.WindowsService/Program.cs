using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Uploader.WindowsService.WindowsService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory);

            IConfiguration configuration = builder.Build();

            return Host.CreateDefaultBuilder(args)
                            .UseWindowsService(options =>
                            {
                                options.ServiceName = "Excel Processor Service";
                            })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton(configuration);
                        services.AddHostedService<ExcelProcessorService>();
                    });
        }
    }
}
