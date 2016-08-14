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

namespace wevo.NET.Core.Individuals
{
    /**
    * Vector of binary numbers.
    */
    public class BinaryVector : Individual
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

        public bool this[int index]
        {
            get
            {
                return GetBit(index);
            }

            set
            {
                SetBit(index, value);
            }
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
        public static BinaryVector Generate(wevoRandom generator, int individualLength)
        {
            bool[] chromosome = new bool[individualLength];
            for (int j = 0; j < chromosome.Length; j++)
            {
                chromosome[j] = generator.NextBoolean();
            }
            return new BinaryVector(chromosome);
        }

        public static BinaryVector Generate(wevoRandom generator, double[] probability_vector, int individualLength)
        {
            if (probability_vector.Length != individualLength)
            {
                throw new ArgumentException(
                    "Probability vector dimension must be equal to space dimension");
            }

            BinaryVector individual = new BinaryVector(individualLength);
            for (int i = 0; i < individualLength; i++)
            {
                individual[i] = generator.NextProbableBoolean(probability_vector[i]);
            }

            return individual;
        }

        /**
         * Generates initial population with random binary individuals.
         * @param generator Random number generator.
         * @param individualLength Size of the individual.
         * @param individuals Number of individuals to generate.
         * @return List of binary individuals that will form the basis for first
         * iteration of the algorithm.
         */
        public static Population<BinaryVector> GeneratePopulationOfRandomBinaryIndividuals(wevoRandom generator, int individualLength, int individuals)
        {
            List<BinaryVector> result = new List<BinaryVector>();
            for (int i = 0; i < individuals; i++)
            {
                result.Add(BinaryVector.Generate(generator, individualLength));
            }
            return new Population<BinaryVector>(result);
        }

        public override Individual Clone()
        {
            BinaryVector clone = new BinaryVector(bits);
            return clone;
        }
    }
}
