using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEvo.NET.Core
{
    public interface Factory<T>
    {
        /**
         * Creates an object of type t.
         * @return Some object.
         */
        T Get();
    }
}
