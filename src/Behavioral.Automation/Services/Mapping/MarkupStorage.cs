using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    [UsedImplicitly]
    public class MarkupStorage : IMarkupStorage
    {
        [CanBeNull]
        private readonly ControlLocation _controlLocation;
        private readonly Dictionary<string, List<ControlComposition>> _mapping;

        private readonly Dictionary<ControlScopeId, IMarkupStorage> _nestedScopeToMarkupMap =
            new Dictionary<ControlScopeId, IMarkupStorage>();

        private bool _newCompositionShouldBeCreated = true;
        public MarkupStorage([CanBeNull] ControlScopeOptions controlScopeOptions = null,
            [CanBeNull] ControlLocation controlLocation = null)
        {
            _controlLocation = controlLocation;
            _mapping = new Dictionary<string, List<ControlComposition>>();
            ScopeOptions = controlScopeOptions ?? ControlScopeOptions.Default();
        }

        public ControlScopeOptions ScopeOptions { get; }

        public void AddAlias(string htmlTag, params string[] aliases)
        {
            var composition = GetOrCreateCompositionFor(htmlTag);
            composition.Aliases.AddRange(aliases);
        }

        public void AddComposition(
            string htmlTag, 
            string id, 
            string caption, 
            string subpath = null)
        {
            var composition = GetOrCreateCompositionFor(htmlTag);
            composition.Descriptions.Add(new ControlDescription(id, caption, subpath));
        }

        public void StartCreationOfNewComposition()
        {
            _newCompositionShouldBeCreated = true;
        }
        public ControlReference TryFind(string alias, string caption)
        {
            try
            {
                IEnumerable<ControlComposition> controlCompositions;
                if (alias == string.Empty)
                {
                    controlCompositions = _mapping.Values.SelectMany(compositions => compositions)
                        .Where(composition =>
                        !composition.Aliases.Any() || composition.Aliases.Contains(alias));
                }
                else
                {
                    controlCompositions = _mapping.Values.SelectMany(compositions => compositions)
                    .Where(composition => composition.Aliases.Contains(alias));
                }

                var controlDescription = controlCompositions
                    .SelectMany(composition => composition.Descriptions)
                    .SingleOrDefault(description =>
                        string.Equals(description.Caption, caption, StringComparison.OrdinalIgnoreCase));

                return controlDescription != null ? new ControlReference(_controlLocation, controlDescription) : null;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(
                    $"More than one mapping is found for alias {alias} and caption {caption}", ex);
            }
        }

        public IMarkupStorageInitializer GetOrCreateControlScopeMarkupStorage(ControlScopeId controlScopeId,
            ControlScopeOptions controlScopeOptions = null)
        {
            IMarkupStorageInitializer controlMarkupStorage = TryGetControlScopeMarkupStorage(controlScopeId);
            if (controlMarkupStorage == null)
            {
                controlMarkupStorage = CreateControlScopeMarkupStorage(controlScopeId, controlScopeOptions);
            }

            return controlMarkupStorage;
        }

        public IMarkupStorage TryGetControlScopeMarkupStorage(ControlScopeId controlScopeId)
        {
            _nestedScopeToMarkupMap.TryGetValue(controlScopeId, out var controlMarkupStorage);
            return controlMarkupStorage;
        }

        public IMarkupStorage CreateControlScopeMarkupStorage(ControlScopeId controlScopeId, ControlScopeOptions controlScopeOptions = null)
        {
            IMarkupStorage controlMarkupStorage = new MarkupStorage(controlScopeOptions,
                new ControlLocation(controlScopeId, controlScopeOptions ?? new ControlScopeOptions(), _controlLocation));
            _nestedScopeToMarkupMap.Add(controlScopeId, controlMarkupStorage);
            return controlMarkupStorage;
        }

        public ControlReference TryFindInNestedScopes(string type, string name)
        {
            foreach (var nestedScope in _nestedScopeToMarkupMap.Values)
            {
                var controlReference = nestedScope.TryFind(type, name)
                                       ?? nestedScope.TryFindInNestedScopes(type, name);

                if (controlReference != null)
                {
                    return controlReference;
                }
            }
            return null;
        }
        private ControlComposition GetOrCreateCompositionFor(string htmlTag)
        {
            if (!_mapping.ContainsKey(htmlTag))
            {
                _mapping[htmlTag] = new List<ControlComposition>();
            }

            List<ControlComposition> compositions = _mapping[htmlTag];

            if (_newCompositionShouldBeCreated)
            {
                compositions.Add(new ControlComposition());
                _newCompositionShouldBeCreated = false;
            }

            return compositions.Last();
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