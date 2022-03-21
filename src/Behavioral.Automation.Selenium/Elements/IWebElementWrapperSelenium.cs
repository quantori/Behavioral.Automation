using System.Collections.Generic;
using Behavioral.Automation.Elements;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Selenium.Elements
{
    /// <summary>
    /// Common interface for every web page element with general properties and methods
    /// </summary>
    public interface IWebElementWrapperSelenium : IWebElementWrapper
    {
        /// <summary>
        /// Web element object of <see cref="IWebElement"/> type
        /// </summary>
        IWebElement Element { get; }
        
        /// <summary>
        /// Collection of elements that are nested inside current web element
        /// </summary>
        /// <param name="locator">mechanism of finding element children using <see cref="By"/> class object</param>
        /// <param name="caption">element caption</param>
        /// <returns></returns>
        public IEnumerable<IWebElementWrapper> FindSubElements([NotNull] By locator, [CanBeNull] string caption);
    }
}
