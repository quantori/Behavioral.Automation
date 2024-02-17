namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IBrowser
{
    public Task<IPage> NewPageAsync();
    public Task CloseAsync();
}