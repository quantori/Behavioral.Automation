using System.Collections.Generic;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services
{
    /// <summary>
    /// Method for caption to element conversion
    /// </summary>
    [UsedImplicitly]
    public sealed class ElementSelectionService : IElementSelectionService
    {
        private readonly IDriverService _driverService;
        private readonly IAutomationIdProvider _provider;
        private readonly IScopeContextRuntime _contextRuntime;
        public ElementSelectionService(
            [NotNull] IDriverService driverService,
            [NotNull] IAutomationIdProvider provider,
            [NotNull] IScopeContextRuntime contextRuntime)
        {
            _driverService = driverService;
            _provider = provider;
            _contextRuntime = contextRuntime;
        }

        /// <summary>
        /// Find element by caption from the step
        /// </summary>
        /// <param name="caption">Element caption</param>
        /// <returns>IElementHandleObject object</returns>
        public IElementHandle Find(string caption)
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

        /// <summary>
        /// Find multiple elements by caption from the step
        /// </summary>
        /// <param name="caption">Element caption</param>
        /// <returns>IElementHandle object collection</returns>
        public IEnumerable<IElementHandle> FindMultipleElements(string caption)
        {
            var description = _provider.Get(caption);
            if (description.Id == null)
            {
                return _driverService.FindElementsByXpath(description.Subpath);
            }
            return _driverService.FindElements(description.Id);
        }
    }
}
