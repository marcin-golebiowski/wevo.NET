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
