using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class TableWrapper : WebElementWrapper
{
    public TableWrapper(WebContext webContext,
        ILocator locator,
        ILocator rowLocator,
        ElementSelector cellsSelector,
        ILocator? headerCellsLocator,
        string caption) :
        base(webContext, locator, caption)
    {
        Rows = rowLocator;
        CellsSelector = cellsSelector;
        HeaderCells = headerCellsLocator;
    }

    public ILocator Rows { get; set; }

    public ElementSelector CellsSelector { get; set; }

    public ILocator? HeaderCells { get; set; }

    public ILocator GetCellsForRow(ILocator row)
    {
        var cellSelector = CellsSelector.IdSelector ?? CellsSelector.Selector;
        return row.Locator(cellSelector);
    }
}