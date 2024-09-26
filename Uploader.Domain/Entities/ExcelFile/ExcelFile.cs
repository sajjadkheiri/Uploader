using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uploader.Domain.Entities.ExcelFile
{
    public class ExcelFile
    {
        public ExcelFile()
        {

        }

        public ExcelFile(string fileName, byte[] file, string filePath, bool isProcessed)
        {
            File = file;
            FileName = fileName;
            FilePath = filePath;
            IsProcessed = isProcessed;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FileName { get; init;}
        public byte[] File { get;init; }
        public string FilePath { get;init; }
        public bool IsProcessed { get; set; }
    }
}
