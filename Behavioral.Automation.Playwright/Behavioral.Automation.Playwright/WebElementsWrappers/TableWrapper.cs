using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class TableWrapper : WebElementWrapper, ITableWrapper
{
    public TableWrapper(WebContext webContext, ILocator locator, ILocator rowLocator, ElementSelector cellsSelector, ILocator? headerCellsLocator, string caption) : base(webContext, locator, caption) {
        Rows = rowLocator;
        CellsSelector = cellsSelector;
        HeaderCells = headerCellsLocator;
    }
    
    public ILocator Rows { get; set; }
    
    public ElementSelector CellsSelector { get; set; }

    public ILocator? HeaderCells { get; set; }
    
    public ILocator GetCellsForRow(ILocator row)
    {
        var cellSelector = CellsSelector.IdSelector ?? CellsSelector.XpathSelector;
        return row.Locator(cellSelector);
    }

    public async Task ColumnsQuantityShouldBeMoreThanZeroAsync()
    {
        var actualTableColumnsQuantity = await GetCellsForRow(Rows.Nth(0)).CountAsync();

        for (var a = 0; a < 4; a++)
        {
            if (actualTableColumnsQuantity > 0) return;
            Thread.Sleep(5000);
            actualTableColumnsQuantity = await GetCellsForRow(Rows.Nth(0)).CountAsync();
        }
        Assert.Fail("Actual table has 0 headers");
    }
}