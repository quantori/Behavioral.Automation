using System;
using System.Collections.Generic;
using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;
using JetBrains.Annotations;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Behavioral.Automation.Services.Mapping
{
    public class ScopeContextRuntime : IScopeContextRuntime
    {
        private readonly Stack<IScopeContext> _contextStack = new Stack<IScopeContext>();
        private readonly PageScopeContext _globalContext;
        private readonly ITestRunner _runner;
        private int _levelOfNesting;

        public ScopeContextRuntime(IScopeMarkupStorageContainer container, ITestRunner runner)
        {
            _runner = runner;
            _globalContext =
                new PageScopeContext(
                    new PageScopeId(string.Empty, string.Empty), 
                    container.GetGlobal(), 
                    new MarkupStorage());
            _contextStack.Push(_globalContext);
        }

        internal IScopeContext CurrentContext => _contextStack.Peek();

        public void SwitchToPageScope(PageScopeContext pageScopeContext)
        {
            _contextStack.Pop();
            _contextStack.Push(pageScopeContext);
        }

        public void EnterControlScope(ControlScopeId controlScopeId)
        {
            var controlScopeBehavior = CurrentContext.GetNestedControlScopeContext(controlScopeId);
            _contextStack.Push(controlScopeBehavior);
            _levelOfNesting++;
        }

        public ControlDescription FindControlDescription(string type, string name)
        {
            return CurrentContext.FindControlReference(type, name)?.ControlDescription;
        }

        public ControlReference FindControlReference(string type, string name)
        {
            return CurrentContext.FindControlReference(type, name);
        }

        public void SwitchToGlobalScope()
        {
            _contextStack.Clear();
            _contextStack.Push(_globalContext);
        }

        public void RunAction(string action, StepDefinitionType stepDefinitionType, Table table)
        {
            switch (stepDefinitionType)
            {
                case StepDefinitionType.Given:
                {
                    _runner.Given(action, null, table);
                }
                    break;
                case StepDefinitionType.When:
                {
                    _runner.When(action, null, table);
                }
                    break;
                case StepDefinitionType.Then:
                {
                    _runner.Then(action, null, table);
                }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(stepDefinitionType), stepDefinitionType, null);
            }
        }

        public bool HasVirtualizedScopeContext(ControlScopeId controlScopeId, ControlScopeId parentControlScopeId = null)
        {
            var parentControlScope = parentControlScopeId != null ? CurrentContext.GetNestedControlScopeContext(parentControlScopeId): CurrentContext;
            var nestedScope = parentControlScope.GetNestedControlScopeContext(controlScopeId);
            return nestedScope is IVirtualizedScopeContext;
        }


        public void RunAction(string action, StepDefinitionType stepDefinitionType)
        {
            switch (stepDefinitionType)
            {
                case StepDefinitionType.Given:
                {
                    _runner.Given(action);
                }
                    break;
                case StepDefinitionType.When:
                {
                    _runner.When(action);
                }
                    break;
                case StepDefinitionType.Then:
                {
                    _runner.Then(action);
                }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(stepDefinitionType), stepDefinitionType, null);
            }
        }

        public void Dispose()
        {
            while (_levelOfNesting > 0)
            {
                _contextStack.Pop();
                _levelOfNesting--;
            }
        }
    }
}
