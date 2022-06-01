﻿using System.Collections.Generic;
using Behavioral.Automation.Elements;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Elements
{
    /// <summary>
    /// Common interface for every web page element with general properties and methods
    /// </summary>
    public interface IWebElementWrapperPlaywright : IWebElementWrapper
    {
        /// <summary>
        /// Web element object of <see cref="IElementHandle"/> type
        /// </summary>
        IElementHandle Element { get; }

        /// <summary>
        /// Nested Element inside current web element
        /// </summary>
        /// <param name="locator">mechanism of finding nested element using <see cref="By"/> class object</param>
        /// <param name="caption">element caption</param>
        /// <returns></returns>
        public IWebElementWrapperPlaywright FindSubElement([NotNull] string locator, [CanBeNull] string caption);
        
        /// <summary>
        /// Collection of elements that are nested inside current web element
        /// </summary>
        /// <param name="locator">string for element locator</param>
        /// <param name="caption">element caption</param>
        /// <returns></returns>
        public IEnumerable<IWebElementWrapperPlaywright> FindSubElements([NotNull] string locator, [CanBeNull] string caption);
    }
}
