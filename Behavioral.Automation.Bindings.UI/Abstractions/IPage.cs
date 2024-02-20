namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IPage
{
    public Task GoToUrlAsync(string url);
    public Task GoToApplicationUrlAsync();
    public Task HaveTitleAsync(string title);
    public Task<IFileChooser> RunAndWaitForFileChooserAsync(Func<Task> action);
    public Task ScreenshotAsync(string path);
}