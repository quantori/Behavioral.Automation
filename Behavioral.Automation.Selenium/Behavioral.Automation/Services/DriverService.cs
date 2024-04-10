using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// WebDriver interactions methods
    /// </summary>
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

        /// <summary>
        /// Find web element using search attribute
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>IWebElement object or null if element was not found</returns>
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

        /// <summary>
        /// Find element using search attribute and xpath
        /// </summary>
        /// <param name="id">Element id</param>
        /// <param name="subpath">Xpath subpath</param>
        /// <returns>IWebElement object or null if element was not found</returns>
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

        /// <summary>
        /// Find element by xpath only
        /// </summary>
        /// <param name="path">Element's xpath</param>
        /// <returns>IWebElement object or null if element was not found</returns>
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

        /// <summary>
        /// Find element collection by id
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>ReadOnlyCollection of IWebElement objects</returns>
        public ReadOnlyCollection<IWebElement> FindElements(string id)
        {
            return Driver.FindElements(By.XPath($"//*[@{SearchAttribute}='{id}']"));
        }

        /// <summary>
        /// Find element collection by xpath
        /// </summary>
        /// <param name="path">Element xpath</param>
        /// <returns>ReadOnlyCollection of IWebElement objects</returns>
        public ReadOnlyCollection<IWebElement> FindElementsByXpath(string path)
        {
            return Driver.FindElements(By.XPath(path));
        }

        /// <summary>
        /// Execute JavaScript
        /// </summary>///
        /// <param name="script">Script text</param>
        /// <param name="args">Script arguments</param>
        /// <returns>ReadOnlyCollection of IWebElement objects</returns>
        public object ExecuteScript(string script, params object[] args)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            return js.ExecuteScript(script, args);
        }

        /// <summary>
        /// Scroll page to element
        /// </summary>
        /// <param name="element">IWebElement object</param>
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

        /// <summary>
        /// Execute mouse click
        /// </summary>
        public void MouseClick()
        {
            Actions actions = new Actions(Driver);
            actions.Click();
            actions.Perform();
        }

        /// <summary>
        /// Remove focus from the element
        /// </summary>
        public void CloseActiveElement()
        {
            var element = Driver.SwitchTo().ActiveElement();
            element.SendKeys(Keys.Escape);
        }

        /// <summary>
        /// Open relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        public void NavigateToRelativeUrl(string url)
        {
            var uri = GetUriFromRelativePath(url);
            Driver.Navigate().GoToUrl(uri);
            _scopeContextManager.SwitchPage(uri);
        }

        /// <summary>
        /// Switch to the last opened window
        /// </summary>
        public void SwitchToTheLastWindow()
        {
            var handle = Driver.WindowHandles.Last();
            Driver.SwitchTo().Window(handle);
        }

        /// <summary>
        /// Switch to the window which was opened first
        /// </summary>
        public void SwitchToTheFirstWindow()
        {
            var handle = Driver.WindowHandles.First();
            Driver.SwitchTo().Window(handle);
        }

        /// <summary>
        /// Close inactive windows
        /// </summary>
        public void CloseInactiveWindows()
        {
            var currentWindowHandle = Driver.CurrentWindowHandle;
            var windowHandles = Driver.WindowHandles;

            foreach (var windowHandle in windowHandles)
            {
                if (!windowHandle.Equals(currentWindowHandle))
                {
                    Driver.SwitchTo().Window(windowHandle);
                    Driver.Close();
                }
            }

            Driver.SwitchTo().Window(currentWindowHandle);
        }
        
        /// <summary>
        /// Get URI from relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        /// <returns>URI</returns>
        private Uri GetUriFromRelativePath(string url)
        {
            return new Uri(new Uri(CurrentUrl), url);
        }

        /// <summary>
        /// Removes focus from active page element
        /// </summary>
        public void RemoveFocusFromActiveElement()
        {
            ExecuteScript("document.activeElement.blur()");
        }

        /// <summary>
        /// Make DOM tree dump into the console
        /// </summary>
        public void DebugDumpPage()
        {
            var page = Driver.PageSource;
            Console.WriteLine(page);
        }
        
        /// <summary>
        /// Refresh currently opened page
        /// </summary>
        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        /// <summary>
        /// Open page by full URL
        /// </summary>
        /// <param name="url">URL</param>
        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
            var uri = new Uri(url);
            _scopeContextManager.SwitchPage(uri);
        }
        
        /// <summary>
        /// Change size of opened browser window
        /// </summary>
        /// <param name="height">Desired height</param>
        /// <param name="width">Desired width</param>
        public void ResizeWindow(int height, int width)
        {
            Driver.Manage().Window.Size = new Size(width, height);
        }
        
        /// <summary>
        /// Make .png screenshot with date and time
        /// </summary>
        /// <returns>Name of the screenshot file</returns>
        public string MakeScreenShot()
        {
            var fileName = new string(TestContext.CurrentContext.Test.Name
                .Where(x => !Path.GetInvalidFileNameChars().Contains(x))
                .ToArray()) + ".png";
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(fileName);
            return fileName;
        }

        /// <summary>
        /// Scroll element to offset
        /// </summary>
        /// <param name="element">IWebElement object</param>
        /// <param name="offset">Offset to scroll element to</param>
        public void ScrollElementTo(IWebElement element, int offset)
        {
            ExecuteScript(@"
                var element = arguments[0];
                var offset = arguments[1];
                element.scrollTo({top: offset, behavior: 'smooth'});", element, offset);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Save browser log into file
        /// </summary>
        /// <returns></returns>
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

        public void ClearCache()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
            ExecuteScript("javascript: localStorage.clear()");
        }
    }
}