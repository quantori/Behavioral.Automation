using System.Collections.ObjectModel;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Selenium.Services
{
    /// <summary>
    /// Interface for DriverService class
    /// </summary>
    public interface IDriverService : IDriverServiceBase
    {
        /// <summary>
        /// IWebDriverObject
        /// </summary>
        IWebDriver Driver { get; }
        
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
        /// <returns>Read only collection of WebElement objects or null if elements were not found</returns>
        ReadOnlyCollection<IWebElement> FindElements([NotNull] string id);

        /// <summary>
        /// Find multiple elements having the same search attribute
        /// </summary>
        /// <param name="path">Xpath string</param>
        /// <returns>Read only collection of WebElement objects or null if elements were not found</returns>
        ReadOnlyCollection<IWebElement> FindElementsByXpath([NotNull] string path);
        
        /// <summary>
        /// Execute mouse click
        /// </summary>
        void MouseClick();

        /// <summary>
        /// Scroll element into view by offset
        /// </summary>
        /// <param name="element">IWebElement object</param>
        /// <param name="offset">Desired offset</param>
        void ScrollElementTo(IWebElement element, int offset);
        
        /// <summary>
        /// Scroll to element
        /// </summary>
        /// <param name="element">Tested web element</param>
        void ScrollTo(IWebElement element);
    }
}