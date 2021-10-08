using Behavioral.Automation.Elements;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.Model;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionContext {
        AssertionType Type { get; set; }
        void SetElement(IWebElementWrapper element);
    }
}
