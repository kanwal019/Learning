using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
    public static class StringExtensions
    {
        public static string Shorten(this String str, int wordLimit)
        {
            if(wordLimit <= 0)
            {
                return String.Empty;
            }

            var words = str.Trim().Split(' ');

            if (words.Length <= wordLimit)
            {
                return str;
            }

            return string.Join(" ", words.Take(wordLimit)) + "...";
        }
    }
}
