using ExcelUploader.Dal;
using ExcelUploader.Dal.Model;
using OfficeOpenXml;

namespace Uploader;

public class ExcelProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public ExcelProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// From the given file path, opens excel file and inserts customers to database.
    /// </summary>
    /// <param name="filePath"></param>
    public void ProcessExcelFile(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var package = new ExcelPackage(stream);

        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension.Rows;

        for (var i = 2; i <= rowCount; i++)
        {
            if (TryGetCustomer(worksheet, i, out var employee)) continue; // Ignore Empty rows.

            SaveCustomerToDatabase(employee); // Ignoring bulk insertion to get the feeling of big excel file.
        }
    }

    private static bool TryGetCustomer(ExcelWorksheet worksheet, int i, out Employee employee)
    {
        employee = null;
        if (worksheet.Cells[i, 1].Value is null || worksheet.Cells[i, 2].Value is null) return true;

        employee = new Employee(
            Convert.ToString(worksheet.Cells[i, 1].Value)!.Trim(),
            Convert.ToString(worksheet.Cells[i, 2].Value)!.Trim());
        return false;
    }

    private void SaveCustomerToDatabase(Employee employee)
    {
        var context = _serviceProvider.GetRequiredService<UploaderDbContext>();
        
        context.Employees.Add(employee);
        context.SaveChanges();

        Thread.Sleep(1000);
    }
}