using System.Text.RegularExpressions;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class RedirectionBinding
    {
        [NotNull] private readonly IScopeContextManager _scopeContextManager;
        private readonly IDriverService _driverService;
        private readonly IUriToPageScopeMapper _uriToPageScopeMapper;
        private bool _WindowsHandleSwitched;

        public RedirectionBinding([NotNull] IScopeContextManager scopeContextManager,
            IDriverService driverService,
            IUriToPageScopeMapper uriToPageScopeMapper)
        {
            _scopeContextManager = scopeContextManager;
            _driverService = driverService;
            _uriToPageScopeMapper = uriToPageScopeMapper;
            _WindowsHandleSwitched = false;
        }

        [Given("user is redirected to (.*) page")]
        [When("user is redirected to (.*) page")]
        [When("user should be redirected to (.*) page")]
        [Then("user should be redirected to (.*) page")]
        public void CheckRedirect([NotNull] string pageName)
        {
            if (_WindowsHandleSwitched)
            {
                _driverService.SwitchToTheFirstWindow();
                _WindowsHandleSwitched = false;
            }
            var pageScopeId = _uriToPageScopeMapper.GetPageScopeId(pageName);
            Assert.ShouldBecome(() => Regex.IsMatch(_driverService.CurrentUrl, pageScopeId.UrlWildCard), true, $"current URL is {_driverService.CurrentUrl}");
            _scopeContextManager.SwitchPage(pageName);
        }
        
        [Given("user sees opened window (.*) page")]
        [When("user sees opened window (.*) page")]
        public void CheckOpened([NotNull] string pageName)
        {
            _driverService.SwitchToTheLastWindow();
            CheckRedirect(pageName);
            _WindowsHandleSwitched = true;
        }
    }
}