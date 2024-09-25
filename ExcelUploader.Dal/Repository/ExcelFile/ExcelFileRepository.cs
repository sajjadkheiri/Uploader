using Uploader.Domain.Entities.ExcelFile;
using Uploader.Persistance.EF;

namespace Uploader.Persistance.Repository.ExcelFile
{

    public class ExcelFileRepository : Repository<Domain.Entities.ExcelFile.ExcelFile>, IExcelFileRepository
    {
        private readonly UploaderDbContext _dbContext;

        public ExcelFileRepository(UploaderDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public override Task AddAsync(Domain.Entities.ExcelFile.ExcelFile entity)
        {
            string fullPath = Path.Combine(entity.FilePath, entity.FileName);

            File.WriteAllTextAsync(fullPath, entity.File.ToString());

            return base.AddAsync(entity);
        }
    }
}
