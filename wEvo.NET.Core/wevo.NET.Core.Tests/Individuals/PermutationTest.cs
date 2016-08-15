using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Tests.Individuals
{
    [TestClass]
    public class PermutationTest
    {
        /** Self-explanatory. */
        [TestMethod]
        public void testIndividualGenerator()
        {
            int individualSize = 10;
            Permutation individual = Permutation.Generate(new dotNetRandom(), individualSize);
            assertGenesFormPermutation(individual.GetValues());
        }

        /**
         * Asserts that each gene from pool [0, individualSize) exists only once.
         * @param genes Chromosome to check.
         */
        private void assertGenesFormPermutation(int[] genes)
        {
            int chromosomeLength = genes.Length;
            int[] genesCount = new int[chromosomeLength];
            for (int i = 0; i < chromosomeLength; i++)
            {
                genesCount[genes[i]]++;
            }

            for (int i = 0; i < chromosomeLength; i++)
            {
                Assert.AreEqual(genesCount[i], 1);
            }
        }

        /**
         * Tests if hash code of an individual is exactly the same iff individuals
         * are equal.
         */
        [TestMethod]
        public void testHashCodeConformsEquals()
        {
            Permutation permutation1 = new Permutation(new int[] { 0, 1, 2 });
            Permutation permutation2 = new Permutation(new int[] { 0, 1, 2 });
            Permutation permutation3 = new Permutation(new int[] { 1, 2, 0 });

            Assert.AreEqual(permutation1.GetHashCode(), permutation2.GetHashCode());
            Assert.AreEqual(permutation1, permutation2);

            Assert.IsFalse(permutation1.GetHashCode() == permutation3.GetHashCode());
            Assert.IsFalse(permutation1.Equals(permutation3));
        }
    }
}
