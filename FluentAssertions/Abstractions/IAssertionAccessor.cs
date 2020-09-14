using Behavioral.Automation.Model;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionAccessor
    {
        bool Validate();
        string Message { get; }
        string ActualValue { get; }
        AssertionType Type { get; }
    }
}
