using System;

namespace OopsConsole.Classes;

public static class StaticClass
{
    private static readonly int _data;

    static StaticClass()
    {
        if (Test.Data < 0)
        {
            _data = 0;
            return;
        }
        _data = 100;
    }

    public static void DisplayResult()
    {
        Console.WriteLine($"Data is set to - {_data}");
    }
}

public class Test
{
    private static int _data;

    public static int Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
        }
    }
}
