using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements.Implementations
{
    public class TableRowWrapper : WebElementWrapper, ITableRowWrapper
    {
        private readonly string _searchAttribute = ConfigServiceBase.SearchAttribute;

        public TableRowWrapper(IWebElementWrapper wrapper, string caption, IDriverService driverService) :
            base(() => wrapper.Element, caption, driverService)
        {
        }

        private IEnumerable<IWebElementWrapper> Cells
        {
            get
            {
                foreach (var cell in FindSubElements(By.XPath($".//*[@{_searchAttribute}='cell']"),
                    $"{Caption} cell"))
                {
                    yield return cell;
                }

            }
        }

        public IEnumerable<string> CellsText
        {
            get
            {
                if (Cells.Any())
                {
                    return Assert.ShouldGet(() => Cells.Select(c => c.Text));
                }

                return Assert.ShouldGet(() => new[] { Text });
            }
        }
    }
}
