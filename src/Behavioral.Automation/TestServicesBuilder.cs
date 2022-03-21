using BoDi;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;

namespace Behavioral.Automation
{
    /// <summary>
    /// Initialise all necessary objects before test execution
    /// </summary>
    public sealed class TestServicesBuilder
    {
        private readonly IObjectContainer _objectContainer;

        public TestServicesBuilder(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }
        
        public void Build()
        {
            _objectContainer.RegisterTypeAs<AutomationIdProvider, IAutomationIdProvider>();
            _objectContainer.RegisterTypeAs<ScopeMarkupStorageContainer, IScopeMarkupStorageContainer>();
            _objectContainer.RegisterTypeAs<ScopeMarkupMapper, IScopeMarkupMapper>();
            _objectContainer.RegisterTypeAs<UriToPageScopeMapper, IUriToPageScopeMapper>();
            _objectContainer.RegisterTypeAs<ScopeContextManager, IScopeContextManager>();
            _objectContainer.RegisterTypeAs<ScopeContextRuntime, IScopeContextRuntime>();
            _objectContainer.RegisterTypeAs<BasicAuthConfig, IBasicAuthConfig>();
            _objectContainer.RegisterTypeAs<ComplexBindingBuilder, IComplexBindingBuilder>();
            _objectContainer.RegisterTypeAs<StepParametersProcessor, IStepParametersProcessor>();

            var builder = _objectContainer.Resolve<IUserInterfaceBuilder>(); 
           builder.Build();
        }
    }
}