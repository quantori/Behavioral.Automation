using System;

namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeMappingPipe : IDisposable
    {
        IHtmlTagMapper Register(string tag);
        IScopeMappingPipe CreateControlMappingPipe(ControlScopeId controlScopeId);
    }
}