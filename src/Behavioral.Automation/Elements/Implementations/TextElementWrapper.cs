using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements.Implementations
{
    public sealed class TextElementWrapper : WebElementWrapper, ITextElementWrapper
    {
        public TextElementWrapper([NotNull] IWebElementWrapper wrapper, string caption, [NotNull] IDriverService driverService)
            : base(() => wrapper.Element, caption, driverService) { }

        public void EnterString(string input)
        {
            Assert.ShouldBecome(() => Displayed, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");
            Element.SendKeys(input);
            Driver.RemoveFocusFromActiveElement();
        }

        public void ClearInput()
        {
            Assert.ShouldBecome(() => Displayed, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");
            Element.Clear();

            while (Element.GetAttribute("value").Length > 0)
            {
                Element.SendKeys(Keys.Backspace);
            }
            Driver.RemoveFocusFromActiveElement();
        }
    }
}