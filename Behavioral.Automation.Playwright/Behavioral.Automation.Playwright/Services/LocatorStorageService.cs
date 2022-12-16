using System;
using Behavioral.Automation.Playwright.Pages;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.Utils;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;

namespace Behavioral.Automation.Playwright.Services;

[UsedImplicitly]
public class LocatorStorageService
{
    //TODO: Impl factory
    public T Get<T>(string elementName)
    {
        var type = typeof(ISelectorStorage);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass);

        IWebElement element = null;
        foreach (var pageType in types)
        {
            var pageTemp = Activator.CreateInstance(pageType);
            var temp = (IWebElement) pageType.GetField(elementName.ToCamelCase())?.GetValue(pageTemp)!;
            if (element != null && temp != null)
                throw new Exception($"found the same selector '{elementName}' in different classes");
            element ??= temp;
        }
        
        if (element == null) throw new Exception($"'{elementName}' selectors not found.");

        element.WebContext = _webContext;
        return (T) element;
    }
}
