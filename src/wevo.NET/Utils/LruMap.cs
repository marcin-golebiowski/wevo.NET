using System;
using CSharpTest.Net.Collections;

namespace wevo.NET.Core.Utils
{
    /**
    * Cache of least recently used objects.
    * @param <T> Type of individuals we map from. 
    * @param <V> Type of individuals we map to.
    */
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
