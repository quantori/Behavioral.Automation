using TechTalk.SpecFlow.Infrastructure;

namespace Behavioral.Automation.API.Services;

public class LoggingHandler : DelegatingHandler
{
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

    public LoggingHandler(HttpMessageHandler innerHandler, ISpecFlowOutputHelper specFlowOutputHelper)
        : base(innerHandler)
    {
        _specFlowOutputHelper = specFlowOutputHelper;
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _specFlowOutputHelper.WriteLine($"Request:{Environment.NewLine}{request}");
        if (request.Content != null)
        {
            _specFlowOutputHelper.WriteLine(request.Content.ReadAsStringAsync(cancellationToken).Result);
        }

        HttpResponseMessage response = base.Send(request, cancellationToken);

        _specFlowOutputHelper.WriteLine($"Response:{Environment.NewLine}{response}");
        _specFlowOutputHelper.WriteLine(response.Content.ReadAsStringAsync(cancellationToken).Result);

        return response;
    }
}