using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopsConsole.Implementation
{
    public class Linq
    {
        public static void DisplayResult()
        {
            ImplementBooks();
        }

        static void ImplementBooks()
        {
            var books = new BooksRepository().GetBooks();
            var cheapBooks = books.FindAll(Common.Classes.Book.IsCheap);

            var cheapBooksLinq = from b in books
                                 where b.Price < 300
                                 orderby b.Name
                                 select new { b.Name, b.Author }; // Gives same result

            var cheapBooksLinqExt = books
                                    .Where(b => b.Price < 300)
                                    .OrderBy(b => b.Name)
                                    .Select(b => new { b.Name, b.Author }); // Gives same result

            foreach (var book in cheapBooks)
            {
                Console.WriteLine(book.Name + " by " + book.Author);
            }

            Console.WriteLine();

            foreach (var book in cheapBooksLinq)
            {
                Console.WriteLine(book.Name + " by " + book.Author);
            }

            Console.WriteLine();

            foreach (var book in cheapBooksLinqExt)
            {
                Console.WriteLine(book.Name + " by " + book.Author);
            }
        }
    }
}
