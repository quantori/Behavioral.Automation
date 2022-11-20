using System.Text;
using System.Web;
using Behavioral.Automation.API.Configs;
using Behavioral.Automation.API.Context;
using Behavioral.Automation.API.Models;
using Behavioral.Automation.API.Services;
using Behavioral.Automation.Configs;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Behavioral.Automation.API.Bindings;

[Binding]
public class HttpRequestSteps
{
    private readonly ApiContext _apiContext;
    private readonly HttpService _httpService;

    public HttpRequestSteps(ApiContext apiContext, HttpService httpService)
    {
        _apiContext = apiContext;
        _httpService = httpService;
    }

    [When("user sends a \"(.*)\" request to \"(.*)\" url")]
    public HttpResponseMessage UserSendsHttpRequest(string httpMethod, string url)
    {
        var method = new HttpMethod(httpMethod.ToUpper());
        
        if(!IsAbsoluteUrl(url))
        {
            url = ConfigManager.GetConfig<Config>().ApiHost + url;
        }
        
        _apiContext.Request = new HttpRequestMessage(method, url);
        _httpService.SendContextRequest();
       
        return _apiContext.Response;
    }

    [When("user sends a \"(.*)\" request to \"(.*)\" url with the following parameters:")]
    public HttpResponseMessage UserSendsHttpRequestWithParameters(string httpMethod, string url, Table tableParameters)
    {
        UserCreatesHttpRequestWithParameters(httpMethod, url, tableParameters);
        _httpService.SendContextRequest();
       
        return _apiContext.Response;
    }
    
    [When("user sends a \"(.*)\" request to \"(.*)\" url with the json:")]
    public HttpResponseMessage UserSendsHttpRequestWithParameters(string httpMethod, string url, string jsonToSend)
    {
        //TODO: add support for the following types of body block:
        // form-data
        // x-www-form-urlencoded
        // raw (Text, JavaScript, JSON, HTML, XML)
        // binary
        // GraphQL
        // Consider adding other types
        var method = new HttpMethod(httpMethod.ToUpper());
        if(!IsAbsoluteUrl(url))
        {
            url = ConfigManager.GetConfig<Config>().ApiHost + url;
        }
        var uriBuilder = new UriBuilder(url);
        
        _apiContext.Request = new HttpRequestMessage(method, uriBuilder.Uri);
        _apiContext.Request.Content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        
        _httpService.SendContextRequest();
       
        return _apiContext.Response;
    }
    
    [Given("user creates a \"(.*)\" request to \"(.*)\" url with the json:")]
    public HttpRequestMessage UserCreatesHttpRequestWithJson(string httpMethod, string url, string jsonToSend)
    {
        var method = new HttpMethod(httpMethod.ToUpper());
        if(!IsAbsoluteUrl(url))
        {
            url = ConfigManager.GetConfig<Config>().ApiHost + url;
        }
        var uriBuilder = new UriBuilder(url);
        
        _apiContext.Request = new HttpRequestMessage(method, uriBuilder.Uri);
        _apiContext.Request.Content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        return _apiContext.Request;
    }
    
    [Given("user creates a \"(.*)\" request to \"(.*)\" url")]
    public HttpRequestMessage GivenUserCreatesARequestToUrl(string httpMethod, string url)
    {
        var method = new HttpMethod(httpMethod.ToUpper());
        if(!IsAbsoluteUrl(url))
        {
            url = ConfigManager.GetConfig<Config>().ApiHost + url;
        }
        var uriBuilder = new UriBuilder(url);
        
        _apiContext.Request = new HttpRequestMessage(method, uriBuilder.Uri);
        return _apiContext.Request;
    }
    
    [Given("user creates a \"(.*)\" request to \"(.*)\" url with the following parameters:")]
    public HttpRequestMessage UserCreatesHttpRequestWithParameters(string httpMethod, string url, Table tableParameters)
    {
        var method = new HttpMethod(httpMethod.ToUpper());
        if(!IsAbsoluteUrl(url))
        {
            url = ConfigManager.GetConfig<Config>().ApiHost + url;
        }

        _apiContext.Request = new HttpRequestMessage(method, url);
        AddParametersToRequest(_apiContext.Request, tableParameters);
        return _apiContext.Request;
    }

    [When("user adds a body and send the request:")]
    public HttpResponseMessage WhenUserAddsJsonBodyAndSendRequest(string jsonToSend)
    {
        _apiContext.Request.Content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        
        _httpService.SendContextRequest();
       
        return _apiContext.Response;
    }

    [When("user adds parameters and send request:")]
    public HttpResponseMessage WhenUserAddsParametersAndSendRequest(Table tableParameters)
    {
        AddParametersToRequest(_apiContext.Request, tableParameters);
        _httpService.SendContextRequest();
       
        return _apiContext.Response;
    }
    
    [When("user sends request")]
    public HttpResponseMessage WhenUserSendsRequest()
    {
        _httpService.SendContextRequest();
        return _apiContext.Response;
    }
    
    [Given("the response status code should be \"(\\d*)\"")]
    public void ChangeResponseStatusCode(int statusCode)
    {
        _apiContext.ExpectedStatusCode = statusCode;
    }

    private static bool IsAbsoluteUrl(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }

    private static void AddParametersToRequest(HttpRequestMessage request, Table tableParameters)
    {
        var uriBuilder = new UriBuilder(request.RequestUri);

        var parameters = tableParameters.CreateSet<HttpParameters>();

        var headers = new List<KeyValuePair<string, IEnumerable<string>>>();
        if (parameters is not null)
        {
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var parameter in parameters)
            {
                var parameterKind = Enum.Parse<RequestParameterKind>(parameter.Kind);

                if (parameterKind is RequestParameterKind.Param)
                {
                    query.Add(parameter.Name, parameter.Value);
                }

                if (parameterKind is RequestParameterKind.Header)
                {
                    var headerValue = parameter.Value.Trim().Split(",");
                    headers.Add(new KeyValuePair<string, IEnumerable<string>>(parameter.Name, headerValue));
                }
            }

            uriBuilder.Query = query.ToString();
        }

        request.RequestUri = uriBuilder.Uri;

        if (headers.Any())
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}