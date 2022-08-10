using Behavioral.Automation.DemoBindings.Elements;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.DemoBindings.Bindings
{
    [Binding]
    public class ElementTransformations
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

        [StepArgumentTransformation("(.*)")]
        public ITextElementWrapper FindTextElement([NotNull] IWebElementWrapper element)
        {
            return new TextElementWrapper(element, element.Caption, _driverService);
        }

        [StepArgumentTransformation("(.*)")]
        public IDropdownWrapper FindDropdownElement([NotNull] IWebElementWrapper element)
        {
            return new DropdownWrapper(element, element.Caption, _driverService);
        }

        [StepArgumentTransformation("(.*)")]
        public IElementCollectionWrapper FindCollectionElement([NotNull] IWebElementWrapper element)
        {
            return new ElementCollectionWrapper(element, element.Caption, _driverService);
        }

        [StepArgumentTransformation("(.*)")]
        public ITableWrapper FindTableElement([NotNull] IWebElementWrapper element)
        {
            return new TableWrapper(element, element.Caption, _driverService);
        }
        
        [StepArgumentTransformation("(.*)")]
        public IListWrapper FindListElement([NotNull] IWebElementWrapper element)
        {
            return new ListWrapper(element, element.Caption, _driverService);
        }
    }
}