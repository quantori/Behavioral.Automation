using System;
using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class PageScopeContext : IScopeContext
    {
        private readonly IMarkupStorage _globalMarkupStorage;

        [CanBeNull]
        private readonly IMarkupStorage _markupStorage;

        public PageScopeContext(PageScopeId scopeId,
            IMarkupStorage globalMarkupStorage,
            [CanBeNull] IMarkupStorage markupStorage = null)
        {
            _markupStorage = markupStorage;
            _globalMarkupStorage = globalMarkupStorage;
            ScopeId = scopeId;
        }

        public ControlDescription FindControlDescription(string type,
            string name)
        {
            var controlDescription = _markupStorage?.TryFind(type,
                                         name) ??
                                     _globalMarkupStorage.TryFind(type,
                                         name)
                                     ?? _markupStorage?.TryFindInNestedScopes(type, name)
                                     ?? _globalMarkupStorage.TryFindInNestedScopes(type, name);

            if (controlDescription == null)
            {
                throw new ArgumentException(
                    $"Control with alias=\"{type}\" and caption=\"{name}\" not found in PageContext with urlWildcard=\"{ScopeId.UrlWildCard}\"");
            }

            return controlDescription;
        }

        public ControlScopeContext GetNestedControlScopeContext(ControlScopeId controlScopeId)
        {
            if (_markupStorage == null)
            {
                throw new InvalidOperationException(
                    $"MarkupStorage for PageContext with name=\"{ScopeId.Name}\" is not defined, so nested control scope with name = \"{controlScopeId.Name} can't be found");
            }

            var controlScopeMarkupStorage = (_markupStorage.TryGetControlScopeMarkupStorage(controlScopeId) ??
                                             _globalMarkupStorage.TryGetControlScopeMarkupStorage(controlScopeId)) ??
                                            _markupStorage.CreateControlScopeMarkupStorage(controlScopeId);

            return new ControlScopeContext(controlScopeId, controlScopeMarkupStorage);
        }
        
        public PageScopeId ScopeId { get; }
    }
}