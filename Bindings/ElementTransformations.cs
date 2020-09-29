using System;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Methods to transform steps' arguments
    /// </summary>
    [Binding]
    public class ElementTransformations
    {
        private readonly IDriverService _driverService;
        private readonly IElementSelectionService _selectionService;

        public ElementTransformations(
            [NotNull] IDriverService driverService, 
            [NotNull] IElementSelectionService selectionService)
        {
            _driverService = driverService;
            _selectionService = selectionService;
        }
        
        /// <summary>
        /// Convert strings "be/is/have/become/be not/is not/ not have/become not" into AssertionBehavior object
        /// </summary>
        /// <param name="verb">String with the verb which is received from Specflow steps</param>
        /// <returns></returns>
        [StepArgumentTransformation, NotNull]
        public AssertionBehavior ParseBehavior(string verb)
        {
            switch (verb)
            {
                case "be":
                case "is":
                case "have":
                    return new AssertionBehavior(AssertionType.Immediate, false);
                case "become":
                    return new AssertionBehavior(AssertionType.Continuous, false);
                case "be not":
                case "is not":
                case "not have":
                    return new AssertionBehavior(AssertionType.Immediate, true);
                case "become not":
                    return new AssertionBehavior(AssertionType.Continuous, true);
                default:
                    throw new ArgumentException($"unknown behaviour verb: {verb}");
            }
        }

        /// <summary>
        /// Convert strings into the numbers
        /// </summary>
        /// <param name="number">String with the number which is received from Specflow steps</param>
        /// <returns></returns>
        [StepArgumentTransformation, NotNull]
        public int ParseNumber(string number)
        {
            return number switch
            {
                ("first") => 1,
                ("second") => 2,
                ("third") => 3,
                ("fourth") => 4,
                ("fifth") => 5,
                ("sixth") => 6,
                ("seventh") => 7,
                ("eighth") => 8,
                ("ninth") => 9,
                ("tenth") => 10,
                _ => StringExtensions.ParseNumberFromString(number)
            };
        }
    }
}
