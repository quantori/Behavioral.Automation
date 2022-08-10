using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using OpenQA.Selenium;
namespace Behavioral.Automation.DemoBindings.Elements
{
    public sealed class ElementCollectionWrapper : WebElementWrapper, IElementCollectionWrapper
    {
        private readonly string _searchAttribute = ConfigServiceBase.SearchAttribute;

        public ElementCollectionWrapper(IWebElementWrapper wrapper,
            string caption,
            IDriverService driverService) :
            base(() => wrapper.Element, caption, driverService)
        {
        }

        private string Id => Element.GetAttribute(_searchAttribute);

        public IEnumerable<IWebElementWrapper> Elements => Assert.ShouldGet(() =>
        {
            var elements = GetActiveElementsFromCurrentView();

            return elements.ToList();
        });

        private IEnumerable<IWebElementWrapper> GetActiveElementsFromCurrentView()
        {
            IEnumerable<IWebElementWrapper> elements = null;
            Assert.ShouldGet(() => elements = GetElementsFromCurrentView());
            Assert.ShouldBecome(() => (!elements.Any() || !elements.FirstOrDefault().Stale), true,
                $"Element {Caption} is stale");
            foreach (var element in elements)
            {
                Assert.ShouldBecome(() =>
                    element.Displayed, true, $"Element {element.Caption} is not visible");
                yield return element;
            }
        }

        private IEnumerable<IWebElementWrapper> GetElementsFromCurrentView()
        {
            var elements =
                FindSubElements(By.XPath($"//*[@{_searchAttribute}='{Assert.ShouldGet(() => Id)}']"),
                    $"{Caption} element");
            return elements;
        }


        public void ClickByIndex(int index)
        {
            IWebElementWrapper selectedElement = null;
            Assert.ShouldGet(() => selectedElement = Elements.ElementAt(index));
            selectedElement.Click();
        }

        public void ClickAll()
        {
            foreach (var element in Elements)
            {
                element.Click();
            }
        }

        public string GetAttributeByIndex(string attribute, int index)
        {
            var selectedElement = Elements.ToList().ElementAt(index);
            return selectedElement.GetAttribute(attribute);
        }
    }
}