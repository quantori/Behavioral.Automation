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
        private readonly IDriverService _driverService;

        public DebugBinding([NotNull] IDriverService driverService)
        {
            _driverService = driverService;
        }

        /// <summary>
        /// Write page content into the console
        /// </summary>
        /// <example>When page dumps content to the output</example>
        [When("page dumps content to the output")]
        [Then("page dumps content to the output")]
        public void DumpPageContent()
        {
            _driverService.DebugDumpPage();
        }

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
