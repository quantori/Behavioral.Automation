namespace Behavioral.Automation.API.Services;

public static class HttpRequestMessageExtension
{
    public static HttpRequestMessage Clone(this HttpRequestMessage req)
    {
        var clone = new HttpRequestMessage(req.Method, req.RequestUri);

        clone.Content = req.Content;
        clone.Version = req.Version;

        foreach (KeyValuePair<string, object> prop in req.Properties)
        {
            clone.Properties.Add(prop);
        }

        foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}