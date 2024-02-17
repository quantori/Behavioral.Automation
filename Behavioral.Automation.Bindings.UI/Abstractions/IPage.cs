namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IPage
{
    public Task GotoAsync(string url);
    public Task GoToApplicationUrlAsync();
    public Task ToHaveTitleAsync(string title);
    public Task<IFileChooser> RunAndWaitForFileChooserAsync(Func<Task> action);
    public Task ScreenshotAsync(string path);

}