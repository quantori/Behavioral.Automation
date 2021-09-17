using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public sealed class DriverService : IDriverService
    {
        [NotNull]
        private readonly IScopeContextManager _scopeContextManager;
        private readonly BrowserContext _browserContext;
        
        public DriverService([NotNull] IScopeContextManager scopeContextManager, BrowserContext browserContext)
        {
            _scopeContextManager = scopeContextManager;
            _browserContext = browserContext;
        }

        private string SearchAttribute = ConfigServiceBase.SearchAttribute;

        public string Title => _browserContext.Driver.Title;

        public string CurrentUrl => _browserContext.Driver.Url;

        public IWebElement FindElement(string id)
        {
            try
            {
                return _browserContext.Driver.FindElement(By.XPath($"//*[@{SearchAttribute}='{id}']"));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public IWebElement FindElement(string id, string subpath)
        {
            try
            {
                return FindElement(id).FindElement(By.XPath(subpath));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public IWebElement FindElementByXpath(string path)
        {
            try
            {
                return _browserContext.Driver.FindElement(By.XPath(path));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(string id)
        {
            return _browserContext.Driver.FindElements(By.XPath($"//*[@{SearchAttribute}='{id}']"));
        }

        public ReadOnlyCollection<IWebElement> FindElementsByXpath(string path)
        {
            return _browserContext.Driver.FindElements(By.XPath(path));
        }

        public void ScrollTo(IWebElement element)
        {
            var scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                          + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                          + "window.scrollBy(0, elementTop-(viewPortHeight/2));";

            _browserContext.Driver.ExecuteScript(scrollElementIntoMiddle, element);
            Actions actions = new Actions(_browserContext.Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void MouseClick()
        {
            Actions actions = new Actions(_browserContext.Driver);
            actions.Click();
            actions.Perform();
        }

        public void CloseActiveElement()
        {
            var element = _browserContext.Driver.SwitchTo().ActiveElement();
            element.SendKeys(Keys.Escape);
        }

        public void NavigateToRelativeUrl(string url)
        {
            var uri = GetUriFromRelativePath(url);
            _browserContext.Driver.Navigate().GoToUrl(uri);
            _scopeContextManager.SwitchPage(uri);
        }

        public void SwitchToTheLastWindow()
        {
            var handle = _browserContext.Driver.WindowHandles.Last();
            _browserContext.Driver.SwitchTo().Window(handle);
        }

        public void SwitchToTheFirstWindow()
        {
            var handle = _browserContext.Driver.WindowHandles.First();
            _browserContext.Driver.SwitchTo().Window(handle);
        }

        private Uri GetUriFromRelativePath(string url)
        {
            return new Uri(new Uri(CurrentUrl), url);
        }

        public void RemoveFocusFromActiveElement()
        {
            _browserContext.Driver.ExecuteScript("document.activeElement.blur()");
        }

        public void DebugDumpPage()
        {
            var page = _browserContext.Driver.PageSource;
            Console.WriteLine(page);
        }

        public void Refresh()
        {
            _browserContext.Driver.Navigate().Refresh();
        }

        public void Navigate(string url)
        {
            _browserContext.Driver.Navigate().GoToUrl(url);
            var uri = new Uri(url);
            _scopeContextManager.SwitchPage(uri);
        }

        public void SwitchToLastWindow()
        {
            _browserContext.Driver.SwitchTo().Window(_browserContext.Driver.WindowHandles.Last());
        }

        public void ResizeWindow(int Height, int Width)
        {
            _browserContext.Driver.Manage().Window.Size = new Size(Width, Height);
        }

        public string MakeScreenShot()
        {
            var fileName = Regex.Replace(TestContext.CurrentContext.Test.Name, @"(\\|\"")", string.Empty) + ".png";
            _browserContext.Driver.GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Png);
            return fileName;
        }

        public void ScrollElementTo(IWebElement element, int offset)
        {
            _browserContext.Driver.ExecuteScript(@"
                var element = arguments[0];
                var offset = arguments[1];
                element.scrollTo({top: offset, behavior: 'smooth'});", element, offset);
            Thread.Sleep(1000);
        }
    }
}