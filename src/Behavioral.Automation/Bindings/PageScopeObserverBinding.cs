using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Allows to automatically switch PageContext, depending on current Driver url
    /// </summary>
    [Binding]
    public class PageScopeObserverBinding
    {
        private readonly IScopeContextManager _scopeContextManager;
        private readonly IDriverService _driverService;

        public PageScopeObserverBinding([NotNull] IScopeContextManager scopeContextManager, [NotNull] IDriverService driverService)
        {
            _scopeContextManager = scopeContextManager;
            _driverService = driverService;
        }

        [BeforeStep]
        public void CheckScopeSwitch()
        {
            _scopeContextManager.SwitchToCurrentUrl(_driverService.CurrentUrl);
        }
    }
}
