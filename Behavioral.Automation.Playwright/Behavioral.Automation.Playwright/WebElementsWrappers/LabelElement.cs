using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class LabelElement: WebElement, ILabelElement
{
    public async Task ShouldHaveTextAsync(string text)
    {
        await Assertions.Expect(Locator).ToHaveTextAsync(text);
    }
}