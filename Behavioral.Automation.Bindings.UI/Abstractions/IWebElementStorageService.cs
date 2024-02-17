namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IWebElementStorageService
{
    static readonly Dictionary<Type, Type> RegisteredImplementations = new();
    static void RegisterWebElementImplementationAs<TType, TInterface>() where TType : class, TInterface
    {
        RegisteredImplementations.Add(typeof(TInterface), typeof(TType));
    }

    T Get<T>(string locatorKey);
    
}