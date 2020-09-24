namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeContext
    {
        ControlDescription FindControlDescription(string type, string name);
        IControlScopeContext GetNestedControlScopeContext(ControlScopeId controlScopeId);
    }
}
