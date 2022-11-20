using Behavioral.Automation.API.Services;
using BoDi;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.API;

[Binding]
public class Hooks
{
    private readonly IObjectContainer _objectContainer;

    public Hooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario(Order = 0)]
    public void Bootstrap()
    {
        _objectContainer.RegisterTypeAs<HttpApiClient, IHttpApiClient>();
    }
    
}