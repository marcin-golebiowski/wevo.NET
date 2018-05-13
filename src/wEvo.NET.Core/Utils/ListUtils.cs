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

namespace wevo.NET.Core.Utils
{
    /**
    * Common utilities for managing lists.
    */
    public static class ListUtils
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /**
        * Builds a list out of given set of elements.
        * @param <T> Type of the elements in a list.
        * @param elements Array of elements.
        * @return Converted list of elements.
        */
        public static List<T> BuildList<T>(params T[] elements)
        {
            List<T> list = new List<T>();
            foreach (T element in elements)
            {
                list.Add(element);
            }
            return list;
        }

        /**
        * Compares two arrays of doubles with given precision.
        * @param a1 First array to compare.
        * @param a2 Second array to compare.
        * @param precision Precision for each element.
        * @return Whether arrays are equal with given precision.
        */
        public static bool CompareArraysOfDoubles(double[] a1, double[] a2, double precision)
        {
            if (a1.Length != a2.Length)
            {
                return false;
            }
            for (int i = 0; i < a1.Length; i++)
            {
                if (Math.Abs(a1[i] - a2[i]) > precision)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
