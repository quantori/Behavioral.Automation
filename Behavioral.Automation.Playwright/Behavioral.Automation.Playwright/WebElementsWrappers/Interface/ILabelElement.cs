using System.Threading.Tasks;

namespace Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

public interface ILabelElement
{
    public Task ShouldHaveTextAsync(string text);
}