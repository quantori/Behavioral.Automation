using System;
using System.Threading;
using Behavioral.Automation.Abstractions;
using Behavioral.Automation.FluentAssertions.Abstractions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.FluentAssertions
{
    public static class Assert
    {
        private static readonly int? AssertAttempts = ConfigServiceBase.AssertAttempts;

        private static ITestRunnerWrapper _runner;

        public static void SetRunner(ITestRunner runner)
        {
            _runner = new TestRunnerWrapper(runner);
        }

        public static void SetRunner(ITestRunnerWrapper runner)
        {
            _runner = runner;
        }

        public static T ShouldGet<T>(Func<T> predicate)
        {
            return TryGetValue(predicate, TimeSpan.FromMilliseconds(500));
        }

        public static void ShouldBecome<T>(this Func<T> predicate, T value, AssertionBehavior behavior, string message)
        {
            if (behavior.Type == AssertionType.Immediate)
            {
                var actualValue = TryGetValue(predicate, TimeSpan.FromMilliseconds(500));
                True(actualValue.Equals(value) == !behavior.Inversion, message);
            }
            else
            {
                ShouldBecome(predicate, value, message, !behavior.Inversion);
            }
        }

        public static void ShouldBecome<T>(this Func<T> predicate, T value, AssertionBehavior behavior, Func<string> message)
        {
            if (behavior.Type == AssertionType.Immediate)
            {
                var actualValue = TryGetValue(predicate, TimeSpan.FromMilliseconds(500));
                True(actualValue.Equals(value) == !behavior.Inversion, message());
            }
            else
            {
                ShouldBecome(predicate, value, message, !behavior.Inversion);
            }
        }

        public static void ShouldBecome<T>(Func<T> predicate, T value, string message, bool direction = true, int? attempts = null)
        {
            attempts ??= AssertAttempts;

            for (int index = 0; index < attempts; index++)
            {
                if (TryGetValue(predicate, TimeSpan.FromMilliseconds(500)).Equals(value) == direction)
                {
                    return;
                }
                Thread.Sleep(500);
            }

            var actual = TryGetValue(predicate, TimeSpan.FromMilliseconds(500));
            message ??= $"actual value is {actual}";

            True(actual.Equals(value) == direction, message);
        }

        public static void ShouldBecome<T>(Func<T> predicate, T value, Func<string> message, bool direction = true, int? attempts = null)
        {
            
            attempts ??= AssertAttempts;

            for (int index = 0; index < attempts; index++)
            {
                if (TryGetValue(predicate, TimeSpan.FromMilliseconds(500)).Equals(value) == direction)
                {
                    return;
                }
                Thread.Sleep(500);
            }

            var actual = TryGetValue(predicate, TimeSpan.FromMilliseconds(500));
            var errorMessage = message();
            errorMessage ??= $"actual value is {actual}";

            True(actual.Equals(value) == direction, errorMessage);
        }

        public static void ShouldBe(IAssertionAccessor assertion, string caption, int? attempts = null)
        {
            attempts ??= AssertAttempts;

            bool isValid = WaitForAssertion(assertion, TimeSpan.FromMilliseconds(500));
            if (assertion.Type == AssertionType.Continuous && (!isValid || !assertion.InterruptValidationOnSuccess))
            {
                for (int i = 0; i < attempts - 1; i++)
                {
                    Thread.Sleep(500);
                    isValid = WaitForAssertion(assertion, TimeSpan.FromMilliseconds(500));
                    if (isValid && assertion.InterruptValidationOnSuccess) break;
                }
            }

            True(isValid, assertion.Message ?? $"{caption} actual value is {assertion.ActualValue}");
        }

        private static bool WaitForAssertion(IAssertionAccessor assertion, TimeSpan timeout, int attempts = 10)
        {
            return assertion.Validate(attempts, timeout);
        }

        public static void True(bool condition, string message)
        {
            if (!condition)
            {
                TestContext.WriteLine($"\nActual:\n{message}");
                NUnit.Framework.Assert.Fail($"Test execution failed: \n{message}");
            }
        }

        public static string BehaviorAppendix(this AssertionBehavior behavior)
        {
            if (!behavior.Inversion)
            {
                return " not";
            }

            return string.Empty;
        }

        private static T TryGetValue<T>(Func<T> getValue, TimeSpan wait, int attempts = 10)
        {
            var counter = 0;
            while (true)
            {
                try
                {
                    return getValue();
                }
                catch (Exception e) when (e is StaleElementReferenceException || e is NoSuchElementException)
                {
                    Thread.Sleep(wait);
                    if (counter++ == attempts)
                        throw;
                }
                catch (NullReferenceException)
                {
                    Thread.Sleep(wait);
                    if (counter++ == attempts)
                        return getValue();
                }
            }
        }
    }
}