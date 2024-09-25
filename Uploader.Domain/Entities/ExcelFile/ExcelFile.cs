namespace Uploader.Domain.Entities.ExcelFile
{
    public class ExcelFile
    {
        public ExcelFile(string fileName, byte[] file, string filePath)
        {
            File = file;
            FileName = fileName;
            FilePath = filePath;
        }

        public string FileName { get; }
        public byte[] File { get; }
        public string FilePath { get; }
        public bool IsProcessed { get; set; }
    }
}
