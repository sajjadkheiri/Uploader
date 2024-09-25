using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uploader.Domain.Abstraction;

namespace Uploader.Domain.Entities.ExcelFile
{
    public interface IExcelFileRepository : IRepository<ExcelFile>
    {
    }
}
