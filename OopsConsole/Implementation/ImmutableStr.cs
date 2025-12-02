using Common.Classes;
using System;

namespace OopsConsole.Implementation;

public class ImmutableStr
{
    public static void DisplayResult()
    {
        ImmutableString iStr = new();
        string str = "Hello World !!";

        foreach(char c in str)
        {
            iStr.Append(c);
        }

        Console.WriteLine(iStr.ToString());
    }
}
