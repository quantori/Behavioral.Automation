using System.Threading;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class DebugBinding
    {
        [Given("wait")]
        [When("wait")]
        [Then("wait")]
        public void Wait()
        {
            Thread.Sleep(5000);
        }
    }
}
