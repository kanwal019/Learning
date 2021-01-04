using Common.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Implementation
{
    public class BooksRepository
    {
        public List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book
                {
                    Author = "Author 1",
                    Id = 1,
                    Name = "Book 1",
                    NumberOfPages = 200,
                    Price = 400
                },
                new Book
                {
                    Author = "Author 2",
                    Id = 2,
                    Name = "Book 2",
                    NumberOfPages = 200,
                    Price = 200
                },
                new Book
                {
                    Author = "Author 3",
                    Id = 3,
                    Name = "Book 3",
                    NumberOfPages = 300,
                    Price = 300
                },
                new Book
                {
                    Author = "Author 4",
                    Id = 4,
                    Name = "Book 4",
                    NumberOfPages = 200,
                    Price = 100
                },
                new Book
                {
                    Author = "Author 5",
                    Id = 5,
                    Name = "Book 5",
                    NumberOfPages = 200,
                    Price = 200
                },
                new Book
                {
                    Author = "Author 6",
                    Id = 6,
                    Name = "Book 6",
                    NumberOfPages = 300,
                    Price = 100
                },
            };
        }

        public EventHandler<StringArgs> BookAdded;

        public void AddBooks(List<Book> books)
        {
            var message = DateTime.Now + " - " + books.Count + (books.Count > 1 ? " books " : " book ") + "added";
            OnBookAdded(message);
        }

        public void Add(Book book)
        {
            AddBooks(new List<Book> { book });
        }

        protected virtual void OnBookAdded(string message)
        {
            BookAdded?.Invoke(this, new StringArgs(message));
        }
    }
}
