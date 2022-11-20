using TechTalk.SpecFlow.Infrastructure;

namespace Behavioral.Automation.API.Services;

public class HttpApiClient : IHttpApiClient
{
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

    public HttpApiClient(ISpecFlowOutputHelper specFlowOutputHelper)
    {
        _specFlowOutputHelper = specFlowOutputHelper;
    }

    public HttpResponseMessage SendHttpRequest(HttpRequestMessage httpRequestMessage)
    {
        HttpClient client = new(new LoggingHandler(new HttpClientHandler(), _specFlowOutputHelper));
        return client.Send(httpRequestMessage);
    }
}