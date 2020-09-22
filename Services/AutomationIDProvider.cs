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
            ParseCaption(caption, out var name, out var type);
            return _scopeContextRuntime.FindControlDescription(type, name);
        }
        public void ParseCaption(string caption, out string name,out string type)
        {
            var result = caption.ParseExact("\"{0}\" {1}");
             name = result[0];
            type = result[1];
        }
    }
}
