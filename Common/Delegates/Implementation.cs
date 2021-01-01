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

            processor.Process("somePhoto", filterHandler);
        }
    }
}
