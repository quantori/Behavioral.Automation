using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class ButtonWrapper: WebElement, IButtonElement
{

    public async Task ClickAsync()
    {
        await Locator.ClickAsync();
    }
}