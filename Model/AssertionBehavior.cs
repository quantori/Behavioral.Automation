namespace Behavioral.Automation.Model
{
    public class AssertionBehavior
    {
        public AssertionBehavior(AssertionType type, bool inversion)
        {
            Type = type;
            Inversion = inversion;
        }

        public AssertionType Type { get; }

        public bool Inversion { get; }
    }
}