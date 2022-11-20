using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.API.Configs;

public class Config
{
    [ConfigurationKeyName("API_HOST")] 
    public string ApiHost { get; set; }
}