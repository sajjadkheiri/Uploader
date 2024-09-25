using Uploader.ApplicationService.Dto;
using Uploader.ApplicationService.Mapper;
using Uploader.Domain.Abstraction;
using Uploader.Domain.Entities.ExcelFile;

namespace Uploader.ApplicationService.Services.ExcelUploader
{
    public class ExcelUploaderService : IExcelUploaderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcelUploaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task UploadFileAsync(ExcelFileInputDto input)
        {
            await _unitOfWork.ExcelFileRepository.AddAsync(Mapper.Mapper.Map(input));
        }

        public async Task ProcessItems()
        {
            // TODO
            //_unitOfWork.
        }
    }
}
