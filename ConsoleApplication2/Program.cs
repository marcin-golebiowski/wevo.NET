namespace ConsoleApplication2
{
    using System;
    using System.Collections.Generic;

    class DelegateTest
    {
        public static void Main()
        {
            A obj = new A();

            A.Test t = new A.Test(() => { Console.WriteLine("+"); });

            obj.Tests();
        }      
    }

    class A
    {
        public delegate void Test();

        public Test Tests;

        public event Test Tests2;
    }


    
}