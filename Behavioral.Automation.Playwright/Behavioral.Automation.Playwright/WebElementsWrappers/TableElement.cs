using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class TableElement : WebElement, ITableWrapper
{

    public ILocator Rows { get; set; }
    
    public ElementSelector CellsSelector { get; set; }

    public ILocator? HeaderCells { get; set; }
    
    public ILocator GetCellsForRow(ILocator row)
    {
        var cellSelector = CellsSelector.IdSelector ?? CellsSelector.XpathSelector;
        return row.Locator(cellSelector);
    }

    public async Task ShouldBecomeVisibleAsync()
    {
        await Assertions.Expect(Locator).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() {Timeout = 60000});
    }

    public async Task ShouldBecomeVisibleAsync(int delay)
    {
        await Assertions.Expect(Locator).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() {Timeout = delay * 1000});
    }
}