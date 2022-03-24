using Behavioral.Automation.Selenium;
using BoDi;

namespace Behavioral.Automation.DemoBindings.Selenium
{
    internal class DemoTestServicesBuilder
    {
        private readonly IObjectContainer _objectContainer;
        private readonly TestServicesBuilder _servicesBuilder;
        private readonly SeleniumTestServicesBuilder _seleniumTestServicesBuilder;

        internal DemoTestServicesBuilder(IObjectContainer objectContainer, TestServicesBuilder servicesBuilder, SeleniumTestServicesBuilder seleniumTestServicesBuilder)
        {
            _objectContainer = objectContainer;
            _servicesBuilder = servicesBuilder;
            _seleniumTestServicesBuilder = seleniumTestServicesBuilder;
        }

        internal void Build()
        {
            _servicesBuilder.Build();
            _seleniumTestServicesBuilder.Build();
        }
    }
}