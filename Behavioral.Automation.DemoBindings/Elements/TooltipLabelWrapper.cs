using Behavioral.Automation.Elements.Implementations;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.Services;

namespace Behavioral.Automation.DemoBindings.Elements
{
    public class TooltipLabelWrapper: WebElementWrapper, ITooltipLabelWrapper
    {
        public TooltipLabelWrapper(IWebElementWrapper wrapper, string caption, IDriverService driverService) :
            base(() => wrapper.Element, caption, driverService)
        { }

        public string Tooltip => GetAttribute("data-test-tooltip-text");
    }
}