﻿/*
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

namespace wevo.NET.Core.Operators
{
    /**
    * Adds a given hardcoded individual to the population.
    * @param <T> type of the individual.
    */
    public class AddHardcodedIndividual<T> : Operator<T> where T : Individuals.Individual
    {   
        /** Factory of individuals to apply. */
        private Factory<T> factory;

        /**
        * Creates the operator.
        * @param factory Factory of hardcoded individuals to apply.
        */
        public AddHardcodedIndividual(Factory<T> factory)
        {
            this.factory = factory;
        }

        public Population<T> Apply(Population<T> population)
        {
            Population<T> result = population;
            result.AddIndividual(factory.Get());
            return result;
        }
    }
}