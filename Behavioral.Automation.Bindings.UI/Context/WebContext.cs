using Behavioral.Automation.Bindings.UI.Abstractions;

namespace Behavioral.Automation.Bindings.UI.Context;

/*
WebContext holds UI-related instances that can be reused across steps:
1. Browser
2. Tab (coming soon)
3. Page
 */
public class WebContext
{
    public IBrowser Browser { get; set; }
    public IPage Page { get; set; }
}