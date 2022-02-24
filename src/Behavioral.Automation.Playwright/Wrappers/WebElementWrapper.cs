using System;
using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Playwright.Elements;
using Behavioral.Automation.Playwright.FluentAssertions;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Wrappers
{
    public class WebElementWrapper : IWebElementWrapper
    {
        private readonly Func<IElementHandle> _elementSelector;

        public WebElementWrapper([NotNull] Func<IElementHandle> elementSelector, [NotNull] string caption)
        {
            _elementSelector = elementSelector;
            Caption = caption;
        }

        public string Caption { get; }

        public IElementHandle Element => _elementSelector();

        public string GetAttribute(string attribute)
        {
            return Element.GetAttributeAsync(attribute).Result;
        }

        public void Click()
        {
            Element.ClickAsync();
        }

        public void MouseHover()
        {
            Element.HoverAsync();
        }

        public void SendKeys(string text)
        {
            Assert.ShouldBecome(() => Enabled, true, $"{Caption} was not enabled");
            Element.FillAsync(text);
        }

        public IEnumerable<IWebElementWrapper> FindSubElements(string locator, [CanBeNull] string caption)
        {
            return Element.QuerySelectorAllAsync(locator).Result
                .Select(h => new WebElementWrapper(() => h, caption));
        }

        public string Text => Element.TextContentAsync().Result;

        public bool Displayed
        {
            get
            {
                try
                {
                    return Element.IsVisibleAsync().Result;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        } 

        public bool Enabled => Element.IsEnabledAsync().Result;

        public bool Stale
        {
            get
            {
                try
                {
                    // Calling any method forces a staleness check
                    var elementEnabled = Enabled;
                    return false;
                }
                catch (Exception e)
                {
                    return true;
                }
            }
        }
    }
}