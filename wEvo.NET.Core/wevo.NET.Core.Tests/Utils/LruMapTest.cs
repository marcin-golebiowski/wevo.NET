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
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Tests
{
    [TestClass]
    public class LruMapTest
    {
        [TestMethod]
        public void LruMap1()
        {
            LruMap<string, int> m = new LruMap<string, int>(2);

            m.Add("a", 1);
            m.Add("b", 2);

            Assert.AreEqual(1, m.Get("a"));
            Assert.AreEqual(2, m.Get("b"));

            m.Add("c", 3);

            Assert.IsFalse(m.ContainsKey("a"));
        }

        [TestMethod]
        public void TestAddingObjects()
        {
            // Magic Number off
            LruMap<string, int> map = new LruMap<string, int>(3);
            map.Add("a", 1);
            map.Add("B", 2);
            map.Add("c", 3);
            map.Add("d", 4);
            // Magic Number on
            Assert.IsTrue(map.ContainsKey("c"));
            Assert.IsFalse(map.ContainsKey("a"));
        }
    }
}
