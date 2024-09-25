using Uploader.Domain.Abstraction;
using Uploader.ApplicationService.Dto;
using OfficeOpenXml;

namespace Uploader.ApplicationService.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task ProcessItems()
        {
            var items = await _unitOfWork.EmployeeRepository.GetUnprocessedItems();

            foreach (var item in items)
            {
                var stream = new FileStream(item.FilePath, FileMode.Open, FileAccess.Read);
                var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;


                for (int i = 2; i <= rowCount; i++)
                {
                    var employee = new Domain.Entities.Employee.Employee(Convert.ToString(worksheet.Cells[i, 1].Value)!.Trim(),
                                                                     Convert.ToString(worksheet.Cells[i, 2].Value)!.Trim());

                    await _unitOfWork.EmployeeRepository.AddAsync(new Domain.Entities.Employee.Employee(employee.FirstName, employee.LastName));
                }
            }
       
            await _unitOfWork.CompleteAsync();
        }

        public async Task BulkProcessItems()
        {
            var items = await _unitOfWork.EmployeeRepository.GetUnprocessedItems();

            foreach (var item in items)
            {
                var stream = new FileStream(item.FilePath, FileMode.Open, FileAccess.Read);
                var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                var entities = new List<Domain.Entities.Employee.Employee>();
                
                for (int i = 2; i <= rowCount; i++)
                {
                    var employee = new Domain.Entities.Employee.Employee(Convert.ToString(worksheet.Cells[i, 1].Value)!.Trim(),
                                                                     Convert.ToString(worksheet.Cells[i, 2].Value)!.Trim());
                    entities.Add(employee);
                }

                var chunkSize = 100;
                var chunks = entities.Chunk(chunkSize);

                foreach (var chunk in chunks)
                {
                    var task = _unitOfWork.EmployeeRepository.AddRangeAsync(chunk);

                    await Task.WhenAll(task);
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task SaveEmployee(List<EmployeeInputDto> employees)
        {
            throw new NotImplementedException();
        }
    }
}
