using System.Net;
using Behavioral.Automation.API.Context;
using FluentAssertions;
using NUnit.Framework;

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
        if (_apiContext.ExpectedStatusCode is null)
        {
            Assert.That((int)_apiContext.Response.StatusCode, Is.InRange(200, 299), "Response status code is not success.");
        }
        else
        {
            Assert.That((int)_apiContext.Response.StatusCode, Is.EqualTo(_apiContext.ExpectedStatusCode));
            _apiContext.ExpectedStatusCode = null;
        }
        _apiContext.ResponseTimeMillis = watch.ElapsedMilliseconds; //save TTFB + Content download time
        return _apiContext.Response;
    }
}