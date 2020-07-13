using System.Collections.Generic;

namespace Behavioral.Automation.Services
{
    public interface IScenarioExecutionConsumer
    {
        void Consume(string text);

        IEnumerable<string> Get();
    }
}