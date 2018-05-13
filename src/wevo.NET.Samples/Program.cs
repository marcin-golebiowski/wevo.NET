using System;
using System.Collections.Generic;
using  wevo.NET.Samples.ObjectiveFunctions;
using wevo.NET;
using wevo.NET.Exitcriteria;
using wevo.NET.Individuals;
using wevo.NET.Operators;
using wevo.NET.Operators.Binary;
using wevo.NET.Operators.Natural;
using wevo.NET.Operators.Reporters;
using wevo.NET.Operators.Reporters.Interpretations;
using wevo.NET.Utils;

namespace  wevo.NET.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericAlgorithm();

            //CGAAlgorithm();

            //PBILAlgorithm();

        }

        private static void PBILAlgorithm()
        {
            int populationSize = 50;
            int len = 100;
            ObjectiveFunction<BinaryVector> function = OneMax.Compute;
            var initialPopulation = BinaryVector.GeneratePopulationOfRandomBinaryIndividuals(new dotNetRandom(), len, populationSize);
            Algorithm<BinaryVector> alg = new Algorithm<BinaryVector>(initialPopulation);

            var functions = new List<CachedObjectiveFunction<BinaryVector>>();
            functions.Add(new CachedObjectiveFunction<BinaryVector>(function, 100));

            alg.AddEvaluationPoint(new MultiThreadedTaskEvaluator<BinaryVector>(functions));
            alg.AddExitPoint(new MaxIterations<BinaryVector>(1));


            var list = new List<ObjectiveFunction<BinaryVector>>();
            list.Add(function);

            alg.AddOperator(new PBIL(function, 10000, 0.1, 0.01, 0.02, populationSize, len, new dotNetRandom()));
            alg.AddOperator(new BestIndividualAndBasicStats<BinaryVector>(list, new BinaryVectorInterpretation()));

            alg.Run();


            Console.ReadLine();
        }

        private static void CGAAlgorithm()
        {
            int populationSize = 50;
            int len = 10;
            ObjectiveFunction<BinaryVector> function = OneMax.Compute;
            var initialPopulation = BinaryVector.GeneratePopulationOfRandomBinaryIndividuals(new dotNetRandom(), len, populationSize);
            Algorithm<BinaryVector> alg = new Algorithm<BinaryVector>(initialPopulation);

            var functions = new List<CachedObjectiveFunction<BinaryVector>>();
            functions.Add(new CachedObjectiveFunction<BinaryVector>(function, 100));

            alg.AddEvaluationPoint(new SingleThreadedEvaluator<BinaryVector>(functions));
            alg.AddExitPoint(new MaxIterations<BinaryVector>(1));


            var list = new List<ObjectiveFunction<BinaryVector>>();
            list.Add(function);

            alg.AddOperator(new CGA(100, 0.2, len, function, new dotNetRandom()));
            alg.AddOperator(new BestIndividualAndBasicStats<BinaryVector>(list, new BinaryVectorInterpretation()));

            alg.Run();


            Console.ReadLine();
        }

        private static void GenericAlgorithm()
        {
            int populationSize = 50;
            int len = 100;
            int iterations = 1000;

            ObjectiveFunction<BinaryVector> function = OneMax.Compute;

            var initialPopulation = BinaryVector.GeneratePopulationOfRandomBinaryIndividuals(new dotNetRandom(), len, populationSize);

            Algorithm<BinaryVector> alg = new Algorithm<BinaryVector>(initialPopulation);

            var functions = new List<CachedObjectiveFunction<BinaryVector>>();
            functions.Add(new CachedObjectiveFunction<BinaryVector>(function, 100));

            alg.AddEvaluationPoint(new SingleThreadedEvaluator<BinaryVector>(functions));
            alg.AddExitPoint(new MaxIterations<BinaryVector>(iterations));

            alg.AddOperator(new BestFractionSelection<BinaryVector>(function, 0.2));
            alg.AddOperator(new Operators.Binary.UniformCrossover(new dotNetRandom()));
            alg.AddOperator(new UniformProbabilityNegationMutation(0.1, new dotNetRandom()));

            var list = new List<ObjectiveFunction<BinaryVector>>();
            list.Add(function);

            alg.AddOperator(new BestIndividualAndBasicStats<BinaryVector>(list, new BinaryVectorInterpretation()));

            alg.Run();


            Console.ReadLine();
        }
    }
}
