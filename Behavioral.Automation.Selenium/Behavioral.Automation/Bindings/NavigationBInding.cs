using System;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for URL navigation and testing
    /// </summary>
    [Binding]
    public sealed class NavigationBinding
    {
        private readonly IDriverService _driverService;
        private readonly IScopeContextManager _scopeContextManager;
        private readonly ITestRunner _testRunner;

        public NavigationBinding([NotNull] IDriverService driverService,
            [NotNull] IScopeContextManager scopeContextManager, 
            [NotNull] ITestRunner testRunner)
        {
            _driverService = driverService;
            _scopeContextManager = scopeContextManager;
            _testRunner = testRunner;
        }

        /// <summary>
        /// Open URL
        /// </summary>
        /// <param name="url">URL to open</param>
        /// <example>Given URL "http://test" is opened</example>
        [Given("URL \"(.*)\" is opened")]
        [When("user opens URL \"(.*)\"")]
        public void Navigate([NotNull] string url)
        {
            if(IsAbsoluteUrl(url))
            {
                _driverService.Navigate(url);
                return;
            }
            _driverService.Navigate(ConfigServiceBase.BaseUrl + url);
        }

        /// <summary>
        /// Open Base URL from config
        /// </summary>
        /// <example>When user opens application URL</example>
        [Given("application URL is opened")]
        [When("user opens application URL")]
        public void NavigateToBaseUrl()
        {
            _driverService.Navigate(ConfigServiceBase.BaseUrl);
        }
        
        /// <summary>
        /// Open URL which is relative to the Base URL
        /// </summary>
        /// <param name="url">Relative URL to be opened</param>
        /// <example>When user opens relative URL "/test-url"</example>
        [Given("relative URL \"(.*)\" is opened")]
        [When("user opens relative URL \"(.*)\"")]
        public void NavigateToRelativeUrl([NotNull] string url)
        {
            _driverService.NavigateToRelativeUrl(url);
        }

        /// <summary>
        /// Check full page URL
        /// </summary>
        /// <param name="url">Expected URL</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <example>Then page "http://test" should become opened</example>
        [Given("page \"(.*)\" (is|is not|become|become not) opened")]
        [Then("page \"(.*)\" should (be|be not|become|become not) opened")]
        public void CheckUrl([NotNull] string url, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => _driverService.CurrentUrl, url, behavior,
                $"current URL is {_driverService.CurrentUrl}");
        }

        /// <summary>
        /// Check URL which is relative to Base URL
        /// </summary>
        /// <param name="url">Expected URL</param>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <example>Then relative URL should become "/test-page"</example>
        [Given("relative URL (is|is not|become|become not) \"(.*)\"")]
        [Then("relative URL should (be|be not|become|become not) \"(.*)\"")]
        public void CheckRelativeUrl([NotNull] AssertionBehavior behavior, [NotNull] string url)
        {
            Assert.ShouldBecome(() => new Uri(_driverService.CurrentUrl).PathAndQuery, url, behavior,
                $"relative URL is {new Uri(_driverService.CurrentUrl).PathAndQuery}");
        }

        /// <summary>
        /// Check that page URL contains specific string
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="url">Expected URL substring</param>
        /// <example>Then page should contain "test" URL</example>
        [Given("page (contains|not contains) \"(.*)\" URL")]
        [Then("page (should|should not) contain \"(.*)\" URL")]
        public void CheckUrlContains(string behavior, [NotNull] string url)
        {
            Assert.ShouldBecome(() => _driverService.CurrentUrl.Contains(url), !behavior.Contains("not"), $"current URL is {_driverService.CurrentUrl}");
        }

        /// <summary>
        /// Check title of the page
        /// </summary>
        /// <param name="behavior">Assertion behavior (instant or continuous)</param>
        /// <param name="title">Expected title of the page</param>
        /// <example>Then page title should be "Test page"</example>
        [Given("page title (is|is not|become|become not) \"(.*)\"")]
        [Then("page title should (be|be not|become|become not) \"(.*)\"")]
        public void CheckPageTitle([NotNull] AssertionBehavior behavior, [CanBeNull] string title)
        {
            Assert.ShouldBecome(() => _driverService.Title, title, behavior,
                $"page title is {_driverService.Title}");
        }

        /// <summary>
        /// Resize opened browser window
        /// </summary>
        /// <param name="pageHeight">Desired window height</param>
        /// <param name="pageWidth">Desired window width</param>
        /// <example>When user resize window to 480 height and 640 width</example>
        [Given("user resized window to (.*) height and (.*) width")]
        [When("user resizes window to (.*) height and (.*) width")]
        public void CheckOpened([NotNull] int pageHeight, [NotNull] int pageWidth)
        {
            _driverService.ResizeWindow(pageHeight, pageWidth);
        }

        /// <summary>
        /// Refresh the page
        /// </summary>
        [Given("user reloaded current page")]
        [When("user reloads current page")]
        public void ReloadCurrentPage()
        {
            _driverService.Refresh();
        }
        
        private static bool IsAbsoluteUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        [Given(@"user navigated to (.*) by (tab click|URL)")]
        [When(@"user navigates to (.*) by (tab click|URL)")]
        public void NavigatesToTabWithDiferentOption([NotNull] string tabName, [NotNull] bool byURL)
        {
            if (byURL)
            {
                string relativeURL = "/";
                if (tabName == "Forecast")
                {
                    relativeURL += "fetchdata";
                }
                else relativeURL += $"{tabName.ToLower()}";

                _driverService.NavigateToRelativeUrl(relativeURL);
            }
            else
            {
                _testRunner.When($"user clicks on \"{tabName}\" link");
            }
        }

        [StepArgumentTransformation("(tab click|URL)")]
        public bool SetNavigateType([NotNull] string byURL)
        {
            return byURL == "URL";
        }

    }
}