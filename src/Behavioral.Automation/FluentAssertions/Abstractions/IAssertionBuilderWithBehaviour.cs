using Behavioral.Automation.Elements;
using System;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionBuilderWithBehaviour
    {
        IAssertionBuilderWithValidatedAssertion Assert<T>(AssertionObject<T> assertion);
        IAssertionBuilderWithValidatedAssertion Assert<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message);
        IAssertionBuilder And { get; }
    }
}
