using System;
using CSharpTest.Net.Collections;

namespace wEvo.NET.Core.Utils
{
    public class LruMap<T, V> : LurchTable<T, V>
    {
        public LruMap(int size) : base(LurchTableOrder.Access, size)
        {
        }

        public V Get(T key)
        {
            V result;
            if (base.TryGetValue(key, out result))
            {
                return result;
            }

            throw new ArgumentException();
        }
    }
}
