using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for element's attributes testing
    /// </summary>
    [Binding]
    public sealed class AttributeBinding
    {
        private readonly RunnerService _runnerService;
        private readonly ScenarioContext _scenarioContext;

        public AttributeBinding([NotNull] RunnerService runnerService, [NotNull] ScenarioContext scenarioContext)
        {
            _runnerService = runnerService;
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Check if element is disabled or enabled
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="enabled">Element expected status (enabled or disabled)</param>
        /// <example>Then "Test" input should become enabled</example>
        [Given("the (.*?) (is|is not|become| become not) (enabled|disabled)")]
        [When("the (.*?) (is|is not|become| become not) (enabled|disabled)")]
        [Then("the (.*?) should (be|be not|become|become not) (enabled|disabled)")]
        public void CheckElementIsDisabled(
            [NotNull] IWebElementWrapper element,
            [NotNull] AssertionBehavior behavior,
            bool enabled)
        {
            var status = enabled ? " not" : string.Empty;
            Assert.ShouldBecome(() => element.Enabled, enabled, behavior,
                $"{element.Caption} is{status} enabled");
        }

        /// <summary>
        /// Check that multiple elements are disabled or enabled
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="enabled">Elements expected status (enabled or disabled)</param>
        /// <param name="table">Specflow table with element names to be tested</param>
        /// <example>
        /// Then the following controls should become enabled:
        /// | control       |
        /// | "Test1" input |
        /// | "Test2" input |
        /// </example>
        [Given("the following controls (are|are not|become|become not) (enabled|disabled)")]
        [Then("the following controls should (be|be not|become|become not) (enabled|disabled):")]
        public void CheckControlTypeCollectionShown([NotNull] string behavior, string enabled, [NotNull] Table table)
        {
            behavior = _runnerService.ConvertBehaviorForGroupRun(_scenarioContext, behavior);

            CheckControlTypeCollectionEnabled(behavior, enabled, table);
        }

        /// <summary>
        /// Executes enabled or disabled check for each row in the accepted table
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="enabled">Elements expected status (enabled or disabled)</param>
        /// <param name="table">Specflow table with tested element's names</param>
        /// <param name="runnerAction">Action object</param>
        private void CheckControlTypeCollectionEnabled(
            [NotNull] string behavior,
            string enabled,
            [NotNull] Table table)
        {
            foreach (var row in table.Rows)
            {
                var runnerAction = _runnerService.GetRunner(_scenarioContext);
                var control = "the \"" + row.Values.First() + "\" " + row.Values.Last();
                runnerAction($"{control} {behavior} {enabled}");
            }
        }
    }
}
