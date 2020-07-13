using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public interface IControlMap
    {
        [NotNull]
        IHtmlTagMapper As([NotNull] string caption);
    }
}