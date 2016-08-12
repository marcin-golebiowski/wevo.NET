using System.Collections.Generic;
using System.Text;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Individuals
{
    /**
    * Individual representing permutation.
    * @author Karol Stosiek (karol.stosiek@gmail.com)
    * @author Michal Anglart (michal.anglart@gmail.com)
    */
    public class Permutation : Individual
    {
        /** Chromosome of the individual. */
        private int[] values;

        /**
         * Constructor accepting permutation size.
         * @param size Size of the individual.
         */
        public Permutation(int size)
        {
            this.values = new int[size];
        }

        /**
         * Constructor accepting genes. Genes have to form a valid permutation.
         * @param genes Genes to be set.
         */

        public Permutation(int[] genes)
        {
            this.values = genes;
        }

        /**
         * Copying constructor.
         * @param individual Individual to be copied.
         */
        public Permutation(Permutation individual) : this(individual.GetValues())
        {
        }

        /**
         * Returns chromosome of the individual.
         * @return Chromosome of the individual.
         */
        public int[] GetValues()
        {
            return this.values;
        }

        /**
         * Returns gene value at given position.
         * @param position Position.
         * @return Gene value at given position.
         */
        public int GetValue(int position)
        {
            return values[position];
        }

        /**
         * Transposes genes on given positions.
         * @param i First gene position. Must be within individual size limit.
         * @param j Second gene position. Must be within individual size limit.
         */
        public void Transpose(int i, int j)
        {
            int geneValueI = values[i];
            int geneValueJ = values[j];
            values[j] = geneValueI;
            values[i] = geneValueJ;
        }

        /**
         * Returns size of the individual.
         * @return Size of the individual. 
         */
        public int GetSize()
        {
            return values.Length;
        }

        /**
         * Returns string representation of this permutation.
         * @return String representation of this permutation.
         */
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<");
            foreach (long value in values)
            {
                if (builder.Length > 1)
                {
                    builder.Append(", ");
                }
                builder.Append(value);
            }
            builder.Append(">");
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return Arrays<int>.GetHashCode(values);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Permutation)) {
                return false;
            }

            Permutation that = (Permutation)obj;
            return Arrays<int>.EqualsArray(this.values, that.values);
        }

        /**
         * Generates single permutation individual.
         * @param generator Random number generator.
         * @param permutationSize Size of the generated individual.
         * @return Randomly generated permutation individual.
         */
        public static Permutation Generate(
            wevoRandom generator,
            int permutationSize)
        {
            List<int> genesPickPool = new List<int>();
            for (int j = 0; j < permutationSize; j++)
            {
                genesPickPool.Add(j);
            }

            int[] chromosome = new int[permutationSize];
            for (int position = 0; position < permutationSize; position++)
            {
                int pickedElement = generator.NextInt(0, genesPickPool.Count);

                chromosome[position] = genesPickPool[pickedElement];
                genesPickPool.RemoveAt(pickedElement);
            }
            return new Permutation(chromosome);
        }

        /**
         * Generates initial population with random permutation individuals.
         * @param generator Random number generator.
         * @param individualSize Size of each individual in the generated population.
         * @param individuals Number of individuals in the population.
         * @return Population of permutation individuals.
         */
        public static Population<Permutation> generatePopulationOfRandomIndividuals(
            wevoRandom generator,
            int individualSize,
            int individuals)
        {
            List<Permutation> result = new List<Permutation>();
            for (int i = 0; i < individuals; i++)
            {
                result.Add(Permutation.Generate(generator, individualSize));
            }

            return new Population<Permutation>(result);
        }
    }
}
