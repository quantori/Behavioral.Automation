using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Drawing;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Drawing.Imaging;

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

        public static RemoteWebDriver Driver;

        public DriverService([NotNull] IScopeContextManager scopeContextManager)
        {
            _scopeContextManager = scopeContextManager;
            Navigate("about:blank");
        }

        private WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(4));
        private ReadOnlyCollection<string> WindowHandles => Driver.WindowHandles;

        private string SearchAttribute = ConfigServiceBase.SearchAttribute;

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
        /// Scroll the page so web element is place in its center
        /// </summary>
        /// <param name="element">Web element to be placed in the center of the page</param>
        public void ScrollTo(IWebElement element)
        {
            var scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                             + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                             + "window.scrollBy(0, elementTop-(viewPortHeight/2));";
            
            Driver.ExecuteScript(scrollElementIntoMiddle, element);
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
        /// Get URI from relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        /// <returns>URI</returns>
        private Uri GetUriFromRelativePath(string url)
        {
            return new Uri(new Uri(CurrentUrl), url);
        }


        /// <summary>
        /// Find element collection by id
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>ReadOnlyCollection of IWebElement objects or null if no elements were found</returns>
        public ReadOnlyCollection<IWebElement> FindElements(string id)
        {
            try
            {
                return Driver.FindElements(By.XPath($"//*[@{SearchAttribute}='{id}']"));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        /// Removes focus from active page element
        /// </summary>
        public void RemoveFocusFromActiveElement()
        {
            Driver.ExecuteScript("document.activeElement.blur()");
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
        /// Switch to the last opened window
        /// </summary>
        public void SwitchToLastWindow()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }
        
        /// <summary>
        /// Change size of opened browser window
        /// </summary>
        /// <param name="Height">Desired height</param>
        /// <param name="Width">Desired width</param>
        public void ResizeWindow(int Height, int Width)
        {
            Driver.Manage().Window.Size = new Size(Width, Height);
        }
        
        /// <summary>
        /// Make .png screenshot with date and time
        /// </summary>
        /// <returns>Name of the screenshot file</returns>
        public string MakeScreenShot()
        {
            var fileName = "screenshot_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".png";
            Driver.GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Png);
            return fileName;
        }
    }
}
