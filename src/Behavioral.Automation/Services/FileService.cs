using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;

namespace Behavioral.Automation.Services
{
    public static class FileService
    {
        public static DataSet GetExcelFileAsDataSet(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                return excelReader.AsDataSet();
            }
        }
    }
}
