using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Model;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class ClickBinding
    {
        private readonly DebugBinding _debugBinding;
        private readonly PresenceBinding _presenceBinding;

        public ClickBinding(DebugBinding debugBinding, PresenceBinding presenceBinding)
        {
            _debugBinding = debugBinding;
            _presenceBinding = presenceBinding;
        }
        [Given("user clicked on (.*)")]
        [When("user clicks on (.*)")]
        public void Click([NotNull] IWebElementWrapper element)
        {
            element.Click();
        }

        [Given("user clicked twice on (.*)")]
        [When("user clicks twice on (.*)")]
        public void ClickTwice([NotNull] IWebElementWrapper element)
        {
            element.Click();
            _debugBinding.Wait(1);
            _presenceBinding.CheckElementShown(element, new AssertionBehavior(AssertionType.Continuous, false));
            element.Click();
        }
        
        [Given("user clicked three times on (.*)")]
        [When("user clicks three times on (.*)")]
        public void ClickThreeTimes([NotNull] IWebElementWrapper element)
        {
            element.Click();
            _debugBinding.Wait(1);
            _presenceBinding.CheckElementShown(element, new AssertionBehavior(AssertionType.Continuous, false));

            element.Click();
            _debugBinding.Wait(1);
            _presenceBinding.CheckElementShown(element, new AssertionBehavior(AssertionType.Continuous, false));

            element.Click();
            _debugBinding.Wait(1);
            _presenceBinding.CheckElementShown(element, new AssertionBehavior(AssertionType.Continuous, false));
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
