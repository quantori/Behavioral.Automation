using System;
using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Assert = Behavioral.Automation.FluentAssertions.Assert;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public class DropdownWrapper : WebElementWrapper, IDropdownWrapper
    {
        private readonly IDriverService _driverService;

        public DropdownWrapper(IWebElementWrapper wrapper, string caption, IDriverService driverService) :
            base(() => wrapper.Element, caption, driverService)
        {
            _driverService = driverService;
        }

        public string SelectedValue
        {
            get
            {
                if (ValueSelector != null && ValueSelector.GetAttribute("value").Length > 1)
                {
                    return ValueSelector.GetAttribute("value");
                }

                if (SelectedAttribute != null)
                {
                    return SelectedAttribute;
                }

                return Text;
            }
        }

        private string SelectedAttribute => GetAttribute("data-test-selected");

        private IWebElementWrapper ValueSelector =>
            FindSubElements(By.XPath(".//input"), $"{Caption} value selector").FirstOrDefault();

        public void Select(params string[] elements)
        {
            Assert.ShouldBecome(() => Enabled, true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not enabled");

            Click();

            Assert.ShouldBecome(() => Elements.Any(d => d.Displayed) && !Elements.Any(d => d.Stale), true,
                $"{Caption} elements are not visible");

            Assert.ShouldBecome(
                () => elements.All(x => Elements.Any(y => y.Text == x)),
                true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"Cannot find elements \"{elements.Aggregate((x, y) => $"{x}, {y}")}\"");
            foreach (var element in elements)
            {
                Elements.Single(x => x.Text == element).Click();
            }
        }

        public bool Empty => string.IsNullOrEmpty(SelectedValue);

        public IEnumerable<string> Items
        {
            get
            {
                Click();
                try
                {
                    var itemTexts = Assert.ShouldGet(() => Elements.Select(x => x.Text).ToList());
                    return itemTexts;
                }
                catch (InvalidOperationException)
                {
                    return Enumerable.Empty<string>();
                }
            }
        }

        public IEnumerable<IWebElementWrapper> Elements => Assert.ShouldGet(() =>
            FindSubElements(By.XPath("//*[@data-automation-id='dropdown-option']|.//option"),
                $"{Caption} element"));

        private string AutoCloseNeeded => Assert.ShouldGet(() => GetAttribute("data-test-close-needed"));

        private void CloseDropdown()
        {
            try
            {
                Driver.CloseActiveElement();
            }
            catch (Exception e) when (e is ElementNotInteractableException || e is StaleElementReferenceException)
            {
                var builder = new Actions(_driverService.Driver);
                builder.SendKeys(Keys.Escape).Build().Perform();
            }
        }
    }
}