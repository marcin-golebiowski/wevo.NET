using System;
using System.Collections.Generic;

namespace wevo.NET.Core.Operators
{
    /**
    * Returns better half of the individuals in the population.
    *
    * @param <T> Type of the individuals the operator should work on.
    */
    public class BestFractionSelection<T> : Operator<T> where T : Individuals.Individual
    {
        private class IndividualsComparer : IComparer<T>
        {
            /** Objective function that scores individuals. */
            private ObjectiveFunction<T> objectiveFunction;

            public IndividualsComparer(ObjectiveFunction<T> objectiveFunction)
            {
                this.objectiveFunction = objectiveFunction;
            }

            public int Compare(T o1, T o2)
            {
                Double v1 = objectiveFunction(o1);
                Double v2 = objectiveFunction(o2);
                if (v1 > v2)
                {
                    return -1;
                }
                else if (v1 == v2)
                {
                    return 0;
                }
                return 1;
            }
        }

        /** Percentage of best individuals to take. 1 makes identity. */
        private double ratio;

        /** Objective function that scores individuals. */
        private ObjectiveFunction<T> objectiveFunction;

        /**
         * Creates the operator that will take the population and return best half 
         * of the individuals.
         * @param objectiveFunction Objective function to score individuals against.
         * @param fraction Fraction of individuals to promote.
         */
        public BestFractionSelection(ObjectiveFunction<T> objectiveFunction, double fraction)
        {
            this.objectiveFunction = objectiveFunction;
            this.ratio = fraction;
        }

        public Population<T> Apply(Population<T> population)
        {
            List<T> individuals = population.GetIndividuals();
            individuals.Sort(new IndividualsComparer(objectiveFunction));
            List<T> result = new List<T>();

            int borderLine = (int)(individuals.Count * ratio);
            int i = 0;
            while (result.Count < individuals.Count)
            {
                result.Add(individuals[i % borderLine]);
                i++;
            }
            return new Population<T>(result);
        }
    }
}
