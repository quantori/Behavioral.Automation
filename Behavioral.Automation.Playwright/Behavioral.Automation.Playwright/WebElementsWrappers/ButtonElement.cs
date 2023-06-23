using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class ButtonElement: WebElement, IButtonElement
{

    public async Task ClickAsync()
    {
        await Locator.ClickAsync();
    }

    public async Task ShouldBecomeVisibleAsync()
    {
        await Assertions.Expect(Locator).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() {Timeout = 50000});
    }
}