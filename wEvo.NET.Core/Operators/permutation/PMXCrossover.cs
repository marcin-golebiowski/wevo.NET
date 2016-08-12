using System.Collections;
using System.Collections.Generic;
using wevo.NET.Core;
using wevo.NET.Core.Utils;

namespace wEvo.NET.Core.Operators.Permutation
{
    public class PMXCrossover : Operator<wevo.NET.Core.Individuals.Permutation>
    {
        /** Beginning of the segment. */
        private int segmentBeginning;

        /** Length of the segment. */
        private int segmentLength;

        /** Random number generator. */
        private wevoRandom randomGenerator;

        /** Indicates whether operator should generate segment bounds randomly. */
        private bool generateSegmentBounds;

        /**
         * Constructor.
         * 
         * @param generator Random number generator.
         * @param beginning the beginning of the exchanged segment.
         * @param length the length of the exchanged segment.
         */
        public PMXCrossover(
            wevoRandom generator,
            int beginning,
            int length)
        {
            this.randomGenerator = generator;
            this.segmentBeginning = beginning;
            this.segmentLength = length;
            this.generateSegmentBounds = false;
        }

        /**
         * Constructor, that causes the operator to generate segment beginning
         * and length randomly.
         * 
         * @param generator Random number generator.
         */
        public PMXCrossover(
            wevoRandom generator)
        {
            this.randomGenerator = generator;
            this.generateSegmentBounds = true;
        }


        /** {@inheritDoc} */
        public Population<wevo.NET.Core.Individuals.Permutation> Apply(Population<wevo.NET.Core.Individuals.Permutation> population)
        {
            if (generateSegmentBounds)
            {
                int chromosomeLength = population.GetIndividuals()[0].GetSize();
                segmentBeginning = randomGenerator.NextInt(0, chromosomeLength);
                segmentLength = randomGenerator.NextInt(0, chromosomeLength - segmentBeginning);
            }

            Population<wevo.NET.Core.Individuals.Permutation> workingCopy = new Population<wevo.NET.Core.Individuals.Permutation>(population);

            workingCopy.GetIndividuals().Shuffle();

            Population<wevo.NET.Core.Individuals.Permutation> result = new Population<wevo.NET.Core.Individuals.Permutation>();

            for (int i = 0; i < workingCopy.GetIndividuals().Count / 2; i++)
            {
                List<wevo.NET.Core.Individuals.Permutation> children = combine(workingCopy.GetIndividuals()[2 * i], workingCopy.GetIndividuals()[2 * i + 1]);
                result.AddIndividual(children[0]);
                result.AddIndividual(children[1]);
            }

            return result;
        }

        /**
         * Combines two parents in a PMX way. Package-visibility for testing.
         * @param parent1 First parent.
         * @param parent2 Second parent.
         * @return List of two children obtained from parents combination.
         */
        List<wevo.NET.Core.Individuals.Permutation> combine(
            wevo.NET.Core.Individuals.Permutation parent1,
            wevo.NET.Core.Individuals.Permutation parent2)
        {

            List<wevo.NET.Core.Individuals.Permutation> result = new List<wevo.NET.Core.Individuals.Permutation>(2);

            int chromosomeLength = parent1.GetValues().Length;

            int[] child1Chromosome = new int[chromosomeLength];
            int[] child2Chromosome = new int[chromosomeLength];

            // Copying the exchanged segments.
            copySegment(child1Chromosome, parent2);
            copySegment(child2Chromosome, parent1);

            // Defining the rest of the genes.
            ExchangeGenes(child1Chromosome, child2Chromosome, parent1);
            ExchangeGenes(child2Chromosome, child1Chromosome, parent2);

            wevo.NET.Core.Individuals.Permutation child1 = new wevo.NET.Core.Individuals.Permutation(child1Chromosome);
            wevo.NET.Core.Individuals.Permutation child2 = new wevo.NET.Core.Individuals.Permutation(child2Chromosome);

            result.Add(child1);
            result.Add(child2);

            return result;
        }

        /**
         * Copying a segment to a child's chromosome from a parent's chromosome. The
         * copied segment is [from,to).
         * 
         * @param childChromosome The child's chromosome to which segment is copied.
         * @param parent The individual from which the segment is copied.
         */
        private void copySegment(
            int[] childChromosome,
            wevo.NET.Core.Individuals.Permutation parent)
        {

            for (int i = segmentBeginning; i < segmentBeginning + segmentLength; i++)
            {
                childChromosome[i] = parent.GetValue(i);
            }
        }

        /**
         * Checks if gene already exists in chromosome in segment defined by
         * segment_beginning and segment_length. If in finds a conflict it returns
         * it's position, else it return -1.
         * 
         * @param gene the gene which we check if already exists in the segment
         * @param chromosome the chromosome in which we search for a conflict
         * @return Index conflicting or -1, if none exists.
         */
        private int IsConflict(
            int gene,
            int[] chromosome)
        {

            for (int j = segmentBeginning; j < segmentBeginning + segmentLength; j++)
            {
                if (gene == chromosome[j])
                {
                    return j;
                }
            }

            return -1;
        }

        /**
         * Copies genes from the parent's to child's chromosome without affecting
         * genes in segment defined by segment beginning and segment length.
         * 
         * @param affectedChildChromosome the chromosome to which genes are copied
         * @param unaffectedChildChromosome the chromosome from which genes are
         *        copied if affected_child_chromosome already contains a gene which is
         *        attempted to be copied
         * @param parent the individual from whose chromosome genes are copied
         */
        private void ExchangeGenes(
            int[] affectedChildChromosome,
            int[] unaffectedChildChromosome,
            wevo.NET.Core.Individuals.Permutation parent)
        {

            int chromosomeLength = affectedChildChromosome.Length;
            int currentGene; // currently selected gene

            // position on which conflict occurs or -1 if there is no conflict.
            int conflictPosition;
            for (int i = 0; i < chromosomeLength; i++)
            {
                currentGene = parent.GetValue(i);
                // Checking if conflict occurs
                conflictPosition =
                   IsConflict(currentGene, affectedChildChromosome);

                // If conflict occurs, then select the gene corresponding 
                // to the current one; otherwise, select current one.
                while (conflictPosition != -1)
                {
                    currentGene = unaffectedChildChromosome[conflictPosition];
                    conflictPosition =
                        IsConflict(currentGene, affectedChildChromosome);
                }
                affectedChildChromosome[i] = currentGene;

                // When we get to the end of the chromosome, jump to the beginning.
                if (i == segmentBeginning - 1)
                {
                    i += segmentLength;
                }
            }
        }
    }
}
