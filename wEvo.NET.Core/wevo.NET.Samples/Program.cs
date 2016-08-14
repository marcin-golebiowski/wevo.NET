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
using System.Collections.Generic;
using wevo.NET.Samples.ObjectiveFunctions;
using wevo.NET.Core;
using wevo.NET.Core.Exitcriteria;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Operators;
using wevo.NET.Core.Operators.Binary;
using wevo.NET.Core.Operators.Natural;
using wevo.NET.Core.Operators.Reporters;
using wevo.NET.Core.Operators.Reporters.Interpretations;
using wevo.NET.Core.Utils;

namespace wevo.NET.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int populationSize = 10;
            int len = 100;
            int iterations = 100;
            var function = new OneMax();
            var initialPopulation = BinaryVector.GeneratePopulationOfRandomBinaryIndividuals(new dotNetRandom(), len, populationSize);

            Algorithm<BinaryVector> alg = new Algorithm<BinaryVector>(initialPopulation);

            var functions = new List<CachedObjectiveFunction<BinaryVector>>();
            functions.Add(new CachedObjectiveFunction<BinaryVector>(function, 100));

            alg.AddEvaluationPoint(new SingleThreadedEvaluator<BinaryVector>(functions));
            alg.AddExitPoint(new MaxIterations<BinaryVector>(iterations));

            alg.AddOperator(new BestFractionSelection<BinaryVector>(new OneMax(), 0.2));
            alg.AddOperator(new Core.Operators.Binary.UniformCrossover(new dotNetRandom()));
            alg.AddOperator(new UniformProbabilityNegationMutation(0.1, new dotNetRandom()));

            var list = new List<ObjectiveFunction<BinaryVector>>();
            list.Add(function);

            alg.AddOperator(new BestIndividualAndBasicStats<BinaryVector>(list, new BinaryVectorInterpretation()));

            alg.Run();


            Console.ReadLine();

        }
    }
}
