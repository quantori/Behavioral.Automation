using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions.Abstractions;
using Behavioral.Automation.Model;
using System;
using System.Collections.Generic;
using Behavioral.Automation.Elements.Interfaces;
using GlobalAssert = Behavioral.Automation.FluentAssertions.Assert;
using NAssert = NUnit.Framework.Assert;

namespace Behavioral.Automation.FluentAssertions
{
    public class AssertionBuilder : IAssertionBuilder, IAssertionBuilderWithBehaviour, IAssertionBuilderWithInversion, IAssertionBuilderWithValidatedAssertion
    {
        private readonly IWebElementWrapper _elementWrapper;

        private BuilderContext _currentContext = new BuilderContext();

        private AssertionType? _definedAssertionType = null;

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

        public IAssertionBuilderWithValidatedAssertion Be<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message) =>
            Be(new AssertionObject<TValue>(valueAcessor, comparer, value, message));

        public IAssertionBuilderWithValidatedAssertion Be<T>(AssertionObject<T> assertion)
        {
            Validate(assertion, AssertionType.Immediate);
            return this;
        }

        public IAssertionBuilderWithValidatedAssertion Become<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message) =>
            Become(new AssertionObject<TValue>(valueAcessor, comparer, value, message));

        public IAssertionBuilderWithValidatedAssertion Become<T>(AssertionObject<T> assertion)
        {
            if (_currentContext.Inversion.HasValue && _currentContext.Inversion.Value){
                assertion.InterruptValidationOnSuccess = false;
            }
            Validate(assertion, AssertionType.Continuous);
            return this;
        }

        public IAssertionBuilderWithValidatedAssertion BecomeNot<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message) =>
            BecomeNot(new AssertionObject<TValue>(valueAcessor, comparer, value, message));

        public IAssertionBuilderWithValidatedAssertion BecomeNot<TValue>(AssertionObject<TValue> assertion){
            _currentContext.Inversion = true;
            Validate(assertion, AssertionType.Continuous);
            return this;
        }

        public IAssertionBuilderWithBehaviour With(AssertionBehavior behaviour)
        {
            if (_definedAssertionType.HasValue)
            {
                NAssert.Inconclusive($"Cannot set behaviour because it is already defined");
            }

            _currentContext.Inversion = behaviour.Inversion;
            _definedAssertionType = behaviour.Type;
            return this;
        }

        public IAssertionBuilderWithValidatedAssertion Assert<T>(AssertionObject<T> assertion)
        {
            Validate(assertion);
            return this;
        }
        public IAssertionBuilderWithValidatedAssertion Assert<TValue>(Func<IWebElementWrapper, TValue> valueAcessor, Func<TValue, TValue, bool> comparer, TValue value, string message) =>
            Assert(new AssertionObject<TValue>(valueAcessor, comparer, value, message));

        private void Validate<TValue>(AssertionObject<TValue> assertion, AssertionType assertionType)
        {
            _definedAssertionType = assertionType;
            Validate(assertion);
        }

        private void Validate<TValue>(AssertionObject<TValue> assertion)
        {
            if (!_definedAssertionType.HasValue)
            {
                NAssert.Inconclusive("Could not validate assertion because assertion type is not defined");
            }

            ConfigureContext(assertion);
            GlobalAssert.ShouldBe(assertion, _elementWrapper.Caption);
            ClearContext();
        }

        private void ConfigureContext<T>(AssertionObject<T> assertion)
        {
            _currentContext.Inversion ??= false;
            if (_definedAssertionType != null) 
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
