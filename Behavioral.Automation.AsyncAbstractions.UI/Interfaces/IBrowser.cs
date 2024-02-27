namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

public interface IBrowser
{
    public Task<IPage> OpenNewPageAsync();
    public Task CloseAsync();
}