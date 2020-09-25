using Behavioral.Automation.Services.Mapping;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    public interface IAutomationIdProvider
    {
        [NotNull]
        ControlDescription Get([NotNull] string caption);

        void ParseCaption(string caption, out string name,out string type);
    }
}
