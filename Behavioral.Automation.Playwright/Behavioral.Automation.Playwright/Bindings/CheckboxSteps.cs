using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class CheckboxSteps
{
    [When(@"user clicks on ""(.+?)"" checkbox")]
    public async Task ClickOnElement(ICheckboxElement element)
    {
        await element.ClickAsync();
    }
}