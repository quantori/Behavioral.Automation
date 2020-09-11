using System;
using System.Threading;
using TechTalk.SpecFlow;
using JetBrains.Annotations;

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

        [Given("wait (.*) sec")]
        [When("wait (.*) sec")]
        [Then("wait (.*) sec")]
        public void Wait([NotNull] string sec)
        {
            int intSec = Convert.ToInt32(sec);
            Thread.Sleep(intSec * 1000);
        }
    }
}
