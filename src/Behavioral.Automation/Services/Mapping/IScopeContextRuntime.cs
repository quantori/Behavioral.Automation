using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IScopeContextRuntime : IDisposable
    {
        void SwitchToPageScope(PageScopeContext pageScopeContext);
        void EnterControlScope(ControlScopeId controlScopeId);
        ControlDescription FindControlDescription(string type, string name);
        void SwitchToGlobalScope();
        void RunAction(string action, StepDefinitionType stepDefinitionType);
        void RunAction(string action, StepDefinitionType stepDefinitionType, Table table);
    }
}