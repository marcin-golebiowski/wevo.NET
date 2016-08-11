using System.Collections.Generic;
using wEvo.NET.Core.Individuals;

namespace wEvo.NET.Core.Operators.Natural
{
    public class IntervalCutoff : Operator<NaturalVector>
    {
        /** Minimum allowed value for a gene. */
        private int min;

        /** Maximum allowed value for a gene. */
        private int max;

        /**
         * Creates a take-back-to-given-interval operator, which for each gene 
         * of the individual if the value is smaller than min, replaces it 
         * with min and if it's larger than max replaces it with max.
         * @param min Minimum value.
         * @param max Maximum value.
         */
        public IntervalCutoff(int min, int max)
        {
            this.min = min;
            this.max = max;
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
                    if (parent.GetValue(i) < min)
                    {
                        offspring.SetValue(i, min);
                    }
                    else if (parent.GetValue(i) > max)
                    {
                        offspring.SetValue(i, max);
                    }
                    else
                    {
                        offspring.SetValue(i, parent.GetValue(i));
                    }
                }
                result.Add(offspring);
            }
            return new Population<NaturalVector>(result);
        }
    }
}
