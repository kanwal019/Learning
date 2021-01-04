using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;

namespace OopsConsole.Implementation
{
    public class Events
    {
        public static void DisplayResult()
        {
            AddOneBook();
        }

        static void AddOneBook()
        {
            var booksRepo = new BooksRepository();
            var book = new Book
            {
                Author = "Author 5",
                Id = 5,
                Name = "Book 5",
                NumberOfPages = 300,
                Price = 150
            };

            booksRepo.BookAdded += LoggerService.FileLogger.OnBookAdded;
            booksRepo.BookAdded += LoggerService.ConsoleLogger.OnBookAdded;
            booksRepo.Add(book);
        }
    }
}
