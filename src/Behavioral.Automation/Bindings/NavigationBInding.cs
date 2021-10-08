using System;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class NavigationBinding
    {
        private readonly IDriverService _driverService;

        public NavigationBinding([NotNull] IDriverService driverService)
        {
            _driverService = driverService;
        }

        [Given("URL \"(.*)\" is opened")]
        [When("user opens URL \"(.*)\"")]
        public void Navigate([NotNull] string url)
        {
            _driverService.Navigate(url);
        }

        [Given("application URL is opened")]
        [When("user opens application URL")]
        public void NavigateToBaseUrl()
        {
            _driverService.Navigate(ConfigServiceBase.BaseUrl);
        }

        [Given("relative URL \"(.*)\" is opened")]
        [When("user opens relative URL \"(.*)\"")]
        [Then("user opens relative URL \"(.*)\"")]
        public void NavigateToRelativeUrl([NotNull] string url)
        {
            _driverService.NavigateToRelativeUrl(url);
        }

        [Then("page \"(.*)\" should (be|be not|become|become not) opened")]
        public void CheckUrl([NotNull] string url, [NotNull] AssertionBehavior behavior)
        {
            Assert.ShouldBecome(() => _driverService.CurrentUrl, url, behavior,
                $"current URL is {_driverService.CurrentUrl}");
        }

        [Then("relative URL should (be|be not|become|become not) \"(.*)\"")]
        public void CheckRelativeUrl([NotNull] AssertionBehavior behavior, [NotNull] string url)
        {
            Assert.ShouldBecome(() => new Uri(_driverService.CurrentUrl).PathAndQuery, url, behavior,
                $"relative URL is {new Uri(_driverService.CurrentUrl).PathAndQuery}");
        }

        [Given("page (contains|not contains) \"(.*)\" URL")]
        [When("page (contains|not contains) \"(.*)\" URL")]
        [Then("page (should|should not) contain \"(.*)\" URL")]
        public void CheckUrlContains(string behavior, [NotNull] string url)
        {
            Assert.ShouldBecome(() => _driverService.CurrentUrl.Contains(url), !behavior.Contains("not"), $"current URL is {_driverService.CurrentUrl}");
        }

        [Then("page title should (be|be not|become|become not) \"(.*)\"")]
        public void CheckPageTitle([NotNull] AssertionBehavior behavior, [CanBeNull] string title)
        {
            Assert.ShouldBecome(() => _driverService.Title, title, behavior,
                $"page title is {_driverService.Title}");
        }

        [When("page dumps content to the output")]
        [Then("page dumps content to the output")]
        public void DumpPageContent()
        {
            _driverService.DebugDumpPage();
        }

        [Given("user resized window to (.*) height and (.*) width")]
        [When("user resize window to (.*) height and (.*) width")]
        public void CheckOpened([NotNull] int pageHeight, [NotNull] int pageWidth)
        {
            _driverService.ResizeWindow(pageHeight, pageWidth);
        }
    }
}