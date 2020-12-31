using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;

namespace Common.Generics
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
        }
    }
}
