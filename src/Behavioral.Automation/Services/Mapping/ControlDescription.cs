using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlDescription
    {
        public ControlDescription(
            [CanBeNull] string id,
            [NotNull] string caption,
            [CanBeNull] string subpath = null)
        {
            Id = id;
            Caption = caption;
            Subpath = subpath;
        }

        public string Id { get; }

        public string Caption { get; }

        public string Subpath { get; }
    }
}