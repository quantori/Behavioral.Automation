using Behavioral.Automation.DemoBindings.Elements;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.DemoBindings.Bindings
{
    [Binding]
    class ElementTransformations
    {
        private readonly IDriverService _driverService;
        private readonly IVirtualizedElementsSelectionService _virtualizedElementsSelectionService;

        public ElementTransformations(
            [NotNull] IDriverService driverService,
            [NotNull] IVirtualizedElementsSelectionService virtualizedElementsSelectionService)
        {
            _driverService = driverService;
            _virtualizedElementsSelectionService = virtualizedElementsSelectionService;
        }

        [StepArgumentTransformation("(.*)")]
        public IDropdownWrapper FindDropdown([NotNull] IWebElementWrapper element)
        {
            return new DropdownWrapper(element, element.Caption, _driverService);
        }

        [StepArgumentTransformation("(.*)")]
        public ITableWrapper FindTable([NotNull] IWebElementWrapper element)
        {
            return new TableWrapper(element, element.Caption, _driverService, _virtualizedElementsSelectionService);
        }
    }
}
