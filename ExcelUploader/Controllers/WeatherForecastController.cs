using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;

namespace ExcelUploader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploaderController : ControllerBase
    {
        private readonly UploaderDbContext _dbContext;

        public UploaderController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet();
                    var dataTable = dataSet.Tables[0];

                    // assuming you have a model called "MyData" with properties matching the Excel columns
                    var data = new List<MyData>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        data.Add(new MyData
                        {
                            Column1 = row["Column1"].ToString(),
                            Column2 = row["Column2"].ToString(),
                            // ...
                        });
                    }

                    await _dbContext.MyData.AddRangeAsync(data);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return Ok("File uploaded successfully");
        }
    }
}
