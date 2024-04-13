using System.Diagnostics;
using System.Net;
using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Implementation.Selenium.Configs;
using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Implementation.Selenium.Bindings;

[Binding]
public class Hooks
{
    private readonly IObjectContainer _objectContainer;
    private readonly ITestRunner _runner;
    private readonly BrowserRunner _browserRunner;
    private static Process _coreRunProcess;
    private readonly WebContext _webContext;

    public Hooks(IObjectContainer objectContainer, ITestRunner runner, BrowserRunner browserRunner, WebContext webContext)
    {
        _objectContainer = objectContainer;
        _runner = runner;
        _browserRunner = browserRunner;
        _webContext = webContext;
    }

    private static bool IsConnectionEstablished()
    {
        try
        {
            WebRequest.CreateHttp(ConfigManager.GetConfig<Config>().BaseUrl).GetResponse();
            return true;
        }
        catch (WebException)
        {
            return false;
        }
    }

    private static void RunTestApp()
    {
        string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "src",
            "BlazorApp");

        _coreRunProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c dotnet run",
                WorkingDirectory = testAppPath
            }
        };
        _coreRunProcess.Start();
    }

    [BeforeTestRun]
    public static void StartDemoApp()
    {
        if (!IsConnectionEstablished())
            RunTestApp();
    }

    [AfterTestRun]
    public static void StopDemoApp()
    {
        if (_coreRunProcess != null)
        {
            _coreRunProcess.Kill(true);
            _coreRunProcess.Dispose();
        }
    }

    [AfterScenario]
    public void CloseBrowser()
    {
        _browserRunner.CloseBrowser();
    }

    [BeforeScenario(Order = 0)]
    public void Bootstrap()
    {
        //Assert.SetRunner(_runner);
        //_objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
        //_servicesBuilder.Build();

        _browserRunner.OpenChrome();
        _webContext.Page = new Page();
        ((Page) _webContext.Page).driver = _browserRunner.Driver;
    }
}