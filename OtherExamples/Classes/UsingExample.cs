using System;
using System.Collections.Generic;
using System.Text;
using FrameworkLibrary.Classes;
using CustomMathEnums = FrameworkLibrary.Helpers.Enums.CustomMathEnums;

namespace OtherExamples.Classes
{
    public class UsingExample
    {
        public static void DisplayResult()
        {
            Console.WriteLine("Enter a number -");
            int input = Convert.ToInt32(Console.ReadLine());
            CustomMathEnums.MethodType methodSelection;

            Console.WriteLine();
            Console.WriteLine("Select the type of Mathematical function -");
            Console.WriteLine("a) Square");
            Console.WriteLine("b) Square Root");
            Console.WriteLine("c) Factorial");
            Console.WriteLine("d) Mod");
            Console.WriteLine();

            string selection = Console.ReadLine().Trim().ToLower();

            switch (selection)
            {
                case "a":
                    methodSelection = CustomMathEnums.MethodType.Square;
                    break;
                case "b":
                    methodSelection = CustomMathEnums.MethodType.SquareRoot;
                    break;
                case "c":
                    methodSelection = CustomMathEnums.MethodType.Factorial;
                    break;
                case "d":
                    methodSelection = CustomMathEnums.MethodType.Mod;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            using (var math = new CustomMath(input))
            {
                Console.WriteLine();
                Console.WriteLine("The value of " + methodSelection.ToString() + " of " + input + " is - " + math.GetValue(methodSelection));
            }
        }
    }
}
