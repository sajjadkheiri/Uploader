using Uploader.Domain.Entities.Employee;
using Uploader.Persistance.EF;

namespace Uploader.Persistance.Repository.Employee
{
    public class EmployeeRepository : Repository<Domain.Entities.Employee.Employee>, IEmployeeRepository
    {
        private readonly UploaderDbContext _dbContext;

        public EmployeeRepository(UploaderDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Domain.Entities.ExcelFile.ExcelFile>> GetUnprocessedItems()
        {
            return _dbContext.ExcelFiles.Where(x => x.IsProcessed == false).ToList();
        }
    }
}
