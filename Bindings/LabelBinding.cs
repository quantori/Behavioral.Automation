using System;
using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class LabelBinding
    {
        private readonly ITestRunner _runner;

        public LabelBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        [Given("the (.*?) text (is|is not|become|become not) \"(.*)\"")]
        [When("the (.*?) text (is|is not|become|become not) \"(.*)\"")]
        [Then("the (.*?) text should (be|be not|become|become not) \"(.*)\"")]
        public void CheckSelectedText(
            [NotNull] IWebElementWrapper element, 
            [NotNull] AssertionBehavior behavior, 
            [NotNull] string value)
        {
            Assert.ShouldBecome(() => StringExtensions.GetElementTextOrValue(element), value, behavior,
                $"{element.Caption} text is \"{StringExtensions.GetElementTextOrValue(element)}\"");
        }

        [Given("(.*?) (contain|not contain) \"(.*)\" text")]
        [Then("(.*?) should (contain|not contain) \"(.*)\" text")]
        public void CheckSelectedTextContain(
            [NotNull] IWebElementWrapper element,
            [NotNull] string behavior,
            [NotNull] string value)
        {
            Assert.ShouldBecome(() => element.Text.Contains(value), !behavior.Contains("not"),
                $"{element.Caption} text is \"{element.Text}\"");
        }

        [Then("the (.*?) should (have|not have) tooltip with text \"(.*)\"")]
        public void CheckElementTooltip(
            [NotNull] IWebElementWrapper element,
            [NotNull] AssertionBehavior behavior,
            [NotNull] string value)
        {
            Assert.ShouldBecome(() => element.Tooltip, value, behavior,
                $"{element.Caption} tooltip is \"{element.Tooltip}\"");
        }

        [Given("(.*?) text (is|is not|become|become not) empty")]
        [Then("(.*?) text should (be|be not|become|become not) empty")]
        public void CheckElementIsEmpty([NotNull] IWebElementWrapper element, AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => StringExtensions.GetElementTextOrValue(element), string.Empty, behavior,
                $"{element.Caption} text is \"{StringExtensions.GetElementTextOrValue(element)}\"");
        }

        [Then("the following controls should (be|be not|become|become not) empty:")]
        public void CheckThenControlTypeCollectionEmpty([NotNull] string behavior, [NotNull] Table table)
        {
            behavior = $"should {behavior}";
            CheckControlTypeCollectionEmpty(behavior, table, _runner.Then);
        }

        [Given("the following controls (are|are not|become|become not) empty:")]
        public void CheckGivenControlTypeCollectionEmpty([NotNull] string behavior, [NotNull] Table table)
        {
            if (behavior.Contains("are"))
            {
                behavior = behavior.Replace("are", "is");
            }
            CheckControlTypeCollectionEmpty(behavior, table, _runner.Given);
        }

        private void CheckControlTypeCollectionEmpty(
            [NotNull] string behavior,
            [NotNull] Table table,
            [NotNull] Action<string> runnerAction)
        {
            foreach (var row in table.Rows)
            {
                var control = "\"" + row.Values.First() + "\" " + row.Values.Last();
                runnerAction($"{control} text {behavior} empty");
            }
        }
    }
}
