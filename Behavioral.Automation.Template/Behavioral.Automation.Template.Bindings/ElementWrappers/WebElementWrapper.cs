using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Behavioral.Automation.Template.Bindings.ElementWrappers
{
     public class WebElementWrapper : IWebElementWrapper
    {
        private readonly Func<IWebElement> _elementSelector;

        public WebElementWrapper([NotNull] Func<IWebElement> elementSelector, [NotNull] string caption,
            [NotNull] IDriverService driverService)
        {
            _elementSelector = elementSelector;
            Driver = driverService;
            Caption = caption;
        }

        public string Caption { get; }

        public IWebElement Element => _elementSelector();

        public string Text
        {
            get
            {
                try
                {
                    return Regex.Replace(Element.Text, @"\t|\n|\r", " ").Replace("  ", " ");
                }
                catch (Exception e) when (e is NullReferenceException or StaleElementReferenceException)
                {
                    return string.Empty;
                }
            }
        }

        public string GetAttribute(string attribute)
        {
            try
            {
                return Element.GetAttribute(attribute);
            }
            catch (Exception e) when (e is NullReferenceException or StaleElementReferenceException)
            {
                return null;
            }
        }

        public void Click()
        {
            Assert.ShouldBecome(() => Enabled, true, $"Unable to click on {Caption}. The element was disabled");
            try
            {
                Element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                MouseHover();
                Driver.MouseClick();
            }
        }

        public void MouseHover()
        {
            Assert.ShouldBecome(() => Displayed, true, $"{Caption} is not visible");
            Driver.ScrollTo(Element);
        }

        public void SendKeys(string text)
        {
            Assert.ShouldBecome(() => Enabled, true, $"{Caption} is disabled");
            Element.SendKeys(text);
        }

        public bool Displayed
        {
            get
            {
                try
                {
                    return Element is not null && Element.Displayed;
                }
                catch (Exception e) when (e is NullReferenceException or StaleElementReferenceException)
                {
                    return false;
                }
            }
        }

        public bool Enabled => Displayed && Element.Enabled;

        public string Tooltip => GetAttribute("data-test-tooltip-text");

        public bool Stale
        {
            get
            {
                try
                {
                    // Calling any method forces a staleness check
                    var elementEnabled = Element.Enabled;
                    return false;
                }
                catch (Exception e) when (e is NullReferenceException or StaleElementReferenceException)
                {
                    return true;
                }
            }
        }

        public IEnumerable<IWebElementWrapper> FindSubElements(By locator, string caption)
        {
            try
            {
                return ElementsToWrappers(Assert.ShouldGet(() => Element.FindElements(locator)), caption);
            }
            catch (Exception e) when (e is NullReferenceException or InvalidOperationException)
            {
                NUnit.Framework.Assert.Fail($"Couldn't find elements with {caption}");
                return null;
            }
        }

        private IEnumerable<IWebElementWrapper> ElementsToWrappers(IEnumerable<IWebElement> elements, string caption)
        {
            return elements.Select(element => new WebElementWrapper(() => element, caption, Driver));
        }

        protected IDriverService Driver { get; }
    }
}
