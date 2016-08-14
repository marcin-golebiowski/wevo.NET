using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Original";
            s.ToLower();
            if (object.ReferenceEquals(s, "Original"))
                Console.WriteLine("They have the same reference");
            else Console.WriteLine("They do not have the same reference");

            s = s.ToLower();

            if (object.ReferenceEquals(s, "original"))
                Console.WriteLine("They have the same reference");
            else
                Console.WriteLine("They do not have the same reference");
        }
    }
}
