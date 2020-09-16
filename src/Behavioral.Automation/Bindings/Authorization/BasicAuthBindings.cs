using Behavioral.Automation.Services;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.Authorization
{
    public interface IBasicAuthConfig {
        public string HomeUrl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IgnoreAuth { get; set; }
    }


    [Binding]
    public class BasicAuthBindings : Steps
    {
        private readonly IDriverService _driverService;
        private readonly IBasicAuthConfig _basicAuthConfig;
        private bool _signInRequired = true;

        public BasicAuthBindings([NotNull]IDriverService driverService, IBasicAuthConfig basicAuthConfig)
        {
            _driverService = driverService;
            _basicAuthConfig = basicAuthConfig;
            _signInRequired = !basicAuthConfig.IgnoreAuth;
        }

        [BeforeScenario(Order = 1), Scope(Tag = "BasicAuth")]
        public async Task BasicAuth()
        {
            if (_signInRequired)
            {
                Uri uri = new Uri(_basicAuthConfig.HomeUrl);
                string newUrl = $"{uri.Scheme}://{_basicAuthConfig.Login}:{_basicAuthConfig.Password}@{uri.Authority}{uri.PathAndQuery}";
                _driverService.Navigate(newUrl);
                _signInRequired = false;
            }
            _driverService.Navigate(_basicAuthConfig.HomeUrl);
            await Task.Delay(5000);
        }
    }
}
