using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uploader.Domain.Abstraction;
using Uploader.Domain.Entities.ExcelFile;

namespace Uploader.Domain.Entities.Employee
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<ExcelFile.ExcelFile>> GetUnprocessedItems();
    }
}
