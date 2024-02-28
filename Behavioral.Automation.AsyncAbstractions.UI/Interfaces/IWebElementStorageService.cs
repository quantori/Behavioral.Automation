namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

/// <summary>
/// Represents a factory service for storing and retrieving web elements.
/// </summary>
public interface IWebElementStorageService
{
    /// <summary>
    /// A dictionary containing registered element types. The dictionary is used application-wide (not thread-specific).
    /// </summary>
    static readonly Dictionary<Type, Type> RegisteredElements = new();

    /// <summary>
    /// Registers the implementation type for a specified interface type.
    /// </summary>
    /// <typeparam name="TType">The implementation type to register.</typeparam>
    /// <typeparam name="TInterface">The interface type to register.</typeparam>
    /// <remarks>
    /// Only class types implementing the specified interface are allowed.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to register ButtonElement class as the implementation
    /// for the IButtonElement interface.
    /// <code>
    /// // Assuming ButtonElement implements IButtonElement interface
    /// IWebElementStorageService.RegisterWebElementImplementationAs&lt;ButtonElement, IButtonElement>();
    /// </code>
    /// </example>
    static void RegisterWebElementImplementationAs<TType, TInterface>() where TType : class, TInterface
    {
        RegisteredElements.Add(typeof(TInterface), typeof(TType));
    }

    T Get<T>(string locatorKey) where T : IWebElement;
}