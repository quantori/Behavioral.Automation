using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// This class stores binding which are used to execute actions inside control scope
    /// </summary>
    [Binding]
    public sealed class ControlScopeSelectionBinding
    {
        private readonly IScopeContextManager _contextManager;

        public ControlScopeSelectionBinding(IScopeContextManager contextManager)
        {
            _contextManager = contextManager;
        }

        /// <summary>
        /// Execute action inside scope
        /// </summary>
        /// <param name="controlScopeId">Name of the scope</param>
        /// <param name="action">Action definition</param>
        /// <example>When inside "Test" scope: "Test" element should become visible</example>
        [Given("inside (.*?): (.*)")]
        [When("inside (.*?): (.*)")]
        [Then("inside (.*?): (.*)")]
        public void ExecuteActionInsideControlScope(string controlScopeId, string action)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            var scopeId = new ControlScopeId(controlScopeId);
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(scopeId))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType);
            }
        }

        /// <summary>
        /// Execute action inside scope which contains table
        /// </summary>
        /// <param name="controlScopeId">Name of the scope</param>
        /// <param name="action">Action definition</param>
        /// <param name="table">Specflow table with test data</param>
        /// <example>
        /// Then inside "Test" scope: "Data" table should contain the following rows:
        /// | rowName |
        /// | row 1   |
        /// | row 2   |
        /// </example>
        [Given("inside (.*?): (.*)")]
        [When("inside (.*?): (.*)")]
        [Then("inside (.*?): (.*)")]
        public void ExecuteActionInsideControlScope(string controlScopeId, string action, Table table)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            var scopeId = new ControlScopeId(controlScopeId);
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(scopeId))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType, table);
            }
        }

        /// <summary>
        /// Execute multiple steps inside scope
        /// </summary>
        /// <param name="controlScopeId">Name of the scope</param>
        /// <param name="actionsTable">Specflow table which contains actions to be executed</param>
        /// <example>
        /// Then inside "Test" scope the following steps should be executed:
        /// | "Name" input should become visible |
        /// | "Data" table should be empty       |
        /// </example>
        [Given("inside (.*?) the following steps were executed:")]
        [When("inside (.*?) the following steps are executed:")]
        [Then("inside (.*?) the following steps should be executed:")]
        public void ExecuteMultipleActionsInsideControlScope(string controlScopeId, [NotNull] Table actionsTable)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            var scopeId = new ControlScopeId(controlScopeId);
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(scopeId))
            {
                foreach (var action in actionsTable.Rows)
                {
                    controlScopeRuntime.RunAction(action.Values.First(), stepDefinitionType);
                }
                
            }
        }

        /// <summary>
        /// Execute action in the nested scope
        /// </summary>
        /// <param name="controlScopeId">Nested scope name</param>
        /// <param name="parentControlSelectionSteps">Parent scope name</param>
        /// <param name="action">Action to be executed</param>
        /// <example>When inside "Filter" panel of "Test" menu: "Data" table become visible</example>
        [Given("inside (.*?) of (.*?): (.*)")]
        [When("inside (.*?) of (.*?): (.*)")]
        [Then("inside (.*?) of (.*?): (.*)")]
        public void ExecuteActionInsideControlScope(string controlScopeId,
            string parentControlSelectionSteps,
            string action)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            var controlScopeSelector = new ControlScopeSelector(controlScopeId, parentControlSelectionSteps);
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType);
            }
        }

        /// <summary>
        /// Execute action with a Specflow table in the nested scope
        /// </summary>
        /// <param name="controlScopeId">Nested scope name</param>
        /// <param name="parentControlSelectionSteps">Parent scope name</param>
        /// <param name="action">Action to be executed</param>
        /// <param name="table">Specflow table with additional step data</param>
        /// <example>
        /// Then inside "Results" panel of "Search" menu: "Data" table should contain the following rows:
        /// | rowName |
        /// | row 1   |
        /// | row 2   |
        /// </example>
        [Given("inside (.*?) of (.*?): (.*)")]
        [When("inside (.*?) of (.*?): (.*)")]
        [Then("inside (.*?) of (.*?): (.*)")]
        public void ExecuteActionInsideControlScope(string controlScopeId,
            string parentControlSelectionSteps,
            string action,
            Table table)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            var controlScopeSelector = new ControlScopeSelector(controlScopeId, parentControlSelectionSteps);
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType, table);
            }
        }
    }
}