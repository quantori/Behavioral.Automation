using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class TextElementBinding
{
    [Given("the (.*?) text contains \"(.*?)\"")]
    [Then("the (.*?) text should contain \"(.*?)\"")]
    public async Task GivenElementTextContains(IWebElementWrapper element, string text)
    {
        await Assertions.Expect(element.Locator).ToContainTextAsync(text);
    }
    
    [Given("the (.*?) text is \"(.*?)\"")]
    [Then("the (.*?) text should become \"(.*?)\"")]
    public async Task GivenElementTextIs(IWebElementWrapper element, string text)
    {
        var locator = element.Locator;
        await Assertions.Expect(locator).ToHaveTextAsync(text);
    }
}