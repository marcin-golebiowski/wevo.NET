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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Core.Tests.Individuals
{
    [TestClass]
    public class BinaryVectorTest
    {
        /** Tests if initial individual is filled with zeros only. */
        [TestMethod]
        public void testZeroedAtStart()
        {
            int size = 3;
            BinaryVector bi = new BinaryVector(size);
            Assert.AreEqual(bi.GetSize(), size);
            for (int i = 0; i < bi.GetSize(); i++)
            {
                Assert.AreEqual(false, bi.GetBit(i));
            }
        }

        /** Self-explanatory. */
        [TestMethod]
        public void testSettingFromArray()
        {
            BinaryVector bi = new BinaryVector(new bool[] { true, false });
            Assert.AreEqual(2, bi.GetSize());
            Assert.AreEqual(true, bi.GetBit(0));
            Assert.AreEqual(false, bi.GetBit(1));
        }

        /** Self-explanatory. */
        [TestMethod]
        public void testSetBit()
        {
            BinaryVector bi = new BinaryVector(new bool[] { true, false });
            bi.SetBit(0, false);
            Assert.AreEqual(false, bi.GetBit(0));

            bi.NegateBit(1);
            Assert.AreEqual(true, bi.GetBit(1));
        }

        /** Self-explanatory. */
        [TestMethod]
        public void testToString()
        {
            BinaryVector bi = new BinaryVector(new bool[] { true, false, true });
            Assert.AreEqual("101", bi.ToString());
        }

        /**
         * Tests if hash code of an individual is exactly the same iff individuals
         * are equal.
         */
        [TestMethod]
        public void testHashCodeConformsEquals()
        {
            BinaryVector binary1 = new BinaryVector(new bool[] { true, true, true });
            BinaryVector binary2 = new BinaryVector(new bool[] { true, true, true });
            BinaryVector binary3 = new BinaryVector(new bool[] { true, false, true });

            Assert.AreEqual(binary1.GetHashCode(), binary2.GetHashCode());
            Assert.AreEqual(binary1, binary2);

            Assert.IsFalse(binary1.GetHashCode() == binary3.GetHashCode());
            Assert.IsFalse(binary1.Equals(binary3));
        }
    }
}
