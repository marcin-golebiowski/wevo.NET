namespace wEvo.NET.Core
{
    public interface TerminationCondition<T> where T : Individuals.Individual
    {
        bool IsSatisfied(Population<T> population);

        /** Resets termination condition. */
        void Reset();
    }
}
