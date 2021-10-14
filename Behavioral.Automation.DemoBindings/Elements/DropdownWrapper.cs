using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.DemoBindings.Elements
{
   public sealed class DropdownWrapper : Automation.Elements.Implementations.WebElementWrapper, IDropdownWrapper
    {
        public DropdownWrapper(IWebElementWrapper wrapper, string caption, IDriverService driverService) : 
            base(() => wrapper.Element, caption, driverService) { }

        public string SelectedValue
        {
            get
            {
                var defaultText = Element.FindElement(By.XPath(".//span")).Text;
                if (defaultText == "")
                {
                    // Attempt to get value from multiselect dropdown
                    defaultText = Element.FindElement(By.CssSelector(".custom-selector__input input")).GetProperty("value");
                }
                return defaultText;
            }
        }

        public void Select(params string[] elements)
        {
            Assert.ShouldBecome(() => Displayed, true, 
                new AssertionBehavior(AssertionType.Continuous, false),
                $"{Caption} is not displayed");

            if (!Autocomplete)
            {
                Click();
            }
            
            Assert.ShouldBecome(() => Stale, false, $"{Caption} is stale");
            
            Assert.ShouldBecome(
                () => elements.All(x => Elements.Any(y => y.Text == x)),
                true,
                new AssertionBehavior(AssertionType.Continuous, false),
                $"can not find elements \"{elements.Aggregate((x, y) => $"{x}, {y}")}\"");
            foreach (var element in elements)
            {
                Elements.Single(x => x.Text == element).Click();
            }
            Driver.CloseActiveElement();
        }

        public bool Empty => GetAttribute("class").Contains("mat-select-empty");

        private bool Autocomplete => GetAttribute("class").Contains("autocomplete");

        public IEnumerable<string> Items => Assert.ShouldGet(() => Elements.Select(x => x.Text).ToList());

        public IEnumerable<IWebElementWrapper> Elements => FindSubElements(By.XPath("//div[contains(@class, 'custom-selector__option')]"), $"{Caption} element");

        public IEnumerable<IWebElementWrapper> Groups => 
            FindSubElements(By.XPath(".//label[contains(@class, 'mat-optgroup-label')]"), $"{Caption} group");

        public IEnumerable<string> GroupTexts
        {
            get
            {
                Assert.ShouldBecome(() => Groups.All(x => x.Stale), false, $"{Caption} groups are stale");
                return Groups.Select(x => x.Text);
            }
        }
    }
}
