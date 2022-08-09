using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public class TableWrapper : WebElementWrapper, ITableWrapper
    {
        private readonly string _searchAttribute = ConfigServiceBase.SearchAttribute;

        public TableWrapper([NotNull] IWebElementWrapper wrapper,
            string caption,
            [NotNull] IDriverService driverService)
            : base(() => wrapper.Element, caption, driverService)
        {
        }

        public IEnumerable<ITableRowWrapper> Rows
        {
            get
            {
                return GetRowElementsFromCurrentView()
                    .Select(r => new TableRowWrapper(r, r.Caption, Driver));
            }
        }

        private IEnumerable<IWebElementWrapper> GetRowElementsFromCurrentView()
        {
            return FindSubElements(By.XPath($".//*[@{_searchAttribute}='row']"), $"{Caption} row");
        }
    }
}