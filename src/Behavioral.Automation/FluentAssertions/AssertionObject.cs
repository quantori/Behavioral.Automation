using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions.Abstractions;
using Behavioral.Automation.Model;
using System;
using System.Threading;

namespace Behavioral.Automation.FluentAssertions
{
    public class AssertionObject<T> : IAssertionAccessor, IAssertionContext
    {
        private IWebElementWrapper _element;
        private Func<T, T, bool> _comparer;

        public AssertionObject(Func<IWebElementWrapper, T> valueAcessor, Func<T, T, bool> comparer, T value, string message)
        {
            PropertyAccessor = valueAcessor;
            Value = value;
            Message = message;
            _comparer = comparer;
        }

        public void SetElement(IWebElementWrapper element)
        {
            _element = element;
        }

        public bool Validate(int attempts, TimeSpan timeout)
        {
            T currentVal = default;
            try
            {
                currentVal = PropertyAccessor(_element);
            } catch
            {
                if (attempts-- > 0)
                    return false;
                Thread.Sleep(timeout);
            }

            return Inversion ? !_comparer(currentVal, Value)
                    : _comparer(currentVal, Value);
        }

        public Func<IWebElementWrapper, T> PropertyAccessor { get; }
        public T Value { get; }
        public string Message { get; }
        public bool Inversion { get; set; }
        public bool InterruptOnTrue { get; set; } = true;

        public string ActualValue => PropertyAccessor(_element).ToString();

        public AssertionType Type { get; set; }
    }
}
