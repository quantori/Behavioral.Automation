﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Behavioral.Automation.Playwright.Services
{
    /// <summary>
    /// WebDriver interactions methods
    /// </summary>
    [UsedImplicitly]
    public sealed class DriverService : IDriverService
    {
        [NotNull] private readonly IScopeContextManager _scopeContextManager;
        private readonly IPage _page;

        public DriverService([NotNull] IScopeContextManager scopeContextManager, IPage page)
        {
            _page = page;
            _scopeContextManager = scopeContextManager;
        }

        private readonly string SearchAttribute = ConfigServiceBase.SearchAttribute;

        public string Title => _page.TitleAsync().Result;

        public string CurrentUrl => _page.Url;

        /// <summary>
        /// Find web element using search attribute
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>IElementHandle object or null if element was not found</returns>
        public IElementHandle FindElement(string id)
        {
            return _page.QuerySelectorAsync($"xpath=//*[@{SearchAttribute}='{id}']").Result;
        }

        /// <summary>
        /// Find element using search attribute and xpath
        /// </summary>
        /// <param name="id">Element id</param>
        /// <param name="subpath">Xpath subpath</param>
        /// <returns>IElementHandle object or null if element was not found</returns>
        public IElementHandle FindElement(string id, string subpath)
        {
            return FindElement(id).QuerySelectorAsync(subpath).Result;
        }

        /// <summary>
        /// Find element by xpath only
        /// </summary>
        /// <param name="path">Element's xpath</param>
        /// <returns>IElementHandle object or null if element was not found</returns>
        public IElementHandle FindElementByXpath(string path)
        {
            return _page.QuerySelectorAsync(path).Result;
        }

        /// <summary>
        /// Find element collection by id
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>ReadOnlyCollection of IElementHandle objects</returns>
        public IEnumerable<IElementHandle> FindElements(string id)
        {
            return _page.QuerySelectorAllAsync($"xpath=//*[@{SearchAttribute}='{id}']").Result;
        }

        /// <summary>
        /// Find element collection by xpath
        /// </summary>
        /// <param name="path">Element xpath</param>
        /// <returns>ReadOnlyCollection of IElementHandle objects</returns>
        public IEnumerable<IElementHandle> FindElementsByXpath(string path)
        {
            return _page.QuerySelectorAllAsync($"xpath=${path}").Result;
        }

        /// <summary>
        /// Execute JavaScript
        /// </summary>///
        /// <param name="script">Script text</param>
        /// <param name="args">Script arguments</param>
        /// <returns>ReadOnlyCollection of IElementHandle objects</returns>
        public object ExecuteScript(string script, params object[] args)
        {
            return _page.EvaluateAsync(script, args).Result;
        }

        /// <summary>
        /// Scroll page to element
        /// </summary>
        /// <param name="element">IElementHandle object</param>
        public void ScrollTo(IElementHandle element)
        {
            element.ScrollIntoViewIfNeededAsync();
        }

        /// <summary>
        /// Open relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        public void NavigateToRelativeUrl(string url)
        {
            var uri = GetUriFromRelativePath(url);
            _page.GotoAsync(uri.ToString());
            _scopeContextManager.SwitchPage(uri);
        }

        /// <summary>
        /// Switch to the last opened window
        /// </summary>
        public void SwitchToTheLastWindow()
        {
            _page.Context.Page += async  (_, page) => {
                await page.WaitForLoadStateAsync();
            };
        }

        /// <summary>
        /// Switch to the window which was opened first
        /// </summary>
        public void SwitchToTheFirstWindow()
        {
            _page.Context.Pages.First().WaitForLoadStateAsync();
        }

        /// <summary>
        /// Get URI from relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        /// <returns>URI</returns>
        private Uri GetUriFromRelativePath(string url)
        {
            return new Uri(new Uri(CurrentUrl), url);
        }

        /// <summary>
        /// Removes focus from active page element
        /// </summary>
        public void RemoveFocusFromActiveElement()
        {
            ExecuteScript("document.activeElement.blur()");
        }

        /// <summary>
        /// Make DOM tree dump into the console
        /// </summary>
        public void DebugDumpPage()
        {
            var page = _page.ContentAsync();
            Console.WriteLine(page);
        }

        /// <summary>
        /// Refresh currently opened page
        /// </summary>
        public void Refresh()
        {
            _page.ReloadAsync();
        }

        /// <summary>
        /// Open page by full URL
        /// </summary>
        /// <param name="url">URL</param>
        public void Navigate(string url)
        {
            _page.GotoAsync(url);
            var uri = new Uri(url);
            _scopeContextManager.SwitchPage(uri);
        }

        /// <summary>
        /// Change size of opened browser window
        /// </summary>
        /// <param name="height">Desired height</param>
        /// <param name="width">Desired width</param>
        public void ResizeWindow(int height, int width)
        {
            _page.SetViewportSizeAsync(width, height);
        }

        /// <summary>
        /// Remove focus from the element
        /// </summary>
        public void CloseActiveElement()
        {
            _page.ClickAsync($"xpath=//body");
        }

        /// <summary>
        /// Make .png screenshot with date and time
        /// </summary>
        /// <returns>Name of the screenshot file</returns>
        public string MakeScreenShot()
        {
            var fileName = new string(TestContext.CurrentContext.Test.Name
                .Where(x => !Path.GetInvalidFileNameChars().Contains(x))
                .ToArray()) + ".png";
            _page.ScreenshotAsync(new PageScreenshotOptions { Path = fileName });
            return fileName;
        }

        /// <summary>
        /// Scroll element to offset
        /// </summary>
        /// <param name="element">IELementHandle object</param>
        /// <param name="offsetX">Offset to scroll element to by X coords</param>
        /// <param name="offsetY">Offset to scroll element to by Y coords</param>
        public void ScrollElementTo(IElementHandle element, int offsetX, int offsetY = 0)
        {
            element.HoverAsync();
            _page.Mouse.WheelAsync(offsetX, offsetY);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Save browser log into file
        /// </summary>
        /// <returns></returns>
        public string SaveBrowserLog()
        {
            var fileName = new string(TestContext.CurrentContext.Test.Name
                .Where(x => !Path.GetInvalidFileNameChars().Contains(x))
                .ToArray()) + ".log";
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            _page.Console += (_, msg) => File.AppendAllText(fileName, msg.Text);

            return fileName;
        }
    }
}