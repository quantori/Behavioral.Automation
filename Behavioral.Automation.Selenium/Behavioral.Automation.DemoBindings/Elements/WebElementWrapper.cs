using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public class WebElementWrapper : IWebElementWrapper
    {
        private readonly Func<IWebElement> _elementSelector;
        private readonly IDriverService _driverService;

        public WebElementWrapper([NotNull] Func<IWebElement> elementSelector, [NotNull] string caption, [NotNull] IDriverService driverService)
        {
            _elementSelector = elementSelector;
            _driverService = driverService;
            Caption = caption;
        }

        public string Caption { get; }

        public IWebElement Element => _elementSelector();

        public string Text => Element.Text;

        public string GetAttribute(string attribute) => Element.GetAttribute(attribute);

        public void Click()
        {
            MouseHover();
            Assert.ShouldGet(() => Enabled);
            _driverService.MouseClick();
        }

        public void MouseHover()
        {
            Assert.ShouldBecome(() => Enabled, true, $"{Caption} is disabled");
            _driverService.ScrollTo(Element);
        }

        public void SendKeys(string text)
        {
            Assert.ShouldBecome(() => Enabled, true, $"{Caption} is disabled");
            Element.SendKeys(text);
        }

        public bool Displayed => Element != null && Element.Displayed;

        public bool Enabled => Displayed && Element.Enabled && AriaEnabled;

        public string Tooltip
        {
            get
            {
                var matTooltip = GetAttribute("matTooltip");
                if (matTooltip != null)
                {
                    return matTooltip;
                }
                var ngReflectTip = GetAttribute("ng-reflect-message"); //some elements have their tooltips' texts stored inside 'ng-reflect-message' attribute
                if (ngReflectTip != null)
                {
                    return ngReflectTip;
                }

                return GetAttribute("aria-label"); //some elements have their tooltips' texts stored inside 'aria-label' attribute
            }
        }

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
            var elements = Assert.ShouldGet(() => Element.FindElements(locator));
            return ElementsToWrappers(elements, caption);
        }

        private IEnumerable<IWebElementWrapper> ElementsToWrappers(IEnumerable<IWebElement> elements, string caption)
        {
            foreach (var element in elements)
            {
                var wrapper = new WebElementWrapper(() => element, caption, _driverService);
                yield return wrapper;
            }
        }

        public IWebElementWrapper FindSubElement(By locator, string caption)
        {
            var element = Assert.ShouldGet(() => Element.FindElement(locator));
            return new WebElementWrapper(() => element, caption, _driverService);
        }

        protected IDriverService Driver => _driverService;

        private bool AriaEnabled
        {
            get
            {
                switch (Element.GetAttribute("aria-disabled"))
                {
                    case null:
                    case "false":
                        return true;
                }

                return false;
            }
        }

        public string TagName => Element.TagName;
    }
}
