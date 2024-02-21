namespace Behavioral.Automation.Interfaces.UI.Abstractions;

public interface IPage
{
    public Task GoToUrlAsync(string url);
    public Task GoToApplicationUrlAsync();
    public Task HaveTitleAsync(string title);
    public Task ScreenshotAsync(string path);
}