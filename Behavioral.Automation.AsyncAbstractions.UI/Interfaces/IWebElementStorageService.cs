namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

public interface IWebElementStorageService
{
    static readonly Dictionary<Type, Type> RegisteredElements = new();

    static void RegisterWebElementImplementationAs<TType, TInterface>() where TType : class, TInterface
    {
        RegisteredElements.Add(typeof(TInterface), typeof(TType));
    }

    T Get<T>(string locatorKey) where T : IWebElement;
}