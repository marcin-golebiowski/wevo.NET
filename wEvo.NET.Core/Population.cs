using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wEvo.NET.Core;
using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core
{
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
            //this.individuals = Arrays.asList((T[])population.individuals.toArray().clone());
            //TODO ....
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

            Population<T> that = castToPopulation(obj);
            return this.individuals.Equals(that.individuals);
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

        /**
         * Shuffles population with given random number generator.
         * @param random Random number generator.
         * @param population Population object to be shuffled. Will not be
         * @param <T> Type of individual in the population.
         * changed during shuffle.
         * @return Shuffled version of the population object.
         */
        public static Population<T> Shuffle(
            WevoRandom random,
            Population<T> population)
        {
            List< T > reorderedIndividuals = new List<T>(population.Size());
            List< T > originalIndividuals = new List<T>(population.GetIndividuals());

            // We pick an individual from the list and append it
            // to the end of new list containing reordered individuals.
            for (int i = population.size(); i > 0; i--)
            {
                int pickedIndividualIndex = random.nextInt(0, i);
                T pickedIndividual = originalIndividuals.get(pickedIndividualIndex);
                originalIndividuals.remove(pickedIndividualIndex);
                reorderedIndividuals.add(pickedIndividual);
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
            WevoRandom random,
            Population<T> population)
        {
            List<T> newIndividuals = new List<T>(population.GetIndividuals());
            newIndividuals.RemoveRange(random.NextInt(0, population.Size()), 1);

            return new Population<T>(newIndividuals);
        }
    }
}
