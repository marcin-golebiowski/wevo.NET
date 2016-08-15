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
 *  Boston, MA  02110-1301  USA
 */

using System;
using wevo.NET.Core;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators.Binary
{
    public class PBIL : Operator<BinaryVector>
    {
        /**
         * Learning rate
         */
        private double theta1;

        /**
         * Mutation probability
         */
        private double theta2;

        /**
         * Mutation rate
         */

        private double theta3;

        /**
         * Probability vector that works is used throughout CGA.
         */
        private double[] probability_vector;

        /**
         * Population size
         */
        private int n;

        /**
         * number of iteration used to generate new opulation
         */
        private int iterations;

        /**
         * Some population for learning probablity vector
         */
        private Population<BinaryVector> population;

        /**
         * function used to evaluate binary individuals in this operator
         */
        private ObjectiveFunction<BinaryVector> objective_function;
        private wevoRandom random;
        private int individualLenght;

        /**
         * Default contructor for PBIL operator.
         * 
         * @param objective_function_ -
         *          objective function which evaluate binary inidivduals
         * @param iterations -
         *          number of iterations after which the new populations is given
         * @param learning_rate -
         *          learning rate of the operator.<BR>
         *          Value must be <code> Double </code> in range <code> [0,1] </code>
         * @param mutation_probability -
         *          chance of random change in individuals.<BR>
         *          Value must be <code> Double </code> in range <code> [0,1] </code>
         * @param mutation_rate -
         *          how much the probability changes after evaluation.<BR>
         *          Value must be <code> Double </code> in range <code> [0,1] </code>
         * @param population_size -
         *          size of the evaluated population.<BR>
         *          Value must be positive <code> Integer </code>
         */
        public PBIL(ObjectiveFunction<BinaryVector> objective_function, int iterations, double learning_rate, 
            double mutation_probability, double mutation_rate, int population_size, int individualLenght, wevoRandom random)
        {

            // check if we give correct values
            if (mutation_rate < 0 || mutation_rate > 1)
            {
                throw new ArgumentException(
                    "PBIL accepts only Mutation_RATE as a parameter"
                        + " which must be a Double in range[0,1]");
            }
            theta3 = mutation_rate;
            if (mutation_probability < 0 || mutation_probability > 1)
            {
                throw new ArgumentException(
                    "PBIL accepts only Mutation_PROBABILITY as a parameter"
                        + " which must be a Double in range[0,1]");
            }
            theta2 = mutation_probability;

            if (learning_rate < 0 || learning_rate > 1)
            {
                throw new ArgumentException(
                    "PBIL accepts only LEARNING_RATE as a parameter"
                        + " which must be a Double in range[0,1]");
            }
            theta1 = learning_rate;

            if (population_size <= 0)
            {
                throw new ArgumentException(
                    "PBIL accepts only POPULATION_SIZE as a parameter"
                        + " which must be a positive Integer");
            }
            n = population_size;

            if (iterations <= 0)
            {
                throw new ArgumentException(
                    "PBIL accepts only iterations as a parameter"
                        + " which must be a positive Integer");
            }
            this.iterations = iterations;

            if (objective_function == null)
            {
                throw new ArgumentException(
                    "objective function cannot be null");
            }

            this.objective_function = objective_function;
            this.random = random;
            this.individualLenght = individualLenght;

            Init();
        }

        /**
         * Initial probability vector is set to 0.5.
         */
        private void initialProbabilityVector(int d)
        {
            probability_vector = new double[d];
            for (int i = 0; i < probability_vector.Length; i++)
            {
                probability_vector[i] = 0.5;
            }
        }

        /**
         * Generates population of given size in the contructor from probability vector
         * evaluated durning previous iterations.<BR>.
         * NOTE: Argument population is not used and should be set to null
         */
        public Population<BinaryVector> Apply(
            Population<BinaryVector> population)
        {
            /*
             * TODO this operator doesnt need population as argument
             */

            for (int i = 0; i < iterations; i++)
            {

                this.population = RandomPopulation();
                BinaryVector best = (BinaryVector)this.GetBestResult();

                // Iteration over bits in probability vector
                for (int k = 0; k < probability_vector.Length; k++)
                {
                    // Change probability_vector[k] by learning
                    probability_vector[k] = probability_vector[k] * (1 - theta1)
                        + (best.GetBit(k) ? 1.0 : 0) * theta1;

                    // Randomly choose if mutate or not mutate
                    if (random.NextDouble(0, 1) < theta2)
                    {
                        // Change probability_vector[k] by mutation
                        probability_vector[k] = probability_vector[k] * (1 - theta3)
                            + (random.NextProbableBoolean(0.5) ? 1.0 : 0)
                            * theta3;
                    }
                }

            }

            return this.population;
        }

        /**
         * Gets the best individual in current population
         */
        public BinaryVector GetBestResult()
        {
            BinaryVector best = population[0];

            for (int i = 1; i < population.Size(); i++)
            {
                if (objective_function(best) < objective_function(population[i]))
                {
                    best = population[i];
                }

            }
            return best;
        }

        /*
         * initializing default probability vector
         */
        private void Init()
        {
            initialProbabilityVector(individualLenght);
        }

        /*
         * generating random population from probability vector
         */
        private Population<BinaryVector> RandomPopulation()
        {
            Population<BinaryVector> pop = new Population<BinaryVector>();

            // Adding individual to population
            for (int i = 0; i < n; i++)
            {
                pop.AddIndividual(BinaryVector.Generate(random, individualLenght));
            }

            return pop;
        }
    }
}
