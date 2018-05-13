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
