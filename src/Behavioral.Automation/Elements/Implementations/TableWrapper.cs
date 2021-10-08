using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements.Implementations
{
    public class TableWrapper : WebElementWrapper, ITableWrapper
    {
        private readonly string _searchAttribute = ConfigServiceBase.SearchAttribute;
        private readonly IVirtualizedElementsSelectionService _virtualizedElementsSelectionService;

        public TableWrapper([NotNull] IWebElementWrapper wrapper, string caption,
            [NotNull] IDriverService driverService,
            IVirtualizedElementsSelectionService virtualizedElementsSelectionService)
            : base(() => wrapper.Element, caption, driverService)
        {
            _virtualizedElementsSelectionService = virtualizedElementsSelectionService;
        }

        public IEnumerable<ITableRowWrapper> Rows
        {
            get
            {
                if (_virtualizedElementsSelectionService.ControlIsVirtualizable(Caption))
                {
                    return _virtualizedElementsSelectionService.FindVirtualized(Caption, GetRowElementsFromCurrentView)
                        .Select(r => new TableRowWrapper(r, r.Caption, Driver));
                }

                return GetRowElementsFromCurrentView()
                    .Select(r => new TableRowWrapper(r, r.Caption, Driver));
            }
        }

        private IEnumerable<IWebElementWrapper> GetRowElementsFromCurrentView()
        {
            return FindSubElements(By.XPath($".//*[@{_searchAttribute}='row']"), $"{Caption} row");
        }

        private IEnumerable<IWebElementWrapper> Cells => FindSubElements(By.XPath(".//div[(contains(@class, 'row'))]"), $"{Caption} cell");

        public IEnumerable<string> CellsText => Assert.ShouldGet(() => Cells.Select(c => c.Text).ToList());
    }
}