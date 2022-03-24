using System.Collections.Generic;
using System.Collections.ObjectModel;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services
{
    /// <summary>
    /// Interface for DriverService class
    /// </summary>
    public interface IDriverService : IDriverServiceBase
    {
        /// <summary>
        /// Find element by search attribute value
        /// </summary>
        /// <param name="id">Search attribute value</param>
        /// <returns>IElementHandle object or null if element was not found</returns>
        [NotNull]
        IElementHandle FindElement([NotNull] string id);


        /// <summary>
        /// Find element by search attribute value and xpath
        /// </summary>
        /// <param name="id">Search attribute value</param>
        /// <param name="subpath">Xpath</param>
        /// <returns>IElementHandle object or null if element was not found</returns>
        [NotNull]
        IElementHandle FindElement([NotNull] string id, [NotNull] string subpath);

        /// <summary>
        /// Find element by xpath only
        /// </summary>
        /// <param name="path">Element xpath</param>
        /// <returns></returns>
        [NotNull]
        IElementHandle FindElementByXpath([NotNull] string path);

        /// <summary>
        /// Find multiple elements having the same search attribute
        /// </summary>
        /// <param name="id">Search attribute</param>
        /// <returns>Read only collection of IElementHandle objects or null if elements were not found</returns>
        IEnumerable<IElementHandle> FindElements([NotNull] string id);

        /// <summary>
        /// Find multiple elements having the same search attribute
        /// </summary>
        /// <param name="path">Xpath string</param>
        /// <returns>Read only collection of IElementHandle objects or null if elements were not found</returns>
        IEnumerable<IElementHandle> FindElementsByXpath([NotNull] string path);

        /// <summary>
        /// Scroll element into view by offset
        /// </summary>
        /// <param name="element">IElementHandle object</param>
        /// <param name="offset">Desired offset</param>
        void ScrollElementTo(IElementHandle element, int offsetX, int offsetY = 0);
        
        /// <summary>
        /// Scroll to element
        /// </summary>
        /// <param name="element">IElementHandle object</param>
        void ScrollTo(IElementHandle element);
    }
}