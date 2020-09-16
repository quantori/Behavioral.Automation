using Behavioral.Automation.Model;
using System;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionAccessor
    {
        bool Validate(int attempts, TimeSpan timeout);
        string Message { get; }
        string ActualValue { get; }
        AssertionType Type { get; }
        bool InterruptValidationOnSuccess { get; }
    }
}
