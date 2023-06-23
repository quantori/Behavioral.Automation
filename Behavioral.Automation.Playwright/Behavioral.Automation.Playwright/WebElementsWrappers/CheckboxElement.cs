using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class CheckboxElement : WebElement, ICheckboxElement
{
    public async Task ClickAsync()
    {
        await Locator.EvaluateAsync("node => node.removeAttribute('checked')");
    }
}