using Uploader.Domain.Abstraction;
using Uploader.Domain.Entities.Employee;
using Uploader.Domain.Entities.ExcelFile;
using Uploader.Persistance.EF;
using Uploader.Persistance.Repository.Employee;
using Uploader.Persistance.Repository.ExcelFile;

namespace Uploader.Persistance.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly UploaderDbContext _dbContext;

        public UnitOfWork(UploaderDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(_dbContext);
            ExcelFileRepository = new ExcelFileRepository(_dbContext);
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEmployeeRepository EmployeeRepository { get; }

        public IExcelFileRepository ExcelFileRepository { get; }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
