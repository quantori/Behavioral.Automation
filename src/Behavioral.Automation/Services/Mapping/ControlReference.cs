namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlReference
    {
        public ControlReference(ControlLocation controlLocation, ControlDescription controlDescription)
        {
            ControlLocation = controlLocation;
            ControlDescription = controlDescription;
        }
        public ControlLocation ControlLocation { get; }
        public ControlDescription ControlDescription { get; }
    }
}
