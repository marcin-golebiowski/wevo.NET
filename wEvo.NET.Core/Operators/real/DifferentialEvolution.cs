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
