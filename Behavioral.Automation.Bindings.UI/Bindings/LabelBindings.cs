using Behavioral.Automation.Bindings.UI.Abstractions;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.Bindings;

[Binding]
public class LabelBindings
{
    [Then(@"""(.+?)"" label should have ""(.+?)"" text")]
    public async Task CheckLabelText(ILabelElement label, string text)
    {
        await label.ShouldHaveTextAsync(text);
    }
    
    [Then(@"""(.+?)"" label should become visible")]
    public async Task CheckLabelVisibility(ILabelElement label)
    {
        await label.IsVisibleAsync();
    }
}