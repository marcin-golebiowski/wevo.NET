using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEvo.NET.Core
{
    public interface Operator<T> where T : Individuals.Individual
    {
        /**
         * Applies the operator to given population. 
         * @param population Source population.
         * @return Population after transformation.
         */
        Population<T> Apply(Population<T> population);
    }
}
