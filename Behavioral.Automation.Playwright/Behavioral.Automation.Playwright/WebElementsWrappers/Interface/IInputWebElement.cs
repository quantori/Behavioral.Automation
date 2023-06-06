using System.Threading.Tasks;

namespace Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

public interface IInputWebElement
{
    public Task TypeAsync(string text);
}