using BoDi;

namespace Behavioral.Automation.DemoBindings
{
    internal class DemoTestServicesBuilder
    {
        private readonly IObjectContainer _objectContainer;
        private readonly TestServicesBuilder _servicesBuilder;

        internal DemoTestServicesBuilder(IObjectContainer objectContainer, TestServicesBuilder servicesBuilder)
        {
            _objectContainer = objectContainer;
            _servicesBuilder = servicesBuilder;
        }

        internal void Build()
        {
            _servicesBuilder.Build();
        }
    }
}