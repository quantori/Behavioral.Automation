using Behavioral.Automation.Abstractions;
using TechTalk.SpecFlow;

namespace Behavioral.Automation
{
    internal sealed class TestRunnerWrapper : ITestRunnerWrapper
    {
        private readonly ITestRunner _runner;

        public string StepInfoText => _runner.ScenarioContext.StepContext.StepInfo.Text;

        public TestRunnerWrapper(ITestRunner runner)
        {
            _runner = runner;
        }
    }
}
