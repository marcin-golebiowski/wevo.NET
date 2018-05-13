using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using wevo.NET.Individuals;
using wevo.NET.Utils;

namespace wevo.NET.Tests
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
