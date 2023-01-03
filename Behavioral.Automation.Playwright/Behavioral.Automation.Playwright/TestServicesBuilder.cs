using Behavioral.Automation.Playwright.Services;
using BoDi;
using Gherkin.Ast;

namespace Behavioral.Automation.Playwright
{
    /// <summary>
    /// Initialise all necessary objects before test execution
    /// </summary>
    public sealed class TestServicesBuilder
    {
        private readonly IObjectContainer _objectContainer;

        public TestServicesBuilder(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            Build();
        }
        
        public void Build()
        {
            _objectContainer.RegisterTypeAs<LocatorProvider, ILocatorProvider>();
        }
    }
}