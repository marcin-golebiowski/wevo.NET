using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEvo.NET.Core
{
    public interface ObjectiveFunction<T> where T : Individuals.Individual
    {
        /**
         * Computes value of the objective function.
         * @param individual Individual to be evaluated.
         * @return Value of the objective function.
         */
        double Compute(T individual);
    }
}
