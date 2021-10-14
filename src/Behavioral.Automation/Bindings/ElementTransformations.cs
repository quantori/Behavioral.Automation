using System;
using Behavioral.Automation.Elements.Implementations;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public class ElementTransformations
    {
        private readonly IDriverService _driverService;
        private readonly IElementSelectionService _selectionService;
        private readonly IVirtualizedElementsSelectionService _virtualizedElementsSelectionService;

        public ElementTransformations(
            [NotNull] IDriverService driverService, 
            [NotNull] IElementSelectionService selectionService,
            [NotNull] IVirtualizedElementsSelectionService virtualizedElementsSelectionService)
        {
            _driverService = driverService;
            _selectionService = selectionService;
            _virtualizedElementsSelectionService = virtualizedElementsSelectionService;
        }

        [StepArgumentTransformation]
        public IWebElementWrapper FindElement([NotNull] string caption)
        {
            return new WebElementWrapper(() => _selectionService.Find(caption), caption, _driverService);
        }

        [StepArgumentTransformation("(.*)")]
        public ITextElementWrapper FindTextElement([NotNull] IWebElementWrapper element)
        {
            return new TextElementWrapper(element, element.Caption, _driverService);
        }

        [StepArgumentTransformation("(.*)")]
        public IElementCollectionWrapper FindElementCollection([NotNull] IWebElementWrapper element)
        {
            return new ElementCollectionWrapper(element, element.Caption, _driverService, _virtualizedElementsSelectionService);
        }

        [StepArgumentTransformation, NotNull]
        public AssertionBehavior ParseBehavior(string verb)
        {
            switch (verb)
            {
                case "be":
                case "is":
                case "have":
                    return new AssertionBehavior(AssertionType.Immediate, false);
                case "become":
                    return new AssertionBehavior(AssertionType.Continuous, false);
                case "be not":
                case "is not":
                case "not have":
                    return new AssertionBehavior(AssertionType.Immediate, true);
                case "become not":
                    return new AssertionBehavior(AssertionType.Continuous, true);
                default:
                    throw new ArgumentException($"unknown behaviour verb: {verb}");
            }
        }

        [StepArgumentTransformation, NotNull]
        public int ParseNumber(string number)
        {
            return number switch
            {
                ("first") => 1,
                ("second") => 2,
                ("third") => 3,
                ("fourth") => 4,
                ("fifth") => 5,
                ("sixth") => 6,
                ("seventh") => 7,
                ("eighth") => 8,
                ("ninth") => 9,
                ("tenth") => 10,
                _ => StringExtensions.ParseNumberFromString(number)
            };
        }

        [StepArgumentTransformation]
        public ControlScopeSelector ParseControlScopeSelector(string selectionSteps)
        {
            return new ControlScopeSelector(selectionSteps);
        }
    }
}
