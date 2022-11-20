﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Context;
using BoDi;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright;

[Binding]
public class Hooks
{
    private readonly WebContext _webContext;
    private static IPlaywright? _playwright;
    private static IBrowser? _browser;
    private readonly ScenarioContext _scenarioContext;
    private static readonly float? SlowMoMilliseconds = ConfigManager.GetConfig<Config>().SlowMoMilliseconds;
    private static readonly bool? Headless = ConfigManager.GetConfig<Config>().Headless;
    private static readonly bool RecordVideo = ConfigManager.GetConfig<Config>().RecordVideo;

    public Hooks(WebContext webContext, ScenarioContext scenarioContext)
    {
        _webContext = webContext;
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static async Task InitBrowser()
    {
        var exitCode = Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }

        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        if (_playwright is null) throw new Exception("Failed to initialize playwright.");
        _browser = await InitBrowserAsync();
        if (_browser is null) throw new Exception("Failed to initialize browser.");
    }

    [BeforeScenario]
    public async Task CreateContextAsync()
    {
        if (_browser is null)
        {
            throw new NullReferenceException($"Playwright browser is not initialized.");
        }

        _webContext.Context = RecordVideo
            ? await _browser.NewContextAsync(new BrowserNewContextOptions { RecordVideoDir = "videos/" })
            : await _browser.NewContextAsync();

        _webContext.Page = await _webContext.Context.NewPageAsync();
    }

    [AfterScenario]
    public async Task CloseContextAsync()
    {
        await _webContext.Context.CloseAsync();
    }

    [AfterTestRun]
    public static void CloseBrowser()
    {
        _browser!.CloseAsync();
        _playwright!.Dispose();
    }

    [AfterScenario(Order = 0)]
    public async Task MakeScreenshot()
    {
        if (_scenarioContext.TestError != null)
        {
            var path = new string(TestContext.CurrentContext.Test.Name
                .Where(x => !Path.GetInvalidFileNameChars().Contains(x))
                .ToArray()) + ".png";
            await _webContext.Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path
            });

            TestContext.AddTestAttachment(path);
        }
    }

    //TODO Implement configuration
    private static async Task<IBrowser?> InitBrowserAsync()
    {
        if (_playwright is null) throw new NullReferenceException($"Playwright is not initialized.");
        return await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = Headless,
            SlowMo = SlowMoMilliseconds,
        });
    }
}