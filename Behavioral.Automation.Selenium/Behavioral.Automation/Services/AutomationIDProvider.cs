using Behavioral.Automation.Services.Mapping;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Contains method used to get control description
    /// </summary>
    [UsedImplicitly]
    public sealed class AutomationIdProvider : IAutomationIdProvider
    {
        private readonly IScopeContextRuntime _scopeContextRuntime;

        public AutomationIdProvider([NotNull] IScopeContextRuntime scopeContextRuntime)
        {
            _scopeContextRuntime = scopeContextRuntime;
        }


        /// <summary>
        /// Get control description by element caption
        /// </summary>
        /// <param name="caption">Caption of the tested element</param>
        /// <returns>Element control description containing its name, type, id and subpath</returns>
        public ControlDescription Get(string caption)
        {
            ParseCaption(caption, out var name, out var type);
            return _scopeContextRuntime.FindControlDescription(type, name);
        }

        public void ParseCaption(string caption, out string name, out string type)
        {
            string[] parsedValues;
            if (caption.TryParseExact("\"{0}\" {1}", out parsedValues, true))
            {
                name = parsedValues[0];
                type = parsedValues[1];
                return;
            }
            
            parsedValues = caption.ParseExact("\"{0}\"",  true);
            name = parsedValues[0];
            type = string.Empty;
        }
    }
}
