using Microsoft.AspNetCore.Mvc;
using Uploader.Checking;
using Uploader.ApplicationService.Dto;
using Uploader.ApplicationService.Services.Employee;
using Uploader.ApplicationService.Services.ExcelUploader;

namespace Uploader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploaderController : ControllerBase
    {
        private readonly ILogger<UploaderController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IExcelUploaderService _excelUploaderService;

        public UploaderController(ILogger<UploaderController> logger,
                                  IExcelUploaderService excelUploaderService,
                                  IEmployeeService employeeService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _excelUploaderService = excelUploaderService ?? throw new ArgumentNullException(nameof(excelUploaderService));
        }

        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            try
            {
                var checkRules = CheckExcelFile(file, out var actionResult);

                if (!checkRules)
                    return actionResult;

                var fileByteArray = ConvertFormFileToByteArray(file);

                ExcelFileInputDto excelFileInputDto = new ExcelFileInputDto()
                {
                    File = fileByteArray,
                    FileName = file.FileName,
                    FilePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles", file.FileName)
                };

                await _excelUploaderService.UploadFileAsync(excelFileInputDto);


                return Ok("File uploaded successfully.");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception.Message);
                return Problem("Unknown error occurred.");
            }
        }

        private static byte[] ConvertFormFileToByteArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
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