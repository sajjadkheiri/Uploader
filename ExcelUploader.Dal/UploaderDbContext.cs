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

        public DbSet<Employee> employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=myserver;Database=mydatabase;User Id=myuser;Password=mypassword;");
        }
    }
}
