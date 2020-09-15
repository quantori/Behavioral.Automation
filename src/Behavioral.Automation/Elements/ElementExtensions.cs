using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.FluentAssertions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Behavioral.Automation.Elements
{
    public static class ElementExtensions
    {
        public static IAssertionBuilder Should(this IWebElementWrapper elementWrapper)
        {
            return new AssertionBuilder(elementWrapper);
        }
    }
}
