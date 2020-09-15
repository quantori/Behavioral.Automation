using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public class ExecutionObserverBinding
    {
        private readonly ITestRunner _runner;
        private readonly IScenarioExecutionConsumer _consumer;

        public ExecutionObserverBinding(ITestRunner runner, IScenarioExecutionConsumer consumer)
        {
            _runner = runner;
            _consumer = consumer;
        }

        [BeforeStep]
        public void Consume()
        {
            var stageState = _runner.ScenarioContext.CurrentScenarioBlock;
            var stage = "UNKNOWN";
            switch (stageState)
            {
                case ScenarioBlock.Given: stage = "Given";
                    break;
                case ScenarioBlock.When: stage = "When";
                    break;
                case ScenarioBlock.Then: stage = "Then";
                    break;
            }
            var text = _runner.ScenarioContext.StepContext.StepInfo.Text;
            _consumer.Consume($"{stage} {text}");
        }
    }
}
