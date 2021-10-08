using Behavioral.Automation.Elements;
using Behavioral.Automation.Model;
using System;
using Behavioral.Automation.Elements.Interfaces;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionBuilder
    {
        IAssertionBuilderWithInversion Not { get; }
        IAssertionBuilderWithBehaviour With(AssertionBehavior behaviour);
        IAssertionBuilderWithValidatedAssertion Be<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message);
        IAssertionBuilderWithValidatedAssertion Be<TValue>(AssertionObject<TValue> assertion);
        IAssertionBuilderWithValidatedAssertion Become<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message);
        IAssertionBuilderWithValidatedAssertion Become<TValue>(AssertionObject<TValue> assertion);
        IAssertionBuilderWithValidatedAssertion BecomeNot<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message);
        IAssertionBuilderWithValidatedAssertion BecomeNot<TValue>(AssertionObject<TValue> assertion);
    }
}
