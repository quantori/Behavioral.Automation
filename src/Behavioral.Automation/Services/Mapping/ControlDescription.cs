using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    /// <summary>
    /// This class contains fields for control description
    /// </summary>
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

        /// <summary>
        /// Control search attribute value
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Control caption to be used in step
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// Control xpath (optional)
        /// </summary>
        public string Subpath { get; }
    }
}