using Behavioral.Automation.Template.Bindings.Elements;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Template.Bindings
{
    [Binding]
    class ElementTransformations
    {
        private readonly IDriverService _driverService;
        private readonly IElementSelectionService _selectionService;

        public ElementTransformations(
            [NotNull] IDriverService driverService,
            [NotNull] IElementSelectionService selectionService)
        {
            _driverService = driverService;
            _selectionService = selectionService;
        }

        [StepArgumentTransformation]
        public IWebElementWrapper FindElement([NotNull] string caption)
        {
            return new WebElementWrapper(() => _selectionService.Find(caption),
                caption,
                _driverService);
        }
    }
}
