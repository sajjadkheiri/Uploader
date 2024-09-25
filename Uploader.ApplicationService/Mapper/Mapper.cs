using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uploader.ApplicationService.Dto;
using Uploader.Domain.Entities.Employee;
using Uploader.Domain.Entities.ExcelFile;

namespace Uploader.ApplicationService.Mapper
{
    public static class Mapper
    {
        public static ExcelFile Map(this ExcelFileInputDto dto)
        {
            return new ExcelFile(dto.FileName, dto.File, dto.FilePath);
        }

        public static Employee Map(this EmployeeInputDto dto)
        {
            return new Employee(dto.FirstName, dto.LastName);
        }

        public static ExcelFileInputDto Map(this ExcelFile dto)
        {
            return new ExcelFileInputDto
            {
                File = dto.File,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
            };
        }

        public static EmployeeInputDto Map(this Employee dto)
        {
            return new EmployeeInputDto
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }
    }
}
