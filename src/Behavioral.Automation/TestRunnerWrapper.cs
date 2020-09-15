using Behavioral.Automation.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Behavioral.Automation
{
    internal class TestRunnerWrapper : ITestRunnerWrapper
    {
        private readonly ITestRunner _runner;

        public string StepInfoText => _runner.ScenarioContext.StepContext.StepInfo.Text;
        public TestRunnerWrapper(ITestRunner runner)
        {
            _runner = runner;
        }
    }
}
