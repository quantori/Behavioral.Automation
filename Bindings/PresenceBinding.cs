using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// This class stores bindings which are used to check elements' visibility
    /// </summary>
    [Binding]
    public sealed class PresenceBinding
    {
        private readonly ITestRunner _runner;

        public PresenceBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        /// <summary>
        /// Check that multiple controls are displayed (for then steps)
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table with tested elements' names</param>
        /// <example>
        /// Then the following controls should become visible:
        /// | controlName    |
        /// | "Test1" input  |
        /// | "Test2" button | 
        /// </example>
        [Then("the following controls should (be|be not|become|become not) visible:")]
        public void CheckThenControlTypeCollectionShown([NotNull] string behavior, [NotNull] Table table)
        {
            behavior = $"should {behavior}";
            CheckControlTypeCollectionShown(behavior, table, _runner.Then);
        }

        /// <summary>
        /// Check that multiple controls are displayed (for given steps)
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table with tested elements' names</param>
        /// <example>
        /// Given the following controls are visible:
        /// | controlName    |
        /// | "Test1" input  |
        /// | "Test2" button | 
        /// </example>
        [Given("the following controls (are|are not|become|become not) visible:")]
        public void CheckGivenControlTypeCollectionShown([NotNull] string behavior, [NotNull] Table table)
        {
            if (behavior.Contains("are"))
            {
                behavior = behavior.Replace("are", "is");
            }
            CheckControlTypeCollectionShown(behavior, table, _runner.Given);
        }

        /// <summary>
        /// Displayed check for each element in the accepted table
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table with tested element's names</param>
        /// <param name="runnerAction">Assertion behavior (instant or continuous)</param>
        private void CheckControlTypeCollectionShown(
            [NotNull] string behavior, 
            [NotNull] Table table, 
            [NotNull] Action<string> runnerAction)
        {
            foreach (var row in table.Rows)
            {
                var control = "\"" + row.Values.First() + "\" " + row.Values.Last();
                runnerAction($"{control} {behavior} visible");
            }
        }


        /// <summary>
        /// Check that single web element is displayed
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <example>Then "Test" input should become visible</example>
        [Given("(.*?) (is|is not|become|become not) visible")]
        [When("(.*?) (is|is not|become|become not) visible")]
        [Then("(.*?) should (be|be not|become|become not) visible")]
        public void CheckElementShown([NotNull] IWebElementWrapper element, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => element.Displayed, true, behavior, $"{element.Caption} is{behavior.BehaviorAppendix()} visible");
        }
    }
}
