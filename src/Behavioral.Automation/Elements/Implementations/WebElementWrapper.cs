using System;
using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements.Implementations
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
                    return Element.Text;
                }
                catch (NullReferenceException)
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
            catch (Exception e) when (e is NullReferenceException || e is StaleElementReferenceException)
            {
                return null;
            }
        }

        public void Click()
        {
            MouseHover();
            Assert.ShouldBecome(() => Enabled, true, $"Unable to click on {Caption}. The element was disabled");
            try
            {
                Element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                Driver.MouseClick();
            }
        }

        public void MouseHover()
        {
            Assert.ShouldBecome(() => Enabled, true, $"{Caption} is disabled");
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
                    return !(Element is null) && Element.Displayed;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }

        public bool Enabled => Displayed && Element.Enabled;

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
                catch (StaleElementReferenceException)
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
            catch (NullReferenceException)
            {
                NUnit.Framework.Assert.Fail($"Couldn't find elements with {caption}");
                return null;
            }
        }

        private IEnumerable<IWebElementWrapper> ElementsToWrappers(IEnumerable<IWebElement> elements, string caption)
        {
            return elements.Select(element => new WebElementWrapper(() => element, caption, Driver)).Cast<IWebElementWrapper>();
        }

        protected IDriverService Driver { get; }
    }
}