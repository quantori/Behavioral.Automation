using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Behavioral.Automation.Abstractions;
using Behavioral.Automation.FluentAssertions.Abstractions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.FluentAssertions
{
    public static class Assert
    {
        private const int Attempts = 30;
        private static ITestRunnerWrapper _runner;
        private static IScenarioExecutionConsumer _consumer;

        public static void SetRunner(ITestRunner runner)
        {
            _runner = new TestRunnerWrapper(runner);
        }

        public static void SetRunner(ITestRunnerWrapper runner)
        {
            _runner = runner;
        }

        public static void SetConsumer(IScenarioExecutionConsumer consumer)
        {
            _consumer = consumer;
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

        public static void ShouldBecome<T>(Func<T> predicate, T value, string message, bool direction = true)
        {

            for (int index = 0; index < Attempts; index++)
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

        public static void ShouldBe(IAssertionAccessor assertion, string caption)
        {
            bool isValid = WaitForAssertion(assertion, TimeSpan.FromMilliseconds(500));
            if (assertion.Type == AssertionType.Continuous && (!isValid || !assertion.InterruptOnTrue))
            {
                for (int i = 0; i < Attempts - 1; i++)
                {
                    Thread.Sleep(500);
                    isValid = WaitForAssertion(assertion, TimeSpan.FromMilliseconds(500));
                    if (isValid && assertion.InterruptOnTrue) break;
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
                NUnit.Framework.Assert.Fail(BuildMessage(message));
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

        private static string BuildMessage(string message)
        {
            IEnumerable<string> executedSteps = _consumer.Get();
            string aggregatedSteps = "";
            if (executedSteps.Any())
            {
                aggregatedSteps = _consumer.Get().Aggregate((x, y) => $"{x}\n{y}");
            }
            return $"{aggregatedSteps}\n\nExpected:\n{_runner.ScenarioContext.StepContext.StepInfo.Text}\nActual:\n{message}";
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
                catch (StaleElementReferenceException)
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
