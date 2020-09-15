using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using NAssert = NUnit.Framework.Assert;
using BAssert = Behavioral.Automation.FluentAssertions.Assert;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using Behavioral.Automation.Model;
using Behavioral.Automation.FluentAssertions.Abstractions;
using Behavioral.Automation.Abstractions;

namespace Behavioral.Automation.UnitTests
{

    public class AssertionBuilderTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
        }

        [Test]
        public void When_BeCalledOnInstantlyGoodElement_NoExceptionIsThrown()
        {
            bool displayPropChecked = false;

            AssertionBuilder builder = CreateBuilder(
                e => e.Displayed,
                true,
                () =>
                {
                    if (!displayPropChecked)
                    {
                        displayPropChecked = true;
                    }
                    else
                    {
                        NAssert.Fail("Display property checked twice");
                    }
                }
                );

            NAssert.DoesNotThrow(() => builder.Be(Assertions.Visible));
            NAssert.IsTrue(displayPropChecked, "Display property was not requested by assertion engine");
        }

        [Test]
        public void When_BeCalledOnBadElement_ExceptionIsThrown()
        {
            AssertionBuilder builder = CreateBuilder(
                e => e.Displayed,
                false,
                null
                );

            //NAssert.Throws<AssertionException>(() => builder.Be(Assertions.Visible));
            NAssert.Throws<NullReferenceException>(() => builder.Be(Assertions.Visible));

        }

        [TestCase(0, false)]
        [TestCase(5, false)]
        [TestCase(31, true)]
        public void When_BecomeCalled_AssertionActsAsExpected(int attemptsUntilOk, bool throws)
        {
            AssertionBuilder builder = CreateBuilderWithCounter(attemptsUntilOk);

            AssertDelegateBehavior<NullReferenceException>(() => builder.Become(Assertions.Visible), throws);
        }

        [TestCase(1, false)]
        [TestCase(0, true)]
        public void When_NotCalled_AndThenBeCalled_CorrectAssertionResultIsReceived(int attemptsUntilOk, bool throws)
        {
            AssertionBuilder builder = CreateBuilderWithCounter(attemptsUntilOk);
            TestDelegate assertDelegate = () => builder.Not.Be(Assertions.Visible);
            AssertDelegateBehavior<NullReferenceException>(assertDelegate, throws);
        }

        [TestCase(30, "become", false, 30)]
        [TestCase(0, "become", true, 30)]
        [TestCase(0, "be", true, 1)]
        [TestCase(1, "be", false, 1)]
        public void When_NotCalled_AndThenBecomeCalled_CorrectAssertionResultIsReceived(int attemptsUntilOk, string action, bool throws, int expectedRequestsCount)
        {

            (AssertionBuilder builder, SharedCounter counter) buidlerWithCounter = CreateBuilderWithSharedCounter(attemptsUntilOk);
            TestDelegate assertDelegate = () =>
            {
                var x = buidlerWithCounter.builder.Not;
                switch (action.ToLower())
                {
                    case "become":
                        x.Become(Assertions.Visible);
                        break;

                    case "be":
                        x.Be(Assertions.Visible);
                        break;

                    default:
                        throw new ArgumentException("Incorrect action provided.");
                }
            };

            AssertDelegateBehavior<NullReferenceException>(assertDelegate, throws);

            NAssert.AreEqual(expectedRequestsCount, buidlerWithCounter.counter.Counter);
        }

        [TestCase(0, "immediate", false, false)]
        [TestCase(20, "immediate", false, true)]
        [TestCase(0, "immediate", true, true)]
        [TestCase(1, "immediate", true, false)]
        [TestCase(21, "continuous", false, false)]
        [TestCase(31, "continuous", false, true)]
        public void When_ImmediateAssertCalledWithPredifinedState_CorrectResult(int attemptsUntilOk, string behaviorString, bool invert, bool throws)
        {
            AssertionBuilder builder = CreateBuilderWithCounter(attemptsUntilOk);
            var behaviorSet = behaviorString.Equals("immediate", StringComparison.OrdinalIgnoreCase) ? AssertionBehavior.Immediate : AssertionBehavior.Continuous;

            AssertionBehavior behavior = invert ? behaviorSet.Inverted : behaviorSet.Direct;
            TestDelegate assertDelegate = () => builder.With(behavior).Assert(Assertions.Visible);

            AssertDelegateBehavior<NullReferenceException>(assertDelegate, throws);
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(30, false)]
        public void When_BecomeNotCalled_AssertionActsAsExpected(int attemptsUntilOk, bool throws)
        {
            AssertionBuilder builder = CreateBuilderWithCounter(attemptsUntilOk);

            TestDelegate assertionDelegate = () => builder.BecomeNot(Assertions.Visible);

            AssertDelegateBehavior<NullReferenceException>(assertionDelegate, throws);
        }
        private void AssertDelegateBehavior<TExpectedException>(TestDelegate testDelegate, bool throws) where TExpectedException : Exception
        {
            if (throws)
            {
                NAssert.Throws<TExpectedException>(testDelegate);
            }
            else
            {
                NAssert.DoesNotThrow(testDelegate);
            }
        }

        private AssertionBuilder CreateBuilderWithCounter(int attemptsUntilOk)
        {
            int counter = 0;
            AssertionBuilder builder = CreateBuilder(
                e => e.Displayed,
                () => counter++ >= attemptsUntilOk
                );

            return builder;
        }

        private (AssertionBuilder builder, SharedCounter counter) CreateBuilderWithSharedCounter(int attemptsUntilOk)
        {
            SharedCounter counter = new SharedCounter();
            AssertionBuilder builder = CreateBuilder(
                e => e.Displayed,
                () => counter.Increment() >= attemptsUntilOk
                );
            return (builder, counter);
        }

        private AssertionBuilder CreateBuilder<TResult>(Expression<Func<IWebElementWrapper, TResult>> expression, TResult result, Action callback = null)
        {
            Mock<IWebElementWrapper> wrapperMock = new Mock<IWebElementWrapper>();

            ISetup<IWebElementWrapper, TResult> expressionSetup = wrapperMock.Setup(expression);
            if (callback != null)
            {
                expressionSetup.Callback(callback);
            }
            expressionSetup.Returns(result);
            return new AssertionBuilder(wrapperMock.Object);
        }

        private AssertionBuilder CreateBuilder<TResult>(Expression<Func<IWebElementWrapper, TResult>> expression, Func<TResult> resultFn, Action callback = null)
        {
            Mock<IWebElementWrapper> wrapperMock = new Mock<IWebElementWrapper>();

            ISetup<IWebElementWrapper, TResult> expressionSetup = wrapperMock.Setup(expression);
            if (callback != null)
            {
                expressionSetup.Callback(callback);
            }
            expressionSetup.Returns(resultFn);
            return new AssertionBuilder(wrapperMock.Object);
        }

        private class SharedCounter
        {
            public int Counter
            {
                private set;
                get;
            } = 0;
            public int Increment() => Counter++;

            public static implicit operator int(SharedCounter counter) => counter.Counter;
        }

    }
}