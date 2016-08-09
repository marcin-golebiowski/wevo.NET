using CSharpTest.Net.Collections;

namespace wEvo.NET.Core.Utils
{
    public class LruMap<T, V> : LurchTable<T, V>
    {
        public LruMap(int size) : base(LurchTableOrder.Access, size)
        {
        }
    }
}
