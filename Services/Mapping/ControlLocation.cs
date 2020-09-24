
namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlLocation
    {
        public ControlLocation(ControlScopeId controlScopeId, ControlScopeOptions scopeOptions, ControlLocation parent)
        {
            ControlScopeId = controlScopeId;
            ScopeOptions = scopeOptions;
            Parent = parent;
        }

        public ControlScopeId ControlScopeId { get; }
        public ControlScopeOptions ScopeOptions { get; }
        public ControlLocation Parent { get; }
    }
}
