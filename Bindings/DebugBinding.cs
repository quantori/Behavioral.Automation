using System.Threading;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// This class stores debug bindings
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
    }
}
