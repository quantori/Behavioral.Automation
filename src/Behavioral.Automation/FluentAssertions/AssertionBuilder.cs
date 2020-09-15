using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions.Abstractions;
using Behavioral.Automation.Model;
using System;
using System.Collections.Generic;
using GlobalAssert = Behavioral.Automation.FluentAssertions.Assert;

namespace Behavioral.Automation.FluentAssertions
{
    public class AssertionBuilder : IAssertionBuilder, IAssertionBuilderWithBehaviour, IAssertionBuilderWithInversion
    {
        private readonly IWebElementWrapper _elementWrapper;

        private BuilderContext _currentContext = new BuilderContext();

        private AssertionType? _definedAssertionType = null;

        List<IAssertionAccessor> _assertions = new List<IAssertionAccessor>();
        public AssertionBuilder(IWebElementWrapper elementWrapper)
        {
            _elementWrapper = elementWrapper;
        }

        public IAssertionBuilderWithInversion Not
        {
            get
            {
                _currentContext.Inversion = true;
                return this;
            }
        }

        public IAssertionBuilder And
        {
            get
            {
                ClearContext();
                return this;
            }
        }

        public IAssertionBuilder Be<TVal>(Func<IWebElementWrapper, TVal> valueAcessor, Func<TVal, TVal, bool> comparer, TVal value, string message) =>
            Be(new AssertionObject<TVal>(valueAcessor, comparer, value, message));

        public IAssertionBuilder Be<T>(AssertionObject<T> assertion)
        {
            Validate(assertion, AssertionType.Immediate);
            return this;
        }

        public IAssertionBuilder Become<TVal>(Func<IWebElementWrapper, TVal> valueAcessor, Func<TVal, TVal, bool> comparer, TVal value, string message) =>
            Become(new AssertionObject<TVal>(valueAcessor, comparer, value, message));

        public IAssertionBuilder Become<T>(AssertionObject<T> assertion)
        {
            if (_currentContext.Inversion.HasValue && _currentContext.Inversion.Value){
                assertion.InterruptOnTrue = false;
            }
            Validate(assertion, AssertionType.Continuous);
            return this;
        }

        public IAssertionBuilder BecomeNot<TVal>(Func<IWebElementWrapper, TVal> valueAcessor, Func<TVal, TVal, bool> comparer, TVal value, string message) =>
            BecomeNot(new AssertionObject<TVal>(valueAcessor, comparer, value, message));

        public IAssertionBuilder BecomeNot<TVal>(AssertionObject<TVal> assertion){
            _currentContext.Inversion = true;
            Validate(assertion, AssertionType.Continuous);
            return this;
        }

        private void Validate<TVal>(AssertionObject<TVal> assertion, AssertionType assertionType)
        {
            _definedAssertionType = assertionType;
            Validate(assertion);
        }

        private void Validate<TVal>(AssertionObject<TVal> assertion)
        {
            if (!_definedAssertionType.HasValue)
            {
                throw new InvalidOperationException("Could not validate assertion because assertion type is not defined");
            }
            ConfigureContext(assertion);
            GlobalAssert.ShouldBe(assertion, _elementWrapper.Caption);
            ClearContext();
        }

        public IAssertionBuilderWithBehaviour With(AssertionBehavior behaviour)
        {
            if (_definedAssertionType.HasValue)
            {
                throw new InvalidOperationException($"Cannot set behaviour because it is already defined");
            }

            _currentContext.Inversion = behaviour.Inversion;
            _definedAssertionType = behaviour.Type;
            return this;
        }

        public IAssertionBuilderWithBehaviour Assert<T>(AssertionObject<T> assertion)
        {
            Validate(assertion);
            return this;
        }
        public IAssertionBuilderWithBehaviour Assert<TVal>(Func<IWebElementWrapper, TVal> valueAcessor, Func<TVal, TVal, bool> comparer, TVal value, string message) =>
            Assert(new AssertionObject<TVal>(valueAcessor, comparer, value, message));

        private void ConfigureContext<T>(AssertionObject<T> assertion)
        {
            _currentContext.Inversion = _currentContext.Inversion ?? false;
            assertion.Type = _definedAssertionType.Value;
            assertion.SetElement(_elementWrapper);
            assertion.Inversion = _currentContext.Inversion.Value;
        }

        private void ClearContext()
        {
            _definedAssertionType = null;
            _currentContext = new BuilderContext();
        }

        private class BuilderContext
        {
            public bool? Inversion { get; set; } = null;
        }
    }
}
