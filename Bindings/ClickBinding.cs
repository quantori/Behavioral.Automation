using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Elements;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for clicks execution
    /// </summary>
    [Binding]
    public sealed class ClickBinding
    {
        /// <summary>
        /// Execute click on the element
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user clicks on "Test" button</example>
        [Given("user clicks on (.*)")]
        [When("user clicks on (.*)")]
        public void Click([NotNull] IWebElementWrapper element)
        {
            element.Click();
        }

        /// <summary>
        /// Execute double click on the element
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user clicks twice on "Test" button</example>
        [Given("user clicks twice on (.*)")]
        [When("user clicks twice on (.*)")]
        public void ClickTwice([NotNull] IWebElementWrapper element)
        {
            element.Click();
            element.Click();
        }

        /// <summary>
        /// Execute triple click on the element
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user clicks three times on "Test" button</example>
        [Given("user clicks three times on (.*)")]
        [When("user clicks three times on (.*)")]
        public void ClickThreeTimes([NotNull] IWebElementWrapper element)
        {
            element.Click();
            element.Click();
            element.Click();
        }

        /// <summary>
        /// Execute click on the specific element in the collection
        /// </summary>
        /// <param name="index">Number of the tested element in the collection</param>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user clicks at first element among "Test" buttons (note that numbers from 1 to 10 can be written as words)</example>
        [When("user clicks at (.*) element among (.*)")]
        public void ClickByIndex(int index, IElementCollectionWrapper elements)
        {
            elements.ClickByIndex(index-1);
        }

        /// <summary>
        /// Execute doulbe click on the specific element in the collection
        /// </summary>
        /// <param name="index">Number of the tested element in the collection</param>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user clicks twice at first element among "Test" buttons (note that numbers from 1 to 10 can be written as words)</example>
        [When("user clicks twice at (.*) element among (.*)")]
        public void ClickTwiceByIndex(int index, IElementCollectionWrapper elements)
        {
            elements.ClickByIndex(index - 1);
            elements.ClickByIndex(index - 1);
        }

        /// <summary>
        /// Execute triple click on the specific element in the collection
        /// </summary>
        /// <param name="index">Number of the tested element in the collection</param>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user clicks three times at first element among "Test" buttons (note that numbers from 1 to 10 can be written as words)</example>
        [When("user clicks three times at (.*) element among (.*)")]
        public void ClickByIndexThreeTimes(int index, IElementCollectionWrapper elements)
        {
            elements.ClickByIndex(index - 1);
            elements.ClickByIndex(index - 1);
            elements.ClickByIndex(index - 1);
        }

        /// <summary>
        /// Hover mouse over element
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user hovers mouse over "Test" button</example>
        [When("user hovers mouse over (.*)")]
        public void HoverMouse(IWebElementWrapper element)
        {
            element.MouseHover();
        }
    }
}
