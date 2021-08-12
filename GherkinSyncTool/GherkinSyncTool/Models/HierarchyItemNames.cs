using System.Collections.Generic;

namespace GherkinSyncTool.Models
{
    public class HierarchyItemNames
    {
        public List<string> FeatureFilesNames { get; set; } = new();
        public List<HierarchyItemNames> ChildItems { get; set; } = new();
        public string Name { get; set; }

        public HierarchyItemNames(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var other = obj as HierarchyItemNames;
            if (other is null) return false;
            return Name == other.Name;
        }
    }
}