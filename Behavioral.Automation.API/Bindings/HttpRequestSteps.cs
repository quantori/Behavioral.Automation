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
        
        _apiContext.Request = new HttpRequestMessage(method, GetUri(url));
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
        //TODO: body can be:
        // form-data
        // x-www-form-urlencoded
        // raw (Text, JavaScript, JSON, HTML, XML)
        // binary
        // GraphQL
        // Consider adding other types
        var method = new HttpMethod(httpMethod.ToUpper());

        _apiContext.Request = new HttpRequestMessage(method, GetUri(url));
        _apiContext.Request.Content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");

        _httpService.SendContextRequest();

        return _apiContext.Response;
    }


    [When("user sends a \"(.*)\" request to \"(.*)\" url with the application/x-www-form-urlencoded:")]
    public HttpResponseMessage UserSendsHttpRequestWithFormUrlEncodedContent(string httpMethod, string url, Table parameters)
    {
        var method = new HttpMethod(httpMethod.ToUpper());

        _apiContext.Request = new HttpRequestMessage(method, GetUri(url));

        var body = parameters.Rows.Select(row => new KeyValuePair<string, string>(row[0], row[1]));

        _apiContext.Request.Content = new FormUrlEncodedContent(body);

        _httpService.SendContextRequest();

        return _apiContext.Response;
    }

    [Given("user creates a \"(.*)\" request to \"(.*)\" url with the json:")]
    public HttpRequestMessage UserCreatesHttpRequestWithJson(string httpMethod, string url, string jsonToSend)
    {
        var method = new HttpMethod(httpMethod.ToUpper());

        _apiContext.Request = new HttpRequestMessage(method, GetUri(url));
        _apiContext.Request.Content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        return _apiContext.Request;
    }

    [Given("user creates a \"(.*)\" request to \"(.*)\" url")]
    public HttpRequestMessage GivenUserCreatesARequestToUrl(string httpMethod, string url)
    {
        var method = new HttpMethod(httpMethod.ToUpper());

        _apiContext.Request = new HttpRequestMessage(method, GetUri(url));
        return _apiContext.Request;
    }

    [Given("user creates a \"(.*)\" request to \"(.*)\" url with the following parameters:")]
    public HttpRequestMessage UserCreatesHttpRequestWithParameters(string httpMethod, string url, Table tableParameters)
    {
        var method = new HttpMethod(httpMethod.ToUpper());

        _apiContext.Request = new HttpRequestMessage(method, GetUri(url));
        AddParametersToRequest(_apiContext.Request, tableParameters);
        return _apiContext.Request;
    }

    [When("user adds a JSON body and send the request:")]
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

    private static Uri GetUri(string url)
    {
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            url = ConfigManager.GetConfig<Config>().ApiHost + url;
        }

        return new UriBuilder(url).Uri;
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
                if (header.Key.Equals("Content-Type", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception(
                        "Remove the Content-Type header, please. The Content-Type header is automatically added with request step bindings.");
                }

                request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}