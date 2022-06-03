using Behavioral.Automation.FluentAssertions.Abstractions;

namespace Behavioral.Automation.FluentAssertions
{
    public interface IAssertionBuilderWithValidatedAssertion
    {
        public IAssertionBuilder And { get; }
    }
}