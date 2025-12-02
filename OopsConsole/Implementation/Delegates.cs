using Common.Classes;
using Common.Delegates;
using System;

namespace OopsConsole.Implementation;

public class Delegates
{
    public static void DisplayResult()
    {
        var processor = new PhotoProcessor();
        var filters = new PhotoFilters();

        Action<Photo> filterHandler = filters.ApplyBrightness;
        filterHandler += filters.ApplyContrast;
        filterHandler += RemoveReadEyeFilter;

        processor.Process("somePhoto", filterHandler);
    }

    static void RemoveReadEyeFilter(Photo photo)
    {
        Console.WriteLine("Remove Red Eye");
    }
}
