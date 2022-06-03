using System;
using Behavioral.Automation.Services.Mapping.Contract;
using Behavioral.Automation.Services.Mapping.PageMapping;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class PageScopeContext : IScopeContext
    {
        private readonly IMarkupStorage _globalMarkupStorage;

        [CanBeNull]
        private IMarkupStorage _markupStorage;

        public PageScopeContext(PageScopeId scopeId,
            IMarkupStorage globalMarkupStorage,
            [CanBeNull] IMarkupStorage markupStorage = null)
        {
            _markupStorage = markupStorage;
            _globalMarkupStorage = globalMarkupStorage;
            ScopeId = scopeId;
        }

        public ControlReference FindControlReference(string type,
            string name)
        {
            var whereToSearch = new[] { _markupStorage, _globalMarkupStorage };

            foreach (var storage in whereToSearch)
            {
                var controlReference = storage?.TryFind(type, name);
                if (controlReference != null)
                {
                    return controlReference;
                }
            }
            foreach (var storage in whereToSearch)
            {
                var controlReference = storage?.TryFindInNestedScopes(type, name);
                if (controlReference != null)
                {
                    return controlReference;
                }
            }

            throw new ArgumentException(
                    $"Control with alias=\"{type}\" and caption=\"{name}\" not found in PageContext with urlWildcard=\"{ScopeId.UrlWildCard}\"");
        }

        public IControlScopeContext GetNestedControlScopeContext(ControlScopeId controlScopeId)
        {
            if (_markupStorage == null)
            {
                TestContext.WriteLine($"\r\nMarkupStorage for PageContext with name=\"{ScopeId.Name}\" is not defined, switching to Global scope\r\n");
                _markupStorage = _globalMarkupStorage;
            }

            var controlScopeMarkupStorage = (_markupStorage.TryGetControlScopeMarkupStorage(controlScopeId) ??
                                             _globalMarkupStorage.TryGetControlScopeMarkupStorage(controlScopeId)) ??
                                            _markupStorage.CreateControlScopeMarkupStorage(controlScopeId);

            if (controlScopeMarkupStorage.ScopeOptions.IsVirtualized)
            {
                return new VirtualizedControlScopeContext(controlScopeId, controlScopeMarkupStorage);
            }
            else
            {
                return new ControlScopeContext(controlScopeId, controlScopeMarkupStorage);
            }
        }

        public PageScopeId ScopeId { get; }
    }
}
