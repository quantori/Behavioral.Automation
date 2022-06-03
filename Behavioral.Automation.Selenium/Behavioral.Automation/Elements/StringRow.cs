using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Behavioral.Automation.Elements
{
    public class StringRow : IEquatable<StringRow>
    {
        public StringRow(string[] values)
        {
            Cells = values;
        }

        public string[] Cells { get; }


        public bool Equals(StringRow other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Cells.SequenceEqual(other.Cells);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StringRow) obj);
        }

        public override int GetHashCode()
        {
            if (Cells != null)
            {
                var hashcode = ((IStructuralEquatable) Cells).GetHashCode(EqualityComparer<string>.Default);
                return hashcode;
            }
            return 0;
        }
    }
}