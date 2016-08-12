package pl.wroc.uni.ii.evolution.objectivefunctions;

import pl.wroc.uni.ii.evolution.engine.individuals.EvBinaryVectorIndividual;
import pl.wroc.uni.ii.evolution.engine.prototype.EvObjectiveFunction;

/** Classic OneMax objective function for BinaryIndividual. */ 
public class EvOneMax implements EvObjectiveFunction<EvBinaryVectorIndividual> {
  
  private static final long serialVersionUID = 1845093068882807321L;

/**
   * Gets onemax fitness of individual.
   * Designed & working properly only for BinaryIndividual.  
   */
  public double evaluate(EvBinaryVectorIndividual individual) {
    int result = 0;
    int individual_dimension = individual.getDimension();
    for (int i = 0; i < individual_dimension; i++) {
      if (individual.vector[i]) {
        result += 1;
      }
    }
    return result;
  }
}
