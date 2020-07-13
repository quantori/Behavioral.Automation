using System;

namespace Behavioral.Automation.Services.Mapping.PageMapping
{
    public sealed class PageScopeId
    {
        public PageScopeId(string name, string urlWildCard)
        {
            Name = name;
            UrlWildCard = urlWildCard;
        }

        public string Name { get; }

        public string UrlWildCard { get; }

        private bool Equals(PageScopeId other)
        {
            return string.Equals(UrlWildCard, other.UrlWildCard, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PageScopeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (UrlWildCard != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(UrlWildCard) : 0);
        }
    }
}