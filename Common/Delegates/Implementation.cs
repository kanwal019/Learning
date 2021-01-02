using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;

namespace Common.Delegates
{
    public class Implementation
    {
        public static void DisplayResult()
        {
            var processor = new PhotoProcessor();
            var filters = new PhotoFilters();

            PhotoProcessor.PhotoFilterHandler filterHandler = filters.ApplyBrightness;
            filterHandler += filters.ApplyContrast;
            filterHandler += RemoveReadEyeFilter;

            processor.Process("somePhoto", filterHandler);
        }

        static void RemoveReadEyeFilter(Photo photo)
        {
            Console.WriteLine("Remove Red Eye");
        }
    }
}
