using System;
using System.Collections.Generic;
using wevo.NET.Utils;

namespace wevo.NET.Operators.Reporters
{
    public class BestIndividualAndBasicStats<T> : Operator<T> where T : Individuals.Individual
    {
        /** Objective function that we're trying to optimize. */
        private List<ObjectiveFunction<T>> objFunction;

        /** Iteration number. */
        private int iterationNumber;

        /** Last time this operator was executed. */
        private long lastExecutionTime = 0;

        /** Interpretation of the individual. */
        private Interpretation<T> interpretation;

        /** Time measurement utility. */
        private wevoClock clock;

        private Logger logger = new Logger();

        /**
        * Creates reporter that reports basic statistics about the population
        * and prints out the best individual.
        * @param objFunction Objective function we're optimizing.
        * @param logger Logger to which output is written.
        * @param interpretation Interpretation of the individual. May be null
          if raw output is enough.
        * @param newClock Time measurement utility.
        */
        public BestIndividualAndBasicStats(List<ObjectiveFunction<T>> objFunction, Interpretation<T> interpretation, wevoClock newClock)
        {
            this.objFunction = objFunction;
            this.interpretation = interpretation;
            this.clock = newClock;
        }

        /**
         * Creates reporter that reports basic statistics about the population
         * and prints out the best individual.
         * @param objFunction Objective function we're optimizing.
         * @param interpretation Interpretation of the individual. May be null
           if raw output is enough.
         This is deprecated. Please switch to using constructor that has all
         parameters injected.
         */
        public BestIndividualAndBasicStats(List<ObjectiveFunction<T>> objFunction, Interpretation<T> interpretation)
        {
            this.objFunction = objFunction;
            this.interpretation = interpretation;
            this.clock = new SystemClock();
        }

        public Population<T> Apply(Population<T> population)
        {
            Dictionary<ObjectiveFunction<T>, Double> meanObjectiveFunctionValues = new Dictionary<ObjectiveFunction<T>, Double>();

            T bestIndividual = FindBestIndividualAndComputeSumOfObjFunctionValues(population, meanObjectiveFunctionValues);

            logger.Info("-");
            logger.Info("Iteration " + iterationNumber++);
            logger.Info("Best individual " + Interprete(bestIndividual));
            logger.Info("Population size " + population.Size());
            foreach (ObjectiveFunction<T> o in objFunction)
            {
                logger.Info("Objective value of " + o + " for best individual is " + o(bestIndividual));
                logger.Info("Mean value for " + o + " is " + meanObjectiveFunctionValues[o] / population.Size());
            }
            UpdateTimer();
            logger.Info("-");
            return population;
        }

        /**
         * Interpretes the individual as string. If an interpretation is
         * available, uses it. Otherwise uses toString method.
         * @param bestIndividual Individual to be interpreted.
         * @return String representation of the individual.
         */
        private String Interprete(T bestIndividual)
        {
            if (interpretation == null)
            {
                return bestIndividual.ToString();
            }
            return interpretation.Interprete(bestIndividual);
        }

        /**
         * Finds best individual (best as in having highest obj. function values
         * for each objective function) and computes the sum of objective function 
         * values.
         * @param population Population to look in.
         * @param sumOfObjectiveFunctionValues Map in which 
           to store sum of obj. function values.
         * @return Best individual in the population.
         */
        private T FindBestIndividualAndComputeSumOfObjFunctionValues(
            Population<T> population,
            Dictionary<ObjectiveFunction<T>, Double> sumOfObjectiveFunctionValues)
        {
            T bestIndividual = population.GetIndividuals()[0];
            foreach (T individual in population.GetIndividuals())
            {
                bool isBetter = true;
                foreach (ObjectiveFunction<T> function in objFunction)
                {
                    double individualObjFunctionValue = function(individual);
                    if (individualObjFunctionValue < function(bestIndividual))
                    {
                        isBetter = false;
                    }

                    double previousValue;

                    if (!sumOfObjectiveFunctionValues.ContainsKey(function))
                    {
                        previousValue = 0.0;
                    }
                    else
                    {
                        previousValue = sumOfObjectiveFunctionValues[function];
                    }
                    
                    sumOfObjectiveFunctionValues[function] = previousValue + individualObjFunctionValue;
                }
                if (isBetter)
                {
                    bestIndividual = individual;
                }
            }
            return bestIndividual;
        }

        /** Updates timing stats. */
        private void UpdateTimer()
        {
            if (lastExecutionTime != 0)
            {
                long time = clock.GetCurrentTimeMillis() - lastExecutionTime;
                logger.Info("Last iteration took " + time + "ms");
            }
            lastExecutionTime = clock.GetCurrentTimeMillis();
        }
    }
}
