namespace Behavioral.Automation.Interfaces.UI.Abstractions;

public interface IBrowser
{
    public Task<IPage> OpenNewPageAsync();
    public Task CloseAsync();
}