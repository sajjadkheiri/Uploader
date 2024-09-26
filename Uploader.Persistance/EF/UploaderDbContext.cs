using Microsoft.EntityFrameworkCore;
using Uploader.Domain.Entities.Employee;
using Uploader.Domain.Entities.ExcelFile;

namespace Uploader.Persistance.EF
{
    public class UploaderDbContext : DbContext
    {
        public UploaderDbContext(DbContextOptions<UploaderDbContext> options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExcelFile> ExcelFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Initial catalog=UploaderDb;User=sa;Password=1qaz@WSX;TrustServerCertificate=True;");
        }
    }
}