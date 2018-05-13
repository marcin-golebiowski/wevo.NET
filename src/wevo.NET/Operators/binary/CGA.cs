using System;
using wevo.NET;
using wevo.NET.Individuals;
using wevo.NET.Utils;

namespace wevo.NET.Operators.Binary
{
    /**
    * Sample implementation of CGA operator for BinaryVector.
    */
    public class CGA : Operator<BinaryVector>
    {
        /**
         * indicated how much the vector of probability changes when mutated
         */
        private double learning_rate;

        /// <summary>
        /// Length of individuals
        /// </summary>
        private int individualLength;

        /**
         * number of iteration used to generate new opulation
         */
        private int iterations;

        /**
         * Two individuals, four variables for clarity.
         */
        private BinaryVector individual1, individual2, best_individual, worst_individual;

        /**
         * Probability vector that works is used throughout CGA.
         */
        private double[] probability_vector;


        private wevoRandom random;
        private ObjectiveFunction<BinaryVector> objectiveFunction;

        /**
         * Default constructor of CGA operator.
         * 
         * @param objective_function_ -
         *          objective function used to evaluate binary individuals
         * @param learning_rate_ -
         *          number of iterations before generating population with apply().
         * @param learning_rate_ -
         *          how much muttation affects vector of probability.
         * @param population_size_ -
         *          size of populaton generated from vector of probability
         * @param solution_space_ -
         *          solution space
         */
        public CGA(int iterations, double learning_rate, int individualLength, ObjectiveFunction<BinaryVector> objectiveFunction, wevoRandom random)
        {
            if (learning_rate < 0 || learning_rate > 1)
            {
                throw new ArgumentException("Learing rate must be a Double in range[0,1]");
            }

            if (iterations <= 0)
            {
                throw new ArgumentException("Iterations must be a positive Integer");
            }

            this.iterations = iterations;
            this.learning_rate = learning_rate;
            this.random = random;
            this.individualLength = individualLength;
            this.objectiveFunction = objectiveFunction;

            Init();
        }

        /* TODO we don't use variable population in function header!! What to do? */
        /**
         * Generates population of given size in the contructor from probability vector
         * evaluated durning previous iterations.<BR>
         * NOTE: Argument population is not used and should be set to null
         */
        public Population<BinaryVector> Apply(Population<BinaryVector> population)
        {
            /*
             * Generation of two individuals with apropriate objective function set.
             * This is not very convenient that a user has to specify objective function
             * each time individual is constructor, but this is a design choice as
             * sometimes objective function changes over time.
             */

            for (int j = 0; j < iterations; j++)
            {

                individual1 = BinaryVector.Generate(random, individualLength);
                individual2 = BinaryVector.Generate(random, individualLength);

                /*
                 * Choosing better and worse of the two.
                 */
                if (objectiveFunction(individual1) > objectiveFunction(individual2))
                {
                    best_individual = individual1;
                    worst_individual = individual2;
                }
                else
                {
                    best_individual = individual2;
                    worst_individual = individual1;
                }

                /*
                 * We update each position in probabilty vector by a theta factor in an
                 * apropriate way if individuals differ on a given position.
                 */

                for (int i = 0; i < individualLength; i++)
                {
                    if (best_individual[i] && !worst_individual[i])
                    {
                        probability_vector[i] = probability_vector[i] + learning_rate;
                        if (probability_vector[i] > 1)
                        {
                            probability_vector[i] = 1;
                        }
                    }
                    else if (!best_individual[i] && worst_individual[i])
                    {
                        probability_vector[i] = probability_vector[i] - learning_rate;
                        if (probability_vector[i] < 0)
                        {
                            probability_vector[i] = 0;
                        }
                    }
                }

            }
            return RandomPopulation(population.GetSize());
        }

        /*
         * Generating random population from probability vector
         */
        private Population<BinaryVector> RandomPopulation(int population_size)
        {
            Population<BinaryVector> randomPopulation = new Population<BinaryVector>();

            // Adding individual to population
            for (int i = 0; i < population_size; i++)
            {
                randomPopulation.AddIndividual(BinaryVector.Generate(random, probability_vector, individualLength));
            }

            return randomPopulation;
        }

        /*
         * initializing default probability vector
         */
        private void Init()
        {
            InitialProbabilityVector(individualLength);
        }

        /**
         * Initial probability vector is set to 0.5.
         */
        private void InitialProbabilityVector(int length)
        {
            probability_vector = new double[length];
            for (int i = 0; i < probability_vector.Length; i++)
            {
                probability_vector[i] = 0.5;
            }
        }
    }
}
