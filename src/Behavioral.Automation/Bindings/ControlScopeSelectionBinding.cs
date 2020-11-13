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


        [Given("inside (.+?) the following steps were executed:")]
        [When("inside (.+?) the following steps are executed:")]
        [Then("inside (.+?) the following conditions should be true:")]
        public void ExecuteMultipleActionsInsideControlScope(ControlScopeSelector controlScopeSelector,
            [NotNull] Table actionsTable)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                foreach (var action in actionsTable.Rows)
                {
                    controlScopeRuntime.RunAction(action.Values.First(), stepDefinitionType);
                }
            }
        }

        [Given("inside (.+?): (.+?)")]
        [When("inside (.+?): (.+?)")]
        [Then("inside (.+?): (.+?)")]
        public void ExecuteActionWithTableArgsInsideControlScope(ControlScopeSelector controlScopeSelector,
            string action,
            Table table)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType, table);
            }
        }

        [Given("inside (.+?): (.+?)")]
        [When("inside (.+?): (.+?)")]
        [Then("inside (.+?): (.+?)")]
        public void ExecuteActionInsideControlScope(ControlScopeSelector controlScopeSelector, string action)
        {
            var stepDefinitionType = ScenarioStepContext.Current.StepInfo.StepDefinitionType;
           
            using (var controlScopeRuntime = _contextManager.UseControlScopeContextRuntime(controlScopeSelector))
            {
                controlScopeRuntime.RunAction(action, stepDefinitionType);
            }
        }

      
    }
}