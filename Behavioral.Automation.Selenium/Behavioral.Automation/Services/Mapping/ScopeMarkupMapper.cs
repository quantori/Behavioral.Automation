using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping
{
    /// <summary>
    /// Creates pipes that are responsible for controls' definitions in specific scopes
    /// </summary>
    public class ScopeMarkupMapper : IScopeMarkupMapper
    {
        private readonly IScopeMarkupStorageContainer _container;

        public ScopeMarkupMapper(IScopeMarkupStorageContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Get existing or create new mapping pipe for page scope
        /// </summary>
        /// <param name="scopeId">Page scope ID</param>
        /// <returns>Page scope mapping pipe</returns>
        public IScopeMappingPipe GetOrCreateMappingPipe(PageScopeId scopeId)
        {
            var markupStorage = _container.GetOrCreateFor(scopeId);
            return new ScopeMappingPipe(markupStorage);
        }

        /// <summary>
        /// Gets global scope mapping pipe (controls, defined in global scope are visible on all pages)
        /// </summary>
        /// <returns>Global scope mapping pipe</returns>
        public IScopeMappingPipe GetGlobalMappingPipe()
        {
            var globalStorage = _container.GetGlobal();
            return new ScopeMappingPipe(globalStorage);
        }

        /// <summary>
        ///  Get existing or create new mapping pipe for multiple pages (controls that are visible on multiple pages, but not on every one)
        /// </summary>
        /// <param name="scopeIds">Page scopes IDs array</param>
        /// <returns>Page scope mapping pipe</returns>
        public IScopeMappingPipe GetOrCreateMappingPipe(params PageScopeId[] scopeIds)
        {
            var multiMarkupStorageProxy = _container.GetOrCreateFor(scopeIds);
            return new ScopeMappingPipe(multiMarkupStorageProxy);
        }
    }
}
