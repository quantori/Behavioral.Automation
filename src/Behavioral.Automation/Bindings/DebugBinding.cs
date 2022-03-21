using System;
using System.Threading;
using TechTalk.SpecFlow;
using JetBrains.Annotations;
using Behavioral.Automation.Services;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for debug
    /// </summary>
    [Binding]
    public sealed class DebugBinding
    {
        /// <summary>
        /// Stop test execution for 5 seconds
        /// </summary>
        ///<example>Then wait</example>
        [Given("wait")]
        [When("wait")]
        [Then("wait")]
        public void Wait()
        {
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Stop test execution for given amount of seconds
        /// </summary>
        /// <param name="sec">Number of seconds</param>
        /// <example>Then wait 5 sec</example>
        [Given("wait (.*) sec")]
        [When("wait (.*) sec")]
        [Then("wait (.*) sec")]
        public void Wait([NotNull] int sec)
        {
            int intSec = Convert.ToInt32(sec);
            Thread.Sleep(intSec * 1000);
        }
    }
}
