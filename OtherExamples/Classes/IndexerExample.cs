using System;

namespace OtherExamples.Classes
{
    public class IndexerExample
    {
        public static void DisplayResult()
        {
            Square sqr = new Square();
            Console.WriteLine("Square value of the integers till 10 are - ");

            for (int i = 1; i <= 10; i++)
            {
                Console.Write(sqr[i] + " ");
            }
                
            Console.WriteLine();
            Console.WriteLine("Figure out the errors in above implementation - ");
            Console.WriteLine(sqr[-1]);
            Console.WriteLine(sqr[15]);
            Console.WriteLine(sqr[17]);
            Console.WriteLine();
        }
    }

    public class Square
    {
        public int this[int index]
        {
            get
            {
                if((index > 0) && (index < 16))
                {
                    return index * index;
                }
                return -1;
            }
        }
    }
}
