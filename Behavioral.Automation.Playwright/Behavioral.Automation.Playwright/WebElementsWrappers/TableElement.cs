using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class TableElement : WebElement, ITableWrapper
{
    public string RowsSelector { get; set; }

    public ILocator Rows
    {
        get => Locator.Locator(RowsSelector);
        set => throw new NotImplementedException();
    }

    public ElementSelector CellsSelector { get; set; }
    
    public string HeaderCellsSelector { get; set; }

    public ILocator? HeaderCells {
        get => Locator.Locator(HeaderCellsSelector);
        set => throw new NotImplementedException();
    }
    
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
        Console.WriteLine($"{DateTime.Now}: Staring checking presence of specified row");
        var comparingResult = await DoesTableWithHeadersContainTablesRows(expectedTable, false);
        for (var c = 0; c < 4; c++)
        {
            if (comparingResult) break;
            Console.WriteLine($"{DateTime.Now}: No row present, starting again...");
            Thread.Sleep(1000);
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
            if (throwError) Assert.Fail("Expected table doesn't have rows");
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
            if (actualTableSize == 0)
            {
                if (throwError) Assert.Fail("Actual table doesn't have rows");
                return false;
            }
            Console.WriteLine($"{DateTime.Now}: Actual table has {actualTableSize} rows");
            var actualTableHeaders = await HeaderCells.AllTextContentsAsync();
            var findRowWithUniqueValue = false;
            var allValuesAreValid = true;
            for (var a = 0; a < actualTableSize; a++)
            {
                Console.WriteLine($"{DateTime.Now}: Starting checking row with index {a}");
                var rowLocator = Rows.Nth(a);
                Console.WriteLine(rowLocator);
                Console.WriteLine($"Content: {rowLocator.TextContentAsync().Result}");
                var cellsLocator = rowLocator.Locator("//*");
                Console.WriteLine($"Cells locator: {cellsLocator}");
                var cells = cellsLocator.AllTextContentsAsync().Result;
                Console.WriteLine($"{DateTime.Now}: Row has the following cells [{string.Join(", ", cells)}]");
                var headerIndex = GetIndex(actualTableHeaders, uniqueTitle);
                if (cells.Count < headerIndex) continue;     //row is too little
                if (!cells[headerIndex].Equals(uniqueValue))
                {
                    Console.WriteLine($"{DateTime.Now}: Actual table row {a} doesn't have unique value");
                    continue;
                }
                Console.WriteLine($"{DateTime.Now}: Actual table row with index {a} has unique value. Continue checking...");
                findRowWithUniqueValue = true;

                foreach (var header in expectedRow.Keys)
                {
                    if (!cells[GetIndex(actualTableHeaders, header)].Equals(expectedRow[header])) findRowWithUniqueValue = false;
                }

                if (findRowWithUniqueValue && allValuesAreValid) break;
            }

            if (!findRowWithUniqueValue || !allValuesAreValid)
            {
                if (throwError) Assert.Fail("Can't find expected rows");
                return false;
            }
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