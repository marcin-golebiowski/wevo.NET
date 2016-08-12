/*
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

namespace wevo.NET.Core
{
    /**
    * Condition which, when met, signals the {@link Algorithm} to terminate
    * computation. For example it can count the number of iterations of the 
    * algorithm that passed or compute the variance of objective function values
    * in the population. It can be arbitrarily simple or complex. It is executed 
    * only on the <strong>master machine</strong> in master-slave distribution.
    *
    * @param <T> Type of the individual for which condition is evaluated.
    */
    public interface TerminationCondition<T> where T : Individuals.Individual
    {
        bool IsSatisfied(Population<T> population);

        /** Resets termination condition. */
        void Reset();
    }
}
