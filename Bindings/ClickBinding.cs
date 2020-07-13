using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Elements;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class ClickBinding
    {
        [Given("user clicks on (.*)")]
        [When("user clicks on (.*)")]
        public void Click([NotNull] IWebElementWrapper element)
        {
            element.Click();
        }

        [Given("user clicks twice on (.*)")]
        [When("user clicks twice on (.*)")]
        public void ClickTwice([NotNull] IWebElementWrapper element)
        {
            element.Click();
            element.Click();
        }
        
        [Given("user clicks three times on (.*)")]
        [When("user clicks three times on (.*)")]
        public void ClickThreeTimes([NotNull] IWebElementWrapper element)
        {
            element.Click();
            element.Click();
            element.Click();
        }
        
        [When("user clicks at (.*) element among (.*)")]
        public void ClickByIndex(int index, IElementCollectionWrapper elements)
        {
            elements.ClickByIndex(index-1);
        }

        [When("user clicks twice at (.*) element among (.*)")]
        public void ClickTwiceByIndex(int index, IElementCollectionWrapper elements)
        {
            elements.ClickByIndex(index - 1);
            elements.ClickByIndex(index - 1);
        }

        [When("user clicks three times at (.*) element among (.*)")]
        public void ClickByIndexThreeTimes(int index, IElementCollectionWrapper elements)
        {
            elements.ClickByIndex(index - 1);
            elements.ClickByIndex(index - 1);
            elements.ClickByIndex(index - 1);
        }

        [When("user hovers mouse over (.*)")]
        public void HoverMouse(IWebElementWrapper element)
        {
            element.MouseHover();
        }
    }
}
