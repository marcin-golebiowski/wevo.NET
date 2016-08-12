using System.Collections.Generic;

namespace wevo.NET.Core
{
    /**
    * Encapsulates the list of operators, evaluators, termination conditions, etc.
    * @author Marcin Brodziak (marcin@nierobcietegowdomu.pl)
    *
    * @param <T> Type of the individuals to be evolved.
    */
    public class Algorithm<T> where T : Individuals.Individual
    {
       
        /** Has the algorithm finished. */
        private bool isFinished;

        /** Population on which algorithms works. */
        private Population<T> population;

        /** List of operators that the algorithm is based on. */
        private List<Operator<T>> operators;

        /** Forces algorithm to reset termination condition. */
        private bool shouldBeReset = false;

        /**
        * Creates the Algorithm that will form the basis for the evolution.
        * @param population Initial population.
        */
        public Algorithm(Population<T> population)
        {
            this.population = population;
            operators = new List<Operator<T>>();
        }

        /**
         * Adds evaluation point in the algorithm.
         * @param evaluator Evaluator to use in the algorithm.
         */
        public void AddEvaluationPoint(PopulationEvaluator<T> evaluator)
        {
            operators.Add(evaluator);
        }

        /**
         * Adds exit point to the algorithm, if the condition is met, the algorithm
         * terminates at the earliest possible point.
         * @param terminationCondition Termination condition which, when met, 
         *      terminates the execution of the algorithm. 
         */
        public void AddExitPoint(TerminationCondition<T> terminationCondition)
        {
            operators.Add(new ExitPoint(this, terminationCondition));
        }

        /**
        * Adds an operator to the algorithm.
        * @param operator Operator to be added.
        */
        public void AddOperator(Operator<T> op)
        {
            operators.Add(op);
        }

        /**
        * Runs the algorithm.
        */
        public void Run()
        {
            long iterationNo = 0;
            while (true)
            {
                iterationNo++;
                foreach (Operator<T> op in operators)
                {
                    if (this.isFinished)
                    {
                        return;
                    }

                    population = op.Apply(population);
                }
            }
        }

        /**  Sets true to flag which forces termination condition to reset. */
        public void Reset()
        {
            shouldBeReset = true;
            isFinished = false;
        }

        /**
         * Returns the population at the end of the algorithm.
         * @return The population at the end of the algorithm.
         */
        public Population<T> GetPopulation()
        {
            return population;
        }

        /**
         * Sets population object.
         * @param population Population object to set.
         */
        public void SetPopulation(Population<T> population)
        {
            this.population = population;
        }

        internal class ExitPoint : Operator<T>
        {
            private Algorithm<T> algorithm;
            private TerminationCondition<T> terminationCondition;

            public ExitPoint(Algorithm<T> algorithm, TerminationCondition<T> terminationCondition)
            {
                this.algorithm = algorithm;
                this.terminationCondition = terminationCondition;
            }

            public Population<T> Apply(Population<T> populationInternal)
            {
                if (this.algorithm.shouldBeReset)
                {
                    this.algorithm.shouldBeReset = false;
                    terminationCondition.Reset();
                }
                if (terminationCondition.IsSatisfied(populationInternal))
                {
                    this.algorithm.isFinished = true;
                }
                return populationInternal;
            }
        }
    }
}
        


