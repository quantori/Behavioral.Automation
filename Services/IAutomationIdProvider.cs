using Behavioral.Automation.Services.Mapping;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Interface for AutomationIdProvider class
    /// </summary>
    public interface IAutomationIdProvider
    {
        /// <summary>
        /// Get control description by element caption
        /// </summary>
        /// <param name="caption">Element caption</param>
        /// <returns></returns>
        [NotNull]
        ControlDescription Get([NotNull] string caption);
    }
}