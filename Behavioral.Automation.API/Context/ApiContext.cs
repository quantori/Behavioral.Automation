namespace Behavioral.Automation.API.Context;

public class ApiContext
{
    private HttpRequestMessage _request;

    public HttpRequestMessage Request
    {
        get => _request;
        set
        {
            _request = value;
            ExpectedStatusCode = 200;
        }
    }
    
    public HttpResponseMessage Response { get; set; }
    
    public long ResponseTimeMillis { get; set; }
    
    public int ExpectedStatusCode { get; set; } = 200;
}