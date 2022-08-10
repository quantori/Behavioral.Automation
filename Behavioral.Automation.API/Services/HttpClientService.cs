using Behavioral.Automation.API.Context;
using FluentAssertions;

namespace Behavioral.Automation.API.Services;

public class HttpService
{
    private readonly IHttpApiClient _client;
    private readonly ApiContext _apiContext;

    public HttpService(IHttpApiClient client, ApiContext apiContext)
    {
        _client = client;
        _apiContext = apiContext;
    }

    public  HttpResponseMessage SendContextRequest()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        _apiContext.Response = _client.SendHttpRequest(_apiContext.Request.Clone());
        watch.Stop();
        ((int)_apiContext.Response.StatusCode).Should().Be(_apiContext.ExpectedStatusCode);
        _apiContext.ResponseTimeMillis = watch.ElapsedMilliseconds; //save TTFB + Content download time
        return _apiContext.Response;
    }
}