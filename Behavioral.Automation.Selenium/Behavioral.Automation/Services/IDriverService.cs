using System.Collections.ObjectModel;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Interface for DriverService class
    /// </summary>
    public interface IDriverService 
    {
        IWebDriver Driver { get; }

        string CurrentUrl { [NotNull] get; }

        string Title { [CanBeNull] get; }

        /// <summary>
        /// Find element by search attribute value
        /// </summary>
        /// <param name="id">Search attribute value</param>
        /// <returns>IWebElement object or null if element was not found</returns>
        [NotNull]
        IWebElement FindElement([NotNull] string id);


        /// <summary>
        /// Find element by search attribute value and xpath
        /// </summary>
        /// <param name="id">Search attribute value</param>
        /// <param name="subpath">Xpath</param>
        /// <returns>IWebElement object or null if element was not found</returns>
        [NotNull]
        IWebElement FindElement([NotNull] string id, [NotNull] string subpath);

        /// <summary>
        /// Find element by xpath only
        /// </summary>
        /// <param name="path">Element xpath</param>
        /// <returns></returns>
        [NotNull]
        IWebElement FindElementByXpath([NotNull] string path);

        /// <summary>
        /// Find multiple elements having the same search attribute
        /// </summary>
        /// <param name="id">Search attribute</param>
        /// <returns>Read only collection of IWebElement objects or null if elements were not found</returns>
        ReadOnlyCollection<IWebElement> FindElements([NotNull] string id);

        /// <summary>
        /// Find multiple elements having the same search attribute
        /// </summary>
        /// <param name="path">Xpath string</param>
        /// <returns>Read only collection of IWebElement objects or null if elements were not found</returns>
        ReadOnlyCollection<IWebElement> FindElementsByXpath([NotNull] string path);

        object ExecuteScript(string script, params object[] args);

        /// <summary>
        /// Refresh opened page
        /// </summary>
        public void Refresh();

        /// <summary>
        /// Navigate to URL
        /// </summary>
        /// <param name="url">URL</param>
        void Navigate([NotNull] string url);

        /// <summary>
        /// Scroll to element
        /// </summary>
        /// <param name="element">Tested web element</param>
        void ScrollTo(IWebElement element);

        /// <summary>
        /// Execute mouse click
        /// </summary>
        void MouseClick();

        /// <summary>
        /// Remove focus from the element (for example text field or input)
        /// </summary>
        void RemoveFocusFromActiveElement();

        /// <summary>
        /// Close active element
        /// </summary>
        void CloseActiveElement();

        /// <summary>
        /// Print current page layout into console
        /// </summary>
        void DebugDumpPage();

        /// <summary>
        /// Open relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        void NavigateToRelativeUrl(string url);

        /// <summary>
        /// Switch to the last opened window
        /// </summary>
        void SwitchToTheLastWindow();

        /// <summary>
        /// Switch to the window which was opened first
        /// </summary>
        void SwitchToTheFirstWindow();

        /// <summary>
        /// Change size of opened browser window
        /// </summary>
        /// <param name="Height">Desired height</param>
        /// <param name="Width">Desired width</param>
        void ResizeWindow(int Height, int Width);

        /// <summary>
        /// Make screenshot
        /// </summary>
        /// <returns>Name of the screenshot file</returns>
        string MakeScreenShot();

        /// <summary>
        /// Scroll element into view by offset
        /// </summary>
        /// <param name="element">IWebElement object</param>
        /// <param name="offset">Desired offset</param>
        void ScrollElementTo(IWebElement element, int offset);

        /// <summary>
        /// Saves browser console log
        /// </summary>
        /// <returns>Path to saved log</returns>
        string SaveBrowserLog();
    }
}