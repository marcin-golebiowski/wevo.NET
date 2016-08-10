using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEvo.NET.Core.Operators
{
    internal class ExitPoint<T> : Operator<T> where T : Individuals.Individual
    {
        private Algorithm<T> algorithm;

        public ExitPoint(Algorithm<T> algorithm)
        {
            this.algorithm = algorithm;
        }

        public Population<T> Apply(Population<T> populationInternal)
        {
            if (Algorithm.this.shouldBeReset) {
                Algorithm.this.shouldBeReset = false;
                terminationCondition.reset();
            }
            if (terminationCondition.isSatisfied(populationInternal))
            {
                Algorithm.this.isFinished = true;
            }
            return populationInternal;
        }
    }
}
