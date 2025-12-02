using System;
using System.Linq;

namespace OopsConsole.Implementation;

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

        //var singleCheapBook = books.SingleOrDefault(b => b.Price < 300); // Throws exception due to multiple results
        var firstCheapBook = books.FirstOrDefault(b => b.Price < 300);
        var lastCheapBook = books.LastOrDefault(b => b.Price < 300);
        var pagedBooks = books.Skip(2).Take(3);

        var maxPrice = books.Max(b => b.Price);
        var minPrice = books.Min(b => b.Price);

        Console.WriteLine($"The most expensive book costs {maxPrice} bucks.");
        Console.WriteLine();

        Console.WriteLine($"The cheapest book costs {minPrice} bucks.");
        Console.WriteLine();

        Console.WriteLine($"{firstCheapBook.Name} by {firstCheapBook.Author}");
        Console.WriteLine();

        Console.WriteLine($"{lastCheapBook.Name} by {lastCheapBook.Author}");
        Console.WriteLine();

        foreach (var book in pagedBooks)
        {
            Console.WriteLine($"{book.Name} by {book.Author}");
        }

        Console.WriteLine();

        foreach (var book in cheapBooks)
        {
            Console.WriteLine($"{book.Name} by {book.Author}");
        }

        Console.WriteLine();

        foreach (var book in cheapBooksLinq)
        {
            Console.WriteLine($"{book.Name} by {book.Author}");
        }

        Console.WriteLine();

        foreach (var book in cheapBooksLinqExt)
        {
            Console.WriteLine($"{book.Name} by {book.Author}");
        }
    }
}
