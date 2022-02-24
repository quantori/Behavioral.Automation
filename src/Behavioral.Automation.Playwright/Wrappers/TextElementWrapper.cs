using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using IWebElementWrapper = Behavioral.Automation.Playwright.Elements.IWebElementWrapper;

namespace Behavioral.Automation.Playwright.Wrappers
{
    public sealed class TextElementWrapper : WebElementWrapper, ITextElementWrapper
    {
        public TextElementWrapper([NotNull] IWebElementWrapper wrapper, string caption)
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