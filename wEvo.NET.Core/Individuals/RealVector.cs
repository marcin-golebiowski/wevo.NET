/*
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
    * Vector of real numbers.
    */
    public class RealVector : Individual
    {
        /** Binary values in the vector. */
        private double[] values;

        /**
         * Constructs {@link RealVector}.
         * @param size Size of the vector.
         */
        public RealVector(int size) : this(new double[size])
        {
        }

        /**
         * Constructs RealVector from a list of real values.
         * @param list Real values that form the basis for this individual.
         */
        public RealVector(double[] list)
        {
            values = new double[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                values[i] = list[i];
            }
        }

        /**
         * Sets the ith value in the vector.
         * @param i Which value to set.
         * @param value What it set to.
         */
        public void SetValue(int i, double value)
        {
            values[i] = value;
        }

        /**
         * Returns ith value in the vector.
         * @param i Which value to return.
         * @return Value of the bit.
         */
        public double GetValue(int i)
        {
            return values[i];
        }

        /**
         * Returns size of the individual.
         * @return Size of the individual.
         */
        public int GetSize()
        {
            return values.Length;
        }
        
        public override int GetHashCode()
        {
            return Arrays<double>.GetHashCode(values);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is RealVector)) {
                return false;
            }

            RealVector that = (RealVector)obj;
            return Arrays<double>.EqualsArray(this.values, that.values);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            foreach (double b in values)
            {
                if (sb.Length > 1)
                {
                    sb.Append(", ");
                }
                sb.Append(b);
            }
            sb.Append(">");
            return sb.ToString();
        }

        /**
         * Array of values in the individual.
         * @return Array of doubles in the individual.
         */
        public double[] GetValues()
        {
            return values;
        }

        /**
         * Generates random individual of given length using given generator.
         * Values in each chromosome is taken randomly with uniform distribution from
         * given range.
         * @param generator Random number generator.
         * @param individualLength Length of individual.
         * @param lowerLimit Lower range limit.
         * @param upperLimit Upper range limit.
         * @return Real individual of given length.
         */
        public static RealVector Generate(
            wevoRandom generator,
            int individualLength,
            int lowerLimit,
            int upperLimit)
        {

            double[] chromosome = new double[individualLength];
            for (int j = 0; j < chromosome.Length; j++)
            {
                chromosome[j] = generator.NextDouble(lowerLimit, upperLimit);
            }
            return new RealVector(chromosome);
        }

        /**
         * Generates initial population with random individuals of given length
         * and given size. Values in each chromosome is limited by given range
         * limits.
         * @param generator Random numbers generator.
         * @param individualLength Length of each individual.
         * @param individuals Number of individuals in population.
         * @param lowerLimit Lower range limit.
         * @param upperLimit Upper range limit.
         * @return Initial population.
         */
        public static Population<RealVector> GeneratePopulationOfRandomRealIndividuals(
            wevoRandom generator,
            int individualLength,
            int individuals,
            int lowerLimit,
            int upperLimit)
        {
            List<RealVector> result = new List<RealVector>();
            for (int i = 0; i < individuals; i++)
            {
                result.Add(RealVector.Generate(generator, individualLength, lowerLimit, upperLimit));
            }
            return new Population<RealVector>(result);
        }
    }
}
