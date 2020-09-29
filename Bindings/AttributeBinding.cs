using System;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{   /// <summary>
    /// Bindings for element's attributes testing
    /// </summary>
    [Binding]
    public sealed class AttributeBinding
    {
        private readonly ITestRunner _runner;

        public AttributeBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        /// <summary>
        /// Check if element is disabled or enabled
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="enabled">Element expected status (enabled or disabled)</param>
        /// <example>Then "Test" input should become enabled</example>
        [Given("(.*?) (is|is not|become| become not) (enabled|disabled)")]
        [When("(.*?) (is|is not|become| become not) (enabled|disabled)")]
        [Then("the (.*?) should (be|be not|become|become not) (enabled|disabled)")]
        public void CheckElementIsDisabled(
            [NotNull] IWebElementWrapper element, 
            [NotNull] AssertionBehavior behavior, 
            bool enabled)
        {
            Assert.ShouldBecome(() => element.Enabled, enabled, behavior,
                $"{element.Caption} is{behavior.BehaviorAppendix()} enabled");
        }

        /// <summary>
        /// Transform "enabled" or "disabled" string into bool value
        /// </summary>
        /// <param name="enabled">"enabled" or "disabled" string</param>
        /// <returns></returns>
        [StepArgumentTransformation("(enabled|disabled)")]
        public bool ConvertEnabledState([NotNull] string enabled)
        {
            return enabled == "enabled";
        }

        /// <summary>
        /// Check that multiple elements are disabled or enabled in "Then" steps
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
        [Then("the following controls should (be|be not|become|become not) (enabled|disabled):")]
        public void CheckThenControlTypeCollectionShown([NotNull] string behavior, string enabled, [NotNull] Table table)
        {
            behavior = $"should {behavior}";
            CheckControlTypeCollectionShown(behavior, enabled, table, _runner.Then);
        }

        /// <summary>
        /// Check that multiple elements are disabled or enabled in "Given" or "When" steps
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="enabled">>Elements expected status (enabled or disabled)</param>
        /// <param name="table">Specflow table with element names to be tested</param>
        [Given("the following controls (are|are not|become| become not) (enabled|disabled):")]
        [When("the following controls (are|are not|become| become not) (enabled|disabled):")]
        public void CheckGivenControlTypeCollectionShown([NotNull] string behavior, string enabled, [NotNull] Table table)
        {
            if (behavior.Contains("are"))
            {
                behavior = behavior.Replace("are", "is");
            }
            CheckControlTypeCollectionShown(behavior, enabled, table, _runner.Given);
        }

        /// <summary>
        /// Executes enabled or disabled check for each row in the accepted table
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="enabled">Elements expected status (enabled or disabled)</param>
        /// <param name="table">Specflow table with tested element's names</param>
        /// <param name="runnerAction">Action object</param>
        private void CheckControlTypeCollectionShown(
            [NotNull] string behavior, 
            string enabled,
            [NotNull] Table table,
            [NotNull] Action<string> runnerAction)
        {
            foreach (var row in table.Rows)
            {
                var control = "\"" + row.Values.First() + "\" " + row.Values.Last();
                runnerAction($"{control} {behavior} {enabled}");
            }
        }
    }
}
