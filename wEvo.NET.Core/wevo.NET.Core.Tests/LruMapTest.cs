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

            try
            {
                m.Get("a");
                Assert.Fail();
            }
            catch
            {
            }
        }
    }
}
