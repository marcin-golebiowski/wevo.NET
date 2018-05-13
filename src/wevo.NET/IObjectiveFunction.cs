using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wevo.NET.Core
{
    public interface IObjectiveFunction<T> where T : Individuals.Individual
    {
        double Compute(T individual);
    }
}
