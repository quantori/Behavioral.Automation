using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class TableWrapper : WebElementWrapper
{
    public readonly TableSelector TableSelector;

    public TableWrapper(WebContext webContext, TableSelector tableSelector, string caption) :
        base(webContext, tableSelector, caption)
    {
        TableSelector = tableSelector;
    }

    public ILocator Rows => GetChildLocator(TableSelector.RowSelector);

    public ILocator CellsSelector => GetChildLocator(TableSelector.CellSelector);

    public ILocator? HeaderCells { get; set; }

    public ILocator GetCellsForRow(ILocator row)
    {
        return GetChildLocator(row, TableSelector.CellSelector);
    }
}