using System;
using System.Collections.Generic;
using System.Text;
using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core.Individuals
{
    class BinaryVector : Individual
    {
        /** Binary values in the vector. */
        private bool[] bits;

        /**
         * Constructs BinaryVector.
         * @param size Size of the vector.
         */
        public BinaryVector(int size)
        {
            this.bits = new bool[size];
        }

        /**
         * Constructs BinaryVector from a list of boolean values.
         * @param list Boolean values that form the basis for this individual.
         */
        public BinaryVector(bool[] list)
        {
            bits = new bool[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                bits[i] = list[i];
            }
        }

        /**
         * Sets the ith bit in the vector.
         * @param i Which bit to set.
         * @param value What it set to.
         */
        public void SetBit(int i, bool value)
        {
            bits[i] = value;
        }

        /**
         * Returns ith bit in the vector.
         * @param i Which bit to return.
         * @return Value of the bit.
         */
        public bool GetBit(int i)
        {
            return bits[i];
        }

        /**
         * Returns copy of the individual with ith bit set to value.
         * @param i Which bit to negate.
         */
        public void NegateBit(int i)
        {
            this.SetBit(i, !this.GetBit(i));
        }

        /**
         * Returns size of the individual.
         * @return Size of the individual.
         */
        public int GetSize()
        {
            return bits.Length;
        }

        /** {@inheritDoc} */

        public override int GetHashCode()
        {
            return Arrays<bool>.GetHashCode(bits);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is BinaryVector))
            {
                return false;
            }

            BinaryVector that = (BinaryVector)obj;
            return Arrays<bool>.EqualsArray(this.bits, that.bits);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (bool b in bits)
            {
                sb.Append(b ? 1 : 0);
            }
            return sb.ToString();
        }

        /**
         * Generates a random individual of given length using given generator.
         * @param generator Random number generator.
         * @param individualLength Size of the individual.
         * @return Binary individual of given length.
         */
        public static BinaryVector Generate(WevoRandom generator, int individualLength)
        {
            bool[] chromosome = new bool[individualLength];
            for (int j = 0; j < chromosome.Length; j++)
            {
                chromosome[j] = generator.NextBoolean();
            }
            return new BinaryVector(chromosome);
        }

        /**
         * Generates initial population with random binary individuals.
         * @param generator Random number generator.
         * @param individualLength Size of the individual.
         * @param individuals Number of individuals to generate.
         * @return List of binary individuals that will form the basis for first
         * iteration of the algorithm.
         */
        public static Population<BinaryVector> GeneratePopulationOfRandomBinaryIndividuals(WevoRandom generator, int individualLength, int individuals)
        {
            List<BinaryVector> result = new List<BinaryVector>();
            for (int i = 0; i < individuals; i++)
            {
                result.Add(BinaryVector.Generate(generator, individualLength));
            }
            return new Population<BinaryVector>(result);
        }
    }
}
