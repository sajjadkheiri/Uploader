using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Uploader.ApplicationService.Services.Employee;

namespace Uploader.WindowsService
{
    public class ExcelProcessorService : BackgroundService
    {
        private readonly IEmployeeService _employeeService;

        public ExcelProcessorService(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var intervalTime = new TimeSpan(0, 30, 0);

            while (!stoppingToken.IsCancellationRequested)
            {
                // You can choose both of them

                await _employeeService.ProcessItems();

                //await _employeeService.BulkProcessItems();

                await Task.Delay(intervalTime, stoppingToken);
            }
        }
    }
}
