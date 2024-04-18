using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.Implementation.Selenium.Bindings;
using NUnit.Framework;
using OpenQA.Selenium;
using IWebElement = Behavioral.Automation.AsyncAbstractions.UI.Interfaces.IWebElement;

namespace Behavioral.Automation.Implementation.Selenium.WebElements;

public class SeleniumWebElement: IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public Task ShouldBecomeVisibleAsync()
    {
        return Locator.Displayed ? Task.Run(() => { }) : Task.Run(() => { Assert.Fail("Element is not visible"); });
    }

    protected SeleniumWebElement(WebContext webContext, ElementSelector baseSelector)
    {
        ElementSelector = baseSelector;
        WebContext = webContext;
    }

    public OpenQA.Selenium.IWebElement Locator
    {
        get
        {
            if (WebContext is null) throw new NullReferenceException("Please set web context.");
            if (ElementSelector.XpathSelector != null)
            {
                return ((Page) WebContext.Page).driver.FindElement(By.XPath(ElementSelector.XpathSelector));
            }

            throw new Exception("Currently only search by Xpath selector is implemented");
        }
    }
}