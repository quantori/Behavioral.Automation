using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;

namespace Behavioral.Automation.Services.Mapping
{
    public class ScopeMarkupStorageContainer : IScopeMarkupStorageContainer
    {
        private readonly Dictionary<PageScopeId, IMarkupStorage> _map =
            new Dictionary<PageScopeId, IMarkupStorage>();

        private readonly MarkupStorage _globalScopeMarkupStorage = new MarkupStorage();

        public IMarkupStorage GetOrCreateFor(PageScopeId scopeId)
        {
            if (!_map.TryGetValue(scopeId, out var markupStorage))
            {
                markupStorage = new MarkupStorage();
                _map.Add(scopeId, markupStorage);
            }
            return markupStorage;
        }

        public MultiMarkupStorageProxy GetOrCreateFor(PageScopeId[] scopeId)
        {
            return new MultiMarkupStorageProxy(scopeId
                .Select(contextId => GetOrCreateFor(contextId))
                .ToArray());
        }

        public IMarkupStorage GetGlobal()
        {
            return _globalScopeMarkupStorage;
        }

        public PageScopeContext GetOrCreatePageScopeContext(Uri uri)
        {
            var markupStorageSearchResult = _map.FirstOrDefault(k =>
                Regex.IsMatch(uri.ToString(), k.Key.UrlWildCard));

            return new PageScopeContext(markupStorageSearchResult.Key ?? new PageScopeId(string.Empty, uri.ToString()),
                _globalScopeMarkupStorage,
                markupStorageSearchResult.Value);
        }

        public PageScopeContext GetOrCreatePageScopeContextByName(string pageName)
        {
            var markupStorageSearchResult =
                _map.FirstOrDefault(k => k.Key.Name.Equals(pageName));

            return new PageScopeContext(markupStorageSearchResult.Key ?? new PageScopeId(pageName, string.Empty),
                _globalScopeMarkupStorage,
                markupStorageSearchResult.Value);
        }
    }
}