using System.Collections.Generic;
using System.IO;

namespace GherkinSyncTool.Models
{
    public class HierarchyItem
    {
        public ulong? Id { get; set; }
        public string Name { get; set; }
        public List<HierarchyItem> ChildrenItems { get; set; } = new();
    }
}