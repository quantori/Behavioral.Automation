using System;
using System.Linq;
using Behavioral.Automation.Playwright.Pages;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.Utils;
using JetBrains.Annotations;

namespace Behavioral.Automation.Playwright.Services;

[UsedImplicitly]
public class LocatorStorageService
{
    //TODO: Impl factory
    public T Get<T>(string elementName)
    {
        if (string.IsNullOrEmpty(elementName)) throw new Exception("element name can not be empty. Please correct test step");
        var type = typeof(ISelectorStorage);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass);

        T? element = default(T);
        foreach (var pageType in types)
        {
            var instanceOfClassWithElements = Activator.CreateInstance(pageType);
            var classField = (T) pageType.GetField(elementName.ToCamelCase())?.GetValue(instanceOfClassWithElements)!;
            if (element != null && classField != null)
                throw new Exception($"found the same selector '{elementName}' in different classes");
            element ??= classField;
        }
        
        if (element == null) throw new Exception($"'{elementName}' selectors not found.");
        return element;
        }
    }
