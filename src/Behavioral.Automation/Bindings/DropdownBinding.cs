using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly AttributeBinding _attributeBinding;
        private readonly ITestRunner _runner;

        public DropdownBinding(AttributeBinding attributeBinding, [NotNull] ITestRunner runner)
        {
            _attributeBinding = attributeBinding;
            _runner = runner;
        }

        /// <summary>
        /// Check dropdown selected value
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected selected value</param>
        /// <example>Given the "Test" dropdown selected value is "Test value"</example>
        [Given("the (.*?) selected value (is|become) \"(.*)\"")]
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
        [Given("the (.+?) has the following values:")]
        [Then("the (.*?) should have the following values:")]
        public void CheckAllItems([NotNull] IDropdownWrapper wrapper, [NotNull] Table items)
        {
            CheckDropdownElements(wrapper.Items, items, $"{wrapper.Caption} values");
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
        [Given("the (.+?) has the following groups:")]
        [Then("(.*?) should have the following groups:")]
        public void CheckDropdownHeaders([NotNull] IGroupedDropdownWrapper wrapper, [NotNull] Table items)
        {
            CheckDropdownElements(wrapper.GroupTexts, items, $"{wrapper.Caption} groups");
        }

        /// <summary>
        /// Check that dropdown contains specific value among its other values
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then "Test" dropdown should contain "Test value"</example>
        [Given("(.*?) (contain|not contain) \"(.*)\"")]
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
        /// Check values stored inside dropdown by partial match
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="items">Specflow table with expected dropdown items</param>
        /// <example>
        /// Then the "Test" dropdown should contain the following values:
        /// | value        |
        /// | Test value 1 |
        /// | Test value 2 |
        /// </example>
        [Given("the (.*?) (contains|not contains) the following values:")]
        [Then("the (.*?) should (contain|not contain) the following values:")]
        [Then("the \"(.*?)\" menu should (contain|not contain) the following values:")]
        public void CheckDropdownContainsMultipleItems([NotNull] IDropdownWrapper wrapper, [NotNull] string behavior, [NotNull] Table table)
        {
            Assert.ShouldBecome(() => table.Rows.Any(), true,
                new AssertionBehavior(AssertionType.Immediate, false), "Please provide data in the table");

            var dropdownItems = wrapper.Items;
            foreach (var row in table.Rows)
            {
                var value = row.Values.FirstOrDefault();
                Assert.ShouldBecome(() => dropdownItems.Contains(value), !behavior.Contains("not"),
                $"{wrapper.Caption} items are {dropdownItems.Aggregate((x, y) => $"{x}, {y}")}");
            }
        }

        /// <summary>
        /// Check that all elements inside dropdown contain specific string
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then "Test" dropdown should have "Test value" in all elements</example>
        [Given("all items in the (.+?) (have|does not have) \"(.+?)\"")]
        [Then("all items in the (.+?) should (have|not have) \"(.+?)\"")]
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
        [Given("user selected \"(.*?)\" in (.*?)")]
        [When("user selects \"(.*?)\" in (.*?)")]
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
        [Given("user selected multiple entries in (.*?):")]
        [When("user selects multiple entries in (.*?):")]
        public void ClickOnMultipleEntries([NotNull] IMultiSelectDropdownWrapper wrapper, [NotNull] Table entries)
        {
            wrapper.Select(entries.Rows.Select(x => x.Values.First()).ToArray());
        }

        /// <summary>
        /// Check that multiple values are selected inside dropdown
        /// </summary>
        /// <param name="wrapper">IMultiselectDropdownWrapper object</param>
        /// <param name="values">Table with values</param>
        /// <example>
        /// Then "the following values should be selected in "Test" dropdown:
        /// | entry  |
        /// | Entry1 |
        /// | Entry2 |
        /// </example>
        [Given("the following values are selected in (.*?):")]
        [Then("the following values should be selected in (.*?):")]
        public void CheckMultipleSelectedValues([NotNull] IMultiSelectDropdownWrapper wrapper, [NotNull] Table values)
        {
            CheckDropdownElements(wrapper.SelectedValuesTexts, values, $"{wrapper.Caption} selected values");
        }

        /// <summary>
        /// Check that value is enabled or disabled inside dropdown
        /// </summary>
        /// <param name="value">Value text</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="enabled">Check type</param>
        /// <param name="wrapper">Dropdown element object</param>
        /// <example>
        /// Then the "Entry 1" value should become disabled in "Test" dropdown
        /// </example>
        [Given("the \"(.+?)\" value (is|become) (enabled|disabled) in (.+?)")]
        [Then("the \"(.+?)\" value should (be|become) (enabled|disabled) in (.+?)")]
        public void CheckValueInDropdownIsEnabled([NotNull] string value, [NotNull] AssertionBehavior behavior,
            bool enabled, IDropdownWrapper wrapper)
        {
            _attributeBinding.CheckElementIsDisabled(wrapper.Elements.Single(x => x.Text == value), behavior, enabled);
        }

        /// <summary>
        /// Check that multiple values are enabled or disabled inside dropdown
        /// </summary>
        /// <param name="value">Value text</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="enabled">Check type</param>
        /// <param name="wrapper">Dropdown element object</param>
        /// <example>
        /// Then the following values should become disabled in "Test" dropdown:
        /// | entry  |
        /// | Entry1 |
        /// | Entry2 |
        /// </example>
        [Given("the following values (are|become) (enabled|disabled) in (.+?):")]
        [Then("the following values should (be|become) (enabled|disabled) in (.+?):")]
        public void CheckMultipleValuesInDropdownAreEnabled([NotNull] string behavior, string enabled,
            IDropdownWrapper wrapper, Table table)
        {
            if (behavior.Contains("are"))
            {
                behavior = behavior.Replace("are", "is");
            }

            CheckDropdownValueCollectionEnabled(behavior, enabled, table, wrapper, _runner.Given);
        }

        private void CheckDropdownValueCollectionEnabled([NotNull] string behavior,
            string enabled,
            [NotNull] Table table,
            [NotNull] IDropdownWrapper wrapper,
            [NotNull] Action<string> runnerAction)
        {
            foreach (var row in table.Rows)
            {
                Assert.ShouldBecome(() => row.Values.Any(), true, new AssertionBehavior(AssertionType.Immediate, false),
                    "One of the rows in the provided table doesn't have values");
                runnerAction($"the \"{row.Values.First()}\" value {behavior} {enabled} in {wrapper.Caption}");
            }
        }

        /// <summary>
        /// Check that no values are ticked inside dropdown
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <example>Then no values are selected in "Test" dropdown</example>
        [Given("no values are selected in (.*?)")]
        [Then("no values should be selected in (.*?):")]
        public void CheckMultiSelectDropdownHasNoValuesSelected([NotNull] IMultiSelectDropdownWrapper wrapper)
        {
            Assert.ShouldBecome(() => !wrapper.SelectedValuesTexts.Any(), true, $"{wrapper.Caption} has the following values : {wrapper.SelectedValuesTexts.Aggregate((x, y) => $"{x}, {y}")}");
        }

        /// <summary>
        /// Check that dropdown has no selected values
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <example>Then "Test" dropdown selected value should be empty</example>
        [Given("(.*?) selected value (is|is not|become|become not) empty")]
        [Then("(.*?) selected value should (be|be not|become|become not) empty")]
        public void CheckDropdownIsEmpty([NotNull] IDropdownWrapper wrapper, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => wrapper.Empty, true, behavior,
                $"{wrapper.Caption} selected value is{behavior.BehaviorAppendix()} empty");
        }

        private void CheckDropdownElements([NotNull] IEnumerable<string> actualValues, [NotNull] Table expectedValues, [NotNull] string valueType)
        {
            for (var i = 0; i < expectedValues.Rows.Count; i++)
            {
                var expectedValue = expectedValues.Rows.ElementAt(i).Values.FirstOrDefault();
                var actualValue = actualValues.Any() ? actualValues.ElementAt(i) : null;
                Assert.ShouldBecome(() => expectedValue, actualValue,
                    $"Expected one of the {valueType} to be {expectedValue} but was {actualValue}");
            }
        }

        /// <summary>
        /// Clears the selection of multidropdown
        /// </summary>
        /// <param name="wrapper">Tested web element wrapper</param>
        [Given("user deselected all values in (.*)")]
        [When("user deselects all values in (.*)")]
        public void ClearSelectedValues([NotNull] IMultiSelectDropdownWrapper wrapper)
        {
            wrapper.ClearSelectedValues();
        }
    }
}

