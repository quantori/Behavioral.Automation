namespace Behavioral.Automation.API.Services;

public interface IHttpApiClient
{
    public HttpResponseMessage SendHttpRequest(HttpRequestMessage httpRequestMessage);
}