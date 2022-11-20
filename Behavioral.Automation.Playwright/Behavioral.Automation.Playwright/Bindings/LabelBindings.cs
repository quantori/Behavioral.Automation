using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class LabelBindings
{
    [Then(@"the ""(.*)"" text should be ""(.*)""")]
    public async Task ThenValueIs(WebElementWrapper element, string expectedString)
    {
        await Assertions.Expect(element.Locator).ToHaveTextAsync(expectedString);
    }
}