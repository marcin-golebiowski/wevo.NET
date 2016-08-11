using System;
using System.Collections.Generic;
using wEvo.NET.Core.Individuals;
using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core.Operators.Natural
{
    /**
    * For each gene of each individual with given probability a mutation occurs.
    * The mutation draws  a number from gaussian distribution with standard 
    * deviation sigma and mean 0, adds it to the value of the gene.
    * @author Marcin Brodziak (marcin.brodziak@gmail.com)
    */
    public class GaussianAdditionMutation : Operator<NaturalVector>
    {
        /** Random number generator. */
        private WevoRandom random;
 
        /** Sigma in gaussian sampling. */
        private double sigma;

        /** Probability of mutation on a single gene. */
        private double probability;

        /**
        * Creates a mutation for natural number individuals. For each gene of each
        * individual with given probability a mutation occurs. The mutation draws 
        * a number from gaussian distribution with standard deviation sigma and
        * mean 0, adds it to the value of the gene
        * and rounds to nearest natural number.
        * @param random Random number genarator.
        * @param probability Probability of mutation for each gene 
        *     of each individual.
        * @param sigma Standard deviation in gaussian distribution.
        */
        public GaussianAdditionMutation(WevoRandom random, double probability, double sigma)
        {
            this.random = random;
            this.probability = probability;
            this.sigma = sigma;
        }

        public Population<NaturalVector> Apply(Population<NaturalVector> population)
        {
            List<NaturalVector> result = new List<NaturalVector>();
            List<NaturalVector> individuals = population.GetIndividuals();
            foreach (NaturalVector parent in individuals)
            {
                NaturalVector offspring = new NaturalVector(parent.GetSize());
                for (int i = 0; i < offspring.GetSize(); i++)
                {
                    double delta = 0;
                    if (random.NextDouble(0.0, 1.0) < probability)
                    {
                        delta = random.NextGaussian() * sigma;
                    }
                    offspring.SetValue(i, (long)Math.Round(parent.GetValue(i) + delta));
                }
                result.Add(offspring);
            }
            return new Population<NaturalVector>(result);
        }
    }
}
