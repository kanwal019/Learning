using Common.Classes;
using System;
using System.IO;

namespace LoggerService
{
    public class ConsoleLogger
    {
        public static void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void OnBookAdded(object source, EventArgs args)
        {
            if (args is StringArgs e)
            {
                LogMessage(e.Args);
                return;
            }
            throw new InvalidCastException();
        }
    }
}
