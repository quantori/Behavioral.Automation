using Behavioral.Automation.Playwright;
using BoDi;

namespace Behavioral.Automation.DemoBindings.Playwright
{
    internal class DemoTestServicesBuilder
    {
        private readonly TestServicesBuilder _servicesBuilder;
        private readonly PlaywrightTestServicesBuilder _playwrightTestServicesBuilder;

        internal DemoTestServicesBuilder(TestServicesBuilder servicesBuilder, 
            PlaywrightTestServicesBuilder playwrightTestServicesBuilder)
        {
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