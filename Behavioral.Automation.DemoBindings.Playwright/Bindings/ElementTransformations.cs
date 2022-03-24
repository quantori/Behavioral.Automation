using Behavioral.Automation.DemoBindings.Playwright.Elements;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Playwright.Elements;
using Behavioral.Automation.Playwright.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.DemoBindings.Playwright.Bindings
{
    [Binding]
    public class ElementTransformations
    {
        private readonly IElementSelectionService _selectionService;

        public ElementTransformations(
            [NotNull] IDriverService driverService,
            [NotNull] IElementSelectionService selectionService)
        {
            _selectionService = selectionService;
        }

        [StepArgumentTransformation]
        public IWebElementWrapperPlaywright FindElementWrapperPlaywright([NotNull] string caption)
        {
            return new WebElementWrapper(() => _selectionService.Find(caption),
                caption);
        }
        
        [StepArgumentTransformation]
        public IWebElementWrapper FindElement([NotNull] string caption)
        {
            return new WebElementWrapper(() => _selectionService.Find(caption), caption);
        }
    }
}
