using Microsoft.AspNetCore.Mvc;
using ExcelUploader;
using Hangfire;
using Uploader.Checking;

namespace Uploader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploaderController : ControllerBase
    {
        private readonly ILogger<UploaderController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UploaderController(ILogger<UploaderController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Inserts uploading excel file's content asynchronously to database using Hangfire background worker.
        /// </summary>
        /// <param name="file">Should be excel file with "xls, xlsx" extensions.</param>
        /// <returns>If the file is a valid excel file and the job is added, it returns successful state.</returns>
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            try
            {
                var checkRules = CheckExcelFile(file, out var actionResult);
                
                if (!checkRules)
                    return actionResult;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles", file.FileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                BackgroundJob.Enqueue(() => new ExcelProcessor(_serviceProvider).ProcessExcelFile(filePath));

                return Ok("File uploaded successfully.");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception.Message);
                return Problem("Unknown error occurred.");
            }
        }

        private bool CheckExcelFile(IFormFile file, out IActionResult actionResult)
        {
            actionResult = null;
            var checker = new Checker(_logger);

            var lengthCheck = checker.FileLengthChecking_Excel(file);

            if (lengthCheck.IsChecked == false)
            {
                actionResult = BadRequest(lengthCheck.Message);
                return lengthCheck.IsChecked;
            }

            var fileExtensionCheck = checker.FileExtensionChecking_Excel(file.FileName);

            if (fileExtensionCheck.IsChecked == false)
            {
                actionResult = BadRequest(fileExtensionCheck.Message);
                return fileExtensionCheck.IsChecked;
            }

            return true;
        }
    }




}