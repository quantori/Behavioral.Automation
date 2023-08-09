using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

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

    public async Task ShouldContainTable(Table expectedTable)
    {
        var comparingResult = await DoesTableWithHeadersContainTablesRows(expectedTable, false);
        for (var c = 0; c < 4; c++)
        {
            if (comparingResult) break;
            Thread.Sleep(5000);
            if (c != 3)
            {
                comparingResult = await DoesTableWithHeadersContainTablesRows(expectedTable, false);
            }
            else
            {
                await DoesTableWithHeadersContainTablesRows(expectedTable, true);
            }
        }
    }
    
    private async Task<bool> DoesTableWithHeadersContainTablesRows(Table expectedTable,
        bool throwError)
    {
        if (expectedTable.RowCount == 0)
        {
            if (throwError) Assert.Fail("Actual table doesn't have rows");
            return false;
        }
        
        for (var i = 0; i < expectedTable.RowCount; i++)
        {
            var hasValues = false;
            var expectedRow = expectedTable.Rows[i];
            var uniqueTitle = expectedRow.Keys.First();
            var uniqueValue = expectedRow[uniqueTitle];

            //iterate actual table until you find unique key:
            var actualTableSize = await Rows.CountAsync();
            var actualTableHeaders = await HeaderCells.AllTextContentsAsync();
            var findRowWithUniqueValue = false;
            var allValuesAreValid = true;
            for (var a = 0; a < actualTableSize; a++)
            {
                var cells = await GetCellsForRow(Rows.Nth(a)).AllTextContentsAsync();
                if (!cells[GetIndex(actualTableHeaders, uniqueTitle)].Equals(uniqueValue)) continue;
                findRowWithUniqueValue = true;

                foreach (var header in expectedRow.Keys)
                {
                    if (!cells[GetIndex(actualTableHeaders, header)].Equals(expectedRow[header])) findRowWithUniqueValue = false;
                }
            }

            if (!findRowWithUniqueValue || !allValuesAreValid) return false;
        }

        return true;
    }

    private int GetIndex(IReadOnlyList<string> list, string value)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(value)) return i;
        }
        return -1;
    }
}