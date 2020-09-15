using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping
{
    public class ScopeMarkupMapper : IScopeMarkupMapper
    {
        private readonly IScopeMarkupStorageContainer _container;

        public ScopeMarkupMapper(IScopeMarkupStorageContainer container)
        {
            _container = container;
        }

        public IScopeMappingPipe GetOrCreateMappingPipe(PageScopeId scopeId)
        {
            var markupStorage = _container.GetOrCreateFor(scopeId);
            return new ScopeMappingPipe(markupStorage);
        }

        public IScopeMappingPipe GetGlobalMappingPipe()
        {
            var globalStorage = _container.GetGlobal();
            return new ScopeMappingPipe(globalStorage);
        }

        public IScopeMappingPipe GetOrCreateMappingPipe(params PageScopeId[] scopeIds)
        {
            var multiMarkupStorageProxy = _container.GetOrCreateFor(scopeIds);
            return new ScopeMappingPipe(multiMarkupStorageProxy);
        }
    }
}
