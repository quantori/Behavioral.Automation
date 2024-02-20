namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IBrowser
{
    public Task<IPage> OpenNewPageAsync();
    public Task CloseAsync();
}