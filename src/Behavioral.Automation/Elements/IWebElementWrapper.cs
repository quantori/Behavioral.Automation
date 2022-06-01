using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    public interface IWebElementWrapper
    {
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