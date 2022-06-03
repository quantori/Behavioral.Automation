using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Methods to interact with downloaded files
    /// </summary>
    public static class FileService
    {
        /// <summary>
        /// Convert Excel file into DataSet object
        /// </summary>
        /// <param name="filePath">Path to Excel file</param>
        /// <returns>DataSet object</returns>
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
