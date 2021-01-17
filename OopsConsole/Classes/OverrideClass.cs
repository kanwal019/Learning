using System;

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

        protected internal virtual void FunctionD()
        {
            Console.WriteLine("Function D of Override class !!");
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

        protected internal sealed override void FunctionD()
        {
            Console.WriteLine("Function D of OverrideDerived class !!");
        }
    }

    class ExtraDerived : OverrideDerived
    {
        protected internal ExtraDerived()
        {
            Console.WriteLine("Constructor of ExtraDerived class !!");
        }

        protected internal override void FunctionA()
        {
            Console.WriteLine("Function A of ExtraDerived class !!");
        }

        protected internal new void FunctionD()
        {
            Console.WriteLine("Function D of ExtraDerived class !!");
        }
    }
}
