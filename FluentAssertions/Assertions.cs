using Behavioral.Automation.Services;
using System;
using System.Collections.Generic;
using System.Text;
using PredicateFactory = System.Func<Behavioral.Automation.Elements.IWebElementWrapper, System.Func<bool>>;

namespace Behavioral.Automation.FluentAssertions
{
    public static class Assertions
    {
        public static readonly AssertionObject<bool> Visible = new AssertionObject<bool>(
            element => element.Displayed,
            (b1, b2) => b1.Equals(b2),
            true,
            "is not visible");

        public static readonly AssertionObject<bool> Enabled = new AssertionObject<bool>(
            element => element.Enabled,
            (b1, b2) => b1.Equals(b2),
            true,
            "is disabled");

        public static AssertionObject<string> TextEqualsTo(string text) =>
            new AssertionObject<string>(
                StringExtensions.GetElementTextOrValue,
                string.Equals,
                text,
                null);

        public static AssertionObject<string> TextContains(string text) =>
            new AssertionObject<string>(
                StringExtensions.GetElementTextOrValue,
                string.Equals,
                text,
                null
            );
    }
}
