using System;
using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeMarkupStorageContainer
    {
        IMarkupStorage GetOrCreateFor(PageScopeId scopeId);
        MultiMarkupStorageProxy GetOrCreateFor(PageScopeId[] scopeId);
        IMarkupStorage GetGlobal();
        PageScopeContext GetOrCreatePageScopeContext(Uri uri);
        PageScopeContext GetOrCreatePageScopeContextByName(string pageName);
    }
}