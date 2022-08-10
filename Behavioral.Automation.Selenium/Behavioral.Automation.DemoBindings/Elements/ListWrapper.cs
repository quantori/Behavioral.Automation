using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using OpenQA.Selenium;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public sealed class ListWrapper : WebElementWrapper, IListWrapper
    {
        public ListWrapper(IWebElementWrapper wrapper, string caption, IDriverService driverService) :
            base(() => wrapper.Element, caption, driverService) { }

        private IEnumerable<IWebElementWrapper> ListElements => FindSubElements(By.XPath(".//li[contains(@automation-id, 'list-item')]"), $"{Caption} item");

        public IEnumerable<string> ListValues => ListElements.Select(x => x.Text.Replace("\r\n", " "));
    }
}

