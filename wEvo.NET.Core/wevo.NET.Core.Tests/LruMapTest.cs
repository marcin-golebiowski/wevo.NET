using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wEvo.NET.Core.Utils;

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
