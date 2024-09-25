using Microsoft.EntityFrameworkCore;
using Uploader.Domain.Entities.Employee;
using Uploader.Domain.Entities.ExcelFile;

namespace Uploader.Persistance.EF
{
    public class UploaderDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExcelFile> ExcelFiles { get; set; }
    }
}