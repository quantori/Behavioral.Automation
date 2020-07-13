using Behavioral.Automation.Services.Mapping;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public sealed class AutomationIdProvider : IAutomationIdProvider
    {
        private readonly IScopeContextRuntime _scopeContextRuntime;

        public AutomationIdProvider([NotNull] IScopeContextRuntime scopeContextRuntime)
        {
            _scopeContextRuntime = scopeContextRuntime;
        }

        public ControlDescription Get(string caption)
        {
            var result = caption.ParseExact("\"{0}\" {1}");
            var name = result[0];
            var type = result[1];
            return _scopeContextRuntime.FindControlDescription(type, name);
        }
    }
}
