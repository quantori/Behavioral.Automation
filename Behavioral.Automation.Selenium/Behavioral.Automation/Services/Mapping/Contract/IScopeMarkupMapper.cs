using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeMarkupMapper
    {
        IScopeMappingPipe GetOrCreateMappingPipe(PageScopeId scopeId);
        IScopeMappingPipe GetOrCreateMappingPipe(params PageScopeId[] scopeIds);
        IScopeMappingPipe GetGlobalMappingPipe();
    }
}