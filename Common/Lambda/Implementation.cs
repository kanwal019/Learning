using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lambda
{
    public class Implementation
    {
        public static void DisplayResult()
        {
            Func<int, int> square = Square;
            Func<int, int> squareLambda = num => num * num;

            Console.WriteLine(Square(5));
            Console.WriteLine(square(5));
            Console.WriteLine(squareLambda(5));
        }

        static int Square(int num)
        {
            return num * num;
        }
    }
}
