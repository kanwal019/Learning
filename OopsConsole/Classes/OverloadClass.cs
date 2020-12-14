using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Classes
{
    class OverloadClass
    {
        protected internal void FunctionA()
        {
            Console.WriteLine("Function A of Overload class !!");
        }

        protected internal void FunctionA(string s)
        {
            Console.WriteLine("Function A of Overload class with param {0} !!", s);
        }

        protected internal void FunctionA(int i, int j)
        {
            Console.WriteLine("Function A of Overload class with param {0} and {1} !!", i, j);
        }

        protected internal void FunctionA(string i, string j)
        {
            Console.WriteLine("Function A of Overload class with param {0} and {1} !!", i, j);
        }
    }

    public class OverloadImplementation
    {
        public static void DisplayResult()
        {
            Console.WriteLine("-- OverloadClass shadow = new OverloadClass(); --");

            OverloadClass overload = new OverloadClass();
            overload.FunctionA();
            overload.FunctionA("Hello");
            overload.FunctionA(100, 200);
            overload.FunctionA("Hello", "World");
            Console.WriteLine();
        }
    }
}
