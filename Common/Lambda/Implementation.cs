using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;

namespace Common.Lambda
{
    public class Implementation
    {
        public static void DisplayResult()
        {
            LambdaStarters();
            Extra();
            ImplementBooks();
        }

        static void ImplementBooks()
        {
            var books = new BooksRepository().GetBooks();
            var cheapBooks = books.FindAll(Book.IsCheap);
            var cheapBooksLambda = books.FindAll(book => book.Price < 300); // Gives same result

            foreach(var book in cheapBooks)
            {
                Console.WriteLine(book.Name + " by " + book.Author);
            }

            foreach (var book in cheapBooksLambda)
            {
                Console.WriteLine(book.Name + " by " + book.Author);
            }
        }

        static void Extra()
        {
            const int num = 10;
            Func<int, int> multiplier = n => n * num; // Lambda can use any variable defined in the method
            Console.WriteLine(multiplier(10));
        }

        static void LambdaStarters()
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
