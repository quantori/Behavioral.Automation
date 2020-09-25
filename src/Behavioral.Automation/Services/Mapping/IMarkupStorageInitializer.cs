using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IMarkupStorageInitializer
    {
        void AddAlias([NotNull] string htmlTag, [NotNull, ItemNotNull] params string[] aliases);

        void AddComposition(
            [NotNull] string htmlTag,
            [NotNull] string id,
            [NotNull] string caption,
            [CanBeNull] string subpath = null);

        IMarkupStorageInitializer GetOrCreateControlScopeMarkupStorage(ControlScopeId controlScopeId,
            ControlScopeOptions controlScopeOptions = null);
    }
}
