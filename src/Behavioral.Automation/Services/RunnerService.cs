using System;
using System.Diagnostics.CodeAnalysis;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Behavioral.Automation.Services
{
    public class RunnerService
    {
        private ITestRunner _runner;

        public RunnerService([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        public Action<string> GetRunner(ScenarioContext scenarioContext)
        {
            Action<string> runner;
            var scenarioAction = scenarioContext.StepContext.StepInfo.StepDefinitionType;
            runner = scenarioAction switch
            {
                StepDefinitionType.Given => _runner.Given,
                StepDefinitionType.When => _runner.When,
                StepDefinitionType.Then => _runner.Then,
                _ => null
            };
            return runner;
        }

        public string ConvertBehaviorForGroupRun(ScenarioContext scenarioContext, string behavior)
            => scenarioContext.StepContext.StepInfo.Text.Contains("should")
            ? $"should {behavior}"
            : behavior.Replace("are", "is");
    }
}