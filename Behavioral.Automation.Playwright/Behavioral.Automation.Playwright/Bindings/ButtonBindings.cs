using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class ButtonBindings
{
    [Given(@"user clicked on ""(.+?)"" button")]
    [When(@"user clicks on ""(.+?)"" button")]
    public async Task ClickOnElement(IButtonElement element)
    {
        await element.ClickAsync();
    }
}