using Uploader.ApplicationService.Dto;

namespace Uploader.ApplicationService.Services.Employee
{
    public interface IEmployeeService
    {
        Task ProcessItems();
        Task BulkProcessItems();
        Task SaveEmployee(List<EmployeeInputDto> employees);
    }
}
