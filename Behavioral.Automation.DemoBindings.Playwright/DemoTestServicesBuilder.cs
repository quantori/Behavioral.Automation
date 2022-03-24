using Behavioral.Automation.Playwright;
using BoDi;

namespace Behavioral.Automation.DemoBindings.Playwright
{
    internal class DemoTestServicesBuilder
    {
        private readonly IObjectContainer _objectContainer;
        private readonly TestServicesBuilder _servicesBuilder;
        private readonly PlaywrightTestServicesBuilder _playwrightTestServicesBuilder;

        internal DemoTestServicesBuilder(IObjectContainer objectContainer, 
            TestServicesBuilder servicesBuilder, 
            PlaywrightTestServicesBuilder playwrightTestServicesBuilder)
        {
            _objectContainer = objectContainer;
            _servicesBuilder = servicesBuilder;
            _playwrightTestServicesBuilder = playwrightTestServicesBuilder;
        }

        internal void Build()
        {
            _servicesBuilder.Build();
            _playwrightTestServicesBuilder.Build();
        }
    }
}