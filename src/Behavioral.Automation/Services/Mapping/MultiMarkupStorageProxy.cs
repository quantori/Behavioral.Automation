using System.Collections.Generic;
using System.Linq;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class MultiMarkupStorageProxy : IMarkupStorageInitializer
    {
        private readonly IEnumerable<IMarkupStorageInitializer> _storages;

        internal MultiMarkupStorageProxy(IEnumerable<IMarkupStorageInitializer> storages)
        {
            _storages = storages;
        }

        public void StartCreationOfNewComposition()
        {
            foreach (var storage in _storages)
            {
                storage.StartCreationOfNewComposition();
            }
        }

        public void AddAlias(string htmlTag, params string[] aliases)
        {
            foreach (var storage in _storages)
            {
                storage.AddAlias(htmlTag, aliases);
            }
        }

        public void AddComposition(string htmlTag, string id, string caption, string subpath = null)
        {
            foreach (var storage in _storages)
            {
                storage.AddComposition(htmlTag, id, caption, subpath);
            }
        }

        public IMarkupStorageInitializer GetOrCreateControlScopeMarkupStorage(ControlScopeId controlScopeId,
            ControlScopeOptions controlScopeOptions = null)
        {
            return new MultiMarkupStorageProxy(_storages
                .Select(v => v.GetOrCreateControlScopeMarkupStorage(controlScopeId, controlScopeOptions)));
        }
    }
}
