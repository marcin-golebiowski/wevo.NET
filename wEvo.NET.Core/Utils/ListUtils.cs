using System;
using System.Collections.Generic;

namespace wEvo.NET.Core.Utils
{
    public class ListUtils
    {
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
