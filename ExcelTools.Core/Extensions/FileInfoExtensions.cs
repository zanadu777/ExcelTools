using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTools.Core.Extensions
{
    public static class FileInfoExtensions
    {
        public static XLWorkbook ToXLWorkbook(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                throw new FileNotFoundException($"The file '{fileInfo.FullName}' does not exist.", fileInfo.FullName);

            return new XLWorkbook(fileInfo.FullName);
        }
    }
}
