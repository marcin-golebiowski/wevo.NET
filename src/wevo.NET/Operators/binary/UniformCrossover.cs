using System.Collections.Generic;
using wevo.NET.Individuals;
using wevo.NET.Utils;

namespace wevo.NET.Operators.Binary
{
    public class UniformCrossover : Operator<BinaryVector>
    {
        /** Head-tail probability. */
        private static double DEFAULT_PROBABILITY = 0.5;

        /** Random number generator. */
        private wevoRandom random;

        /**
        * Creates a uniform crossover object.
        * @param random Random to be used
        */   
        public UniformCrossover(wevoRandom random)
        {
            this.random = random;
        }

        public Population<BinaryVector> Apply(Population<BinaryVector> population)
        {
            var enumerator = population.GetIndividuals().GetEnumerator();
            List<BinaryVector> output = new List<BinaryVector>();

            while (enumerator.MoveNext())
            {
                BinaryVector b1 = enumerator.Current;
                if (enumerator.MoveNext())
                {
                    BinaryVector b2 = enumerator.Current;

                    bool[] o1 = new bool[b1.GetSize()];
                    bool[] o2 = new bool[b1.GetSize()];
                    for (int i = 0; i < b1.GetSize(); i++)
                    {
                        if (random.NextDouble(0.0, 1.0) < DEFAULT_PROBABILITY)
                        {
                            o1[i] = b1.GetBit(i);
                            o2[i] = b2.GetBit(i);
                        }
                        else
                        {
                            o1[i] = b2.GetBit(i);
                            o2[i] = b1.GetBit(i);
                        }
                    }
                    output.Add(new BinaryVector(o1));
                    output.Add(new BinaryVector(o2));
                }
            }

            return new Population<BinaryVector>(output);
        }
    }
}
