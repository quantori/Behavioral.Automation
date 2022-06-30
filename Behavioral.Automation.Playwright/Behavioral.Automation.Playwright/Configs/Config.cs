using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.Playwright.Configs;

public class Config
{
    [ConfigurationKeyName("BASE_URL")] 
    public string BaseUrl { get; set; }
    
    [ConfigurationKeyName("SEARCH_ATTRIBUTE")] 
    public string SearchAttribute { get; set; }
    
    [ConfigurationKeyName("ASSERT_TIMEOUT_MILLISECONDS")] 
    public float? AssertTimeoutMilliseconds { get; set; }

    [ConfigurationKeyName("SLOW_MO_MILLISECONDS")]
    public float? SlowMoMilliseconds { get; set; }

    [ConfigurationKeyName("HEADLESS")] public bool? Headless { get; set; } = true;
}