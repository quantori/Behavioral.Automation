using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public sealed class ElementSelectionService : IElementSelectionService
    {
        private readonly IDriverService _driverService;
        private readonly IAutomationIdProvider _provider;

        public ElementSelectionService(
            [NotNull] IDriverService driverService,
            [NotNull] IAutomationIdProvider provider)
        {
            _driverService = driverService;
            _provider = provider;
        }

        public IWebElement Find(string caption)
        {
            var description = _provider.Get(caption);
            if (description.Id == null)
            {
                return _driverService.FindElementByXpath(description.Subpath);
            }
            if (description.Subpath != null)
            {
                return _driverService.FindElement(description.Id, description.Subpath);
            }
            return _driverService.FindElement(description.Id);
        }
    }
}
