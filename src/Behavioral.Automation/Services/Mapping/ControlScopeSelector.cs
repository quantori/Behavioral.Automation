using System.Collections.Generic;
using System.Linq;

namespace Behavioral.Automation.Services.Mapping
{
    public sealed class ControlScopeSelector
    {
        private readonly Stack<ControlScopeId> _selectionSteps = new Stack<ControlScopeId>();

        internal ControlScopeSelector(string selectionSteps)
        {
            var controlScopeIds = ParseParentControlSelectionSteps(selectionSteps);
          
            foreach (var scope in controlScopeIds)
            {
                AddSelectionStep(scope);
            }
        }

        public ControlScopeSelector(string controlScopeId, ControlLocation controlLocation)
        {
            AddSelectionStep(new ControlScopeId(controlScopeId));

            var parentOfControl = controlLocation;
            while (parentOfControl != null)
            {
                AddSelectionStep(parentOfControl.ControlScopeId);
                parentOfControl = parentOfControl.Parent;
            }
        }

        public IEnumerable<ControlScopeId> SelectionSteps => _selectionSteps;

        private void AddSelectionStep(ControlScopeId controlScopeId)
        {
            _selectionSteps.Push(controlScopeId);
        }

        private static ControlScopeId[] ParseParentControlSelectionSteps(string selectionSteps)
        {
            var controlScopeIds = selectionSteps.Split("of")
                .Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).Select(s => new ControlScopeId(s));
            return controlScopeIds.ToArray();
        }
    }
}