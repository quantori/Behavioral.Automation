namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeContext
    {
        ControlReference FindControlReference(string type, string name);
        IControlScopeContext GetNestedControlScopeContext(ControlScopeId controlScopeId);
    }
}
