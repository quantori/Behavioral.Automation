using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements.Implementations
{
    public sealed class ElementCollectionWrapper : WebElementWrapper, IElementCollectionWrapper
    {
        private readonly string _searchAttribute = ConfigServiceBase.SearchAttribute;
        private readonly IVirtualizedElementsSelectionService _virtualizedElementsSelectionService;

        public ElementCollectionWrapper(IWebElementWrapper wrapper,
            string caption,
            IDriverService driverService,
            IVirtualizedElementsSelectionService virtualizedElementsSelectionService
        ) :
            base(() => wrapper.Element, caption, driverService)
        {
            _virtualizedElementsSelectionService = virtualizedElementsSelectionService;
        }

        private string Id => Element.GetAttribute(_searchAttribute);


        public IEnumerable<IWebElementWrapper> Elements => Assert.ShouldGet(() =>
        {
            if (_virtualizedElementsSelectionService.ControlIsVirtualizable(Caption))
            {
                return _virtualizedElementsSelectionService.FindVirtualized(Caption, GetElementsFromCurrentView);
            }
            else
            {
                var elements = GetActiveElementsFromCurrentView();

                return elements;
            }
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
                    element.Enabled, true, $"Element {element.Caption} is not enabled");
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
            foreach (IWebElementWrapper element in Elements)
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
