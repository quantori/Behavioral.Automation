using Behavioral.Automation.API.Models;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.API;

[Binding]
public class StepArgumentTransformations
{
    [StepArgumentTransformation]
    public AssertionType ParseBehavior(string verb)
    {
        switch (verb)
        {
            case "be":
                return AssertionType.Be;
            case "become":
                return AssertionType.Become;
            default:
                throw new ArgumentException($"Unknown behaviour verb: {verb}");
        }
    }
}