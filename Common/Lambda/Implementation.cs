using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lambda
{
    public class Implementation
    {
        public static void DisplayResult()
        {
            Console.WriteLine(Square(5));
        }

        static int Square(int num)
        {
            return num * num;
        }
    }
}
