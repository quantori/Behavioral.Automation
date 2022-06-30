using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

public interface IDropdownWrapper: IWebElementWrapper
{
    public ILocator Items { get; set; }
    
    public Task<IReadOnlyList<string>> ItemsTexts { get; }

    public Task SelectValue(params string[] elements);

    public ILocator GetOption(string option);
}