using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings to check elements' visibility
    /// </summary>
    [Binding]
    public sealed class PresenceBinding
    {
        private readonly RunnerService _runnerService;
        private readonly ScenarioContext _scenarioContext;

        public PresenceBinding([NotNull] RunnerService runnerService, [NotNull] ScenarioContext scenarioContext)
        {
            _runnerService = runnerService;
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Check that multiple controls are displayed
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table with tested elements' names</param>
        /// <example>
        /// Then the following controls should become visible:
        /// | controlName    | controlType |
        /// | "Test1"        | input  |
        /// | "Test2"        | button | 
        /// </example>
        [Given("the following controls (are|are not|become|become not) visible:")]
        [Then("the following controls should (be|be not|become|become not) visible:")]
        public void CheckControlTypeCollectionShown([NotNull] string behavior, [NotNull] Table table)
        {
            behavior = _runnerService.ConvertBehaviorForGroupRun(_scenarioContext, behavior);
            CheckControlTypeCollectionVisibility(behavior, table);
        }

        /// <summary>
        /// Displayed check for each element in the accepted table
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="table">Specflow table with tested element's names</param>
        /// <param name="runnerAction">Assertion behavior (instant or continuous)</param>
        private void CheckControlTypeCollectionVisibility(
            [NotNull] string behavior, 
            [NotNull] Table table)
        {
            foreach (var row in table.Rows)
            {
                var runnerAction = _runnerService.GetRunner(_scenarioContext);
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
