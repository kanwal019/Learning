using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Classes
{
    class OverrideClass
    {
        protected internal OverrideClass()
        {
            Console.WriteLine("Constructor of Override class !!");
        }

        protected internal virtual void FunctionA()
        {
            Console.WriteLine("Function A of Override class !!");
        }

        protected internal virtual void FunctionB()
        {
            Console.WriteLine("Function B of Override class !!");
        }

        protected internal virtual void FunctionC()
        {
            Console.WriteLine("Function C of Override class !!");
        }
    }

    class OverrideDerived : OverrideClass
    {
        protected internal OverrideDerived()
        {
            Console.WriteLine("Constructor of OverrideDerived class !!");
        }

        protected internal override void FunctionA()
        {
            Console.WriteLine("Function A of OverrideDerived class !!");
        }

        protected internal new void FunctionB()
        {
            Console.WriteLine("Function B of OverrideDerived class !!");
        }

        protected internal void FunctionC()
        {
            Console.WriteLine("Function C of OverrideDerived class !!");
        }
    }

    public class OverrideImplementation
    {
        public static void DisplayResult()
        {
            Console.WriteLine("------- Override Implementation -------");
            Console.WriteLine();

            Console.WriteLine("-- OverrideClass override = new OverrideClass(); --");

            OverrideClass overridd = new OverrideClass();
            overridd.FunctionA();
            overridd.FunctionB();
            overridd.FunctionC();
            Console.WriteLine();

            Console.WriteLine("-- override = new OverrideDerived(); --");

            overridd = new OverrideDerived();
            overridd.FunctionA();
            overridd.FunctionB();
            overridd.FunctionC();
            Console.WriteLine();

            Console.WriteLine("-- Override Derived overrideDerived = new OverrideDerived(); --");

            OverrideDerived overrideDerived = new OverrideDerived();
            overrideDerived.FunctionA();
            overrideDerived.FunctionB();
            overrideDerived.FunctionC();
            Console.WriteLine();

            Console.WriteLine("-- overrideDerived = (new OverrideClass()) as OverrideDerived; --");
            overrideDerived = (new OverrideClass()) as OverrideDerived;

            /*
            overrideDerived.FunctionA();
            overrideDerived.FunctionB();
            overrideDerived.FunctionC();
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
