using System;
using System.Collections.Generic;
using Logging;

namespace Common.Classes
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
            };
        }

        public delegate string BookAddedEventHandler(object source, EventArgs args);
        public event BookAddedEventHandler BookAdded;

        public string AddBooks(List<Book> books)
        {
            var message = DateTime.Now + " - " + books.Count + (books.Count > 1 ? " books " : " book ") + "added";
            FileLogger.LogMessage(message);
            OnBookAdded();
            return books.Count + " books added !!";
        }

        public string Add(Book book)
        {
            return AddBooks(new List<Book> { book });
        }

        protected virtual void OnBookAdded()
        {
            if(BookAdded != null)
            {
                BookAdded(this, EventArgs.Empty);
            }
        }
    }
}
