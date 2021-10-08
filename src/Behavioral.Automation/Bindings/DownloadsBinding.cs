using System.IO;
using System.Linq;
using FluentAssertions;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class DownloadsBinding
    {

        [Then("\"(.*?)\" file should be downloaded")]
        public void CheckFileDownloaded(string filename)
        {
            var filePath = ConfigServiceBase.DownloadPath + filename;
            Assert.ShouldBecome(() => File.Exists(filePath), true, $"file {filePath} does not exists");

            var length = new FileInfo(filePath).Length;
            Assert.ShouldBecome(() => length == new FileInfo(filePath).Length && length != 0, true, $"file {filePath} is empty");
            
        }

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
