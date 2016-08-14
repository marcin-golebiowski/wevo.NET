/*
 * wevo.NET - Distributed Evolutionary Computation Library
 *
 * Based on wevo and wevo2 libraries.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor,
   Boston, MA  02110-1301  USA
 */

namespace wevo.NET.Core.Exitcriteria
{
    /**
    * Terminates the evolution after for given number of iterations the
    * individual hasn't changed.
    *
    * @param <T> Type of the individual for which it is evaluated.
    */
    public class IndividualHasntImproved<T> : TerminationCondition<T> where T : Individuals.Individual
    {
        /** Number of iterations the individual is allowed to not change. */
        private int maxIter;

        /** Best individual so far. */
        private T individual;

        /** Number of iterations individual hasn't changed. */
        private int iterationsWithNoChange;

        /** Objective function to look at when finding best individual. */
        private ObjectiveFunction<T> objFunction;

        /** 
         * Creates the termination condition which will succeed after
         * maximum number of iterations has passed.
         * @param maxIter Maximum number of iterations.
         * @param objFunction Objective function to look at.
         */
        public IndividualHasntImproved(int maxIter,
            ObjectiveFunction<T> objFunction)
        {
            this.maxIter = maxIter;
            this.objFunction = objFunction;
        }

        public bool IsSatisfied(Population<T> population)
        {
            var individuals = population.GetIndividuals();
            individuals.Sort((T o1, T o2) => objFunction.Compute(o1) > objFunction.Compute(o2) ? 1 : -1);

            if (individuals.Count == 0)
            {
                throw new System.Exception("Population is empty");
            }

            T bestIndividual = individuals[0];

            if (individual != null && objFunction.Compute(bestIndividual) <= objFunction.Compute(individual))
            {
                iterationsWithNoChange++;
            }
            else
            {
                individual = bestIndividual;
                iterationsWithNoChange = 0;
            }
            return iterationsWithNoChange > maxIter - 1;
        }

        public void Reset()
        {
            iterationsWithNoChange = 0;
        }
    }
}
