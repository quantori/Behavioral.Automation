using System;
using Behavioral.Automation.Services.Mapping.Contract;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ScopeContextManager : IScopeContextManager
    {
        private readonly IScopeContextRuntime _scopeContextRuntime;
        private readonly IUriToPageScopeMapper _uriToPageScopeMapper;
        private Uri LastVisitedUrl = default;
        private readonly Uri EmptyPageUrl = new Uri("data:,");

        public ScopeContextManager(IScopeContextRuntime scopeContextRuntime, IUriToPageScopeMapper uriToPageScopeMapper)
        {
            _scopeContextRuntime = scopeContextRuntime;
            _uriToPageScopeMapper = uriToPageScopeMapper;
        }

        public void SwitchPage(Uri uri)
        {
            if (_uriToPageScopeMapper.IsGlobalScope(uri))
            {
                _scopeContextRuntime.SwitchToGlobalScope();
            }
            else
            {
                var pageScopeContext = _uriToPageScopeMapper.GetPageScopeContext(uri);
                _scopeContextRuntime.SwitchToPageScope(pageScopeContext);
            }
        }

        public IScopeContextRuntime UseControlScopeContextRuntime(ControlScopeSelector controlScopeSelector)
        {
            foreach (var step in controlScopeSelector.SelectionSteps)
            {
                _scopeContextRuntime.EnterControlScope(step);
            }
            return _scopeContextRuntime;
        }

        public void SwitchPage(string pageName)
        {
            var pageScopeContext = _uriToPageScopeMapper.GetPageScopeContext(pageName);
            _scopeContextRuntime.SwitchToPageScope(pageScopeContext);
        }

        public void SwitchToCurrentUrl(Uri currentUrl)
        {
            if (currentUrl == EmptyPageUrl || currentUrl == LastVisitedUrl)
                return;

            SwitchPage(currentUrl);
            LastVisitedUrl = currentUrl;
        }
    }
}