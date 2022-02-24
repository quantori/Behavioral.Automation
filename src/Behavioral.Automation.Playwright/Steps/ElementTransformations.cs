using Behavioral.Automation.Elements;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.Wrappers;
using JetBrains.Annotations;
using Behavioral.Automation.Playwright.Elements;
using TechTalk.SpecFlow;
using IWebElementWrapper = Behavioral.Automation.Playwright.Elements.IWebElementWrapper;

namespace Behavioral.Automation.Playwright.Steps
{
    [Binding]
    public class ElementTransformations
    {
        private readonly ElementSelectionService _selectionService;

        public ElementTransformations(ElementSelectionService selectionService)
        {
            _selectionService = selectionService;
        }

        [StepArgumentTransformation("(.+?)")]
        public IWebElementWrapper FindElement([NotNull] string caption)
        {
            return new WebElementWrapper(() => _selectionService.Find(caption), caption);
        }
        
        [StepArgumentTransformation("(.*)")]
        public ITextElementWrapper FindTextElement([NotNull] IWebElementWrapper element)
        {
            return new TextElementWrapper(element, element.Caption);
        }
    }
}