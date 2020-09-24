using System;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlScopeContext : IControlScopeContext
    {
        private readonly ControlScopeId _contextId;

        [CanBeNull]
        private readonly IMarkupStorage _markupStorage;

        public ControlScopeContext(ControlScopeId contextId,
            [CanBeNull] IMarkupStorage markupStorage)
        {
            _markupStorage = markupStorage;
            _contextId = contextId;
        }

        public ControlDescription FindControlDescription(string type, string name)
        {
            var controlDescription = _markupStorage?.TryFind(type, name);

            if (controlDescription == null)
            {
                throw new ArgumentException(
                    $"Control with alias=\"{type}\" and caption=\"{name}\" not found in ControlContext with name=\"{_contextId.Name}\"");
            }

            return controlDescription;
        }

        public IControlScopeContext GetNestedControlScopeContext(ControlScopeId controlScopeId)
        {
            if (_markupStorage == null)
            {
                throw new InvalidOperationException(
                    $"MarkupStorage for ControlContext with name=\"{_contextId.Name}\" is not defined, so nested control scope with name = \"{controlScopeId.Name} can't be found");
            }

            var nestedControlMarkupStorage = _markupStorage.TryGetControlScopeMarkupStorage(controlScopeId) ??
                                             _markupStorage.CreateControlScopeMarkupStorage(controlScopeId);

            if (nestedControlMarkupStorage.ScopeOptions.IsVirtualized)
            {
                return new VirtualizedControlScopeContext(controlScopeId, nestedControlMarkupStorage);
            }

            return new ControlScopeContext(controlScopeId, nestedControlMarkupStorage);
        }
    }
}
