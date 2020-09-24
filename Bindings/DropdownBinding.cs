using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for dropdowns testing
    /// </summary>
    [Binding]
    public sealed class DropdownBinding
    {
        /// <summary>
        /// Check dropdown selected value
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected selected value</param>
        /// <example>Given "Test" dropdown selected value is "Test value"</example>
        [Given("(.*?) selected value (is|become) \"(.*)\"")]
        [Then("the (.*?) selected value should (be|become) \"(.*)\"")]
        public void CheckSelectedValue([NotNull] IDropdownWrapper wrapper, AssertionBehavior behavior, [NotNull] string value)
        {
            Assert.ShouldBecome(() => wrapper.SelectedValue, value, behavior,
                $"{wrapper.Caption} selected value is{behavior.BehaviorAppendix()} {value}");
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
        [Then("the (.*?) should have the following values:")]
        public void CheckAllItems([NotNull] IDropdownWrapper wrapper, [NotNull] Table items)
        {
            if (!wrapper.Autocomplete)
            {
                wrapper.Click();
            }
            wrapper.Items.Should().BeEquivalentTo(items.Rows.Select(x => x.Values.Single()));
        }

        /// <summary>
        /// Check header of groups stored inside dropdown
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="items">Specflow table with expected dropdown items</param>
        /// <example>
        /// Then "Test" dropdown should have the following groups:
        /// | group          |
        /// | Group header 1 |
        /// | Group header 2 |
        /// </example>
        [Then("(.*?) should have the following groups:")]
        public void CheckDropdownHeaders([NotNull] IDropdownWrapper wrapper, [NotNull] Table items)
        {
            if (!wrapper.Autocomplete)
            {
                wrapper.Click();
            }
            wrapper.GroupTexts.Should().BeEquivalentTo(items.Rows.Select(x => x.Values.Single()));
        }

        /// <summary>
        /// Check that dropdown contains specific value among its other values
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then "Test" dropdown should contain "Test value"</example>
        [When("(.*?) (contain|not contain) \"(.*)\"")]
        [Then("(.*?) should (contain|not contain) \"(.*)\"")]
        public void CheckDropdownContainsItems(
            [NotNull] IDropdownWrapper wrapper,
            [NotNull] string behavior,
            [NotNull] string value)
        {
            Assert.ShouldBecome(
                () => wrapper.Items.Contains(value), 
                !behavior.Contains("not"), 
                $"{wrapper.Caption} items are {wrapper.Items.Aggregate((x, y) => $"{x}, {y}")}");
        }

        /// <summary>
        /// Check that all elements inside dropdown contain specific string
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then "Test" dropdown should have "Test value" in all elements</example>
        [When("(.*?) (contains|not contains) \"(.*)\" in all elements")]
        [Then("(.*?) should (have|not have) \"(.*)\" in all elements")]
        public void CheckAllItemsContainString(
            [NotNull] IDropdownWrapper wrapper,
            [NotNull] string behavior,
            [NotNull] string value)
        {
            Assert.ShouldBecome(() => wrapper.Stale, false, $"{wrapper.Caption} is stale");
            var items = wrapper.Items;
            Assert.ShouldBecome(() => wrapper.Items.All(x => x.ToLower().Contains(value.ToLower().Trim())), 
                !behavior.Contains("not"), $"{wrapper.Caption} items are {items.Aggregate((x, y) => $"{x}, {y}")}");
        }

        /// <summary>
        /// Select entry inside dropdown
        /// </summary>
        /// <param name="entry">Entry's value</param>
        /// <param name="wrapper">Tested web element wrapper</param>
        [When("user selects \"(.*)\" in (.*)")]
        public void ClickOnEntry([NotNull] string entry, [NotNull] IDropdownWrapper wrapper)
        {
            wrapper.Select(entry);
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
        [When("user selects multiple entries in (.*):")]
        public void ClickOnMultipleEntries([NotNull] IDropdownWrapper wrapper, [NotNull] Table entries)
        {
            wrapper.Select(entries.Rows.Select(x => x.Values.First()).ToArray());
        }

        /// <summary>
        /// Check that dropdown has no values selected
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <example>Then "Test" dropdown selected should be empty</example>
        [Given("(.*?) selected value (is|is not|become|become not) empty")]
        [Then("(.*?) selected value should (be|be not|become|become not) empty")]
        public void CheckDropdownIsEmpty([NotNull] IDropdownWrapper wrapper, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => wrapper.Empty , true, behavior,
                $"{wrapper.Caption} selected value is{behavior.BehaviorAppendix()} empty");
        }
    }
}

