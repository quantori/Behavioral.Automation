using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public sealed class DriverService : IDriverService
    {
        [NotNull]
        private readonly IScopeContextManager _scopeContextManager;
        private readonly BrowserRunner _browserRunner;

        public DriverService([NotNull] IScopeContextManager scopeContextManager, BrowserRunner browserRunner)
        {
            _scopeContextManager = scopeContextManager;
            _browserRunner = browserRunner;
        }

        public IWebDriver Driver => _browserRunner.Driver;

        private readonly string SearchAttribute = ConfigServiceBase.SearchAttribute;

        public string Title => Driver.Title;

        public string CurrentUrl => Driver.Url;

        public IWebElement FindElement(string id)
        {
            try
            {
                return Driver.FindElement(By.XPath($"//*[@{SearchAttribute}='{id}']"));
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
                return Driver.FindElement(By.XPath(path));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(string id)
        {
            return Driver.FindElements(By.XPath($"//*[@{SearchAttribute}='{id}']"));
        }

        public ReadOnlyCollection<IWebElement> FindElementsByXpath(string path)
        {
            return Driver.FindElements(By.XPath(path));
        }

        public object ExecuteScript(string script, params object[] args)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            return js.ExecuteScript(script, args);
        }

        public void ScrollTo(IWebElement element)
        {
            var scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                          + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                          + "window.scrollBy(0, elementTop-(viewPortHeight/2));";

            ExecuteScript(scrollElementIntoMiddle, element);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void MouseClick()
        {
            Actions actions = new Actions(Driver);
            actions.Click();
            actions.Perform();
        }

        public void CloseActiveElement()
        {
            var element = Driver.SwitchTo().ActiveElement();
            element.SendKeys(Keys.Escape);
        }

        public void NavigateToRelativeUrl(string url)
        {
            var uri = GetUriFromRelativePath(url);
            Driver.Navigate().GoToUrl(uri);
            _scopeContextManager.SwitchPage(uri);
        }

        public void SwitchToTheLastWindow()
        {
            var handle = Driver.WindowHandles.Last();
            Driver.SwitchTo().Window(handle);
        }

        public void SwitchToTheFirstWindow()
        {
            var handle = Driver.WindowHandles.First();
            Driver.SwitchTo().Window(handle);
        }

        private Uri GetUriFromRelativePath(string url)
        {
            return new Uri(new Uri(CurrentUrl), url);
        }

        public void RemoveFocusFromActiveElement()
        {
            ExecuteScript("document.activeElement.blur()");
        }

        public void DebugDumpPage()
        {
            var page = Driver.PageSource;
            Console.WriteLine(page);
        }

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
            var uri = new Uri(url);
            _scopeContextManager.SwitchPage(uri);
        }

        public void SwitchToLastWindow()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }

        public void ResizeWindow(int Height, int Width)
        {
            Driver.Manage().Window.Size = new Size(Width, Height);
        }

        public string MakeScreenShot()
        {
            var fileName = new string(TestContext.CurrentContext.Test.Name
                .Where(x => !Path.GetInvalidFileNameChars().Contains(x))
                .ToArray()) + ".png";
            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            return fileName;
        }

        public void ScrollElementTo(IWebElement element, int offset)
        {
            ExecuteScript(@"
                var element = arguments[0];
                var offset = arguments[1];
                element.scrollTo({top: offset, behavior: 'smooth'});", element, offset);
            Thread.Sleep(1000);
        }

        public string SaveBrowserLog()
        {
            var fileName = new string(TestContext.CurrentContext.Test.Name
                               .Where(x => !Path.GetInvalidFileNameChars().Contains(x))
                               .ToArray()) + ".log";
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            var log = Driver.Manage().Logs.GetLog(LogType.Browser);
            File.AppendAllLines(fileName, log.Select(l => l.ToString()));

            return fileName;
        }
    }
}