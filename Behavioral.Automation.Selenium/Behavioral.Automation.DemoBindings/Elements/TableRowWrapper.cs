using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public class TableRowWrapper : WebElementWrapper, ITableRowWrapper
    {
        private readonly string _searchAttribute = ConfigServiceBase.SearchAttribute;

        public TableRowWrapper(IWebElementWrapper wrapper, string caption, IDriverService driverService) :
            base(() => wrapper.Element, caption, driverService)
        {
        }

        public IEnumerable<IWebElementWrapper> Cells =>
            FindSubElements(By.XPath($".//*[@{_searchAttribute}='table-cell']"),
                $"{Caption} cell");

        public IEnumerable<string> CellsText
        {
            get
            {
                return Cells.Any()
                    ? Assert.ShouldGet(() => Cells.Select(c => c.Text))
                    : Assert.ShouldGet(() => new[] { Text });
            }
        }
    }
}