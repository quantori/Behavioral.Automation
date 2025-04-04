using System.Threading.Tasks;
using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Playwright.Selectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class LabelElement : PlaywrightWebElement, ILabelElement
{
    public LabelElement(WebContext webContext, LabelSelector selector) : base(webContext, selector)
    {
    }

    public async Task ShouldHaveTextAsync(string text)
    {
        await Assertions.Expect(Locator).ToHaveTextAsync(text);
    }

    public async Task ShouldNotHaveTextAsync(string text)
    {
        await Assertions.Expect(Locator).Not.ToHaveTextAsync(text);
    }
}