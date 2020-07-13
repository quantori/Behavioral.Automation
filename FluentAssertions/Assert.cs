using System;
using System.Linq;
using System.Threading;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.FluentAssertions
{
    public static class Assert
    {
        private const int Attempts = 30;

        private static ITestRunner _runner;
        private static IScenarioExecutionConsumer _consumer;

        public static void SetRunner(ITestRunner runner)
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
            var executedSteps = _consumer.Get().Aggregate((x, y) => $"{x}\n{y}");
            return $"{executedSteps}\n\nExpected:\n{_runner.ScenarioContext.StepContext.StepInfo.Text}\nActual:\n{message}";
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
