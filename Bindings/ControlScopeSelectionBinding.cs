using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class ControlScopeSelectionBinding
    {
        private readonly IScopeContextManager _contextManager;

        public ControlScopeSelectionBinding(IScopeContextManager contextManager)
        {
            _contextManager = contextManager;
        }

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