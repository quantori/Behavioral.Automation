using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements.Interfaces
{
    public interface IDropdownWrapper : IWebElementWrapper
    {
        void Select(params string[] elements);

        string SelectedValue { [CanBeNull] get; }

        IEnumerable<string> Items { [NotNull, ItemNotNull] get; }

        IEnumerable<IWebElementWrapper> Elements { [NotNull, ItemNotNull] get; }

        IEnumerable<IWebElementWrapper> Groups { [NotNull, ItemNotNull] get; }

        bool Empty { get; }
    }
}