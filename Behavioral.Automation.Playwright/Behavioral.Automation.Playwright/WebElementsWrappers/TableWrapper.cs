using System;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class TableWrapper : WebElementWrapper, ITableWrapper
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
        var cellSelector = CellsSelector.IdSelector ?? CellsSelector.XpathSelector;
        return row.Locator(cellSelector);
    }

    public async Task ShouldContainRowsAsync(Table expectedTable)
    {
        for (var i = 0; i < expectedTable.Rows.Count; i++)
        {
            var rowToCheck = Rows.Nth(i);
            var actualRowValues = await GetCellsForRow(rowToCheck).AllTextContentsAsync();
            var actualRowValuesTrimmed = actualRowValues.Select(s => s.Trim());
            CollectionAssert.IsSubsetOf(expectedTable.Rows[i].Values, actualRowValuesTrimmed,
                $"Row #{i}{Environment.NewLine}Row selector:{await rowToCheck.TextContentAsync()}{Environment.NewLine}Cell selector:{CellsSelector}{Environment.NewLine}");
        }
    }
}