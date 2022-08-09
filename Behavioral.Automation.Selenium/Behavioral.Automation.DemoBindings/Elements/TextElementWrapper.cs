using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public sealed class TextElementWrapper : WebElementWrapper, ITextElementWrapper
    {
        public TextElementWrapper([NotNull] IWebElementWrapper wrapper, string caption,
            [NotNull] IDriverService driverService)
            : base(() => wrapper.Element, caption, driverService)
        {
        }

        public void EnterString(string input)
        {
            Assert.ShouldBecome(() => Displayed && !Stale, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");
            InputElement.SendKeys(input);
        }

        public new string Text => string.IsNullOrEmpty(base.Text) ? InputElement.GetAttribute("value") : base.Text;

        private IWebElementWrapper InputElement => Element.TagName != "input"
            ? FindSubElements(By.XPath(".//input"), "Input node").First()
            : this;

        public void ClearInput()
        {
            Assert.ShouldBecome(() => Displayed, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");
            var attempts = 0;
            if (Text != null && Text.Length > 0)
            {
                attempts = Text.Length;
            }
            else
            {
                attempts = InputElement.Text.Length;
            }

            for (var i = 0; i < attempts; i++)
            {
                InputElement.SendKeys(Keys.Backspace);
            }

            InputElement.Element.Clear();
            Assert.ShouldBecome(() => string.IsNullOrEmpty(Text), true, "Something went wrong. Input was not cleared");
        }
    }
}