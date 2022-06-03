using System;
using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IUriToPageScopeMapper
    {
        bool IsGlobalScope(Uri uri);
        PageScopeContext GetPageScopeContext(Uri uri);
        PageScopeContext GetPageScopeContext(string pageName);
        PageScopeId GetPageScopeId(string pageName);
    }
}