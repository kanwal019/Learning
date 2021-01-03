using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;

namespace Common.Events
{
    public class Implementation
    {
        public static void DisplayResult()
        {
            AddOneBook();
        }

        static void AddOneBook()
        {
            var book = new Book
            {
                Author = "Author 5",
                Id = 5,
                Name = "Book 5",
                NumberOfPages = 300,
                Price = 150
            };

            Console.WriteLine(new BooksRepository().Add(book));
        }
    }
}
