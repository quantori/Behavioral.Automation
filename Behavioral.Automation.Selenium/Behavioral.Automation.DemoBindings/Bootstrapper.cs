using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using BoDi;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.DemoBindings
{
    [Binding]
    public class Bootstrapper
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ITestRunner _runner;
        private readonly DemoTestServicesBuilder _servicesBuilder;
        private readonly BrowserRunner _browserRunner;
        private static Process _coreRunProcess;
        
        public Bootstrapper(IObjectContainer objectContainer, ITestRunner runner, BrowserRunner browserRunner)
        {
            _objectContainer = objectContainer;
            _runner = runner;
            _browserRunner = browserRunner;
            _servicesBuilder = new DemoTestServicesBuilder(objectContainer, new TestServicesBuilder(_objectContainer));
        }

        private static bool IsConnectionEstablished()
        {
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri(ConfigServiceBase.BaseUrl)})
                {
                    while (!client.Send(new HttpRequestMessage(HttpMethod.Get, client.BaseAddress)).IsSuccessStatusCode)
                    {
                        Thread.Sleep(500);
                    }
                    client.Dispose();
                }

                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        private static void RunTestApp()
        {
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "src",
                "BlazorApp");
            if (!Directory.Exists(testAppPath))
                throw new DirectoryNotFoundException($"Directory with Blazor.App does not exist in {testAppPath}");

            _coreRunProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c dotnet run",
                    WorkingDirectory = testAppPath,
                    CreateNoWindow = false,
                    UseShellExecute = true
                }
            };
            _coreRunProcess.Start();                 
        }

        [BeforeTestRun]
        public static void StartDemoApp()
        {
            RunTestApp();
            while (!IsConnectionEstablished())
            {
                Thread.Sleep(1000);
            }
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
            _browserRunner.Driver.Dispose();
        }

        [BeforeScenario(Order = 0)]
        public void Bootstrap()
        {
            Assert.SetRunner(_runner);
            _objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
            _servicesBuilder.Build();
            _browserRunner.OpenChrome();
        }
    }
}
