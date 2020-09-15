using Behavioral.Automation.Elements;
using System;

namespace Behavioral.Automation.FluentAssertions.Abstractions
{
    public interface IAssertionBuilderWithBehaviour
    {
        IAssertionBuilderWithBehaviour Assert<T>(AssertionObject<T> assertion);
        IAssertionBuilderWithBehaviour Assert<TVal>(Func<IWebElementWrapper, TVal> valueAcessor, Func<TVal, TVal, bool> comparer, TVal value, string message);
        IAssertionBuilder And { get; }
    }
}
