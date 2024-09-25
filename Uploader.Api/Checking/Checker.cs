using Uploader.Controllers;

namespace Uploader.Checking;

public class Checker
{
    private readonly ILogger<UploaderController> _logger;

    public Checker(ILogger<UploaderController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public ResultChecker FileExtensionChecking_Excel(string fileName)
    {
        var resultChecker = new ResultChecker();
        var fileExtension = Path.GetExtension(fileName).ToLower();

        if (fileExtension is ".xls" or ".xlsx")
        {
            resultChecker.IsChecked = true;
        }
        else
        {
            var logMessage = $"{fileName} is not an excel file.";

            _logger.Log(LogLevel.Warning, logMessage);
            {
                resultChecker.IsChecked = false;
                resultChecker.Message = logMessage;
            }
        }

        return resultChecker;
    }

    public ResultChecker FileLengthChecking_Excel(IFormFile file)
    {
        var resultChecker = new ResultChecker();

        if (file.Length == default)
        {
            var logMessage = $"{file.FileName} is Empty.";

            _logger.Log(LogLevel.Warning, logMessage);
            {
                resultChecker.IsChecked = false;
                resultChecker.Message = logMessage;
            }
        }
        else
        {
            resultChecker.IsChecked = true;
        }

        return resultChecker;
    }
}