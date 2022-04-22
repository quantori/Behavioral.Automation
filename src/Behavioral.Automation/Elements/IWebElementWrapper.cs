using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Common interface for every web page element with general properties and methods
    /// </summary>
    public interface IWebElementWrapper
    {
        /// <summary>
        /// Web element object of <see cref="IWebElement"/> type
        /// </summary>
        IWebElement Element { get; }

        /// <summary>
        /// Element caption
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Text representation of element
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Web element attribute
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>attribute value</returns>
        public string GetAttribute([NotNull] string attribute);

        /// <summary>
        /// Performs a click on web element
        /// </summary>
        public void Click();

        /// <summary>
        /// Moves pointer over web element
        /// </summary>
        public void MouseHover();

        /// <summary>
        /// Collection of elements that are nested inside current web element
        /// </summary>
        /// <param name="locator">mechanism of finding element children using <see cref="By"/> class object</param>
        /// <param name="caption">element caption</param>
        /// <returns></returns>
        public IEnumerable<IWebElementWrapper> FindSubElements([NotNull] By locator, [CanBeNull] string caption);

        /// <summary>
        /// Nested Element inside current web element
        /// </summary>
        /// <param name="locator">mechanism of finding nested element using <see cref="By"/> class object</param>
        /// <param name="caption">element caption</param>
        /// <returns></returns>
        public IWebElementWrapper FindSubElement([NotNull] By locator, [CanBeNull] string caption);

        /// <summary>
        /// Sends given text symbol by symbol, including special symbols. See <see cref="Keys"/>
        /// </summary>
        /// <param name="text">text to enter</param>
        public void SendKeys(string text);

        /// <summary>
        /// Visibility of the element
        /// </summary>
        public bool Displayed { get; }

        /// <summary>
        /// Enabled state of the element
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// Element staleness. See <seealso cref="StaleElementReferenceException"/>
        /// </summary>
        public bool Stale { get; }
    }
}
