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
using System.Text;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core
{
    /**
    * Represents the population on which {@link Algorithm} works.
    *
    * @param <T> Type of the individual that this population contains.
    */
    public class Population<T> where T : Individuals.Individual
    {
        /** List of individuals in the population. */
        private List<T> individuals;

        /**
         * Creates the population from given initial list of individuals.
         * @param list List of individuals in initial population.
         */
        public Population(List<T> list)
        {
            individuals = list;
        }

        /** Creates an empty population. */
        public Population()
        {
            individuals = new List<T>();
        }


        /**
        * Copying constructor.
        * @param population Population to copy.
        */
        public Population(Population<T> population)
        {
            this.individuals = new List<T>();

            foreach (Individuals.Individual ind in population.individuals)
            {
                this.individuals.Add((T)ind.Clone());
            }
        }

        public T this[int index]
        {
            get
            {
                return GetIndividuals()[index];
            }
        }

        /**
         * Returns individuals in the population.
         * @return List of individuals in the population.
         */
        public List<T> GetIndividuals()
        {
            return individuals;
        }

        /**
         * Adds a new individual to the population.
         * @param individual Individual to be added.
         */
        public void AddIndividual(T individual)
        {
            individuals.Add(individual);
        }

        /**
         * Merges this population with a given one without removing duplicates.
         * @param population Population to merge with. Must not be null.
         */
        public void MergeWith(Population<T> population)
        {
            this.individuals.AddRange(population.individuals);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (T i in individuals)
            {
                sb.Append(i);
                sb.Append("\n");
            }
            return sb.ToString();
        }

        /** 
         * Returns size of the population.
         * @return Size of the population.
         */
        public int Size()
        {
            return individuals.Count;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Population<T>)) {
                return false;
            }

            Population<T> that = CastToPopulation(obj);

            if (this.individuals.Count != that.individuals.Count)
            {
                return false;
            }

            for (int i = 0; i < this.individuals.Count; i++)
            {
                if (!this.individuals[i].Equals(that.individuals[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /** {@inheritDoc} */
        public override int GetHashCode()
        {
            return individuals.GetHashCode();
        }

        /**
        * Casts given object to a Population object. Will throw
        * a ClassCastException when given object is not an instance
        * of Population class.
        * 
        * @param object Object to be casted to Population.
        * @return Population object that is the result of a cast.
        * Never null.
        */
        private Population<T> CastToPopulation(Object obj)
        {
            Population<T> population = (Population<T>)obj;
            
            // EmptyBlock off
            // We're doing only type-checking here, empty block is OK.
            foreach (T individual in population.GetIndividuals()) { }
            // EmptyBlock on

            return population;
        }

        public int GetSize()
        {
            return this.individuals.Count;
        }

        /**
         * Shuffles population with given random number generator.
         * @param random Random number generator.
         * @param population Population object to be shuffled. Will not be
         * @param <T> Type of individual in the population.
         * changed during shuffle.
         * @return Shuffled version of the population object.
         */
        public static Population<T> Shuffle(
            wevoRandom random,
            Population<T> population)
        {
            List<T> reorderedIndividuals = new List<T>(population.Size());
            List<T> originalIndividuals = new List<T>(population.GetIndividuals());

            // We pick an individual from the list and append it
            // to the end of new list containing reordered individuals.
            for (int i = population.Size(); i > 0; i--)
            {
                int pickedIndividualIndex = random.NextInt(0, i);
                T pickedIndividual = originalIndividuals[pickedIndividualIndex];
                originalIndividuals.RemoveAt(pickedIndividualIndex);
                reorderedIndividuals.Add(pickedIndividual);
            }

            return new Population<T>(reorderedIndividuals);
        }

        /**
         * Removes random individual from population.
         * @param random Random number generator.
         * @param population Population from which the individual will be removed.
         * This object will be intact.
         * @param <T> Type of individual in the population.
         * @return Population without random individual.
         */
        public static Population<T> RemoveRandomIndividual(
            wevoRandom random,
            Population<T> population)
        {
            List<T> newIndividuals = new List<T>(population.GetIndividuals());
            newIndividuals.RemoveRange(random.NextInt(0, population.Size()), 1);

            return new Population<T>(newIndividuals);
        }
    }
}
