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
    /// <summary>
    /// Bindings for labels or other text elements testing
    /// </summary>
    [Binding]
    public sealed class LabelBinding
    {
        private readonly ITestRunner _runner;

        public LabelBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        /// <summary>
        /// Check that element's text is equal to the expected one
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then "Test" element text should be "expected text"</example>
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

        /// <summary>
        /// Check that element's text contains expected string
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then "Test" element should contains "expected string" text</example>
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

        /// <summary>
        /// Check element's tooltip text
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="value">Expected value</param>
        /// <example>Then the "Test" element should have tooltip with text "expected string"</example>
        [Then("the (.*?) should (have|not have) tooltip with text \"(.*)\"")]
        public void CheckElementTooltip(
            [NotNull] IWebElementWrapper element,
            [NotNull] AssertionBehavior behavior,
            [NotNull] string value)
        {
            Assert.ShouldBecome(() => element.Tooltip, value, behavior,
                $"{element.Caption} tooltip is \"{element.Tooltip}\"");
        }

        /// <summary>
        /// Check that element text is empty
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <example>Then "Test" element text should be empty</example>
        [Given("(.*?) text (is|is not|become|become not) empty")]
        [Then("(.*?) text should (be|be not|become|become not) empty")]
        public void CheckElementIsEmpty([NotNull] IWebElementWrapper element, AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => StringExtensions.GetElementTextOrValue(element), string.Empty, behavior,
                $"{element.Caption} text is \"{StringExtensions.GetElementTextOrValue(element)}\"");
        }

        /// <summary>
        /// Check that multiple elements' texts are empty (binding for "Then" steps)
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table which contains element names</param>
        /// <example>
        /// Then the following controls should be empty:
        /// | controlName   |
        /// | "Test1" label |
        /// | "Test2" label |
        /// </example>
        [Then("the following controls should (be|be not|become|become not) empty:")]
        public void CheckThenControlTypeCollectionEmpty([NotNull] string behavior, [NotNull] Table table)
        {
            behavior = $"should {behavior}";
            CheckControlTypeCollectionEmpty(behavior, table, _runner.Then);
        }

        /// <summary>
        /// Check that multiple elements' texts are empty (binding for "Given" steps)
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table which contains element names</param>
        /// <example>
        /// Given the following controls are empty:
        /// | controlName   |
        /// | "Test1" label |
        /// | "Test2" label |
        /// </example>
        [Given("the following controls (are|are not|become|become not) empty:")]
        public void CheckGivenControlTypeCollectionEmpty([NotNull] string behavior, [NotNull] Table table)
        {
            if (behavior.Contains("are"))
            {
                behavior = behavior.Replace("are", "is");
            }
            CheckControlTypeCollectionEmpty(behavior, table, _runner.Given);
        }

        /// <summary>
        /// Executes empty check for each element in the table
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table which contains element names</param>
        /// <param name="runnerAction">Action object</param>
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
