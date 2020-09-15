using Behavioral.Automation.Model;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionAccessor
    {
        bool Validate(int attempts, System.TimeSpan timeout);
        string Message { get; }
        string ActualValue { get; }
        AssertionType Type { get; }
        bool InterruptOnTrue { get; }
    }
}
