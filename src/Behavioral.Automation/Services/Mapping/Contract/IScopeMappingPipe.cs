using System;

namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeMappingPipe : IDisposable
    {
        IHtmlTagMapper Register(string tag);
        IScopeMappingPipe CreateControlMappingPipe(ControlScopeId controlScopeId, ControlScopeOptions controlScopeOptions = null);

        IScopeMappingPipe CreateControlMappingPipe(string controlScopeId, bool isVirtualized = false);
    }
}
