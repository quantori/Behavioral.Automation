using Behavioral.Automation.Elements;
using System;
using Behavioral.Automation.Elements.Interfaces;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionBuilderWithInversion
    {
        IAssertionBuilderWithValidatedAssertion Be<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message);
        IAssertionBuilderWithValidatedAssertion Be<T>(AssertionObject<T> assertion);
        IAssertionBuilderWithValidatedAssertion Become<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message);
        IAssertionBuilderWithValidatedAssertion Become<T>(AssertionObject<T> assertion);
    }
}
