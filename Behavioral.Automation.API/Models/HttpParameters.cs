namespace Behavioral.Automation.API.Models;

public class HttpParameters
{
    public string Name;
    public string Value;
    public string Kind;
}

public enum RequestParameterKind
{
    Param,
    Header
}