namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlScopeOptions
    {

        private static readonly ControlScopeOptions DefaultOptions = new ControlScopeOptions();

        public ControlScopeOptions(bool isVirtualized = false)
        {
            IsVirtualized = isVirtualized;
        }

        public bool IsVirtualized { get; }

        public static ControlScopeOptions Default()
        {
            return DefaultOptions;
        }
    }
}
