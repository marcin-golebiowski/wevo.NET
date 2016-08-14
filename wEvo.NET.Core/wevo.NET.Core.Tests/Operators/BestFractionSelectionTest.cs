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

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Operators;

namespace wevo.NET.Core.Tests.Operators
{
    /// <summary>
    /// Summary description for BestFractionSelectionTest
    /// </summary>
    [TestClass]
    public class BestFractionSelectionTest
    {
        [TestMethod]
        public void TestOperator()
        {
            List<BinaryVector> vectors = new List<BinaryVector>();
            vectors.Add(new BinaryVector(new bool[] { true, false }));
            vectors.Add(new BinaryVector(new bool[] { true, true, true }));

            Population<BinaryVector> p = new Population<BinaryVector>(vectors);
            BestFractionSelection<BinaryVector> op = new BestFractionSelection<BinaryVector>((BinaryVector v) => { return v.GetSize(); }, 0.5);
            p = op.Apply(p);

            Assert.AreEqual(p.GetIndividuals().Count, 2);
            Assert.AreEqual(p.GetIndividuals()[0], new BinaryVector(new bool[] { true, true, true }));
            Assert.AreEqual(p.GetIndividuals()[1], new BinaryVector(new bool[] { true, true, true }));
        }
    }
}
