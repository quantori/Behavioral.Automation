namespace Behavioral.Automation.Services
{
    public interface IBasicAuthConfig
    {
        public string HomeUrl { get;}
        public string Login { get; }
        public string Password { get; }
        public bool IgnoreAuth { get; }
    }
}