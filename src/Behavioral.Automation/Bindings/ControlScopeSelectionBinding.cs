using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings to execute actions inside control scopes
    /// </summary>
    [Binding]
    public sealed class ControlScopeSelectionBinding
    {
        private readonly IScopeContextManager _contextManager;
        private readonly ScenarioContext _scenarioContext;

        public ControlScopeSelectionBinding(IScopeContextManager contextManager, ScenarioContext scenarioContext)
        {
            _contextManager = contextManager;
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Execute multiple steps inside scope
        /// </summary>
        /// <param name="controlScopeId">Name of the scope</param>
        /// <param name="actionsTable">Specflow table which contains actions to be executed</param>
        /// <example>
        /// Then inside "Profile" panel the following steps should be executed:
        /// | "Name" input should become visible |
        /// | "Data" table should be empty       |
        /// </example>
        [Given("inside (.+?) the following steps were executed:")]
        [When("inside (.+?) the following steps are executed:")]
        [Then("inside (.+?) the following conditions should be true:")]
        public void ExecuteMultipleActionsInsideControlScope(ControlScopeSelector controlScopeSelector,
            [NotNull] Table actionsTable)
        {
            var stepDefinitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                foreach (var action in actionsTable.Rows)
                {
                    controlScopeRuntime.RunAction(action.Values.First(), stepDefinitionType);
                }
            }
        }

        /// <summary>
        /// Execute action inside scope which contains table
        /// </summary>
        /// <param name="controlScopeId">Name of the scope</param>
        /// <param name="action">Action definition</param>
        /// <param name="table">Specflow table with test data</param>
        /// <example>
        /// Then inside "Calculations" panel: "Data" table should contain the following rows:
        /// | rowName |
        /// | row 1   |
        /// | row 2   |
        /// </example>
        [Given("inside (.+?): (.+?)")]
        [When("inside (.+?): (.+?)")]
        [Then("inside (.+?): (.+?)")]
        public void ExecuteActionWithTableArgsInsideControlScope(ControlScopeSelector controlScopeSelector,
            string action,
            Table table)
        {
            var stepDefinitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType, table);
            }
        }

        /// <summary>
        /// Execute action inside scope
        /// </summary>
        /// <param name="controlScopeId">Name of the scope</param>
        /// <param name="action">Action definition</param>
        /// <example>When inside "Login" page: "Email" input should become visible</example>
        [Given("inside (.+?): (.+?)")]
        [When("inside (.+?): (.+?)")]
        [Then("inside (.+?): (.+?)")]
        public void ExecuteActionInsideControlScope(ControlScopeSelector controlScopeSelector, string action)
        {
            var stepDefinitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;

            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType);
            }
        }
    }
}