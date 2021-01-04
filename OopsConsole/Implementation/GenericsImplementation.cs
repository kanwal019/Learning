using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;
using Common.Generics;

namespace OopsConsole.Implementation
{
    class GenericsImplementation
    {
        public static void DisplayResult()
        {
            var book = new Book
            {
                Id = 1,
                Name = "Sample",
                Price = 200,
                Author = "John Smith",
                NumberOfPages = 150
            };

            var numbers = new GenericList<int>();
            numbers.Add(200);
            numbers.Add(500);

            var books = new GenericList<Book>();
            books.Add(book);
            books.Add(new Book());

            var dictionary = new GenericDictionary<string, Book>();
            dictionary.Add("Sample", book);

            var number = new Common.Generics.Nullable<int>(5);
            Console.WriteLine("Has Value ? " + number.HasValue);
            Console.WriteLine("Value - " + number.GetValueOrDefault());
        }
    }
}
