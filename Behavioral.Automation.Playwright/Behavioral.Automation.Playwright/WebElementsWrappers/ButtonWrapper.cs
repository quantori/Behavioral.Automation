using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class ButtonWrapper: WebElementWrapper, IButtonElement
{
    public ButtonWrapper([NotNull] WebContext webContext, [NotNull] ILocator locator, [NotNull] string caption) : base(webContext, locator, caption)
    {
    }

    public async Task ClickAsync()
    {
        await Locator.ClickAsync();
    }
}