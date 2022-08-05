﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Behavioral.Automation.Configs;
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
            Assert.SetRunner(_runner);
            _objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
            _servicesBuilder.Build();
            _browserRunner.OpenChrome();
        }
    }
}
