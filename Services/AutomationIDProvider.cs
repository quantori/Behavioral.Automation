using Behavioral.Automation.Services.Mapping;
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

        public AutomationIdProvider([NotNull] IScopeContextRuntime scopeContextRuntime)
        {
            _scopeContextRuntime = scopeContextRuntime;
        }


        /// <summary>
        /// Get control description by element caption
        /// </summary>
        /// <param name="caption">Caption of the tested element</param>
        /// <returns>Element control description containing its name, type, id and subpath</returns>
        public ControlDescription Get(string caption)
        {
            var result = caption.ParseExact("\"{0}\" {1}");
            var name = result[0];
            var type = result[1];
            return _scopeContextRuntime.FindControlDescription(type, name);
        }
    }
}
