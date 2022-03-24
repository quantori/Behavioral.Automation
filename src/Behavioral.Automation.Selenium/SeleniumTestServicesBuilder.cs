using Behavioral.Automation.Selenium.Services;
using Behavioral.Automation.Services;
using BoDi;

namespace Behavioral.Automation.Selenium
{
    /// <summary>
    /// Initialise all necessary objects before test execution with Selenium
    /// </summary>
    public sealed class SeleniumTestServicesBuilder
    {
        private readonly IObjectContainer _objectContainer;

        public SeleniumTestServicesBuilder(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public void Build()
        {
            _objectContainer.RegisterTypeAs<DriverService, IDriverServiceBase>();
            _objectContainer.RegisterTypeAs<DriverService, IDriverService>();
            _objectContainer.RegisterTypeAs<ElementSelectionService, IElementSelectionService>();
            _objectContainer
                .RegisterTypeAs<VirtualizedElementsSelectionService, IVirtualizedElementsSelectionService>();
        }
    }
}