namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IFileChooser
{
    public Task UploadFileAsync(string filePath);
}