using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wevo.NET.Core.Utils
{
    class Arrays<T>
    {
        private static readonly EqualityComparer<T> elementComparer = EqualityComparer<T>.Default;

        public static bool EqualsArray(T[] first, T[] second)
        {
            if (first == second)
            {
                return true;
            }
            if (first == null || second == null)
            {
                return false;
            }
            if (first.Length != second.Length)
            {
                return false;
            }
            for (int i = 0; i < first.Length; i++)
            {
                if (!elementComparer.Equals(first[i], second[i]))
                {
                    return false;
                }
            }
            return true;
        }

        internal static int GetHashCode(T[] array)
        {
            // http://stackoverflow.com/questions/7244699/gethashcode-on-byte-array
            unchecked
            {
                if (array == null)
                {
                    return 0;
                }
                int hash = 17;
                foreach (T element in array)
                {
                    hash = hash * 31 + elementComparer.GetHashCode(element);
                }
                return hash;
            }
        }
    }
}
