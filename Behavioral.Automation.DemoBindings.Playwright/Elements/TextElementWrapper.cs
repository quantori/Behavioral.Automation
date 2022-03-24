using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Playwright.Elements;

namespace Behavioral.Automation.DemoBindings.Playwright.Elements
{
    public sealed class TextElementWrapper : WebElementWrapper, ITextElementWrapper
    {
        public TextElementWrapper([NotNull] IWebElementWrapperPlaywright wrapper, string caption)
            : base(() => wrapper.Element, caption) { }

        public void EnterString(string input)
        {
            Assert.ShouldBecome(() => Displayed && !Stale, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");
            SendKeys(input);

        }

        public new string Text =>  base.Text;
        

        public void ClearInput()
        {
            Assert.ShouldBecome(() => Displayed, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");
            if (GetAttribute("value").Length > 0)
            {
                while (GetAttribute("value").Length > 0)
                {
                    Element.PressAsync("Backspace");
                }
            }
        }
    }
}