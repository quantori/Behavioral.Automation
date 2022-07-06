using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services;

public interface ILocatorProvider
{
    public ILocator GetLocator(ElementSelector selector);
}