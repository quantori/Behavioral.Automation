using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Context;

public class WebContext
{
    public IBrowserContext Context { get; set; }
    public IPage Page { get; set; }
}