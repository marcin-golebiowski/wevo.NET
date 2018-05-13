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
using System.Collections.Generic;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators
{
    /**
    * Takes a result of another operator plus the best individual from
    * a given population (one for each objective function) and combines
    * them.
    * @param <T> type of the individual.
    */
    public class CombineBestWithOtherOperator<T> : Operator<T> where T : Individuals.Individual
    {
        /** Objective functions used in evaluation. */
        private List<ObjectiveFunction<T>> objectiveFunctions;

        /** Operator that serves the basis for this one. */
        private Operator<T> op;

        /** Random number generator. */
        private wevoRandom random;

        /**
        * Creates the operator.
        * @param functions Objective function to use.
        * @param operator  Operator to apply.
        * @param random Random number generator.
        */
        public CombineBestWithOtherOperator( List<ObjectiveFunction<T>> functions, Operator<T> op, wevoRandom random)
        {
            this.objectiveFunctions = functions;
            this.op = op;
            this.random = random;
        }

        public Population<T> Apply(Population<T> population)
        {
            Population<T> result = op.Apply(population);

            foreach (ObjectiveFunction<T> function in objectiveFunctions)
            {
                T bestIndividual = population.GetIndividuals()[0];
                for (int i = 1; i < population.Size(); i++)
                {
                    if (function(population.GetIndividuals()[i]) > function(bestIndividual))
                    {
                        bestIndividual = population.GetIndividuals()[i];
                    }
                }

                result = Core.Population<T>.RemoveRandomIndividual(random, result);
                result.AddIndividual(bestIndividual);
            }
            return result;
        }
    }
}
