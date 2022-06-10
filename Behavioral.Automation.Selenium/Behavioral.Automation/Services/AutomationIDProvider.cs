using System;
using System.Threading;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Contains method used to get control description
    /// </summary>
    [UsedImplicitly]
    public sealed class AutomationIdProvider : IAutomationIdProvider
    {
        private readonly IScopeContextRuntime _scopeContextRuntime;
        private readonly IDriverService _driverService;
        private readonly IScopeContextManager _scopeContextManager;

        public AutomationIdProvider(
            [NotNull] IScopeContextRuntime scopeContextRuntime,
            [NotNull] IDriverService driverService,
            [NotNull] IScopeContextManager scopeContextManager)
        {
            _scopeContextManager = scopeContextManager;
            _driverService = driverService;
            _scopeContextRuntime = scopeContextRuntime;
        }


        /// <summary>
        /// Get control description by element caption
        /// </summary>
        /// <param name="caption">Caption of the tested element</param>
        /// <returns>Element control description containing its name, type, id and subpath</returns>
        public ControlDescription Get(string caption)
        {
            ParseCaption(caption, out var name, out var type);

            var controlDescription = _scopeContextRuntime.FindControlDescription(type, name);

            if (controlDescription != null) 
                return controlDescription;

            Thread.Sleep(500);
            _scopeContextManager.SwitchToCurrentUrl(new Uri(_driverService.CurrentUrl));

            var scopeId = ((PageScopeContext)((ScopeContextRuntime)_scopeContextRuntime).CurrentContext).ScopeId;
            return _scopeContextRuntime.FindControlDescription(type, name) ??
                   throw new ArgumentException($"Control with alias=\"{type}\" and caption=\"{name}\" not found in PageContext with urlWildcard=\"{scopeId.UrlWildCard}\"");
        }

        public void ParseCaption(string caption, out string name, out string type)
        {
            string[] parsedValues;
            if (caption.TryParseExact("\"{0}\" {1}", out parsedValues, true))
            {
                name = parsedValues[0];
                type = parsedValues[1];
                return;
            }
            
            parsedValues = caption.ParseExact("\"{0}\"",  true);
            name = parsedValues[0];
            type = string.Empty;
        }
    }
}
