using System;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Utils;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class DropdownBinding
{
    /// <summary>
    /// Select entry inside dropdown
    /// </summary>
    /// <param name="entry">Entry's value</param>
    /// <param name="element">Tested web element wrapper</param>
    [Given(@"user selected ""(.+?)"" in ""(.+?)""")]
    [When(@"user selects ""(.+?)"" in ""(.+?)""")]
    public async Task SelectValueInDropdown(string entry, DropdownWrapper element)
    {
        await element.SelectValue(entry);
    }

    /// <summary>
    /// Check dropdown selected value
    /// </summary>
    /// <param name="wrapper">Tested web element wrapper</param>
    /// <param name="behavior">Assertion behavior (instant or continuous)</param>
    /// <param name="value">Expected selected value</param>
    /// <example>Given the "Test" dropdown selected value is "Test value"</example>
    [Given(@"the ""(.+?)"" selected value is ""(.+?)""")]
    [Then(@"the ""(.+?)"" selected value should be ""(.+?)""")]
    public async Task CheckSelectedValue(DropdownWrapper wrapper, string value)
    {
        await Assertions.Expect(wrapper.Locator).ToHaveValueAsync(value);
    }

    /// <summary>
    /// Check values stored inside dropdown
    /// </summary>
    /// <param name="wrapper">Tested web element wrapper</param>
    /// <param name="items">Specflow table with expected dropdown items</param>
    /// <example>
    /// Then the "Test" dropdown should have the following values:
    /// | value        |
    /// | Test value 1 |
    /// | Test value 2 |
    /// </example>
    [Given(@"the ""(.+?)"" has the following values:")]
    [Then(@"the ""(.*?)"" should have the following values:")]
    public async Task CheckAllItems(DropdownWrapper wrapper, Table items)
    {
        await CheckDropdownElements(wrapper, items, $"{wrapper.Caption} values");
    }

    /// <summary>
    /// Check that dropdown contains specific value among its other values
    /// </summary>
    /// <param name="wrapper">Tested web element wrapper</param>
    /// <param name="behavior">Assertion behavior</param>
    /// <param name="value">Expected value</param>
    /// <example>Then "Test" dropdown should contain "Test value"</example>
    [Given(@"the ""(.+?)"" (contains|does not contain) ""(.+?)""")]
    [Then(@"the ""(.+?)"" should (contain|not contain) ""(.+?)""")]
    public async Task CheckDropdownContainsItems(
        DropdownWrapper wrapper,
        string behavior,
        string value)
    {
        var actualValues = await wrapper.ItemsTexts;
        if (behavior.Contains("not"))
        {
            CollectionAssert.DoesNotContain(actualValues, value,
                actualValues.CreateDropdownErrorMessage(wrapper.Caption));
        }
        else
        {
            CollectionAssert.Contains(actualValues, value, actualValues.CreateDropdownErrorMessage(wrapper.Caption));
        }
    }

    /// <summary>
    /// Check values stored inside dropdown by partial match
    /// </summary>
    /// <param name="wrapper">Tested web element wrapper</param>
    /// <param name="behavior">Positive or negative assertion</param>
    /// <param name="table">Specflow table with expected dropdown items</param>
    /// <example>
    /// Then the "Test" dropdown should contain the following values:
    /// | value        |
    /// | Test value 1 |
    /// | Test value 2 |
    /// </example>
    [Given(@"the ""(.+?)"" (contains|does not contain) the following values:")]
    [Then(@"the ""(.+?)"" should (contain|not contain) the following values:")]
    public async Task CheckDropdownContainsMultipleItems(DropdownWrapper wrapper, string behavior, Table table)
    {
        foreach (var row in table.Rows)
        {
            await CheckDropdownContainsItems(wrapper, behavior, row.Values.First());
        }
    }

    /// <summary>
    /// Select multiple value inside dropdown (if dropdown supports multi select)
    /// </summary>
    /// <param name="wrapper">Tested web element wrapper</param>
    /// <param name="entries">Specflow table which contains expected values</param>
    /// <example>
    /// When user selects multiple entries in "Test" dropdown:
    /// | entry  |
    /// | Entry1 |
    /// | Entry2 |
    /// </example>
    [Given(@"user selected multiple entries in ""(.+?)"":")]
    [When(@"user selects multiple entries in ""(.+?)"":")]
    public void ClickOnMultipleEntries(DropdownWrapper wrapper, Table entries)
    {
        wrapper.SelectValue(entries.Rows.Select(x => x.Values.First()).ToArray());
    }

    /// <summary>
    /// Check that value is enabled or disabled inside dropdown
    /// </summary>
    /// <param name="value">Value text</param>
    /// <param name="enabled">Check type</param>
    /// <param name="wrapper">Dropdown element object</param>
    /// <example>
    /// Then the "Entry 1" value should become disabled in "Test" dropdown
    /// </example>
    [Given(@"the ""(.+?)"" value is (enabled|disabled) in ""(.+?)""")]
    [Then(@"the ""(.+?)"" value should be (enabled|disabled) in ""(.+?)""")]
    public async Task CheckValueInDropdownIsEnabled(string value, bool enabled, DropdownWrapper wrapper)
    {
        var optionToCheck = wrapper.GetOption(value);
        if (enabled)
        {
            await Assertions.Expect(optionToCheck).Not.ToBeDisabledAsync();
        }
        else
        {
            await Assertions.Expect(optionToCheck).ToBeDisabledAsync();
        }
    }

    /// <summary>
    /// Check that multiple values are enabled or disabled inside dropdown
    /// </summary>
    /// <param name="enabled">Check type</param>
    /// <param name="wrapper">Dropdown element object</param>
    /// <example>
    /// Then the following values should become disabled in "Test" dropdown:
    /// | entry  |
    /// | Entry1 |
    /// | Entry2 |
    /// </example>
    [Given(@"the following values are (enabled|disabled) in ""(.+?)"":")]
    [Then(@"the following values should be (enabled|disabled) in ""(.+?)"":")]
    public async Task CheckMultipleValuesInDropdownAreEnabled(bool enabled,
        DropdownWrapper wrapper, Table table)
    {
        foreach (var row in table.Rows)
        {
            await CheckValueInDropdownIsEnabled(row.Values.First(), enabled, wrapper);
        }
    }

    private async Task CheckDropdownElements(DropdownWrapper wrapper, Table expectedValues, string valueType)
    {
        for (var i = 0; i < expectedValues.Rows.Count; i++)
        {
            var expectedValue = expectedValues.Rows.ElementAt(i).Values.FirstOrDefault();
            var actualValues = await wrapper.ItemsTexts;
            var elementToCheck = actualValues.ElementAt(i);

            CollectionAssert.AreEqual(expectedValue, elementToCheck,
                $"Actual:   {string.Join(" | ", elementToCheck)}{Environment.NewLine}" +
                $"Expected: {string.Join(" | ", expectedValue)}{Environment.NewLine}");
        }
    }
}