using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class PresenceBinding
    {
        private readonly ITestRunner _runner;

        public PresenceBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        [Then("the following controls should (be|be not|become|become not) visible:")]
        public void CheckThenControlTypeCollectionShown([NotNull] string behavior, [NotNull] Table table)
        {
            behavior = $"should {behavior}";
            CheckControlTypeCollectionShown(behavior, table, _runner.Then);
        }

        [Given("the following controls (are|are not|become|become not) visible:")]
        public void CheckGivenControlTypeCollectionShown([NotNull] string behavior, [NotNull] Table table)
        {
            if (behavior.Contains("are"))
            {
                behavior = behavior.Replace("are", "is");
            }
            CheckControlTypeCollectionShown(behavior, table, _runner.Given);
        }

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

        [Given("(.*?) (is|is not|become|become not) visible")]
        [When("(.*?) (is|is not|become|become not) visible")]
        [Then("(.*?) should (be|be not|become|become not) visible")]
        public void CheckElementShown([NotNull] IWebElementWrapper element, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => element.Displayed, true, behavior, $"{element.Caption} is{behavior.BehaviorAppendix()} visible");
        }
    }
}
