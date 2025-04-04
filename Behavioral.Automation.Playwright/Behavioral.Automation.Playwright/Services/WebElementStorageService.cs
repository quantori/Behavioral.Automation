using System;
using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Playwright.Pages;
using Behavioral.Automation.Playwright.Utils;
using BoDi;

namespace Behavioral.Automation.Playwright.Services;

public class WebElementStorageService : IWebElementStorageService
{
    private readonly WebContext _webContext;
    private readonly IObjectContainer _objectContainer;

    public WebElementStorageService(WebContext webContext, IObjectContainer objectContainer)
    {
        _webContext = webContext;
        _objectContainer = objectContainer;
    }

    public T Get<T>(string elementName) where T : IWebElement
    {
        var pages = GetAllPagesWithElements();
        var elementSelector = GetElementSelector(pages, elementName);
        
        // Select proper realisation for element according to registered class in DI framework:
        // TODO: add validation. Throw meaningful error if realization is not registered
        var classType = IWebElementStorageService.RegisteredElements[typeof(T)];
        var referenceToANewWebElement = Activator.CreateInstance(classType, _webContext, elementSelector);
        // TODO: Add more meaningful exception message:
        if (referenceToANewWebElement == null) throw new Exception("Can't create instance of a Web Element");
        var element = (IWebElement) referenceToANewWebElement;      
        element.Description = elementName;
        return (T) element;
    }

    private IEnumerable<Type> GetAllPagesWithElements()
    {
        var type = typeof(ISelectorStorage);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass); 
    }

    private ElementSelector GetElementSelector(IEnumerable<Type> pages, string elementName)
    {
        ElementSelector elementSelector = null;
        var camelCaseElementName = elementName.ToCamelCase();

        foreach (var pageType in pages)
        {
            var pageTemp = Activator.CreateInstance(pageType);
            var temp = (ElementSelector) pageType.GetField(camelCaseElementName)?.GetValue(pageTemp)!;
            if (elementSelector != null && temp != null)
                throw new Exception($"found the same selector '{elementName}' in different classes");
            elementSelector ??= temp;
        }

        if (elementSelector == null) throw new Exception($"'{elementName}' transformed to '{camelCaseElementName}' selectors not found.");
        return elementSelector;
    }
}