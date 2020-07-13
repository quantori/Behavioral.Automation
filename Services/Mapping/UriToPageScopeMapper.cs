using System;
using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping
{
    public class UriToPageScopeMapper : IUriToPageScopeMapper
    {
        private readonly IScopeMarkupStorageContainer _container;

        public UriToPageScopeMapper(IScopeMarkupStorageContainer container)
        {
            _container = container;
        }

        public bool IsGlobalScope(Uri uri)
        {
            return uri.ToString().Equals(ConfigServiceBase.BaseUrl, StringComparison.InvariantCultureIgnoreCase);
        }

        public PageScopeContext GetPageScopeContext(Uri uri)
        {
            var pageScopeContext = _container.GetOrCreatePageScopeContext(uri);
            return pageScopeContext;
        }
        
        public PageScopeContext GetPageScopeContext(string pageName)
        {
            var pageScopeContext = _container.GetOrCreatePageScopeContextByName(pageName);
            return pageScopeContext;
        }

        public PageScopeId GetPageScopeId(string pageName)
        {
            return GetPageScopeContext(pageName).ScopeId;
        }
    }
}