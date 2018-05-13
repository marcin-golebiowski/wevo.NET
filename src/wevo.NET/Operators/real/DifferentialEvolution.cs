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
using wevo.NET.Core.Individuals;

namespace wevo.NET.Core.Operators.Real
{
    /**
    * An operator inspired by differential evolution. Resulting vector is of a form
    * <code>offsprint = parent1 + alpha * (parent2 - parent3)</code>. 
    * 
    (marcin.brodziak@gmail.com)
    */
    public class DifferentialEvolution : Operator<RealVector>
    {
        /** Alpha coefficient of differential evolution. */
        private double alpha;

        /**
         * Creates differential evolution operator.
         * @param alpha Alpha coefficient of the operator.
         */
        public DifferentialEvolution(double alpha)
        {
            this.alpha = alpha;
        }

        /** {@inheritDoc} */
        public Population<RealVector> Apply(Population<RealVector> population)
        {
            List<RealVector> result = new List<RealVector>();

            List<RealVector> individuals = population.GetIndividuals();
            int size = individuals.Count;
            for (int individual = 0; individual < size; individual++)
            {
                RealVector parent1 = individuals[individual % size];
                RealVector parent2 = individuals[(individual + 1) % size];
                RealVector parent3 = individuals[(individual + 2) % size];
                double[] offspring = new double[parent2.GetSize()];
                for (int position = 0; position < offspring.Length; position++)
                {
                    offspring[position] = parent1.GetValue(position)
                        + alpha * (parent2.GetValue(position) - parent3.GetValue(position));
                }
                result.Add(new RealVector(offspring));
            }
            return new Population<RealVector>(result);
        }
    }
}
