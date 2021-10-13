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

        [Given("(.*?) selected value (is|become) \"(.*)\"")]
        [Then("the (.*?) selected value should (be|become) \"(.*)\"")]
        public void CheckSelectedValue([NotNull] IDropdownWrapper wrapper, AssertionBehavior behavior, [NotNull] string value)
        {
            Assert.ShouldBecome(() => wrapper.SelectedValue, value, behavior,
                $"{wrapper.Caption} selected value is{behavior.BehaviorAppendix()} {value}");
        }

        [Then("the (.*?) should have the following values:")]
        public void CheckAllItems([NotNull] IDropdownWrapper wrapper, [NotNull] Table items)
        {
            CheckDropdownElements(wrapper.Items, items, $"{wrapper.Caption} values");
        }

        [Then("(.*?) should have the following groups:")]
        public void CheckDropdownHeaders([NotNull] IGroupedDropdownWrapper wrapper, [NotNull] Table items)
        {
            CheckDropdownElements(wrapper.GroupTexts, items, $"{wrapper.Caption} groups");
        }

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

        [Given("user selected \"(.*?)\" in (.*?)")]
        [When("user selects \"(.*?)\" in (.*?)")]
        public void ClickOnEntry([NotNull] string entry, [NotNull] IDropdownWrapper wrapper)
        {
            wrapper.Select(entry);
        }

        [Given("user selected multiple entries in (.*?):")]
        [When("user selects multiple entries in (.*?):")]
        public void ClickOnMultipleEntries([NotNull] IMultiSelectDropdownWrapper wrapper, [NotNull] Table entries)
        {
            wrapper.Select(entries.Rows.Select(x => x.Values.First()).ToArray());
        }

        [Given("the following values are selected in (.*?):")]
        [Then("the following values should be selected in (.*?):")]
        public void CheckMultipleSelectedValues([NotNull] IMultiSelectDropdownWrapper wrapper, [NotNull] Table values)
        {
            CheckDropdownElements(wrapper.SelectedValuesTexts, values, $"{wrapper.Caption} selected values");
        }

        [Given("the \"(.+?)\" value (is|become) (enabled|disabled) in (.+?)")]
        [Then("the \"(.+?)\" value should (be|become) (enabled|disabled) in (.+?)")]
        public void CheckValueInDropdownIsEnabled([NotNull] string value, [NotNull] AssertionBehavior behavior,
            bool enabled, IDropdownWrapper wrapper)
        {
            _attributeBinding.CheckElementIsDisabled(wrapper.Elements.Single(x => x.Text == value), behavior, enabled);
        }

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

        [Given("no values are selected in (.*?)")]
        [Then("no values should be selected in (.*?):")]
        public void CheckMultiSelectDropdownHasNoValuesSelected([NotNull] IMultiSelectDropdownWrapper wrapper)
        {
            Assert.ShouldBecome(() => !wrapper.SelectedValuesTexts.Any(), true, $"{wrapper.Caption} has the following values : {wrapper.SelectedValuesTexts.Aggregate((x, y) => $"{x}, {y}")}");
        }

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
                var actualValue = actualValues.Any()? actualValues.ElementAt(i) : null;
                Assert.ShouldBecome(() => expectedValue, actualValue,
                    $"Expected one of the {valueType} to be {expectedValue} but was {actualValue}");
            }
        }
    }
}

