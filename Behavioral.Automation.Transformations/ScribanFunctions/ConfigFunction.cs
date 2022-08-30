using Behavioral.Automation.Configs;
using Scriban.Runtime;

namespace Behavioral.Automation.Transformations.ScribanFunctions;

public class ConfigFunction : ScriptObject
{
    public static string Config(string configName)
    {
        return ConfigManager.GetConfig(configName);
    }
}