using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    public interface IMultiSelectDropdownWrapper : IDropdownWrapper
    {
        IEnumerable<string> SelectedValuesTexts { [NotNull] get; }
    }
}
