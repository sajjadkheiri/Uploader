using Uploader.ApplicationService.Dto;

namespace Uploader.ApplicationService.Services.ExcelUploader
{
    public interface IExcelUploaderService
    {        
        Task UploadFileAsync(ExcelFileInputDto input);
    }
}
