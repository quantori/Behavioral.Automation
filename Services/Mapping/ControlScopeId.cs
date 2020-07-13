using System;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlScopeId
    {
        public ControlScopeId(string name)
        {
            Name = name;
        }

        public string Name { get; }

        private bool Equals(ControlScopeId other)
        {
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ControlScopeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0;
        }
    }
}