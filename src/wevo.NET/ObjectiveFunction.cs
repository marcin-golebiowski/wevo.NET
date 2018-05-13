namespace wevo.NET
{
    /**
      * Computes value of the objective function.
      * @param individual Individual to be evaluated.
      * @return Value of the objective function.
      */
    public delegate double ObjectiveFunction<T>(T individual) where T : Individuals.Individual;
}
