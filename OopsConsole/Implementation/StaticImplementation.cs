using System;

namespace OopsConsole.Implementation;

public class StaticImplementation
{
    public static void DisplayResult()
    {
        Classes.Test.Data = -10;
        Classes.StaticClass.DisplayResult();
        Console.WriteLine();
        Classes.Test.Data = 50;
        Classes.StaticClass.DisplayResult();
    }
}
