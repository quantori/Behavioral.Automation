using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for testing of the downloaded files
    /// </summary>
    [Binding]
    public sealed class DownloadsBinding
    {
        /// <summary>
        /// Check that file was downloaded
        /// </summary>
        /// <param name="filename">Name of the tested file</param>
        /// <example>Then "Test.pdf" file should be downloaded</example>
        [Then("\"(.*?)\" file should be downloaded")]
        public void CheckFileDownloaded(string filename)
        {
            var filePath = ConfigServiceBase.DownloadPath + filename;
            Assert.ShouldBecome(() => File.Exists(filePath), true, $"file {filePath} does not exists");

            var length = new FileInfo(filePath).Length;
            Assert.ShouldBecome(() => length == new FileInfo(filePath).Length && length != 0, true, $"file {filePath} is empty");
            
        }

        /// <summary>
        /// Check Excel file data
        /// </summary>
        /// <param name="filename">Name of the tested file</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table which stores excel table rows</param>
        /// <example>
        /// Then "Test.xsls" file should contain the following data:
        /// | row1  | row2  |
        /// | data1 | data2 |
        /// </example>
        [Then("\"(.*?)\" file should (contain|not contain) the following data:")]
        public void CheckExcelFileData(string filename, string behavior, Table table)
        {
            var filePath = ConfigServiceBase.DownloadPath + filename;
            var dataList = ListServices.ExcelToCellsList(FileService.GetExcelFileAsDataSet(filePath), 
                    table.RowCount, table.Rows.First().Count);

            var check =  dataList.SequenceEqual(ListServices.TableToCellsList(table));
            
            File.Delete(filePath);
            check.Should().Be(!behavior.Contains("Not"));
        }
    }
}
