using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Individuals;
using System.Collections.Generic;

namespace wevo.NET.Tests
{
    public abstract class PopulationEvaluatorTest
    {
        /**
        * Dummy individual only for testing if evaluators work fine.
        */
        public class DummyIndividual : Individual
        {
            /** Indicates whether first objective function was called. */
            private bool firstFunctionCalled;

            /** Indicates whether second objective function was called. */
            private bool secondFunctionCalled;

            /**
             * Mocks a call to the first objective function.
             * @return Dummy function value. 
             */
            public double CallFirstFunction()
            {
                this.firstFunctionCalled = true;
                return 0;
            }

            /**
             * Mocks a call to the second objective function.
             * @return Dummy function value.
             */
            public double CallSecondFunction()
            {
                this.secondFunctionCalled = true;
                return 0;
            }

            /** Checks whether both objective functions were called. */
            public void AssertBothFunctionsEvaluated()
            {
                Assert.IsTrue(firstFunctionCalled && secondFunctionCalled);
            }

            public override Individual Clone()
            {
                throw new NotImplementedException();
            }
        }

        /**
        * Tests whether both objective functions were called during evaluation.
        */
        [TestMethod]
        protected void TestObjectiveFunctions()
        {
            PopulationEvaluator<DummyIndividual> evaluator = GetEvaluator();
            Population<DummyIndividual> population = CreatePopulation();
            evaluator.Apply(population);
            AssertObjectiveFunctionsEvaluatedOnEachIndividual(population);
        }

        /**
         * Self-explanatory.
         * @param population Population to evaluate.
         */
        private void AssertObjectiveFunctionsEvaluatedOnEachIndividual(
            Population<DummyIndividual> population)
        {
            foreach (DummyIndividual individual in population.GetIndividuals())
            {
                individual.AssertBothFunctionsEvaluated();
            }
        }

        /**
         * Creates a population of dummy individuals for tests.
         * @return Population created.
         */
        private Population<DummyIndividual> CreatePopulation()
        {
            List<DummyIndividual> list = new List<DummyIndividual>();
            list.Add(new DummyIndividual());
            return new Population<DummyIndividual>(list);
        }

        /**
         * Creates a list of objective functions for tests.
         * @return List of objective functions to be called in population
         * evaluation.
         */
        public List<CachedObjectiveFunction<DummyIndividual>> CreateObjectiveFunctions()
        {
            List<CachedObjectiveFunction<DummyIndividual>> objectiveFunctions = new List<CachedObjectiveFunction<DummyIndividual>>();

            objectiveFunctions.Add(new CachedObjectiveFunction<DummyIndividual>((DummyIndividual i) => { i.CallFirstFunction(); return 0; }, 100));
            objectiveFunctions.Add(new CachedObjectiveFunction<DummyIndividual>((DummyIndividual i) => { i.CallSecondFunction(); return 0; }, 100));

            return objectiveFunctions;
        }

        /**
         * Returns evaluator instance to be tested. Each test has to return
         * its own evaluator, obviously.
         * @return Evaluator to be tested.
         */
        public abstract PopulationEvaluator<DummyIndividual> GetEvaluator();
    }
}
