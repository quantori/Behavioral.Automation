using System.Collections.Generic;
using System.Linq;

namespace Behavioral.Automation.Services
{
    public sealed class ScenarioExecutionConsumer : IScenarioExecutionConsumer
    {
        private readonly List<string> _steps;

        public ScenarioExecutionConsumer()
        {
            _steps = new List<string>();
        }

        public void Consume(string text) => _steps.Add(text);

        public IEnumerable<string> Get() => _steps.Take(_steps.Count - 1);
    }
}
