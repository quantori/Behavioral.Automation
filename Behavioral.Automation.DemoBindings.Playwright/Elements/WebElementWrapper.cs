using System;
using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Playwright.Elements;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.DemoBindings.Playwright.Elements
{
    public class WebElementWrapper : IWebElementWrapperPlaywright
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
            try
            {
                return Element.GetAttributeAsync(attribute).Result;
            }
            catch (AggregateException)
            {
                return null;
            }
            
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
        
        public IEnumerable<IWebElementWrapperPlaywright> FindSubElements(string locator, string caption)
        {
            try
            {
                return Element.QuerySelectorAllAsync(locator).Result
                    .Select(h => new WebElementWrapper(() => h, caption));
            }
            catch (Exception e) when (e is NullReferenceException or InvalidOperationException)
            {
                NUnit.Framework.Assert.Fail($"Couldn't find elements with {caption}");
                return null;
            }
        }
        
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
                catch (Exception)
                {
                    return true;
                }
            }
        }
    }
}