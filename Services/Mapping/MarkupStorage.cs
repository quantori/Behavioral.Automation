using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    [UsedImplicitly]
    public class MarkupStorage : IMarkupStorage
    {
        private readonly Dictionary<string, ControlComposition> _mapping;

        private readonly Dictionary<ControlScopeId, IMarkupStorage> _nestedScopeToMarkupMap =
            new Dictionary<ControlScopeId, IMarkupStorage>();

        public MarkupStorage()
        {
            _mapping = new Dictionary<string, ControlComposition>();
        }

        public void AddAlias(string htmlTag, params string[] aliases)
        {
            if (!_mapping.ContainsKey(htmlTag))
            {
                _mapping[htmlTag] = new ControlComposition();
            }
            _mapping[htmlTag].Aliases.AddRange(aliases);
        }

        public void AddComposition(
            string htmlTag, 
            string id, 
            string caption, 
            string subpath = null)
        {
            if (!_mapping.ContainsKey(htmlTag))
            {
                _mapping[htmlTag] = new ControlComposition();
            }
            _mapping[htmlTag].Descriptions.Add(new ControlDescription(id, caption, subpath));
        }

        public ControlDescription TryFind(string alias, string caption)
        {
            try
            {
                var controlDescription = _mapping.Values.Where(composition => composition.Aliases.Contains(alias))
                    .SelectMany(composition => composition.Descriptions)
                    .SingleOrDefault(desciption =>
                        string.Equals(desciption.Caption, caption, StringComparison.OrdinalIgnoreCase));

                return controlDescription;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(
                    $"More than one mapping is found for alias {alias} and caption {caption}", ex);
            }
        }

        public IMarkupStorageInitializer GetOrCreateControlScopeMarkupStorage(ControlScopeId controlScopeId)
        {
            IMarkupStorageInitializer controlMarkupStorage = TryGetControlScopeMarkupStorage(controlScopeId);
            if (controlMarkupStorage == null)
            {
                controlMarkupStorage = CreateControlScopeMarkupStorage(controlScopeId);
            }

            return controlMarkupStorage;
        }

        public IMarkupStorage TryGetControlScopeMarkupStorage(ControlScopeId controlScopeId)
        {
            _nestedScopeToMarkupMap.TryGetValue(controlScopeId, out var controlMarkupStorage);
            return controlMarkupStorage;
        }

        public IMarkupStorage CreateControlScopeMarkupStorage(ControlScopeId controlScopeId)
        {
            IMarkupStorage controlMarkupStorage = new MarkupStorage();
            _nestedScopeToMarkupMap.Add(controlScopeId, controlMarkupStorage);
            return controlMarkupStorage;
        }

        public ControlDescription TryFindInNestedScopes(string type, string name)
        {
            foreach (var nestedScope in _nestedScopeToMarkupMap.Values)
            {
                var controlDescription = nestedScope.TryFind(type, name)
                                         ?? nestedScope.TryFindInNestedScopes(type, name);

                if (controlDescription != null)
                {
                    return controlDescription;
                }

            }
            return null;
        }

        private class ControlComposition
        {
            public ControlComposition()
            {
                Aliases = new List<string>();
                Descriptions = new List<ControlDescription>();
            }

            public List<string> Aliases { get; }

            public List<ControlDescription> Descriptions { get; }
        }
    }
}