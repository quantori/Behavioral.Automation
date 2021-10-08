using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.FluentAssertions.Abstractions;
using System.Text;
using Behavioral.Automation.Elements.Interfaces;

namespace Behavioral.Automation.Elements
{
    public static class ElementExtensions
    {
        public static IAssertionBuilder Should(this IWebElementWrapper elementWrapper)
        {
            return new AssertionBuilder(elementWrapper);
        }
    }
}
