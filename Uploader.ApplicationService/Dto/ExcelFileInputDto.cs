
namespace Uploader.ApplicationService.Dto
{
    public class ExcelFileInputDto
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string FilePath { get; set; }
    }
}
