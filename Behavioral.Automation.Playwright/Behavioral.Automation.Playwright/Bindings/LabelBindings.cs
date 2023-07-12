using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class LabelBindings
{
    [Then(@"label ""(.+?)"" should have ""(.+?)"" text")]
    public async Task CheckLabelText(ILabelElement element, string text)
    {
        await element.ShouldHaveTextAsync(text);
    }
}