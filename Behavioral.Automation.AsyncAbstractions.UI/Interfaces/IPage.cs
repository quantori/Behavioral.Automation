namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

public interface IPage
{
    public Task GoToUrlAsync(string url);
    public Task GoToApplicationUrlAsync();
    public Task HaveTitleAsync(string title);
}