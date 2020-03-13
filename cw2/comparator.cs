using System;
using System.Collections.Generic;
using System.Text;

namespace cw2
{
    class Comparator : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.name} {x.surname} {x.index}",$"{y.name} {y.surname} {y.index}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.name} {obj.surname} {obj.index}");
        }

    }
}