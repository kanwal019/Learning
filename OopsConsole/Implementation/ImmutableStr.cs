using Common.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Implementation
{
    public class ImmutableStr
    {
        public static void DisplayResult()
        {
            ImmutableString iStr = new ImmutableString();
            string str = "Hello World !!";

            foreach(char c in str)
            {
                iStr.Append(c);
            }

            Console.WriteLine(iStr.ToString());
        }
    }
}
