namespace Behavioral.Automation.API.Context;

public class ApiContext
{
    public HttpRequestMessage Request { get; set; }
    public HttpResponseMessage Response { get; set; }
    public long ResponseTimeMillis { get; set; }
    public int? ExpectedStatusCode { get; set; }
}