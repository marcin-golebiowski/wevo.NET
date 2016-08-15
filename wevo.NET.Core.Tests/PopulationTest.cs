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
using Moq;
using System.Collections.Generic;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Tests
{
    [TestClass]
    public class PopulationTest
    {
        private Population<BinaryVector> originalPopulation;
        private Population<BinaryVector> originalPopulationClone;

        [TestInitialize]
        public void Setup()
        {
            originalPopulation = new Population<BinaryVector>(
                new List<BinaryVector> {
                    new BinaryVector(1), new BinaryVector(2), new BinaryVector(3) });

            originalPopulationClone = new Population<BinaryVector>(
                new List<BinaryVector> {
                    new BinaryVector(1), new BinaryVector(2), new BinaryVector(3) });
        }


        /** Tests population shuffling. */
        [TestMethod]
        public void testPopulationShuffling()
        {
            Population<BinaryVector> expectedPopulation = new Population<BinaryVector>(new List<BinaryVector> {
                new BinaryVector(3),
                new BinaryVector(2),
                new BinaryVector(1)});

            var randomMock = new Mock<wevoRandom>();

            randomMock.Setup(m => m.NextInt(0, 3)).Returns(2);
            randomMock.Setup(m => m.NextInt(0, 2)).Returns(1);
            randomMock.Setup(m => m.NextInt(0, 1)).Returns(0);

            Population<BinaryVector> shuffleResult = Population<BinaryVector>.Shuffle(randomMock.Object, originalPopulation);
            randomMock.Verify(m => m.NextInt(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(3));

            Assert.AreEqual(shuffleResult, expectedPopulation);
            assertThatOriginalPopulationWasIntact(originalPopulation);
        }

        /** Tests population shuffling. */
        [TestMethod]
        public void testPopulationShufflingWithRepeatingIndex()
        {
            Population<BinaryVector> expectedPopulation = new Population<BinaryVector>(new List<BinaryVector> {
                new BinaryVector(2),
                new BinaryVector(3),
                new BinaryVector(1)});

            var randomMock = new Mock<wevoRandom>();

            randomMock.Setup(m => m.NextInt(0, 3)).Returns(1);
            randomMock.Setup(m => m.NextInt(0, 2)).Returns(1);
            randomMock.Setup(m => m.NextInt(0, 1)).Returns(0);

            Population<BinaryVector> shuffleResult = Population<BinaryVector>.Shuffle(randomMock.Object, originalPopulation);
            randomMock.Verify(m => m.NextInt(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(3));

            Assert.AreEqual(shuffleResult, expectedPopulation);
            assertThatOriginalPopulationWasIntact(originalPopulation);
        }

        /**
         * Assert that the original population was not changed during execution
         * of the tested method.
         * @param population Original population that fed the tested method.
         */
        private void assertThatOriginalPopulationWasIntact(
            Population<BinaryVector> population)
        {
            Assert.AreEqual(population, originalPopulationClone);
        }

        /** Tests removing random individual from the population. */
        [TestMethod]
        public void testRandomIndividualRemoval()
        {
            Population<BinaryVector> expectedPopulation = new Population<BinaryVector>(new List<BinaryVector> {
                new BinaryVector(1),
                new BinaryVector(3)});

            var randomMock = new Mock<wevoRandom>();

            randomMock.Setup(m => m.NextInt(0, 3)).Returns(1);

            Population<BinaryVector> removalResult = Population<BinaryVector>.RemoveRandomIndividual(randomMock.Object, originalPopulation);
            randomMock.Verify(m => m.NextInt(0, 3), Times.Once);

            Assert.AreEqual(removalResult, expectedPopulation);

            assertThatOriginalPopulationWasIntact(originalPopulation);

        }
    }
}
