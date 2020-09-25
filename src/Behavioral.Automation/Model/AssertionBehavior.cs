namespace Behavioral.Automation.Model
{
    public sealed class AssertionBehavior
    {
        public AssertionBehavior(AssertionType type, bool inversion)
        {
            Type = type;
            Inversion = inversion;
        }

        public AssertionType Type { get; }

        public bool Inversion { get; }

        public static (AssertionBehavior Direct, AssertionBehavior Inverted) Immediate
        {
            get => (new AssertionBehavior(AssertionType.Immediate, false),
                    new AssertionBehavior(AssertionType.Immediate, true));
        }

        public static (AssertionBehavior Direct, AssertionBehavior Inverted) Continuous
        {
            get => (new AssertionBehavior(AssertionType.Continuous, false),
                    new AssertionBehavior(AssertionType.Continuous, true));
        }

    }
}