using Uploader.Domain.Entities.Employee;
using Uploader.Domain.Entities.ExcelFile;

namespace Uploader.Domain.Abstraction
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IExcelFileRepository ExcelFileRepository { get; }
        Task CompleteAsync();
    }
}
