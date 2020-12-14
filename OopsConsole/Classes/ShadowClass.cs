using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Classes
{
    class ShadowClass
    {
        protected internal ShadowClass()
        {
            Console.WriteLine("Contructor of Shadow class !!");
        }

        protected internal void FunctionA()
        {
            Console.WriteLine("Function A of Shadow class !!");
        }

        protected internal void FunctionB()
        {
            Console.WriteLine("Function B of Shadow class !!");
        }

        protected internal void FunctionC()
        {
            Console.WriteLine("Function C of Shadow class !!");
        }
    }

    class ShadowDerived : ShadowClass
    {
        protected internal ShadowDerived()
        {
            Console.WriteLine("Contructor of ShadowDerived class !!");
        }

        protected internal new void FunctionA()
        {
            Console.WriteLine("Function A of ShadowDerived class !!");
        }

        protected internal void FunctionB()
        {
            Console.WriteLine("Function B of ShadowDerived class !!");
        }
    }

    public class ShadowImplementation
    {
        public static void DisplayResult()
        {
            Console.WriteLine("------- Shadow Implementation -------");
            Console.WriteLine();

            Console.WriteLine("-- ShadowClass shadow = new ShadowClass(); --");
            
            ShadowClass shadow = new ShadowClass();
            shadow.FunctionA();
            shadow.FunctionB();
            shadow.FunctionC();
            Console.WriteLine();

            Console.WriteLine("-- shadow = new ShadowDerived(); --");

            shadow = new ShadowDerived();
            shadow.FunctionA();
            shadow.FunctionB();
            shadow.FunctionC();
            Console.WriteLine();

            Console.WriteLine("-- ShadowDerived shadowDerived = new ShadowDerived(); --");

            ShadowDerived shadowDerived = new ShadowDerived();
            shadowDerived.FunctionA();
            shadowDerived.FunctionB();
            shadowDerived.FunctionC();
            Console.WriteLine();

            Console.WriteLine("-- shadowDerived = (new ShadowClass()) as ShadowDerived; --");
            shadowDerived = (new ShadowClass()) as ShadowDerived;

            /*
            shadowDerived.FunctionA();
            shadowDerived.FunctionB();
            shadowDerived.FunctionC();
            Console.WriteLine();
            */

            /*
             * This code will return an error as a child class reference variable can not point 
             * to base class object because no child class object is created, whereas, when we create 
             * a child class object, the base class object is auto initiated, so base class reference 
             * variable can point to child class object.
             */

            Console.WriteLine();
        }
    }
}
