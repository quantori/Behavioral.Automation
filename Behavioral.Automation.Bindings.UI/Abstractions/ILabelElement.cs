namespace Behavioral.Automation.Bindings.UI.Abstractions;

/// <summary>
/// Represents an interface for label web elements.
/// </summary>
public interface ILabelElement : IWebElement
{
    /// <summary>
    /// Asynchronously verifies that the element's text exactly matches the specified text.
    /// </summary>
    /// <param name="text">The expected text of the element.</param>
    /// <returns>A task representing the asynchronous check.</returns>
    public Task ShouldHaveTextAsync(string text);

    /// <summary>
    /// Asynchronously verifies that the element's text contains the specified text.
    /// </summary>
    /// <param name="text">The text to search for within the element's text.</param>
    /// <returns>A task representing the asynchronous check.</returns>
    public Task ContainsTextAsync(string text);
}