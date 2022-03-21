using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Services;
using BoDi;

namespace Behavioral.Automation.Playwright;

/// <summary>
/// Initialise all necessary objects before test execution with Selenium
/// </summary>
public sealed class PlaywrightTestServicesBuilder
{
    private readonly IObjectContainer _objectContainer;

    public PlaywrightTestServicesBuilder(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    public void Build()
    {
        _objectContainer.RegisterTypeAs<DriverService, IDriverService>();
        _objectContainer.RegisterTypeAs<ElementSelectionService, IElementSelectionService>();
        _objectContainer.RegisterTypeAs<VirtualizedElementsSelectionService, IVirtualizedElementsSelectionService>();
    }
}