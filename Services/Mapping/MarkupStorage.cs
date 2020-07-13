using System;
using System.Collections.Generic;
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
            foreach (var controlComposition in _mapping.Values)
            {
                if (controlComposition.Aliases.Contains(alias))
                {
                    var result = controlComposition.Descriptions.Find(x =>
                        string.Equals(x.Caption, caption, StringComparison.OrdinalIgnoreCase));
                    return result;
                }
            }
            return null;
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