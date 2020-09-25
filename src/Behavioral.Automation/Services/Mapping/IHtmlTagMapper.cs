using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IHtmlTagMapper
    {
        [NotNull]
        IHtmlTagMapper Alias([NotNull] string alias);

        [NotNull]
        IControlMap With([NotNull] string id, [CanBeNull] string subpath = null);

        IControlMap WithPath([NotNull] string path);
    }
}