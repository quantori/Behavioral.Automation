using System;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class TableBinding
{
    private static readonly float? Timeout = ConfigManager.GetConfig<Config>().AssertTimeoutMilliseconds;

    [Given(@"user clicked clicked on ""(.+?)"" header in ""(.+?)""")]
    [When(@"user clicks clicked on ""(.+?)"" header in ""(.+?)""")]
    public async Task ClickOnHeaderCell(string headerTitle, TableWrapper element)
    {
        var headerToClick = element.HeaderCells.Filter(new LocatorFilterOptions {HasTextString = headerTitle});
        await headerToClick.ClickAsync();
    }

    [Given(@"the ""(.+?)"" has the following rows (with|without) headers:")]
    [Then(@"the ""(.+?)"" should have the following rows (with|without) headers:")]
    public async Task CheckTableRows(TableWrapper element,string countingHeaders, Table expectedTable)
    {
        var checkHeadersNeeded = countingHeaders != "without";
        if (checkHeadersNeeded)
        {
            var expectedHeader = expectedTable.Header;
            var actualHeaderText = await element.HeaderCells.AllTextContentsAsync();
            CollectionAssert.AreEqual(expectedHeader, actualHeaderText,
                $"Actual:   {string.Join(" | ", actualHeaderText)}{Environment.NewLine}" +
                $"Expected: {string.Join(" | ", expectedHeader)}{Environment.NewLine}");
        }

        await element.WebContext.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var actualRows = element.Rows;
        var rowCount = await actualRows.CountAsync();

        Assert.That(rowCount, Is.EqualTo(expectedTable.Rows.Count), "Rows count mismatch.");

        for (var i = 0; i < expectedTable.Rows.Count; i++)
        {
            var rowToCheck = element.Rows.Nth(i);
            var actualRowValues = await element.GetCellsForRow(rowToCheck).AllTextContentsAsync();
            CollectionAssert.AreEqual(expectedTable.Rows[i].Values, actualRowValues,
                "Row #{i} mismatch. {Environment.NewLine} " +
                $"Actual:   {string.Join(" | ", actualRowValues)}{Environment.NewLine}" +
                $"Expected: {string.Join(" | ", expectedTable.Rows[i].Values)}{Environment.NewLine}");
        }
    }

    [Given(@"the ""(.+?)"" contains the following rows (with|without) headers:")]
    [Then(@"the ""(.+?)"" should contain the following rows (with|without) headers:")]
    public async Task CheckTableContainRows(TableWrapper element, string countingHeaders, Table expectedTable)
    {
        var checkHeadersNeeded = countingHeaders != "without";
        if (checkHeadersNeeded)
        {
            var actualHeaderText = await element.HeaderCells.AllTextContentsAsync();
            var actualHeaderTextTrimmed = actualHeaderText.Select(s => s.Trim());

            CollectionAssert.IsSubsetOf(expectedTable.Header, actualHeaderTextTrimmed,
                $"Element: Header of {element.Caption}{Environment.NewLine}");
        }

        for (var i = 0; i < expectedTable.Rows.Count; i++)
        {
            var rowToCheck = element.Rows.Nth(i);
            var actualRowValues = await element.GetCellsForRow(rowToCheck).AllTextContentsAsync();
            var actualRowValuesTrimmed = actualRowValues.Select(s => s.Trim());
            CollectionAssert.IsSubsetOf(expectedTable.Rows[i].Values, actualRowValuesTrimmed,
                $"Row #{i}{Environment.NewLine}Row selector:{ await rowToCheck.TextContentAsync()}{Environment.NewLine}Cell selector:{element.CellsSelector}{Environment.NewLine}");
        }
    }

    /// <summary>
    /// Check number of table rows
    /// </summary>
    /// <param name="element">Tested web element wrapper</param>
    /// <param name="expectedRowsCount">Expected number of rows</param>
    /// <example>Then "Test" table should have 5 items</example>
    [Given(@"the ""(.+?)"" has ""(.+?)"" items")]
    [Then(@"the ""(.+?)"" should have ""(.+?)"" items")]
    public async Task CheckTableItemsCount(TableWrapper element, int expectedRowsCount)
    {
        await Assertions.Expect(element.Rows).ToHaveCountAsync(expectedRowsCount, new LocatorAssertionsToHaveCountOptions { Timeout = Timeout });
    }
}