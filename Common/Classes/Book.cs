using System;
using System.Text;

namespace Common.Classes
{
    public class Book : Product
    {
        public string Author
        {
            get; set;
        }

        public int NumberOfPages
        {
            get; set;
        }

        public static bool IsCheap(Book book)
        {
            return book.Price < 300;
        }
    }
}
