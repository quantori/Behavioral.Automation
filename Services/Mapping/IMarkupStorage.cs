using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IMarkupStorage: IMarkupStorageInitializer
    {
        [CanBeNull]
        ControlReference TryFind([NotNull] string alias, [NotNull] string caption);

        [CanBeNull]
        IMarkupStorage TryGetControlScopeMarkupStorage(ControlScopeId controlScopeId);

        IMarkupStorage CreateControlScopeMarkupStorage(ControlScopeId controlScopeId, ControlScopeOptions controlScopeOptions = null);

        ControlReference TryFindInNestedScopes(string type, string name);

        public ControlScopeOptions ScopeOptions { get; }
    }
}
