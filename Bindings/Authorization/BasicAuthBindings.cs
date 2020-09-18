using Behavioral.Automation.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Text;
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

        /// <summary>
        /// Hook which executes basic authentication before scenarios with @BasicAuth tag
        /// </summary>
        /// <returns></returns>
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
