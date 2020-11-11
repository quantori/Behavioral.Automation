using System.Linq;
using FluentAssertions;
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
            wrapper.Items.Should().BeEquivalentTo(items.Rows.Select(x => x.Values.Single()));
        }

        [Then("(.*?) should have the following groups:")]
        public void CheckDropdownHeaders([NotNull] IGroupedDropdownWrapper wrapper, [NotNull] Table items)
        {
            wrapper.GroupTexts.Should().BeEquivalentTo(items.Rows.Select(x => x.Values.Single()));
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

        [Given("user selected \"(.*?)\" in (.*?)")]
        [When("user selects \"(.*?)\" in (.*?)")]
        public void ClickOnEntry([NotNull] string entry, [NotNull] IDropdownWrapper wrapper)
        {
            wrapper.Select(entry);
        }

        [Given("user selected multiple entries in (.*?):")]
        [When("user selects multiple entries in (.*?):")]
        public void ClickOnMultipleEntries([NotNull] IDropdownWrapper wrapper, [NotNull] Table entries)
        {
            wrapper.Select(entries.Rows.Select(x => x.Values.First()).ToArray());
        }

        [Given("(.*?) selected value (is|is not|become|become not) empty")]
        [Then("(.*?) selected value should (be|be not|become|become not) empty")]
        public void CheckDropdownIsEmpty([NotNull] IDropdownWrapper wrapper, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => wrapper.Empty , true, behavior,
                $"{wrapper.Caption} selected value is{behavior.BehaviorAppendix()} empty");
        }
    }
}

