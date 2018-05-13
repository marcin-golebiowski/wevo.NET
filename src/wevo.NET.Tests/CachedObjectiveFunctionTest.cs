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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Core.Tests.Utils
{
    /**
    * Test for {@link CachedObjectiveFunction}.
    */
    [TestClass]
    public class CachedObjectiveFunctionTest
    {
        /**
         * Sample objective function counting how many times it was called.
         * @author marcin.brodziak@gmail.com (Marcin Brodziak)
         */
        private class SampleObjectiveFunction : IObjectiveFunction<BinaryVector>
        {
            /** Number of times the function was called. */
            private int howManyTimesCalled = 0;

            public double Compute(BinaryVector individual)
            {
                return ++howManyTimesCalled;
            }
        }

        /** Tests if caching is done properly. */
        [TestMethod]
        public void testCaching()
        {
            var objectiveFunction = new SampleObjectiveFunction();

            // Magic Number off
            CachedObjectiveFunction<BinaryVector> function = new CachedObjectiveFunction<BinaryVector>(objectiveFunction, 100);
            // Magic Number on
            function.Precompute(new BinaryVector(10));
            Assert.AreEqual(function.Compute(new BinaryVector(10)), 1.0);

            function.Precompute(new BinaryVector(10));
            Assert.AreEqual(function.Compute(new BinaryVector(10)), 1.0);

            function.Precompute(new BinaryVector(11));
            Assert.AreEqual(function.Compute(new BinaryVector(11)), 2.0);
        }

        /** Tests if exception is thrown on empty cache. */
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testThrowsWithEmptyCache()
        {
            CachedObjectiveFunction<BinaryVector> function = new CachedObjectiveFunction<BinaryVector>(new SampleObjectiveFunction(), 100);
            Assert.AreEqual(1, function.Compute(new BinaryVector(10)));
        }

        /** Test of the cache expiration. */
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testCacheExpiration()
        {
            CachedObjectiveFunction<BinaryVector> function = new CachedObjectiveFunction<BinaryVector>(new SampleObjectiveFunction(), 1);
            function.Precompute(new BinaryVector(1));
            function.Precompute(new BinaryVector(2));

            Assert.AreEqual(function.Compute(new BinaryVector(2)), 2.0);

            function.Compute(new BinaryVector(1));
        }
    }
}
