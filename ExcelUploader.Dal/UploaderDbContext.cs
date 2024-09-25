using ExcelUploader.Dal.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ExcelUploader.Dal
{
    public class UploaderDbContext : DbContext
    {
        public UploaderDbContext(DbContextOptions<UploaderDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}