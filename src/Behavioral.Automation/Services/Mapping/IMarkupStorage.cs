using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IMarkupStorage: IMarkupStorageInitializer
    {
        [CanBeNull]
        ControlDescription TryFind([NotNull] string alias, [NotNull] string caption);

        [CanBeNull]
        IMarkupStorage TryGetControlScopeMarkupStorage(ControlScopeId controlScopeId);

        IMarkupStorage CreateControlScopeMarkupStorage(ControlScopeId controlScopeId);
    }
}