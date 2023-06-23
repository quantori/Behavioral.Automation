using System;
using System.Linq;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Pages;
using Behavioral.Automation.Playwright.Utils;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

namespace Behavioral.Automation.Playwright.Services;

public class LocatorStorageService
{
    private readonly WebContext _webContext;

    public LocatorStorageService(WebContext webContext)
    {
        _webContext = webContext;
    }

    //TODO: Impl factory
    public T Get<T>(string elementName)
    {
        var type = typeof(ISelectorStorage);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass);

        IWebElement element = null;
        var camelCaseElementName = elementName.ToCamelCase();

        foreach (var pageType in types)
        {
            var pageTemp = Activator.CreateInstance(pageType);
            var temp = (IWebElement) pageType.GetField(camelCaseElementName)?.GetValue(pageTemp)!;
            if (element != null && temp != null)
                throw new Exception($"found the same selector '{elementName}' in different classes");
            element ??= temp;
        }

        if (element == null) throw new Exception($"'{elementName}' transformed to '{camelCaseElementName}' selectors not found.");

        element.WebContext = _webContext;
        element.Description = elementName;
        
        return (T) element;
    }
}