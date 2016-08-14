﻿/*
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


using wevo.NET.Core;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Samples.ObjectiveFunctions
{
    internal class OneMax : ObjectiveFunction<BinaryVector>
    {
        public double Compute(BinaryVector individual)
        {
            int result = 0;
            int individual_dimension = individual.GetSize();
            for (int i = 0; i < individual_dimension; i++)
            {
                if (individual.GetBit(i))
                {
                    if (i % 2 == 0)
                    {
                        result += 1;
                    }
                    else
                    {
                        result -= 1;

                    }

                }
                else
                {
                    if (i % 2 == 1)
                    {
                        result += 1;
                    }
                    else
                    {
                        result -= 1;

                    }
                }
            }
            return result;
        }
    }
}
